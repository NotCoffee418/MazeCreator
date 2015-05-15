namespace MazeCreator
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.objectIdTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.objectSpacingTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.wallHeightTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.xCountTextBox = new System.Windows.Forms.TextBox();
            this.yCountTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.floorCheckBox = new System.Windows.Forms.CheckBox();
            this.roofCheckBox = new System.Windows.Forms.CheckBox();
            this.saveConfigButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.xTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.yTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.zTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.mapTextBox = new System.Windows.Forms.TextBox();
            this.advancedCheckBox = new System.Windows.Forms.CheckBox();
            this.openMazeButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.levelCountTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.heightWarningLabel = new System.Windows.Forms.Label();
            this.configTabControl = new System.Windows.Forms.TabControl();
            this.basicConfigTab = new System.Windows.Forms.TabPage();
            this.advancedConfigTab = new System.Windows.Forms.TabPage();
            this.advancedSaveConfigButton = new System.Windows.Forms.Button();
            this.configTabControl.SuspendLayout();
            this.basicConfigTab.SuspendLayout();
            this.advancedConfigTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // objectIdTextBox
            // 
            this.objectIdTextBox.Enabled = false;
            this.objectIdTextBox.Location = new System.Drawing.Point(128, 31);
            this.objectIdTextBox.Name = "objectIdTextBox";
            this.objectIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.objectIdTextBox.TabIndex = 8;
            this.objectIdTextBox.Text = "745000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Walls Object ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "(default: crates)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Space between objects";
            // 
            // objectSpacingTextBox
            // 
            this.objectSpacingTextBox.Enabled = false;
            this.objectSpacingTextBox.Location = new System.Drawing.Point(128, 57);
            this.objectSpacingTextBox.Name = "objectSpacingTextBox";
            this.objectSpacingTextBox.Size = new System.Drawing.Size(100, 20);
            this.objectSpacingTextBox.TabIndex = 9;
            this.objectSpacingTextBox.Text = "2.5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Wall Height";
            // 
            // wallHeightTextBox
            // 
            this.wallHeightTextBox.Enabled = false;
            this.wallHeightTextBox.Location = new System.Drawing.Point(128, 83);
            this.wallHeightTextBox.Name = "wallHeightTextBox";
            this.wallHeightTextBox.Size = new System.Drawing.Size(100, 20);
            this.wallHeightTextBox.TabIndex = 10;
            this.wallHeightTextBox.Text = "2";
            this.wallHeightTextBox.TextChanged += new System.EventHandler(this.wallHeightTextBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(235, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "How many objects";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Maze Size";
            // 
            // xCountTextBox
            // 
            this.xCountTextBox.Location = new System.Drawing.Point(137, 32);
            this.xCountTextBox.Name = "xCountTextBox";
            this.xCountTextBox.Size = new System.Drawing.Size(34, 20);
            this.xCountTextBox.TabIndex = 5;
            this.xCountTextBox.Text = "15";
            // 
            // yCountTextBox
            // 
            this.yCountTextBox.Location = new System.Drawing.Point(203, 32);
            this.yCountTextBox.Name = "yCountTextBox";
            this.yCountTextBox.Size = new System.Drawing.Size(34, 20);
            this.yCountTextBox.TabIndex = 6;
            this.yCountTextBox.Text = "15";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(177, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "x";
            // 
            // floorCheckBox
            // 
            this.floorCheckBox.AutoSize = true;
            this.floorCheckBox.Checked = true;
            this.floorCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.floorCheckBox.Location = new System.Drawing.Point(11, 85);
            this.floorCheckBox.Name = "floorCheckBox";
            this.floorCheckBox.Size = new System.Drawing.Size(80, 17);
            this.floorCheckBox.TabIndex = 11;
            this.floorCheckBox.Text = "Create floor";
            this.floorCheckBox.UseVisualStyleBackColor = true;
            // 
            // roofCheckBox
            // 
            this.roofCheckBox.AutoSize = true;
            this.roofCheckBox.Checked = true;
            this.roofCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.roofCheckBox.Location = new System.Drawing.Point(11, 108);
            this.roofCheckBox.Name = "roofCheckBox";
            this.roofCheckBox.Size = new System.Drawing.Size(78, 17);
            this.roofCheckBox.TabIndex = 12;
            this.roofCheckBox.Text = "Create roof";
            this.roofCheckBox.UseVisualStyleBackColor = true;
            // 
            // saveConfigButton
            // 
            this.saveConfigButton.Location = new System.Drawing.Point(247, 87);
            this.saveConfigButton.Name = "saveConfigButton";
            this.saveConfigButton.Size = new System.Drawing.Size(118, 38);
            this.saveConfigButton.TabIndex = 13;
            this.saveConfigButton.Text = "Open Editor";
            this.saveConfigButton.UseVisualStyleBackColor = true;
            this.saveConfigButton.Click += new System.EventHandler(this.saveConfigButton_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(115, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(12, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "x";
            // 
            // xTextBox
            // 
            this.xTextBox.Location = new System.Drawing.Point(137, 6);
            this.xTextBox.Name = "xTextBox";
            this.xTextBox.Size = new System.Drawing.Size(50, 20);
            this.xTextBox.TabIndex = 2;
            this.xTextBox.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(193, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(12, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "y";
            // 
            // yTextBox
            // 
            this.yTextBox.Location = new System.Drawing.Point(211, 6);
            this.yTextBox.Name = "yTextBox";
            this.yTextBox.Size = new System.Drawing.Size(47, 20);
            this.yTextBox.TabIndex = 3;
            this.yTextBox.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(260, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(12, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "z";
            // 
            // zTextBox
            // 
            this.zTextBox.Location = new System.Drawing.Point(276, 6);
            this.zTextBox.Name = "zTextBox";
            this.zTextBox.Size = new System.Drawing.Size(53, 20);
            this.zTextBox.TabIndex = 4;
            this.zTextBox.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "MapID";
            // 
            // mapTextBox
            // 
            this.mapTextBox.Location = new System.Drawing.Point(53, 6);
            this.mapTextBox.Name = "mapTextBox";
            this.mapTextBox.Size = new System.Drawing.Size(32, 20);
            this.mapTextBox.TabIndex = 1;
            this.mapTextBox.Text = "0";
            // 
            // advancedCheckBox
            // 
            this.advancedCheckBox.AutoSize = true;
            this.advancedCheckBox.Location = new System.Drawing.Point(6, 6);
            this.advancedCheckBox.Name = "advancedCheckBox";
            this.advancedCheckBox.Size = new System.Drawing.Size(186, 17);
            this.advancedCheckBox.TabIndex = 7;
            this.advancedCheckBox.Text = "I know what I\'m doing (advanced)";
            this.advancedCheckBox.UseVisualStyleBackColor = true;
            this.advancedCheckBox.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // openMazeButton
            // 
            this.openMazeButton.Location = new System.Drawing.Point(137, 101);
            this.openMazeButton.Name = "openMazeButton";
            this.openMazeButton.Size = new System.Drawing.Size(91, 24);
            this.openMazeButton.TabIndex = 28;
            this.openMazeButton.Text = "Open Maze File";
            this.openMazeButton.UseVisualStyleBackColor = true;
            this.openMazeButton.Click += new System.EventHandler(this.openMazeButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "MyMaze.maze";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(244, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "(Use edit menu in editor)";
            this.label6.Visible = false;
            // 
            // levelCountTextBox
            // 
            this.levelCountTextBox.Location = new System.Drawing.Point(137, 58);
            this.levelCountTextBox.Name = "levelCountTextBox";
            this.levelCountTextBox.Size = new System.Drawing.Size(100, 20);
            this.levelCountTextBox.TabIndex = 31;
            this.levelCountTextBox.Text = "1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 61);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "3D Maze levels";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(244, 61);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(121, 13);
            this.label15.TabIndex = 32;
            this.label15.Text = "(Use edit menu in editor)";
            this.label15.Visible = false;
            // 
            // heightWarningLabel
            // 
            this.heightWarningLabel.AutoSize = true;
            this.heightWarningLabel.ForeColor = System.Drawing.Color.Red;
            this.heightWarningLabel.Location = new System.Drawing.Point(235, 93);
            this.heightWarningLabel.Name = "heightWarningLabel";
            this.heightWarningLabel.Size = new System.Drawing.Size(136, 13);
            this.heightWarningLabel.TabIndex = 33;
            this.heightWarningLabel.Text = "Stairs won\'t place correctly!";
            this.heightWarningLabel.Visible = false;
            // 
            // configTabControl
            // 
            this.configTabControl.Controls.Add(this.basicConfigTab);
            this.configTabControl.Controls.Add(this.advancedConfigTab);
            this.configTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configTabControl.Location = new System.Drawing.Point(0, 0);
            this.configTabControl.Name = "configTabControl";
            this.configTabControl.SelectedIndex = 0;
            this.configTabControl.Size = new System.Drawing.Size(384, 183);
            this.configTabControl.TabIndex = 34;
            // 
            // basicConfigTab
            // 
            this.basicConfigTab.Controls.Add(this.label13);
            this.basicConfigTab.Controls.Add(this.label8);
            this.basicConfigTab.Controls.Add(this.label15);
            this.basicConfigTab.Controls.Add(this.xCountTextBox);
            this.basicConfigTab.Controls.Add(this.levelCountTextBox);
            this.basicConfigTab.Controls.Add(this.yCountTextBox);
            this.basicConfigTab.Controls.Add(this.label14);
            this.basicConfigTab.Controls.Add(this.label9);
            this.basicConfigTab.Controls.Add(this.label6);
            this.basicConfigTab.Controls.Add(this.floorCheckBox);
            this.basicConfigTab.Controls.Add(this.openMazeButton);
            this.basicConfigTab.Controls.Add(this.roofCheckBox);
            this.basicConfigTab.Controls.Add(this.saveConfigButton);
            this.basicConfigTab.Controls.Add(this.xTextBox);
            this.basicConfigTab.Controls.Add(this.mapTextBox);
            this.basicConfigTab.Controls.Add(this.label10);
            this.basicConfigTab.Controls.Add(this.label12);
            this.basicConfigTab.Controls.Add(this.yTextBox);
            this.basicConfigTab.Controls.Add(this.zTextBox);
            this.basicConfigTab.Controls.Add(this.label11);
            this.basicConfigTab.Location = new System.Drawing.Point(4, 22);
            this.basicConfigTab.Name = "basicConfigTab";
            this.basicConfigTab.Padding = new System.Windows.Forms.Padding(3);
            this.basicConfigTab.Size = new System.Drawing.Size(376, 157);
            this.basicConfigTab.TabIndex = 0;
            this.basicConfigTab.Text = "Basic Configuration";
            this.basicConfigTab.UseVisualStyleBackColor = true;
            // 
            // advancedConfigTab
            // 
            this.advancedConfigTab.Controls.Add(this.advancedSaveConfigButton);
            this.advancedConfigTab.Controls.Add(this.advancedCheckBox);
            this.advancedConfigTab.Controls.Add(this.heightWarningLabel);
            this.advancedConfigTab.Controls.Add(this.objectIdTextBox);
            this.advancedConfigTab.Controls.Add(this.label1);
            this.advancedConfigTab.Controls.Add(this.label7);
            this.advancedConfigTab.Controls.Add(this.label2);
            this.advancedConfigTab.Controls.Add(this.wallHeightTextBox);
            this.advancedConfigTab.Controls.Add(this.objectSpacingTextBox);
            this.advancedConfigTab.Controls.Add(this.label5);
            this.advancedConfigTab.Controls.Add(this.label4);
            this.advancedConfigTab.Location = new System.Drawing.Point(4, 22);
            this.advancedConfigTab.Name = "advancedConfigTab";
            this.advancedConfigTab.Padding = new System.Windows.Forms.Padding(3);
            this.advancedConfigTab.Size = new System.Drawing.Size(376, 157);
            this.advancedConfigTab.TabIndex = 1;
            this.advancedConfigTab.Text = "Advanced Configuration";
            this.advancedConfigTab.UseVisualStyleBackColor = true;
            // 
            // advancedSaveConfigButton
            // 
            this.advancedSaveConfigButton.Location = new System.Drawing.Point(238, 109);
            this.advancedSaveConfigButton.Name = "advancedSaveConfigButton";
            this.advancedSaveConfigButton.Size = new System.Drawing.Size(118, 38);
            this.advancedSaveConfigButton.TabIndex = 34;
            this.advancedSaveConfigButton.Text = "Open Editor";
            this.advancedSaveConfigButton.UseVisualStyleBackColor = true;
            this.advancedSaveConfigButton.Click += new System.EventHandler(this.saveConfigButton_Click);
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.saveConfigButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 183);
            this.Controls.Add(this.configTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigForm";
            this.Text = "Maze Configuration";
            this.configTabControl.ResumeLayout(false);
            this.basicConfigTab.ResumeLayout(false);
            this.basicConfigTab.PerformLayout();
            this.advancedConfigTab.ResumeLayout(false);
            this.advancedConfigTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox objectIdTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox objectSpacingTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox wallHeightTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox xCountTextBox;
        private System.Windows.Forms.TextBox yCountTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox floorCheckBox;
        private System.Windows.Forms.CheckBox roofCheckBox;
        private System.Windows.Forms.Button saveConfigButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox xTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox yTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox zTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox mapTextBox;
        private System.Windows.Forms.CheckBox advancedCheckBox;
        private System.Windows.Forms.Button openMazeButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox levelCountTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label heightWarningLabel;
        private System.Windows.Forms.TabControl configTabControl;
        private System.Windows.Forms.TabPage basicConfigTab;
        private System.Windows.Forms.TabPage advancedConfigTab;
        private System.Windows.Forms.Button advancedSaveConfigButton;
    }
}

