using sa2_hunting_teacher.Knuckles;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;

namespace sa2_hunting_teacher {
	internal partial class SA2Manager : IDisposable {
		private const string SONIC_EXECUTABLE = "sonic2app";
		private static bool CanRun = true;

		private IntPtr? sa2;
		private readonly IntPtr FrameCount = 0x174B038;
		private readonly IntPtr EmeraldManagerPtr = 0x1AF014C;
		private readonly IntPtr IsInWinScreenPtr = 0x174B002;

		private readonly Process targetProcess;
		private readonly HuntingLevel level;
		private readonly HuntingTeacherForm teacherForm;

		private SA2Manager(Level selection, byte repetitions, HuntingTeacherForm teacherForm) {
			this.teacherForm = teacherForm;
			this.level = selection switch {
				Level.WildCanyon => new WildCanyon(this, repetitions, teacherForm),
				Level.PumpkinHill => new PumpkinHill(this, repetitions, teacherForm),
				Level.AquaticMine => new AquaticMine(this, repetitions, teacherForm),
				Level.DeathChamber => new DeathChamber(this, repetitions, teacherForm),
				Level.MeteorHerd => new MeteorHerd(this, repetitions, teacherForm),
				_ => throw new ArgumentException("Unsupported Level Selected!")
			};

			Process[] processes = Process.GetProcessesByName(SONIC_EXECUTABLE);
			if (processes.Length < 1) {
				throw new ArgumentException("SA2 Is Not Running!");
			}

			teacherForm.Invoke(() => {
				HuntingTeacherForm.AddLogItem("Starting sequence run on " + this.level.ToString() + "...");
			});

			this.targetProcess = processes[0];
			this.sa2 = OpenProcess(ProcessAccessFlags.VMOperation | ProcessAccessFlags.VMWrite | ProcessAccessFlags.VMRead, false, this.targetProcess.Id);
		}

		public void SetSeed(int seed) {
			UIntPtr bytesWritten;
			byte[] buffer = BitConverter.GetBytes(seed + 1023);
			bool success = WriteProcessMemory((IntPtr) this.sa2!, FrameCount, buffer, (uint)buffer.Length, out bytesWritten);
			if (!success) {
				this.HandleMemoryError("Failed to write set to SA2 process.");
			}

			this.LogMessage("Writing Seed: " + seed);
		}

		public bool IsLevelLoading() {
			byte[] buffer = new byte[4];
			bool success = ReadProcessMemory((IntPtr)this.sa2!, EmeraldManagerPtr, buffer, 4, out _);
			if (!success) {
				this.HandleMemoryError("Failed to access SA2 process.");
				return false;
			}

			IntPtr emManagerAddress = new(BitConverter.ToInt32(buffer, 0));
			if (emManagerAddress == IntPtr.Zero) {
				return false;
			}

			byte[] emManagerBuffer = new byte[1];
			success = ReadProcessMemory((IntPtr)this.sa2!, emManagerAddress, emManagerBuffer, 1, out _);
			if (!success) {
				this.HandleMemoryError("Failed to access SA2 process.");
				return false;
			}

			byte action = emManagerBuffer[0];
			return action == 2;
		}

		public bool IsInWinScreen() {
			byte[] buffer = new byte[1];
			bool success = ReadProcessMemory((IntPtr)this.sa2!, this.IsInWinScreenPtr, buffer, 1, out _);
			if (!success) {
				this.HandleMemoryError("Failed to access SA2 process.");
				return false;
			}

			return buffer[0] != 0;
		}

		private void LogMessage(string msg) {
			this.teacherForm.Invoke(() => {
				HuntingTeacherForm.AddLogItem(msg);
			});
		}

		private void HandleMemoryError(string msg) {
			this.teacherForm.Invoke(() => {
				HuntingTeacherForm.AddLogItem(msg);
				this.teacherForm.ResetBtn_Click(this.teacherForm, new EventArgs());
			});
		}

		public void Dispose() {
			this.CloseResource();
			GC.SuppressFinalize(this);
		}

		private void CloseResource() {
			if (this.sa2 != null) {
				CloseHandle((IntPtr) this.sa2);
				this.sa2 = null;
			}
		}

		~SA2Manager() {
			this.CloseResource();
		}

		public static void Start(Level selection, byte repetitions, HuntingTeacherForm teacherForm) {
			CanRun = true;

			using (SA2Manager instance = new(selection, repetitions, teacherForm)) {
				while (CanRun && !instance.level.SequenceComplete() && !instance.targetProcess.HasExited) {
					instance.level.RunSequence();
				}

				if (instance.targetProcess.HasExited) {
					instance.LogMessage("SA2 Process Terminated - Resetting State.");
				}
			}

			teacherForm.Invoke(() => {
				teacherForm.ResetBtn_Click(teacherForm, new EventArgs());
			});
		}

		public static void Stop() {
			CanRun = false;
		}

		[LibraryImport("kernel32.dll")]
		private static partial IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

		[LibraryImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static partial bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint dwSize, out UIntPtr lpNumberOfBytesRead);

		[LibraryImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static partial bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint dwSize, out UIntPtr lpNumberOfBytesWritten);

		[LibraryImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static partial bool CloseHandle(IntPtr hProcess);
	}

	[Flags]
	internal enum ProcessAccessFlags : uint {
		VMRead = 0x0010,
		VMWrite = 0x0020,
		VMOperation = 0x0008,
		QueryInformation = 0x0400,
		All = 0x001F0FFF
	}
}
