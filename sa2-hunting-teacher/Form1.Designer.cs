using sa2_hunting_teacher.DropdownControls;

namespace sa2_hunting_teacher
{
    partial class HuntingTeacherForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HuntingTeacherForm));
			panel1 = new Panel();
			mspReverseHints = new CheckBox();
			label2 = new Label();
			repetitions = new NumericUpDown();
			resetBtn = new Button();
			startBtn = new Button();
			label1 = new Label();
			levelSelector = new GroupedComboBox();
			panel2 = new Panel();
			logBox = new TextBox();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)repetitions).BeginInit();
			panel2.SuspendLayout();
			SuspendLayout();
			// 
			// panel1
			// 
			panel1.Controls.Add(mspReverseHints);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(repetitions);
			panel1.Controls.Add(resetBtn);
			panel1.Controls.Add(startBtn);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(levelSelector);
			panel1.Dock = DockStyle.Top;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(862, 53);
			panel1.TabIndex = 0;
			// 
			// mspReverseHints
			// 
			mspReverseHints.AutoSize = true;
			mspReverseHints.Location = new Point(356, 15);
			mspReverseHints.Name = "mspReverseHints";
			mspReverseHints.Size = new Size(129, 24);
			mspReverseHints.TabIndex = 6;
			mspReverseHints.Text = "Reversed Hints";
			mspReverseHints.UseVisualStyleBackColor = true;
			mspReverseHints.Visible = false;
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label2.AutoSize = true;
			label2.Location = new Point(512, 16);
			label2.Name = "label2";
			label2.Size = new Size(87, 20);
			label2.TabIndex = 5;
			label2.Text = "Repetitions:";
			// 
			// repetitions
			// 
			repetitions.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			repetitions.Location = new Point(607, 14);
			repetitions.Maximum = new decimal(new int[] { 9, 0, 0, 0 });
			repetitions.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			repetitions.Name = "repetitions";
			repetitions.Size = new Size(43, 27);
			repetitions.TabIndex = 4;
			repetitions.Value = new decimal(new int[] { 3, 0, 0, 0 });
			// 
			// resetBtn
			// 
			resetBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			resetBtn.Enabled = false;
			resetBtn.Location = new Point(656, 12);
			resetBtn.Name = "resetBtn";
			resetBtn.Size = new Size(94, 29);
			resetBtn.TabIndex = 3;
			resetBtn.Text = "Reset";
			resetBtn.UseVisualStyleBackColor = true;
			resetBtn.Click += ResetBtn_Click;
			// 
			// startBtn
			// 
			startBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			startBtn.Location = new Point(756, 11);
			startBtn.Name = "startBtn";
			startBtn.Size = new Size(94, 29);
			startBtn.TabIndex = 2;
			startBtn.Text = "Start";
			startBtn.UseVisualStyleBackColor = true;
			startBtn.Click += StartBtn_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 15);
			label1.Name = "label1";
			label1.Size = new Size(90, 20);
			label1.TabIndex = 1;
			label1.Text = "Level Select:";
			// 
			// levelSelector
			// 
			levelSelector.DataSource = null;
			levelSelector.DropDownStyle = ComboBoxStyle.DropDownList;
			levelSelector.FormattingEnabled = true;
			levelSelector.Location = new Point(105, 12);
			levelSelector.Name = "levelSelector";
			levelSelector.Size = new Size(245, 28);
			levelSelector.TabIndex = 0;
			levelSelector.SelectedIndexChanged += LevelSelector_SelectedIndexChanged;
			// 
			// panel2
			// 
			panel2.Controls.Add(logBox);
			panel2.Dock = DockStyle.Fill;
			panel2.Location = new Point(0, 53);
			panel2.Name = "panel2";
			panel2.Size = new Size(862, 412);
			panel2.TabIndex = 1;
			// 
			// logBox
			// 
			logBox.Dock = DockStyle.Fill;
			logBox.Location = new Point(0, 0);
			logBox.Multiline = true;
			logBox.Name = "logBox";
			logBox.ReadOnly = true;
			logBox.ScrollBars = ScrollBars.Vertical;
			logBox.Size = new Size(862, 412);
			logBox.TabIndex = 0;
			logBox.WordWrap = false;
			// 
			// HuntingTeacherForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(862, 465);
			Controls.Add(panel2);
			Controls.Add(panel1);
			Icon = (Icon)resources.GetObject("$this.Icon");
			MinimumSize = new Size(845, 384);
			Name = "HuntingTeacherForm";
			Text = "Hunting Teacher";
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)repetitions).EndInit();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Panel panel1;
		private Panel panel2;
		private Label label1;
		private Button resetBtn;
		private Button startBtn;
		private TextBox logBox;
		private NumericUpDown repetitions;
		private Label label2;
		private CheckBox mspReverseHints;
		private GroupedComboBox levelSelector;
	}
}
