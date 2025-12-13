namespace sa2_hunting_teacher.Knuckles {
	internal class AquaticMine(SA2Manager manager, byte repetitions) : HuntingLevel(manager, repetitions) {
		public override LevelId LevelId => LevelId.AquaticMine;

		protected override Set[] Sequence { get; } = { };

		public override string ToString() => "Aquatic Mine";
	}
}
