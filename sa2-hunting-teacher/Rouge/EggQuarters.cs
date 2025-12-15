namespace sa2_hunting_teacher.Knuckles {
	internal class EggQuarters(SA2Manager manager, byte repetitions) : HuntingLevel(manager, repetitions) {
		public override LevelId LevelId => LevelId.EggQuarters;

		protected override Set[] Sequence { get; } = [
		];

		public override string ToString() => "Egg Quarters";

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
