#pragma once

#include <stdint.h>

#ifndef NDEBUG
FunctionPointer(int, PrintDebug, (const char* a1, ...), 0x426740);
#endif

typedef Uint32 NJD_SPRITE;
typedef Uint32 NJD_FLAG;
typedef Uint32 NJD_CONTROL_3D;
typedef DWORD _DWORD;
typedef Uint32 NJD_COLOR_TARGET;
typedef Uint32 NJD_COLOR_BLENDING;
typedef Uint32 NJD_TEX_SHADING;
typedef Uint32 NJD_DRAW;
typedef Uint32 NJD_TEXATTR;

// SA2 Structs

struct EntityData1;
struct SETEntry;
struct SETObjectData;
struct CollisionInfo;
struct AnimationInfo;
struct CollisionData;
struct CharObj2Base;
struct EntityData2;
struct ChaoDataBase;
struct ChaoDebugData1;
struct UnknownData2;
struct ChaoData1;
struct ObjUnknownA;
struct ObjUnknownB;
struct ObjectMaster;
struct LoopHead;

using ObjectFuncPtr = void(__cdecl*)(ObjectMaster*);

// All structs should be packed.
#pragma pack(push, 1)

struct AllocatedMem {
	int Cookie;
	void* Data;
};

struct AnimationInfo {
	__int16 AnimNum;
	__int16 ModelNum;
	__int16 mtnmode;
	__int16 NextAnimation;
	float TransitionSpeed;
	float AnimationSpeed;
};

struct CharAnimInfo {
	__int16 mtnmode;
	__int16 Next;
	__int16 Current;
	__int16 Animation3;
	__int16 field_8;
	__int16 acttimer;
	__int16 field_C;
	char field_E;
	char field_F;
	float nframe;
	float field_14;
	float field_18;
	char field_1C;
	char field_1D;
	char field_1E;
	char field_1F;
	char field_20;
	char field_21;
	char field_22;
	char field_23;
	AnimationInfo* Animations;
	NJS_MOTION* Motion;
};

struct PhysicsData {
	int HangTime;
	float FloorGrip;
	float SpeedCapH;
	float SpeedCapV;
	float SpeedMaxH;
	float PushSpeedMax;
	float JumpSpeed;
	float NoControlSpeed;
	float SlideSpeed;
	float JogSpeed;
	float RunSpeed;
	float RushSpeed;
	float KnockbackSpeed;
	float DashSpeed;
	float JumpAdd;
	float RunAccel;
	float AirAccel;
	float RunDecel;
	float RunBrake;
	float AirBrake;
	float AirDecel;
	float AirResist;
	float AirResistV;
	float AirResistH;
	float GroundFriction;
	float GroundFrictionH;
	float FrictionCap;
	float RatBound;
	float Radius;
	float Height;
	float Weight;
	float CameraY;
	float CenterHeight;
};

struct Rotation {
	Angle x, y, z;
};

struct CollisionData {
	char kind; // an identifier for colliding entities
	char form;
	char push;
	char damage;
	unsigned int attr;
	NJS_VECTOR center;
	Float param1;
	Float param2;
	Float param3;
	Float param4;
	Rotation rotation;
};

struct CollisionHitInfo {
	__int8 my_num;
	__int8 hit_num;
	unsigned __int16 flag;
	EntityData1* hit_entity;
};

struct CollisionInfo {
	unsigned __int16 Id;
	__int16 HitCount;
	unsigned __int16 Flag;
	unsigned __int16 Count;
	float Range;
	CollisionData* CollisionArray;
	CollisionHitInfo CollisionHits[16]; // the first 16 entities that collide with this
	NJS_POINT3 Normal;
	ObjectMaster* Object;
	__int16 my_num;
	__int16 hit_num;
	CollisionInfo* CollidingObject; // the first colliding object
};

struct EntityData1 {
	char Action;
	char NextAction;
	char field_2;
	char Index;
	__int16 Status;
	__int16 Timer;
	Rotation Rotation;
	NJS_VECTOR Position;
	NJS_VECTOR Scale;
	CollisionInfo* Collision;
};

