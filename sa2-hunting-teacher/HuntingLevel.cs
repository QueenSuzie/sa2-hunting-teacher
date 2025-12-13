namespace sa2_hunting_teacher {
	public enum Level {
		WildCanyon,
		PumpkinHill,
		AquaticMine,
		DeathChamber,
		MeteorHerd
	};

	internal abstract class HuntingLevel(SA2Manager manager, byte repetitions) {
		protected abstract Set[] Sequence { get; }
		public abstract LevelId LevelId { get; }

		private int Next = 0;
		private int SequenceCount = 0;

		protected SA2Manager Manager { get; } = manager;

		protected byte Repetitions { get; } = repetitions;

		public bool SequenceComplete() {
			return SequenceCount >= this.Sequence.Length * this.Repetitions;
		}

		public void RunSequence() {
			this.Manager.ApplySet(this.Sequence[Next], this.Next + 1, this.Sequence.Length, (int) Math.Ceiling((double)(this.SequenceCount + 1) / (double)this.Sequence.Length));

			if (this.Manager.IsInWinScreen()) {
				this.Next = this.NextSequence();
				this.SequenceCount++;
			}
		}

		private int NextSequence() {
			return this.Next + 1 >= this.Sequence.Length ? 0 : this.Next + 1;
		}
	}
}
