namespace sa2_hunting_teacher.Knuckles {
	internal class WildCanyon(SA2Manager manager, byte repetitions) : HuntingLevel(manager, repetitions) {
		public override LevelId LevelId => LevelId.WildCanyon;

		protected override Set[] Sequence { get; } = [
			new((int)P1Ids.BirdBoxRedPing, 0x0305, 0x0605, "In front of the double containers. (right)", "Where the wind blows.", "The beginning room.")
		];

		public override string ToString() => "Wild Canyon";
	}

	internal enum EnemyIds {
		SuspendedCeilingEnemy = 0x000A,
		SandyPathRhino = 0x010A,
		SandyPathNearHawk = 0x020A,
		SandyPathFarHawk = 0x030A,
		NearSixPillarsRedCircle = 0x040A,
		NearSixPillarsMetalBoxes = 0x050A,
		NearSixPillarsScaryEyes = 0x060A,
		NearSixPillarsRailways = 0x070A,
		StoneStatueQuartetHeadSide = 0x080A,
		StoneStatueQuartetLonelySide = 0x090A
	}

	internal enum P1Ids {
		InFrontDoubleContainersLeft = 0x0001,
		InFrontDoubleContainersRight = 0x0101,
		InFrontBirdChestUnderFourFeathers = 0x0201,
		OnSideBirdChestUnderFourFeathers = 0x0301,
		SuspendedCeilingExposed = 0x0401,
		GoldenCircleOnRedCircle = 0x0501,
		HesHoldingABox = 0x0601,
		AParakeet = 0x0701,
		BottomSwirlTilePyramid = 0x0801,
		ByAWindJumpBoard = 0x0901,
		PlacePointedTwoPriests = 0x0A01,
		HeadOfThe4thStoneStatue = 0x0B01,
		BetweenTwinStars = 0x0C01,
		APairOfEarrings = 0x0D01,
		OnThePillar = 0x0E01,
		BottomOfAPillar = 0x0F01,
		SpiralTile = 0x1001,
		OverTheHead = 0x1101,
		AltarWithSixPillars = 0x1201,
		BetweenFourBirds = 0x1301,
		BetweenRedAndGreenStripedPillars = 0x1401,
		WildCanyonTrack = 0x1501,
		BottomOfHugeWallPainting = 0x1601,
		TopOfTenFingers = 0x1701,
		DoubleContainersLeft = 0x0003,
		DoubleContainersRight = 0x0103,
		InsideStatueStorageAreaRight = 0x0203,
		StatueWithScaryEyes = 0x0303,
		InsideStatueStorageAreaLeft = 0x0403,
		TreasureOfFourStoneStatues = 0x0503,
		BirdBoxRedPing = 0x0603,
		SwirlingContainer = 0x0703
	}

	internal enum P2Ids {
		ArmoredCarpoolCenter = 0x0000,
		ArmoredCarpoolCorner = 0x0100,
		AtTheFootOfAPillar = 0x0200,
		SwirlPyramid = 0x0300,
		TerraceWithTwoLegsUnder = 0x0400,
		TerraceWithTwoLegsBehind = 0x0500,
		NearAChair = 0x0600,
		SandyPathLeadsStoneStatueHead = 0x0700,
		FourWhirlingEyes = 0x0002,
		FourSwirlingEyes = 0x0102,
		InsideStatueStorageArea = 0x0202,
		BirdBoxInTheShade = 0x0402,
		BottomOfAPillar = 0x0502,
		BirdBoxUpper = 0x0602,
		UnderTheElbowLonely = 0x0702,
		UnderWingsOfTwoBirds = 0x0005,
		RedGateChest = 0x0105,
		RedGateBox = 0x0205,
		BirdBoxMiddle = 0x0305,
		WhereTheWindBlows = 0x0405,
		SwirlingContainer = 0x0505,
		BirdBoxLeft = 0x0605,
		WildCanyonTrackCorner = 0x0705,
		UnderTheElbowHead = 0x0805,
		BehindAFaceRight = 0x0905,
		BehindAFaceMiddle = 0x0A05,
		OnARailwayUpper = 0x0B05,
		OnARailwayBottomRight = 0x0C05,
		OnARailwayBottomLeft = 0x0D05,
		SwirlingArrow = 0x0E05,
		WildCanyonTrackCenter = 0x0F05
	}

	internal enum P3Ids {

	}
}
