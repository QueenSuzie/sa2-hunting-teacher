namespace sa2_hunting_teacher.Knuckles {
	internal class MeteorHerd(SA2Manager manager, byte repetitions) : HuntingLevel(manager, repetitions) {
		public override LevelId LevelId => LevelId.MeteorHerd;

		protected override Set[] Sequence { get; } = { };

		public override string ToString() => "Meteor Herd";
	}
}
