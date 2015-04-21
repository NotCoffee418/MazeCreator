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
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillBordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillWholeMazeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearWholeMazeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mazeGrid = new System.Windows.Forms.DataGridView();
            this.ExportSqlDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveToFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mazeGrid)).BeginInit();
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
            this.removeSelectedColumnsToolStripMenuItem});
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
            // mazeGrid
            // 
            this.mazeGrid.AllowUserToAddRows = false;
            this.mazeGrid.AllowUserToDeleteRows = false;
            this.mazeGrid.AllowUserToResizeColumns = false;
            this.mazeGrid.AllowUserToResizeRows = false;
            this.mazeGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mazeGrid.Location = new System.Drawing.Point(0, 24);
            this.mazeGrid.Name = "mazeGrid";
            this.mazeGrid.Size = new System.Drawing.Size(976, 489);
            this.mazeGrid.TabIndex = 2;
            this.mazeGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // ExportSqlDialog
            // 
            this.ExportSqlDialog.DefaultExt = "sql";
            this.ExportSqlDialog.FileName = "Maze";
            this.ExportSqlDialog.Filter = "SQL File|*.sql";
            // 
            // saveToFileDialog
            // 
            this.saveToFileDialog.DefaultExt = "sql";
            this.saveToFileDialog.FileName = "MyMaze";
            this.saveToFileDialog.Filter = "Maze File|*.maze";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "MyMaze.maze";
            // 
            // Creator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 513);
            this.Controls.Add(this.mazeGrid);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Creator";
            this.Text = "Maze Creator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Creator_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Creator_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mazeGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToSQLToolStripMenuItem;
        private System.Windows.Forms.DataGridView mazeGrid;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillBordersToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog ExportSqlDialog;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillWholeMazeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearWholeMazeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveToFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertColumnLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertColumnRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertRowTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertRowBottomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedColumnsToolStripMenuItem;
    }
}