// Movement information (motionwk internally)
struct EntityData2 {
	NJS_POINT3 Velocity;
	NJS_POINT3 Acceleration;
	Rotation Forward;
	Rotation SpeedAngle;
	float Radius;
	float Height;
	float Weight;
};

struct ChaoEmotions {
	__int16 field_124;
	__int16 Category1Timer;
	__int16 SicknessTimer;
	__int16 Category2Timer;
	char Joy;
	char Anger;
	char UrgeToCry;
	char Fear;
	char Surprise;
	char Dizziness;
	char Relax;
	char Total;
	__int16 Sleepiness;
	__int16 Tiredness;
	__int16 Hunger;
	__int16 MateDesire;
	__int16 Boredom;
	__int16 Lonely;
	__int16 Tire;
	__int16 Stress;
	__int16 Nourish;
	__int16 Conditn;
	__int16 Energy;
	char Normal_Curiosity;
	char Kindness;
	char CryBaby_Energetic;
	char Naive_Normal;
	char Solitude;
	char Vitality;
	char Glutton;
	char Regain;
	char Skillful;
	char Charm;
	char Chatty;
	char Normal_Carefree;
	char Fickle;
	char FavoriteFruit;
	char field_34;
	char field_35;
	char CoughLevel;
	char ColdLevel;
	char RashLevel;
	char RunnyNoseLevel;
	char HiccupsLevel;
	char StomachAcheLevel;
};

struct ChaoDNA {
	char ResetTrigger;
	char field_1[91];
	char SwimStatGrade1;
	char SwimStatGrade2;
	char FlyStatGrade1;
	char FlyStatGrade2;
	char RunStatGrade1;
	char RunStatGrade2;
	char PowerStatGrade1;
	char PowerStatGrade2;
	char StaminaStatGrade1;
	char StaminaStatGrade2;
	char LuckStatGrade1;
	char LuckStatGrade2;
	char IntelligenceStatGrade1;
	char IntelligenceStatGrade2;
	char UnknownStatGrade1;
	char UnknownStatGrade2;
	char field_4A4[34];
	char FavoriteFruit1;
	char FavoriteFruit2;
	char field_4C8[4];
	char Color1;
	char Color2;
	char MonotoneFlag1;
	char MonotoneFlag2;
	char Texture1;
	char Texture2;
	char ShinyFlag1;
	char ShinyFlag2;
	char EggColor1;
	char EggColor2;
	char gap_4D6[6];
};

struct ChaoCharacterBond {
	char a;
	char b;
	int unknown;
};

struct  ChaoDataBase {
	char gap_0[18];
	char Name[7];
	char field_19;
	char GBATexture;
	char field_1B[5];
	char SwimFraction;
	char FlyFraction;
	char RunFraction;
	char PowerFraction;
	char StaminaFraction;
	char LuckyFraction;
	char IntelligenceFraction;
	char UnknownFraction;
	char StatGrades[8];
	char StatLevels[8];
	__int16 StatPoints[8];
	int field_48;
	int field_4C;
	int field_50;
	int field_54;
	int field_58;
	int field_5C;
	int field_60;
	int field_64;
	int someGBAthingstart;
	int field_6C;
	int field_70;
	int field_74;
	int field_78;
	int someGBAthingend;
	ChaoType Type;
	char Garden;
	__int16 Happiness;
	__int16 InKindergarten;
	__int16 ClockRollovers;
	__int16 AdultClockRollovers;
	__int16 Lifespan;
	__int16 Lifespan2;
	__int16 Reincarnations;
	int field_90;
	int field_94;
	int Seed;
	int field_9C;
	int field_A0;
	int TimescaleTimer;
	float PowerRun;
	float FlySwim;
	float Alignment;
	int field_B4;
	int field_B8;
	float LastLifeAlignment;
	float EvolutionProgress;
	char gap_C4[13];
	char EyeType;
	char MouthType;
	char BallType;
	char gap_D4[1];
	char Headgear;
	char HideFeet;
	char Medal;
	unsigned char Color;
	char MonotoneHighlights;
	char Texture;
	char Shiny;
	char EggColor;
	SADXBodyType BodyType;
	char BodyTypeAnimal;
	char field_DF[41];
	__int16 DoctorMedal;
	char field_10A[14];
	int SA2AnimalBehavior;
	SA2BAnimal SA2BArmType;
	SA2BAnimal SA2BEarType;
	SA2BAnimal SA2BForeheadType;
	SA2BAnimal SA2BHornType;
	SA2BAnimal SA2BLegType;
	SA2BAnimal SA2BTailType;
	SA2BAnimal SA2BWingType;
	SA2BAnimal SA2BFaceType;
	ChaoEmotions Emotion;
	char ClassRoomFlags[4];
	__int16 SA2BToys;
	__int16 field_166;
	__int16 field_168;
	__int16 field_16A;
	ChaoCharacterBond SA2BCharacterBonds[6];
	char field_190[680];
	ChaoDNA DNA;
};

