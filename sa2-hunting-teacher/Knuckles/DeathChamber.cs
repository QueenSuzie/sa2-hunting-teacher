namespace sa2_hunting_teacher.Knuckles {
	internal class DeathChamber(SA2Manager manager, byte repetitions) : HuntingLevel(manager, repetitions) {
		public override LevelId LevelId => LevelId.DeathChamber;

		protected override Set[] Sequence { get; } = { };

		public override string ToString() => "Death Chamber";
	}
}
