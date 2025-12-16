#include "pch.h"
#include "HunterHelper.h"
#include <algorithm>
#include <string>
#include <vector>

UsercallFuncVoid(hAwardWin, (signed int* player), (player), 0x43E6D0, rESI);
UsercallFuncVoid(hExitHandler, (int a1, int a2, int a3), (a1, a2, a3), 0x4016D0, rECX, rEBX, rESI);
UsercallFuncVoid(hSetPhysicsAndGiveUpgrades, (ObjectMaster* character, int a2), (character, a2), (intptr_t)0x4599C0, rEAX, rECX);
FunctionHook<void*, const void*> hLoadStageHintsFile((intptr_t)0x73B6C0);
FunctionHook<void, EmeraldManager*> hLoadEmeraldLocations((intptr_t)EmeraldLocations_1POr2PGroup3);
FunctionHook<void> hLoadLevel((intptr_t)0x43C970);
StdcallFunctionHook<LRESULT, HWND, UINT, WPARAM, LPARAM> hWndProc((intptr_t)0x401810);

void HunterHelper::Init() {
	if (!HunterHelper::TeacherDataState) {
		HunterHelper::OpenSharedMemory();
	}

	hLoadLevel.Hook(HunterHelper::LoadLevel);
	hAwardWin.Hook(HunterHelper::AwardWin);
	hSetPhysicsAndGiveUpgrades.Hook(HunterHelper::SetPhysicsAndGiveUpgrades);
	hLoadStageHintsFile.Hook(HunterHelper::EmeraldHintsFileLoaderInterceptor);
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

void HunterHelper::SetPhysicsAndGiveUpgrades(ObjectMaster* character, int a2) {
	hSetPhysicsAndGiveUpgrades.Original(character, a2);
	if (CurrentLevel != HunterHelper::TeacherDataState->currentLevel) {
		if (CurrentLevel == LevelIDs_PumpkinHill) {
			MainCharObj2[0]->Upgrades |= Upgrades_KnucklesShovelClaw;
		}

		if (CurrentLevel == LevelIDs_EggQuarters) {
			MainCharObj2[0]->Upgrades |= Upgrades_RougePickNails;
		}
	}
}

bool HunterHelper::IsShiftJISCharacter(uint8_t leadByte, uint8_t trailByte) {
	return ((leadByte >= 0x81 && leadByte <= 0x9F) || (leadByte >= 0xE0 && leadByte <= 0xFC))
		&& ((trailByte >= 0x40 && trailByte <= 0x7E) || (trailByte >= 0x80 && trailByte <= 0xFC));
}

void HunterHelper::ReverseShiftJISLine(uint8_t* line, size_t len) {
	struct Token { const uint8_t* ptr; size_t len; };
	std::vector<Token> tokens;
	tokens.reserve(len);

	for (size_t i = 0; i < len; i++) {
		if ((i + 1) < len && HunterHelper::IsShiftJISCharacter(line[i], line[i + 1])) {
			tokens.push_back({ line + i, 2 });
			i++;
		} else {
			tokens.push_back({ line + i, 1 });
		}
	}

	std::vector<uint8_t> temp;
	temp.reserve(len);

	for (auto it = tokens.rbegin(); it != tokens.rend(); ++it) {
		temp.insert(temp.end(), it->ptr, it->ptr + it->len);
	}

	if (temp.size() == len) {
		std::memcpy(line, temp.data(), len);
	}
}

void HunterHelper::ReverseShiftJISHint(uint8_t* hint) {
	// ensure hint is properly null terminated
	uint8_t* nul = static_cast<uint8_t*>(std::memchr(hint, 0, HunterHelper::MAX_STR_LEN));
	if (!nul) {
		return;
	}

	size_t hintLen = size_t(nul - hint);
	size_t pos = 0;
	while (pos < hintLen) {
		size_t lineEnd = pos;
		while (lineEnd < hintLen && hint[lineEnd] != '\n') {
			++lineEnd;
		}

		size_t lineLen = lineEnd - pos;

		// handle windows style \r\n new lines.
		if (lineLen && hint[pos + lineLen - 1] == '\r') {
			--lineLen;
		}

		HunterHelper::ReverseShiftJISLine(hint + pos, lineLen);

		pos = (lineEnd < hintLen) ? (lineEnd + 1) : lineEnd;
	}
}

void* HunterHelper::EmeraldHintsFileLoaderInterceptor(const void* hintsFileName) {
	if (CurrentLevel != LevelIDs_MadSpace || HunterHelper::TeacherDataState->mspReversedHints) {
		return hLoadStageHintsFile.Original(hintsFileName);
	}

	void* data = hLoadStageHintsFile.Original(hintsFileName);
	uint8_t* base = (uint8_t *)data;
	auto table = reinterpret_cast<uint32_t*>(base);
	for (size_t i = 0; i < HunterHelper::MAX_HINT_SIZE; ++i) {
		uint32_t off = table[i];
		if (off == HunterHelper::FILE_END_BITS) {
			break;
		}

		char* hint = reinterpret_cast<char*>(base + off);
		if (i % 3 == 0) {
			if (TextLanguage == Language_Japanese) {
				HunterHelper::ReverseShiftJISHint((uint8_t*)(hint + 4));
			} else {
				char* start = hint + 4;
				size_t len = std::strlen(start);
				std::reverse(start, start + len);
			}
		}
	}

	return data;
}

void HunterHelper::LoadEmeraldLocations(EmeraldManager* emManager) {
	if (CurrentLevel != HunterHelper::TeacherDataState->currentLevel) {
		return hLoadEmeraldLocations.Original(emManager);
	}

	Life_Count[0] = 99;
	if (emManager->Piece1.id != -2) {
		Emerald* p1 = HunterHelper::GetPieceById(emManager, HunterHelper::TeacherDataState->p1Id);
		*&emManager->Piece1.id = *&p1->id;
		emManager->Piece1.v = p1->v;
		emManager->EmeraldsSpawned++;
	}

	if (emManager->Piece2.id != -2) {
		Emerald* p2 = HunterHelper::GetPieceById(emManager, HunterHelper::TeacherDataState->p2Id);
		*&emManager->Piece2.id = *&p2->id;
		emManager->Piece2.v = p2->v;
		emManager->EmeraldsSpawned++;
	}

	if (emManager->Piece3.id != -2) {
		Emerald* p3 = HunterHelper::GetPieceById(emManager, HunterHelper::TeacherDataState->p3Id);
		*&emManager->Piece3.id = *&p3->id;
		emManager->Piece3.v = p3->v;
		emManager->EmeraldsSpawned++;
	}
}

Emerald* HunterHelper::GetPieceById(EmeraldManager* emManager, int id) {
	byte idLowByte = id & 0xFF;
	Emerald* emeralds = nullptr;
	int emeraldsLength = 0;
	switch (idLowByte) {
		case 0:
		case 2:
		case 5:
			emeralds = emManager->Slot2Emeralds;
			emeraldsLength = emManager->Slot2ArrayLen;
			break;
		case 1:
		case 3:
			emeralds = emManager->Slot1Emeralds;
			emeraldsLength = emManager->Slot1ArrayLen;
			break;
		case 4:
		case 7:
		case 8:
			emeralds = emManager->Slot3Emeralds;
			emeraldsLength = emManager->Slot3ArrayLen;
			break;
		case 0xA:
			emeralds = emManager->EnemySlotEmeralds;
			emeraldsLength = emManager->EnemySlotArrayLen;
			break;
	}

	for (int i = 0; i < emeraldsLength; i++) {
		if (emeralds[i].id == id) {
			return &emeralds[i];
		}
	}

	MessageBox(NULL, L"Invalid ID Detected! Please report this along with the level and set that was last loaded.", L"Error!", MB_OK | MB_ICONERROR);
	exit(1);

	return nullptr;
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