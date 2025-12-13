namespace sa2_hunting_teacher {
	public class Set(int id, string p1, string p2, string p3) {
		public int Id = id;
		public string P1 = p1;
		public string P2 = p2;
		public string P3 = p3;

		public override string ToString() {
			return $"{Environment.NewLine}	P1: {this.P1}{Environment.NewLine}	P2: {this.P2}{Environment.NewLine}	P3: {this.P3}"; 
		}
	}
}