struct MotionTableData {
	__int16 flag;
	__int16 gap2;
	int AnimID;
	int TransitionToID;
	float frameIncreaseSpeed_;
	float someSpeedThing;
	float dword14;
	__int16 SomeFlagThingInEntry2;
	__int16 field_1A;
	float frame;
	float StartFrame2;
	float EndFrame2;
	float PlaySpeed2;
	NJS_MOTION* LastNJS_Motion;
	__int16 SomeFlagThingInEntry;
	__int16 field_32;
	float StartFrame;
	float StartFrame_;
	float EndFrame;
	float PlaySpeed;
	NJS_MOTION* NJS_MOTION;
	void* PointerToAnimations;
};

struct ChaoBehaviourInfothing {
	int field_0[8];
};

struct ChaoCurrentActionInfo {
	__int16 field_0;
	unsigned __int16 Index;
	__int16 field_4;
	__int16 field_6;
	int field_8;
	int Timer;
	int field_10;
	int field_14;
	int BehaviourTime;
};

struct ChaoBehaviourInfo {
	ChaoCurrentActionInfo CurrentActionInfo;
	__int16 CanThink_;
	__int16 field_1E;
	int field_20;
	int field_24;
	ChaoBehaviourInfothing field_28;
	ChaoBehaviourInfothing field_48;
	char field_68[8];
	char field_70[72];
	int field_B8;
	int field_BC;
	int field_C0;
	char field_288[420];
	int field_268;
	int field_430;
	float someFloat;
	NJS_VECTOR someKindaPosition;
	int LastBehaviour;
	int behaviourNumber;
	int currentBehaviourIndex;
	int BehaviourBase[15];
	int field_2C8;
	int BehaviourTimer[15];
};

struct ChaoEvos {
	NJS_OBJECT* child[40];
	NJS_OBJECT* normal[40];
	NJS_OBJECT* swim[40];
	NJS_OBJECT* fly[40];
	NJS_OBJECT* run[40];
	NJS_OBJECT* power[40];
};

struct ChaoToyChunk {
	NJS_OBJECT* model;
	NJS_TEXLIST* texlist;
	float scale;
	int exists;
};

struct ChaoFacialData {
	int eyeTimer;
	__int16 field_4;
	__int16 field_6;
	__int16 Eye;
	__int16 field_A;
	int mouthTimer;
	__int16 field_10;
	__int16 Mouth;
	NJS_VECTOR somekindaposition;
	float field_20;
	int field_24;
	NJS_CNK_MODEL* Eye1;
	NJS_CNK_MODEL* Eye2;
	int field_30;
	int blinkState;
	int blinkTimer;
	int dword3C;
	unsigned int unsigned40;
	int gap44;
	int field_48;
	int VertRotAlt;
	int VertRot;
	int field_54;
	int HorizRotAlt;
	int HorizRot;
};

