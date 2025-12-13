namespace sa2_hunting_teacher {
	public enum Level {
		WildCanyon,
		PumpkinHill,
		AquaticMine,
		DeathChamber,
		MeteorHerd
	};

	internal abstract class HuntingLevel(SA2Manager manager, byte repetitions, HuntingTeacherForm teacherForm) {
		protected abstract int[] Sequence { get; }

		private int Next = 0;
		private int SequenceCount = 0;
		private bool LevelLoading = false;
		private bool InLevel = false;

		protected SA2Manager Manager { get; } = manager;

		protected byte Repetitions { get; } = repetitions;

		protected HuntingTeacherForm TeacherForm { get; } = teacherForm;

		public bool SequenceComplete() {
			return SequenceCount >= this.Sequence.Length * this.Repetitions;
		}

		public void RunSequence() {
			if (!this.InLevel && !this.LevelLoading) {
				this.LevelLoading = this.Manager.IsLevelLoading();
				return;
			}

			if (this.LevelLoading) {
				this.TeacherForm.Invoke(() => {
					HuntingTeacherForm.AddLogItem("Level Starting!");
				});

				this.LevelLoading = false;
				this.Manager.SetSeed(this.Sequence[Next]);
				this.InLevel = true;
				return;
			}

			// if we make it here then we must be in level
			if (this.Manager.IsInWinScreen()) {
				this.Next = this.NextSequence();
				this.SequenceCount++;
				this.InLevel = false;
			}
		}

		private int NextSequence() {
			return Next + 1 >= this.Sequence.Length ? 0 : Next + 1;
		}
	}
}
