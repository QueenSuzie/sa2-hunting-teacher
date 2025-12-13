namespace sa2_hunting_teacher.Knuckles {
	internal class DeathChamber(SA2Manager manager, byte repetitions, HuntingTeacherForm teacherForm) : HuntingLevel(manager, repetitions, teacherForm) {
		protected override int[] Sequence { get; } = { 0 };

		public override string ToString() => "Death Chamber";
	}
}