struct EmotionBallData {
	__int16 ballID;
	__int16 ballID2;
	int notsure;
	int timer;
	int sizeSine;
	int yPosSine;
	int color;
	__int16 torchTimer;
	__int16 timer1;
	int randomZRot;
	NJS_VECTOR HeadPosUnitTransPortion;
	NJS_VECTOR parentPosition;
	__int16 gap38;
	unsigned __int16 field_3A;
	__int16 field_3C;
	__int16 field_3E;
	float field_40;
	float sizeTimer;
	int gap48;
	NJS_VECTOR position;
	NJS_VECTOR velocity;
	NJS_VECTOR someSize;
	__int16 field_70;
	__int16 field_72;
	__int16 field_74;
	__int16 field_76;
	__int16 field_78;
	__int16 field_7A;
	__int16 field_7C;
	__int16 field_7E;
	__int16 field_80;
	__int16 field_82;
	int field_84;
	float float88;
	int field_8C;
	NJS_VECTOR float90;
	NJS_VECTOR float9C;
	NJS_VECTOR field_A8;
};

struct ChaoObjectListInfo {
	float MinimumDistance;
	int a2;
	int MinimumRotation;
	float a4;
	float a5;
};

struct ChaoSomeUnknownA {
	__int16 index;
	__int16 index2;
	__int16 ID;
	__int16 someFlag;
	int saveData;
	int field_C;
	NJS_VECTOR locationVector;
	int field_1C;
	int field_20;
	int field_24;
	float field_28;
	float field_2C;
	float playerDistance;
	__int16 setSomeIndex;
	__int16 field_36;
	__int16 field_38;
	__int16 field_3A;
	ObjectMaster* pointerToOwner;
	ChaoSomeUnknownA* heldBy;
	ChaoSomeUnknownA* field_44;
};

struct ChaoGlobalObjectDefinition {
	__int16 field_0;
	__int16 field_2;
	__int16 field_4;
	__int16 field_6;
	float Distance;
	int field_C;
	int field_10;
	int field_14;
	ChaoSomeUnknownA* UnknownA;
};

struct ChaoObjectList {
	__int16 Count;
	__int16 field_2;
	int IsSet;
	int field_8;
	int field_C;
	float Distance;
	__int16 Count2;
	__int16 field_16;
	ChaoGlobalObjectDefinition field_18[32];
};

struct ChaoData1 {
	EntityData1 entity;
	int gap_30;
	int field_34;
	ObjectMaster* ObjectMaster_ptr1;
	int field_38;
	ObjectMaster* ObjectMaster_ptr2;
	int field_44;
	float field_54;
	int field_4C;
	int field_50;
	int field_60;
	int field_58;
	ChaoDataBase* ChaoDataBase_ptr;
	char field_70[40];
	int field_88;
	int field_8C;
	char field_90[16];
	int Flags;
	__int16 field_B4;
	__int16 field_A6;
	float waypointID;
	MotionTableData MotionTable;
	MotionTableData BodyTypeNone_MotionTable;
	char gap144[112];
	ChaoBehaviourInfo ChaoBehaviourInfo;
	int field_4BC[21];
	int PointerToStructWithCnkObject;
	float ChaoNodes[40];
	ChaoEvos* NormalModels;
	ChaoEvos* HeroModels;
	ChaoEvos* DarkModels;
	NJS_VECTOR BaseTranslationPos;
	NJS_VECTOR HeadTranslationPos;
	NJS_VECTOR LeftHandTranslationPos;
	NJS_VECTOR RightHandTranslationPos;
	NJS_VECTOR LeftLegTranslationPos;
	NJS_VECTOR RightLegTranslationPos;
	NJS_VECTOR NoseTranslationPos;
	NJS_VECTOR NoseUnitTransPortion;
	NJS_VECTOR LeftEyeTranslationPos;
	NJS_VECTOR LeftEyeUnitTransPortion;
	NJS_VECTOR RightEyeTranslationPos;
	NJS_VECTOR RightEyeUnitTransPortion;
	ChaoToyChunk LeftHandToyChunk;
	ChaoToyChunk RightHandToyChunk;
	int field_670;
	int field_674;
	int field_678;
	int field_67C;
	int field_680;
	int field_684;
	int field_688;
	ChaoFacialData ChaoFacialData;
	EmotionBallData EmotionBallData;
	NJS_VECTOR field_7A4;
	float waypointLerpFactor;
	NJS_VECTOR field_7B4;
	NJS_VECTOR field_7C0;
	ChaoObjectListInfo ObjectListInfo;
	ChaoObjectList PlayerObjects;
	ChaoObjectList ChaoObjects;
	ChaoObjectList FruitObjects;
	ChaoObjectList TreeObjects;
	ChaoObjectList ToyObjects;
	char ObjectListEnd;
	char field_19D9[927];
};

