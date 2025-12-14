#pragma once

#pragma pack(push, 1)
struct HunterTeacherData {
	int currentLevel;
	bool inWinScreen;
	bool levelLoading;
	int p1Id;
	int p2Id;
	int p3Id;
};
#pragma pack(pop)

class HunterHelper {
	public:
		static void Init();
		static void LoadLevel();
		static void AwardWin(signed int* player);
		static void SetPhysicsAndGiveUpgrades(ObjectMaster* character, int a2);
		static void LoadEmeraldLocations(EmeraldManager* emManager);
		static void ExitHandler(int a1, int a2, int a3);
		static LRESULT __stdcall WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
		static void CleanUp();

		static inline HANDLE hMap = nullptr;
		static inline HunterTeacherData* TeacherDataState = nullptr;

	private:
		static void OpenSharedMemory();
		static Emerald* GetPieceById(EmeraldManager* emManager, int id);
};

