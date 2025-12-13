#pragma once

class HunterHelper {
	public:
		static void Init();
		static void AwardWin(signed int* player);
		static void LoadEmeraldLocations(EmeManObj2* emManager);

	private:
		static void OpenSharedMemory();
};

#pragma pack(push, 1)
struct HunterTeacherData {
	int currentSet;
	int currentLevel;
	bool inWinScreen;
};
#pragma pack(pop)