struct ChaoDebugData1 {
	char CurrentMenu;
	char MenuSelection;
	char field_2[18];
	__int16 ChaoID;
	char field_16[10];
};

union __declspec(align(2)) Data1Ptr {
	void* Undefined;
	EntityData1* Entity;
	ChaoData1* Chao;
	ChaoDebugData1* ChaoDebug;
};

struct ObjUnknownB {
	int Time;
	int Index;
	int Mode;
	int field_C;
};

struct UnknownData2_B {
	int field_0;
	char gap_4[12];
	float field_10;
	NJS_VECTOR field_14;
};

struct UnknownData2 {
	NJS_VECTOR vector_0;
	char gap_C[4];
	float field_10;
	char gap_14[8];
	int field_1C;
	char gap_20[8];
	int field_28;
	int field_2C;
	float field_30;
	int field_34;
	char gap_38[4];
	int field_3C;
	__int16 field_40;
	__int16 Index;
	char gap_44[4];
	float field_48;
	char gap_4C[20];
	NJS_VECTOR some_vector;
	char gap_6C[4];
	float field_70;
	char gap_74[28];
	float field_90;
	float field_94;
	float field_98;
	float field_9C;
	float field_A0;
	float field_A4;
	float field_A8;
	float field_AC;
	char gap_B0[4];
	float field_B4;
	float field_B8;
	float field_BC;
	float field_C0;
	float field_C4;
	float field_C8;
	float field_CC;
	float field_D0;
	char gap_D4[4];
	float field_D8;
	float field_DC;
	NJS_VECTOR field_E0;
	UnknownData2_B WeirdStructures[12];
};

union Data2Ptr {
	void* Undefined;
	ObjUnknownB* UnknownB;
	EntityData2* Entity;
	CharObj2Base* Character;
	UnknownData2* Unknown_Chao;
};

// Vertical surface information for shadows and ripples
struct CharSurfaceInfo {
	Angle AngX;
	Angle AngZ;
	SurfaceFlags TopSurface;
	SurfaceFlags BottomSurface;
	Float TopSurfaceDist;
	Float BottomSurfaceDist;
	SurfaceFlags PrevTopSurface;
	SurfaceFlags PrevBottomSurface;
};

// Contains input (first 4 variables) and output information for the dynamic collision system.
struct csts {
	float radius;
	NJS_POINT3 pos;
	NJS_POINT3 spd;
	NJS_POINT3 tnorm;
	unsigned __int16 find_count;
	unsigned __int16 selected_nmb;
	float yt;
	float yb;
	int angx;
	int angz;
	NJS_POINT3 normal;
	NJS_POINT3 normal2;
	NJS_POINT3 onpoly;
	NJS_POINT3 pshbk;
	NJS_POINT3 anaspdh;
	NJS_POINT3 anaspdv;
};

struct SETEntry {
	__int16 ID;
	__int16 XRot;
	__int16 YRot;
	__int16 ZRot;
	NJS_VECTOR Position;
	NJS_VECTOR Scale;
};

