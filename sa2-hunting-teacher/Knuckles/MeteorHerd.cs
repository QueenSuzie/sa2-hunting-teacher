namespace sa2_hunting_teacher.Knuckles {
	internal class MeteorHerd(SA2Manager manager, byte repetitions, HuntingTeacherForm teacherForm) : HuntingLevel(manager, repetitions, teacherForm) {
		protected override int[] Sequence { get; } = { 0 };

		public override string ToString() => "Meteor Herd";
	}
}
