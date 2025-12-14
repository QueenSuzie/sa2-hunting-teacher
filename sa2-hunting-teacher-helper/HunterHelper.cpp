#include "pch.h"
#include "HunterHelper.h"
#include <string>

UsercallFuncVoid(hAwardWin, (signed int* player), (player), 0x43E6D0, rESI);
UsercallFuncVoid(hExitHandler, (int a1, int a2, int a3), (a1, a2, a3), 0x4016D0, rECX, rEBX, rESI);
FunctionHook<void, EmeManObj2*> hLoadEmeraldLocations((intptr_t)EmeraldLocations_1POr2PGroup3);
FunctionHook<void> hLoadLevel((intptr_t)0x43C970);
StdcallFunctionHook<LRESULT, HWND, UINT, WPARAM, LPARAM> hWndProc((intptr_t)0x401810);

void HunterHelper::Init() {
	if (!HunterHelper::TeacherDataState) {
		HunterHelper::OpenSharedMemory();
	}

	hLoadLevel.Hook(HunterHelper::LoadLevel);
	hAwardWin.Hook(HunterHelper::AwardWin);
	hLoadEmeraldLocations.Hook(HunterHelper::LoadEmeraldLocations);
	hExitHandler.Hook(HunterHelper::ExitHandler);
	hWndProc.Hook(HunterHelper::WndProc);
}

void HunterHelper::LoadLevel() {
	if (CurrentLevel == HunterHelper::TeacherDataState->currentLevel) {
		HunterHelper::TeacherDataState->levelLoading = true;
	}

	hLoadLevel.Original();
}

void HunterHelper::AwardWin(signed int* player) {
	HunterHelper::TeacherDataState->inWinScreen = true;
	hAwardWin.Original(player);
}

void HunterHelper::LoadEmeraldLocations(EmeManObj2* emManager) {
	if (CurrentLevel != HunterHelper::TeacherDataState->currentLevel) {
		return hLoadEmeraldLocations.Original(emManager);
	}

	EmeManThing* p1 = &emManager->ptr_a[HunterHelper::TeacherDataState->p1Index];
	EmeManThing* p2 = &emManager->ptr_a[HunterHelper::TeacherDataState->p2Index];
	EmeManThing* p3 = &emManager->ptr_a[HunterHelper::TeacherDataState->p3Index];

	if (emManager->byte2C[0].byte0 != -2) {
		*(_DWORD*)&emManager->byte2C[0].byte0 = *(_DWORD*)&p1->byte0;
		emManager->byte2C[0].v = p1->v;
		emManager->byte5++;
	}

	if (emManager->byte2C[1].byte0 != -2) {
		*(_DWORD*)&emManager->byte2C[1].byte0 = *(_DWORD*)&p2->byte0;
		emManager->byte2C[1].v = p2->v;
		emManager->byte1 = ((_DWORD*)&p2->byte0)[1];
		emManager->byte5++;
	}

	if (emManager->byte2C[2].byte0 != -2) {
		*(_DWORD*)&emManager->byte2C[2].byte0 = *(_DWORD*)&p3->byte0;
		emManager->byte2C[2].v = p3->v;
		emManager->byte1 = ((_DWORD*)&p3->byte0)[1];
		emManager->byte5++;
	}
}

void HunterHelper::OpenSharedMemory() {
	HunterHelper::hMap = OpenFileMappingA(FILE_MAP_READ | FILE_MAP_WRITE, FALSE, "SA2-Hunter-Teacher");
	if (!HunterHelper::hMap) {
		DWORD error = GetLastError();
		std::string errorMsg = std::string("Failed to access Hunter Teacher: ");
		MessageBox(NULL, (std::wstring(errorMsg.begin(), errorMsg.end()) + std::to_wstring(error)).c_str(), L"Error!", MB_OK | MB_ICONERROR);
		exit(1);
	}

	HunterHelper::TeacherDataState = (HunterTeacherData*)MapViewOfFile(HunterHelper::hMap, FILE_MAP_READ | FILE_MAP_WRITE, 0, 0, sizeof(HunterTeacherData));
}

void HunterHelper::CleanUp() {
	if (HunterHelper::TeacherDataState) {
		UnmapViewOfFile(HunterHelper::TeacherDataState);
		HunterHelper::TeacherDataState = nullptr;
	}

	if (HunterHelper::hMap) {
		CloseHandle(HunterHelper::hMap);
		HunterHelper::hMap = nullptr;
	}
}

void HunterHelper::ExitHandler(int a1, int a2, int a3) {
	HunterHelper::CleanUp();
	hExitHandler.Original(a1, a2, a3);
}

LRESULT __stdcall HunterHelper::WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
	if (uMsg == WM_CLOSE) {
		HunterHelper::CleanUp();
	}

	return hWndProc.Original(hWnd, uMsg, wParam, lParam);
}