struct SETObjectData {
	uint8_t LoadCount;
	char field_1;
	__int16 Flags;
	ObjectMaster* Object;
	SETEntry* SETEntry;
	float Distance;
};

struct ObjUnknownA {
	int field_0;
	int field_4;
	int field_8;
	int field_C;
	int field_10;
	int field_14;
	int field_18;
	int field_1C;
	int field_20;
	float field_24;
	float field_28;
	float field_2C;
	int field_30;
	int field_34;
};

struct ObjectMaster {
	ObjectMaster* PrevObject;
	ObjectMaster* NextObject;
	ObjectMaster* Parent;
	ObjectMaster* Child;
	ObjectFuncPtr MainSub;
	ObjectFuncPtr DisplaySub;
	ObjectFuncPtr DeleteSub;
	ObjectFuncPtr DisplaySub_Delayed1;
	ObjectFuncPtr DisplaySub_Delayed2;
	ObjectFuncPtr DisplaySub_Delayed3;
	ObjectFuncPtr DisplaySub_Delayed4;
	void* field_2C;
	SETObjectData* SETData;
	Data1Ptr Data1;
	UnknownData2* EntityData2;
	ObjUnknownA* UnknownA_ptr;
	Data2Ptr Data2;
	char* Name;
	char* NameAgain;
	void* field_4C;
};

struct LoopPoint {
	__int16 XRot;
	__int16 YRot;
	float Distance;
	NJS_VECTOR Position;
};

struct LoopHead {
	__int16 anonymous_0;
	__int16 Count;
	float TotalDistance;
	LoopPoint* Points;
	ObjectFuncPtr Object;
};

// Player-specific data, common base for all characters.
struct CharObj2Base {
	char PlayerNum;
	char CharID;
	char Costume;
	char CharID2;
	char ActionWindowItems[8];
	char ActionWindowItemCount;
	char field_D[3];
	__int16 Powerups;
	__int16 field_12;
	__int16 DisableControlTimer;
	__int16 UnderwaterTime;
	__int16 IdleTime;
	__int16 ConfuseTime;
	char gap1C[8];
	int Upgrades;
	float field_28;
	Angle TiltAngle;
	float PathDist;
	float Up;
	char field_38[8];
	float SomeMultiplier;
	int field_44;
	float MechHP;
	NJS_POINT3 Eff;
	NJS_POINT3 Acceleration;
	NJS_VECTOR Speed;
	NJS_POINT3 WallNormal;
	NJS_POINT3 FloorNormal;
	SurfaceFlags CurrentSurfaceFlags;
	SurfaceFlags PreviousSurfaceFlags;
	csts* DynColInfo;
	ObjectMaster* HeldObject;
	char gap98[4];
	ObjectMaster* HoldTarget;
	ObjectMaster* CurrentDyncolTask;
	int field_A4;
	char gapA8[16];
	LoopHead* PathData;
	NJS_MOTION** Animation;
	PhysicsData PhysData;
	NJS_VECTOR SomeVectors[4];
	CharAnimInfo AnimInfo;
	CharSurfaceInfo SurfaceInfo;
};

struct Emerald {
	int id;
	NJS_VECTOR v;
};

struct EmeraldManager {
	uint8_t Action;
	uint8_t byte1;
	uint8_t byte2;
	uint8_t HintCount;
	uint8_t Status;
	uint8_t EmeraldsSpawned;
	uint8_t Slot1ArrayLen;
	uint8_t Slot2ArrayLen;
	uint8_t Slot3ArrayLen;
	uint8_t EnemySlotArrayLen;
	uint8_t byteA;
	uint8_t byteB;
	Emerald duplicate_emerald_1;
	Emerald duplicate_emerald_2;
	Emerald Piece1;
	Emerald Piece2;
	Emerald Piece3;
	Emerald* Slot1Emeralds;
	Emerald* Slot2Emeralds;
	Emerald* Slot3Emeralds;
	Emerald* EnemySlotEmeralds;
	uint32_t timer;
	NJS_TEXLIST* TexList;
};

#pragma pack(pop)