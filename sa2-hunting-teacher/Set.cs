namespace sa2_hunting_teacher {
	public class Set(int p1Id, int p2Id, int p3Id, string p1, string p2, string p3) {
		public int P1Id = p1Id;
		public int P2Id = p2Id;
		public int P3Id = p3Id;
		public string P1 = p1;
		public string P2 = p2;
		public string P3 = p3;

		public override string ToString() {
			return $"{Environment.NewLine}	P1: {this.P1}{Environment.NewLine}	P2: {this.P2}{Environment.NewLine}	P3: {this.P3}"; 
		}
	}
}
