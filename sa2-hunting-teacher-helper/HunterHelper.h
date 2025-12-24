#pragma once

#pragma pack(push, 1)
struct HunterTeacherData {
	int currentLevel;
	bool inWinScreen;
	bool levelLoading;
	bool mspReversedHints;
	bool backToMenu;
	int p1Id;
	int p2Id;
	int p3Id;
};

struct FindActiveWindow {
	DWORD pid;
	HWND hwnd;
};
#pragma pack(pop)

DataPointer(__int16, CurrentLevel, 0x1934B70);
DataPointer(char, CurrentLanguage, 0x174AFD1);
DataArray(__int16, Life_Count, 0x174B024, 2);
DataArray(CharObj2Base*, MainCharObj2, 0x1DE9600, 8);
DataPointer(EmeraldManager*, EmeraldManagerObj, 0x1AF014C);
DataPointer(__int16, GameState, 0x1934BE0);

static const void* const KillPlayerPtr = (void*)0x46B110;
static inline void KillPlayer(int playerNum) {
	__asm {
		mov ebx, [playerNum]
		call KillPlayerPtr
	}
}

class HunterHelper {
	public:
		static void Init();
		static void LoadLevel();
		static void AwardWin(signed int* player);
		static void SetPhysicsAndGiveUpgrades(ObjectMaster* character, int a2);
		static void* EmeraldHintsFileLoaderInterceptor(const void* hintsFileName);
		static void LoadEmeraldLocations(EmeraldManager* emManager);
		static void ExitHandler(int a1, int a2, int a3);
		static LRESULT __stdcall WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
		static BOOL CALLBACK EnumWindowsProc(HWND hwnd, LPARAM lParam);
		static void CleanUp();

		static inline HANDLE hMap = nullptr;
		static inline HunterTeacherData* TeacherDataState = nullptr;

	private:
		static void HookActiveWindow();
		static void OpenSharedMemory();
		static Emerald* GetPieceById(EmeraldManager* emManager, int id);
		static bool IsShiftJISCharacter(uint8_t leadByte, uint8_t trailByte);
		static void ReverseShiftJISHint(uint8_t* hintStart, uint8_t* hintEnd);
		static inline const int PIECE_COLLECTED = 254;
		static inline const int MAX_HINT_SIZE = 8192;
		static inline const int MAX_STR_LEN = 4096;
		static inline const uint32_t FILE_END_BITS = 0xFFFFFFFFu;
		static inline WNDPROC OldWndProc = nullptr;
};

