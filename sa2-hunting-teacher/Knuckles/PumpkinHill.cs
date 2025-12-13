namespace sa2_hunting_teacher.Knuckles {
	internal class PumpkinHill(SA2Manager manager, byte repetitions) : HuntingLevel(manager, repetitions) {
		protected override int[] Sequence { get; } = { 1, 40 };

		public override string ToString() => "Pumpkin Hill";
	}
}
