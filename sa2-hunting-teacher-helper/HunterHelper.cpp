#include "pch.h"
#include "HunterHelper.h"
#include <string>

static HunterTeacherData* teacherDataState = nullptr;
UsercallFuncVoid(hAwardWin, (signed int* player), (player), 0x43E6D0, rESI);
FunctionHook<void, EmeManObj2*> hLoadEmeraldLocations((intptr_t)EmeraldLocations_1POr2PGroup3);
void HunterHelper::Init() {
	if (!teacherDataState) {
		HunterHelper::OpenSharedMemory();
	}

	hAwardWin.Hook(HunterHelper::AwardWin);
	hLoadEmeraldLocations.Hook(HunterHelper::LoadEmeraldLocations);
}

void HunterHelper::AwardWin(signed int* player) {
	teacherDataState->inWinScreen = true;
	hAwardWin.Original(player);
}

void HunterHelper::LoadEmeraldLocations(EmeManObj2* emManager) {
	if (CurrentLevel == teacherDataState->currentLevel) {
		FrameCount = teacherDataState->currentSet;
	}

	hLoadEmeraldLocations.Original(emManager);
}

void HunterHelper::OpenSharedMemory() {
	HANDLE hMap = OpenFileMappingA(FILE_MAP_READ | FILE_MAP_WRITE, FALSE, "SA2-Hunter-Teacher");
	if (!hMap) {
		DWORD error = GetLastError();
		std::string errorMsg = std::string("Failed to access Hunter Teacher: ");
		MessageBox(NULL, (std::wstring(errorMsg.begin(), errorMsg.end()) + std::to_wstring(error)).c_str(), L"Error!", MB_OK | MB_ICONERROR);
		exit(1);
	}

	teacherDataState = (HunterTeacherData*)MapViewOfFile(hMap, FILE_MAP_READ | FILE_MAP_WRITE, 0, 0, sizeof(HunterTeacherData));
}