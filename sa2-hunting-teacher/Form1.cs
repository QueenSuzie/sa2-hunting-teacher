using System.Security.AccessControl;

namespace sa2_hunting_teacher {
	public partial class HuntingTeacherForm : Form {
		private static TextBox? currentLogBox;
		private static readonly Dictionary<Level, string> SupportedLevels = new() {
			{ Level.WildCanyon, "Wild Canyon" },
			{ Level.PumpkinHill, "Pumpkin Hill" },
			{ Level.AquaticMine, "Aquatic Mine" },
			{ Level.DeathChamber, "Death Chamber" },
			{ Level.MeteorHerd, "Meteor Herd" },
		};

		public HuntingTeacherForm() {
			InitializeComponent();

			levelSelector.DataSource = SupportedLevels.ToList();
			levelSelector.DisplayMember = "Value";
			levelSelector.ValueMember = "Key";
			levelSelector.SelectedIndex = 0;
			currentLogBox = logBox;
		}

		public static void AddLogItem(string value) {
			if (currentLogBox == null) {
				return;
			}

			currentLogBox.AppendText(value + Environment.NewLine);
		}

		private void StartBtn_Click(object sender, EventArgs e) {
			Level selectedLevel = (Level)levelSelector.SelectedValue!;
			startBtn.Enabled = false;
			repetitions.Enabled = false;
			mspReverseHints.Enabled = false;
			resetBtn.Enabled = true;

			Task.Run(() => {
				try {
					SA2Manager.Start(selectedLevel, (byte)repetitions.Value, this);
				} catch (ArgumentException) {
					this.Invoke(() => {
						MessageBox.Show(this, "A running instance of SA2 could not be found.\n" +
							"Make sure SA2 is running first before trying to start a level.", "SA2 Not Running", MessageBoxButtons.OK, MessageBoxIcon.Error);

						this.ResetBtn_Click(sender, e);
					});
				}
			});
		}

		public void ResetBtn_Click(object sender, EventArgs e) {
			SA2Manager.Stop();
			resetBtn.Enabled = false;
			repetitions.Enabled = true;
			mspReverseHints.Enabled = true;
			startBtn.Enabled = true;
		}

		private void LevelSelector_SelectedIndexChanged(object sender, EventArgs e) {
			mspReverseHints.Visible = false;
			if (this.levelSelector.SelectedItem != null && ((KeyValuePair<Level, string>)this.levelSelector.SelectedItem).Key == Level.MadSpace) {
				mspReverseHints.Visible = true;
			}
		}

		public bool MspReversedHints() {
			return mspReverseHints.Checked;
		}
	}
}
