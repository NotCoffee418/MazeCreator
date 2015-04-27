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
            this.label3 = new System.Windows.Forms.Label();
            this.openMazeButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.levelCountTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // objectIdTextBox
            // 
            this.objectIdTextBox.Enabled = false;
            this.objectIdTextBox.Location = new System.Drawing.Point(137, 111);
            this.objectIdTextBox.Name = "objectIdTextBox";
            this.objectIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.objectIdTextBox.TabIndex = 8;
            this.objectIdTextBox.Text = "745000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Walls Object ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "(default: crates)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Space between objects";
            // 
            // objectSpacingTextBox
            // 
            this.objectSpacingTextBox.Enabled = false;
            this.objectSpacingTextBox.Location = new System.Drawing.Point(137, 137);
            this.objectSpacingTextBox.Name = "objectSpacingTextBox";
            this.objectSpacingTextBox.Size = new System.Drawing.Size(100, 20);
            this.objectSpacingTextBox.TabIndex = 9;
            this.objectSpacingTextBox.Text = "2.5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Wall Height";
            // 
            // wallHeightTextBox
            // 
            this.wallHeightTextBox.Location = new System.Drawing.Point(137, 163);
            this.wallHeightTextBox.Name = "wallHeightTextBox";
            this.wallHeightTextBox.Size = new System.Drawing.Size(100, 20);
            this.wallHeightTextBox.TabIndex = 10;
            this.wallHeightTextBox.Text = "2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(244, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "How many objects";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Maze Size";
            // 
            // xCountTextBox
            // 
            this.xCountTextBox.Location = new System.Drawing.Point(141, 32);
            this.xCountTextBox.Name = "xCountTextBox";
            this.xCountTextBox.Size = new System.Drawing.Size(34, 20);
            this.xCountTextBox.TabIndex = 5;
            this.xCountTextBox.Text = "15";
            // 
            // yCountTextBox
            // 
            this.yCountTextBox.Location = new System.Drawing.Point(199, 32);
            this.yCountTextBox.Name = "yCountTextBox";
            this.yCountTextBox.Size = new System.Drawing.Size(34, 20);
            this.yCountTextBox.TabIndex = 6;
            this.yCountTextBox.Text = "15";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(181, 35);
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
            this.floorCheckBox.Location = new System.Drawing.Point(18, 217);
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
            this.roofCheckBox.Location = new System.Drawing.Point(18, 242);
            this.roofCheckBox.Name = "roofCheckBox";
            this.roofCheckBox.Size = new System.Drawing.Size(78, 17);
            this.roofCheckBox.TabIndex = 12;
            this.roofCheckBox.Text = "Create roof";
            this.roofCheckBox.UseVisualStyleBackColor = true;
            // 
            // saveConfigButton
            // 
            this.saveConfigButton.Location = new System.Drawing.Point(245, 220);
            this.saveConfigButton.Name = "saveConfigButton";
            this.saveConfigButton.Size = new System.Drawing.Size(91, 38);
            this.saveConfigButton.TabIndex = 13;
            this.saveConfigButton.Text = "Save Config";
            this.saveConfigButton.UseVisualStyleBackColor = true;
            this.saveConfigButton.Click += new System.EventHandler(this.saveConfigButton_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(119, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(12, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "x";
            // 
            // xTextBox
            // 
            this.xTextBox.Location = new System.Drawing.Point(141, 6);
            this.xTextBox.Name = "xTextBox";
            this.xTextBox.Size = new System.Drawing.Size(50, 20);
            this.xTextBox.TabIndex = 2;
            this.xTextBox.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(197, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(12, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "y";
            // 
            // yTextBox
            // 
            this.yTextBox.Location = new System.Drawing.Point(215, 6);
            this.yTextBox.Name = "yTextBox";
            this.yTextBox.Size = new System.Drawing.Size(47, 20);
            this.yTextBox.TabIndex = 3;
            this.yTextBox.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(264, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(12, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "z";
            // 
            // zTextBox
            // 
            this.zTextBox.Location = new System.Drawing.Point(280, 6);
            this.zTextBox.Name = "zTextBox";
            this.zTextBox.Size = new System.Drawing.Size(53, 20);
            this.zTextBox.TabIndex = 4;
            this.zTextBox.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "MapID";
            // 
            // mapTextBox
            // 
            this.mapTextBox.Location = new System.Drawing.Point(57, 6);
            this.mapTextBox.Name = "mapTextBox";
            this.mapTextBox.Size = new System.Drawing.Size(32, 20);
            this.mapTextBox.TabIndex = 1;
            this.mapTextBox.Text = "0";
            // 
            // advancedCheckBox
            // 
            this.advancedCheckBox.AutoSize = true;
            this.advancedCheckBox.Location = new System.Drawing.Point(15, 86);
            this.advancedCheckBox.Name = "advancedCheckBox";
            this.advancedCheckBox.Size = new System.Drawing.Size(231, 17);
            this.advancedCheckBox.TabIndex = 7;
            this.advancedCheckBox.Text = "I\'ll choose my own GameObject (advanced)";
            this.advancedCheckBox.UseVisualStyleBackColor = true;
            this.advancedCheckBox.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Warning: Large mazes may cause server lag";
            // 
            // openMazeButton
            // 
            this.openMazeButton.Location = new System.Drawing.Point(140, 234);
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
            this.label6.Location = new System.Drawing.Point(242, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "(Use edit menu)";
            this.label6.Visible = false;
            // 
            // levelCountTextBox
            // 
            this.levelCountTextBox.Location = new System.Drawing.Point(137, 189);
            this.levelCountTextBox.Name = "levelCountTextBox";
            this.levelCountTextBox.Size = new System.Drawing.Size(100, 20);
            this.levelCountTextBox.TabIndex = 31;
            this.levelCountTextBox.Text = "1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 192);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "3D Maze levels";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(244, 192);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(81, 13);
            this.label15.TabIndex = 32;
            this.label15.Text = "(Use edit menu)";
            this.label15.Visible = false;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 270);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.levelCountTextBox);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.openMazeButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.advancedCheckBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.mapTextBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.zTextBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.yTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.xTextBox);
            this.Controls.Add(this.saveConfigButton);
            this.Controls.Add(this.roofCheckBox);
            this.Controls.Add(this.floorCheckBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.yCountTextBox);
            this.Controls.Add(this.xCountTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.wallHeightTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.objectSpacingTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.objectIdTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigForm";
            this.Text = "Maze Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button openMazeButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox levelCountTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
    }
}

