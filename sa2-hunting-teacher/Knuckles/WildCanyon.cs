namespace sa2_hunting_teacher.Knuckles {
	internal class WildCanyon(SA2Manager manager, byte repetitions) : HuntingLevel(manager, repetitions) {
		public override LevelId LevelId => LevelId.WildCanyon;

		protected override Set[] Sequence { get; } = [
			new(15, 2, 3, "In front of the double containers. (right)", "Where the wind blows.", "The beginning room.")
		];

		public override string ToString() => "Wild Canyon";
	}
}
