namespace sa2_hunting_teacher.Rouge {
	internal class MadSpace(SA2Manager manager, byte repetitions) : HuntingLevel(manager, repetitions) {
		public override LevelId LevelId => LevelId.MadSpace;

		protected override Set[] Sequence { get; } = [
		];

		public override string ToString() => "Mad Space";

		public static Dictionary<int, string> PieceToHint { get; } = new Dictionary<int, string> {
		};

		internal enum EnemyId {
		}

		internal enum P1Id {
		}

		internal enum P2Id {
		}

		internal enum P3Id {
		}
	}
}
