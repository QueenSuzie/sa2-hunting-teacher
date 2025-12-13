namespace sa2_hunting_teacher {
	internal static class Program {
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			if (!Environment.IsPrivilegedProcess) {
				MessageBox.Show("Please run this application as an administrator.", "Privileges Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
				System.Environment.Exit(1);
			}

			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			Application.Run(new HuntingTeacherForm());
		}
	}
}