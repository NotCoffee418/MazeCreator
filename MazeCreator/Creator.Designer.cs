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
            this.insertColumnLeftsoonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertColumnRightsoonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertRowTopsoonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertRowBottomsoonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSelectedRowsoonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSelectedColumnssoonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.insertColumnLeftsoonToolStripMenuItem,
            this.insertColumnRightsoonToolStripMenuItem,
            this.insertRowTopsoonToolStripMenuItem,
            this.insertRowBottomsoonToolStripMenuItem,
            this.removeSelectedRowsoonToolStripMenuItem,
            this.removeSelectedColumnssoonToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // changeConfigToolStripMenuItem
            // 
            this.changeConfigToolStripMenuItem.Name = "changeConfigToolStripMenuItem";
            this.changeConfigToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.changeConfigToolStripMenuItem.Text = "Change config";
            this.changeConfigToolStripMenuItem.Click += new System.EventHandler(this.changeConfigToolStripMenuItem_Click);
            // 
            // insertColumnLeftsoonToolStripMenuItem
            // 
            this.insertColumnLeftsoonToolStripMenuItem.Name = "insertColumnLeftsoonToolStripMenuItem";
            this.insertColumnLeftsoonToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.insertColumnLeftsoonToolStripMenuItem.Text = "Insert column left (soon)";
            // 
            // insertColumnRightsoonToolStripMenuItem
            // 
            this.insertColumnRightsoonToolStripMenuItem.Name = "insertColumnRightsoonToolStripMenuItem";
            this.insertColumnRightsoonToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.insertColumnRightsoonToolStripMenuItem.Text = "Insert column right (soon)";
            // 
            // insertRowTopsoonToolStripMenuItem
            // 
            this.insertRowTopsoonToolStripMenuItem.Name = "insertRowTopsoonToolStripMenuItem";
            this.insertRowTopsoonToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.insertRowTopsoonToolStripMenuItem.Text = "Insert row top (soon)";
            // 
            // insertRowBottomsoonToolStripMenuItem
            // 
            this.insertRowBottomsoonToolStripMenuItem.Name = "insertRowBottomsoonToolStripMenuItem";
            this.insertRowBottomsoonToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.insertRowBottomsoonToolStripMenuItem.Text = "Insert row bottom (soon)";
            // 
            // removeSelectedRowsoonToolStripMenuItem
            // 
            this.removeSelectedRowsoonToolStripMenuItem.Name = "removeSelectedRowsoonToolStripMenuItem";
            this.removeSelectedRowsoonToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.removeSelectedRowsoonToolStripMenuItem.Text = "Remove selected rows (soon)";
            // 
            // removeSelectedColumnssoonToolStripMenuItem
            // 
            this.removeSelectedColumnssoonToolStripMenuItem.Name = "removeSelectedColumnssoonToolStripMenuItem";
            this.removeSelectedColumnssoonToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.removeSelectedColumnssoonToolStripMenuItem.Text = "Remove selected columns (soon)";
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
        private System.Windows.Forms.ToolStripMenuItem insertColumnLeftsoonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertColumnRightsoonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertRowTopsoonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertRowBottomsoonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedRowsoonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedColumnssoonToolStripMenuItem;
    }
}