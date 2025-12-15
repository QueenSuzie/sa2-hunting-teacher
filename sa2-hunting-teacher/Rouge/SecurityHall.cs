namespace sa2_hunting_teacher.Knuckles {
	internal class SecurityHall(SA2Manager manager, byte repetitions) : HuntingLevel(manager, repetitions) {
		public override LevelId LevelId => LevelId.SecurityHall;

		protected override Set[] Sequence { get; } = [
		];

		public override string ToString() => "Security Hall";

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
