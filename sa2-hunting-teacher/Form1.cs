namespace sa2_hunting_teacher {
	public partial class HuntingTeacherForm : Form {
		private static TextBox? currentLogBox;

		public sealed class LevelRow {
			public Level Level { get; init; } = default;
			public string Text { get; init; } = "";
			public string Group { get; init; } = "";
		}

		private static readonly Dictionary<Level, (string LevelText, string Category)> SupportedLevels = new() {
			/** Knuckles */
			{ Level.WildCanyon, ("Wild Canyon", "Knuckles") },
			/*{ Level.PumpkinHill, ("Pumpkin Hill", "Knuckles") },
			{ Level.AquaticMine, ("Aquatic Mine", "Knuckles") },
			{ Level.DeathChamber, ("Death Chamber", "Knuckles") },
			{ Level.MeteorHerd, ("Meteor Herd", "Knuckles") },*/
			/** Rouge */
			{ Level.DryLagoon, ("Dry Lagoon", "Rouge") },
			/*{ Level.EggQuarters, ("Egg Quarters", "Rouge") },
			{ Level.SecurityHall, ("Security Hall", "Rouge") },
			{ Level.MadSpace, ("Mad Space", "Rouge") },*/
		};

		public HuntingTeacherForm() {
			InitializeComponent();

			currentLogBox = logBox;
			levelSelector.DisplayMember = nameof(LevelRow.Text);
			levelSelector.ValueMember = nameof(LevelRow.Level);
			levelSelector.GroupMember = nameof(LevelRow.Group);
			levelSelector.SelectedValue = Level.WildCanyon;
			levelSelector.DataSource = SupportedLevels.Select(kvp => new LevelRow {
				Level = kvp.Key,
				Text = kvp.Value.LevelText,
				Group = kvp.Value.Category
			}).ToList();

			InitSettings();
		}

		private void InitSettings() {
			this.mspReverseHints.Checked = Properties.Settings.Default.mspReversedHints;
		}

		private void SaveSettings() {
			Properties.Settings.Default.mspReversedHints = this.mspReverseHints.Checked;
			Properties.Settings.Default.Save();
		}

		public static void AddLogItem(string value) {
			if (currentLogBox == null) {
				return;
			}

			currentLogBox.AppendText(value + Environment.NewLine);
		}
		public bool MspReversedHints() {
			return mspReverseHints.Checked;
		}

		private void StartBtn_Click(object sender, EventArgs e) {
			Level selectedLevel = (Level)levelSelector.SelectedValue!;
			startBtn.Enabled = false;
			repetitions.Enabled = false;
			mspReverseHints.Enabled = false;
			resetBtn.Enabled = true;

			Task.Run(() => {
				try {
					SA2Manager.Start(selectedLevel, (byte)repetitions.Value, this);
				} catch (ArgumentException) {
					this.Invoke(() => {
						MessageBox.Show(this, "A running instance of SA2 could not be found.\n" +
							"Make sure SA2 is running first before trying to start a level.", "SA2 Not Running", MessageBoxButtons.OK, MessageBoxIcon.Error);

						this.ResetBtn_Click(sender, e);
					});
				}
			});
		}

		public void ResetBtn_Click(object sender, EventArgs e) {
			SA2Manager.Stop();
			resetBtn.Enabled = false;
			repetitions.Enabled = true;
			mspReverseHints.Enabled = true;
			startBtn.Enabled = true;
		}

		private void LevelSelector_SelectedIndexChanged(object sender, EventArgs e) {
			mspReverseHints.Visible = false;
			if (this.levelSelector.SelectedItem != null && ((LevelRow)this.levelSelector.SelectedItem).Level == Level.MadSpace) {
				mspReverseHints.Visible = true;
			}
		}

		private void MspReverseHints_CheckedChanged(object sender, EventArgs e) {
			this.SaveSettings();
		}
	}
}
