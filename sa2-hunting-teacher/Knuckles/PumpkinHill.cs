namespace sa2_hunting_teacher.Knuckles {
	internal class PumpkinHill(SA2Manager manager, byte repetitions) : HuntingLevel(manager, repetitions) {
		public override LevelId LevelId => LevelId.PumpkinHill;

		protected override Set[] Sequence { get; } = { };

		public override string ToString() => "Pumpkin Hill";
	}
}
