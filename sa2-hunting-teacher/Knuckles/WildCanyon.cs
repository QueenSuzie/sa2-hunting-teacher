namespace sa2_hunting_teacher.Knuckles {
	internal class WildCanyon(SA2Manager manager, byte repetitions) : HuntingLevel(manager, repetitions) {
		protected override int[] Sequence { get; } = { 17, 17 };

		public override string ToString() => "Wild Canyon";
	}
}
