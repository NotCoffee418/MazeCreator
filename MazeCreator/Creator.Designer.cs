namespace MazeCreator
{
    partial class Creator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Creator));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToSQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertColumnLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertColumnRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertRowTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertRowBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSelectedRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSelectedColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.add3DLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillBordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillWholeMazeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearWholeMazeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelTabControl = new System.Windows.Forms.TabControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.stairsUpButton = new System.Windows.Forms.ToolStripButton();
            this.stairsDownButton = new System.Windows.Forms.ToolStripButton();
            this.stairsLeftButton = new System.Windows.Forms.ToolStripButton();
            this.stairsRightButton = new System.Windows.Forms.ToolStripButton();
            this.removeStairsButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(976, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exportToSQLToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exportToSQLToolStripMenuItem
            // 
            this.exportToSQLToolStripMenuItem.Name = "exportToSQLToolStripMenuItem";
            this.exportToSQLToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.exportToSQLToolStripMenuItem.Text = "Export to SQL";
            this.exportToSQLToolStripMenuItem.Click += new System.EventHandler(this.exportToSQLToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeConfigToolStripMenuItem,
            this.insertColumnLeftToolStripMenuItem,
            this.insertColumnRightToolStripMenuItem,
            this.insertRowTopToolStripMenuItem,
            this.insertRowBottomToolStripMenuItem,
            this.removeSelectedRowToolStripMenuItem,
            this.removeSelectedColumnsToolStripMenuItem,
            this.add3DLevelToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // changeConfigToolStripMenuItem
            // 
            this.changeConfigToolStripMenuItem.Name = "changeConfigToolStripMenuItem";
            this.changeConfigToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.changeConfigToolStripMenuItem.Text = "Change config";
            this.changeConfigToolStripMenuItem.Click += new System.EventHandler(this.changeConfigToolStripMenuItem_Click);
            // 
            // insertColumnLeftToolStripMenuItem
            // 
            this.insertColumnLeftToolStripMenuItem.Name = "insertColumnLeftToolStripMenuItem";
            this.insertColumnLeftToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.insertColumnLeftToolStripMenuItem.Text = "Insert column left";
            this.insertColumnLeftToolStripMenuItem.Click += new System.EventHandler(this.insertColumnLeftToolStripMenuItem_Click);
            // 
            // insertColumnRightToolStripMenuItem
            // 
            this.insertColumnRightToolStripMenuItem.Name = "insertColumnRightToolStripMenuItem";
            this.insertColumnRightToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.insertColumnRightToolStripMenuItem.Text = "Insert column right";
            this.insertColumnRightToolStripMenuItem.Click += new System.EventHandler(this.insertColumnRightToolStripMenuItem_Click);
            // 
            // insertRowTopToolStripMenuItem
            // 
            this.insertRowTopToolStripMenuItem.Name = "insertRowTopToolStripMenuItem";
            this.insertRowTopToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.insertRowTopToolStripMenuItem.Text = "Insert row top";
            this.insertRowTopToolStripMenuItem.Click += new System.EventHandler(this.insertRowTopToolStripMenuItem_Click);
            // 
            // insertRowBottomToolStripMenuItem
            // 
            this.insertRowBottomToolStripMenuItem.Name = "insertRowBottomToolStripMenuItem";
            this.insertRowBottomToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.insertRowBottomToolStripMenuItem.Text = "Insert row bottom";
            this.insertRowBottomToolStripMenuItem.Click += new System.EventHandler(this.insertRowBottomToolStripMenuItem_Click);
            // 
            // removeSelectedRowToolStripMenuItem
            // 
            this.removeSelectedRowToolStripMenuItem.Name = "removeSelectedRowToolStripMenuItem";
            this.removeSelectedRowToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.removeSelectedRowToolStripMenuItem.Text = "Remove selected rows";
            this.removeSelectedRowToolStripMenuItem.Click += new System.EventHandler(this.removeSelectedRowToolStripMenuItem_Click);
            // 
            // removeSelectedColumnsToolStripMenuItem
            // 
            this.removeSelectedColumnsToolStripMenuItem.Name = "removeSelectedColumnsToolStripMenuItem";
            this.removeSelectedColumnsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.removeSelectedColumnsToolStripMenuItem.Text = "Remove selected columns";
            this.removeSelectedColumnsToolStripMenuItem.Click += new System.EventHandler(this.removeSelectedColumnsToolStripMenuItem_Click);
            // 
            // add3DLevelToolStripMenuItem
            // 
            this.add3DLevelToolStripMenuItem.Name = "add3DLevelToolStripMenuItem";
            this.add3DLevelToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.add3DLevelToolStripMenuItem.Text = "Add 3D level";
            this.add3DLevelToolStripMenuItem.Click += new System.EventHandler(this.add3DLevelToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fillBordersToolStripMenuItem,
            this.fillWholeMazeToolStripMenuItem,
            this.clearWholeMazeToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // fillBordersToolStripMenuItem
            // 
            this.fillBordersToolStripMenuItem.Name = "fillBordersToolStripMenuItem";
            this.fillBordersToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.fillBordersToolStripMenuItem.Text = "Fill borders";
            this.fillBordersToolStripMenuItem.Click += new System.EventHandler(this.fillBordersToolStripMenuItem_Click);
            // 
            // fillWholeMazeToolStripMenuItem
            // 
            this.fillWholeMazeToolStripMenuItem.Name = "fillWholeMazeToolStripMenuItem";
            this.fillWholeMazeToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.fillWholeMazeToolStripMenuItem.Text = "Fill maze";
            this.fillWholeMazeToolStripMenuItem.Click += new System.EventHandler(this.fillWholeMazeToolStripMenuItem_Click);
            // 
            // clearWholeMazeToolStripMenuItem
            // 
            this.clearWholeMazeToolStripMenuItem.Name = "clearWholeMazeToolStripMenuItem";
            this.clearWholeMazeToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.clearWholeMazeToolStripMenuItem.Text = "Clear maze";
            this.clearWholeMazeToolStripMenuItem.Click += new System.EventHandler(this.clearWholeMazeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // levelTabControl
            // 
            this.levelTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.levelTabControl.Location = new System.Drawing.Point(0, 24);
            this.levelTabControl.Name = "levelTabControl";
            this.levelTabControl.SelectedIndex = 0;
            this.levelTabControl.Size = new System.Drawing.Size(976, 489);
            this.levelTabControl.TabIndex = 3;
            this.levelTabControl.SelectedIndexChanged += new System.EventHandler(this.levelTabControl_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stairsUpButton,
            this.stairsDownButton,
            this.stairsLeftButton,
            this.stairsRightButton,
            this.removeStairsButton});
            this.toolStrip1.Location = new System.Drawing.Point(944, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(32, 489);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // stairsUpButton
            // 
            this.stairsUpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stairsUpButton.Image = global::MazeCreator.Properties.Resources.stairs_up;
            this.stairsUpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stairsUpButton.Name = "stairsUpButton";
            this.stairsUpButton.Size = new System.Drawing.Size(29, 20);
            this.stairsUpButton.Text = "Place stairs up";
            this.stairsUpButton.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // stairsDownButton
            // 
            this.stairsDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stairsDownButton.Image = global::MazeCreator.Properties.Resources.stairs_down;
            this.stairsDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stairsDownButton.Name = "stairsDownButton";
            this.stairsDownButton.Size = new System.Drawing.Size(29, 20);
            this.stairsDownButton.Text = "Place stairs down";
            this.stairsDownButton.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // stairsLeftButton
            // 
            this.stairsLeftButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stairsLeftButton.Image = global::MazeCreator.Properties.Resources.stairs_left;
            this.stairsLeftButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stairsLeftButton.Name = "stairsLeftButton";
            this.stairsLeftButton.Size = new System.Drawing.Size(29, 20);
            this.stairsLeftButton.Text = "Place stairs to left";
            this.stairsLeftButton.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // stairsRightButton
            // 
            this.stairsRightButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stairsRightButton.Image = global::MazeCreator.Properties.Resources.stairs_right;
            this.stairsRightButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stairsRightButton.Name = "stairsRightButton";
            this.stairsRightButton.Size = new System.Drawing.Size(29, 20);
            this.stairsRightButton.Text = "Place stairs to right";
            this.stairsRightButton.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // removeStairsButton
            // 
            this.removeStairsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeStairsButton.Image = global::MazeCreator.Properties.Resources.stairs_remove;
            this.removeStairsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeStairsButton.Name = "removeStairsButton";
            this.removeStairsButton.Size = new System.Drawing.Size(29, 20);
            this.removeStairsButton.Text = "Remove Stairs";
            this.removeStairsButton.Click += new System.EventHandler(this.removeStairsButton_Click);
            // 
            // Creator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 513);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.levelTabControl);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Creator";
            this.Text = "Maze Creator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Creator_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Creator_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToSQLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillBordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillWholeMazeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearWholeMazeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertColumnLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertColumnRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertRowTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertRowBottomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedColumnsToolStripMenuItem;
        private System.Windows.Forms.TabControl levelTabControl;
        private System.Windows.Forms.ToolStripMenuItem add3DLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton stairsUpButton;
        private System.Windows.Forms.ToolStripButton stairsDownButton;
        private System.Windows.Forms.ToolStripButton stairsLeftButton;
        private System.Windows.Forms.ToolStripButton stairsRightButton;
        private System.Windows.Forms.ToolStripButton removeStairsButton;
    }
}