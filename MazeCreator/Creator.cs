using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Data;
using System.Windows.Forms;

namespace MazeCreator
{
    public partial class Creator : Form
    {
        public bool changedSinceSave = false;

        public Creator()
        {
            InitializeComponent();
        }

        public void LoadMaze(string data)
        {
            LoadMaze(data.Split('\n'));
        }
        public void LoadMaze(string[] rows)
        {
            if (rows.Count() > 1)
                LoadData(rows);
        }

        /// <summary>
        /// Adds tabs & grids
        /// </summary>
        /// <param name="rows"></param>
        public void LoadData(string[] rows = null)
        {
            Config.LEVELS = new List<Level>();
            levelTabControl.Controls.Clear();

            for (int i = 0; i < Config.LEVEL_COUNT; i++)
                AddLevel(i, rows);

            App.activeGrid = 0;
        }

        private void AddLevel(int id = -1, string[] rows = null)
        {
            if (id == -1) id = levelTabControl.Controls.Count;

            // Create grid
            DataGridView grid = new DataGridView();
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeColumns = false;
            grid.AllowUserToResizeRows = false;
            grid.MultiSelect = false;
            grid.Dock = DockStyle.Fill;            
            
            // Event handlers
            grid.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            grid.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView1_CellPainting);

            // Add tab
            TabPage page = new TabPage();
            page.Location = new System.Drawing.Point(4, 22);
            page.Name = "levelPage" + (id + 1);
            page.Padding = new System.Windows.Forms.Padding(3);
            page.Size = new System.Drawing.Size(968, 463);
            page.TabIndex = 0;
            page.Text = "Level " + (id + 1);
            page.UseVisualStyleBackColor = true;
            page.Controls.Add(grid);

            // Add to LEVELS and levelControl
            Level l = new Level();
            l.Tab = page;
            l.Grid = grid;
            Config.LEVELS.Add(l);
            levelTabControl.Controls.Add(page);
            LoadGrid(id, rows, 1 + (Config.Y_COUNT * id));
        }

        /// <summary>
        /// Load any maze data
        /// </summary>
        /// <param name="rows"></param>
        private void LoadGrid(int grid, string[] rows = null, int lastRow = 0)
        {
            // Clear datagrid
            while (Config.LEVELS[grid].Grid.Columns.Count > 0)
                Config.LEVELS[grid].Grid.Columns.RemoveAt(0);

            // set columns
            int[] defaultRow = new int[Config.X_COUNT];
            for (int i = 0; i < Config.X_COUNT; i++)
            {
                DataGridViewCheckBoxColumn c = new DataGridViewCheckBoxColumn();
                c.ValueType = typeof(int);
                c.Width = 15;
                c.HeaderText = (i + 1).ToString();
                Config.LEVELS[grid].Grid.Columns.Add(c);
                defaultRow[i] = 0;
            }

            // Remove extra column
            if (Config.LEVELS[grid].Grid.Columns.Count == 1 + Config.X_COUNT)
                Config.LEVELS[grid].Grid.Columns.RemoveAt(0);


            // Set rows
            for (int i = 0; i < Config.Y_COUNT; i++)
            {
                DataGridViewRow r = new DataGridViewRow();
                r.Height = 15;
                r.HeaderCell.Value = (i + 1).ToString();
                r.SetValues(defaultRow);
                Config.LEVELS[grid].Grid.Rows.Add(r);

                // Set integer value for all cells
                for (int j = 0; j < Config.X_COUNT; j++)
                {
                    Config.LEVELS[grid].Grid.Rows[i].Cells[j].Value = 0;
                }
            }

            // Sets rows if none given
            if (rows == null) // From existing if no rows were given
                rows = Config.MAZEDATA;

            // Loads data to grid
            for (int i = 0; i < Config.Y_COUNT; i++)
            {
                int col = 0;
                if (rows.Count() > lastRow + i && rows[lastRow + i] != "") // Check if any data were
                    for (int j = 0; j < Config.X_COUNT; j++)
                    {
                        int v = int.Parse(rows[lastRow + i][j].ToString()); 
                        Config.LEVELS[grid].Grid.Rows[i].Cells[col].Value = v;
                        col++;
                    }
            }
            ReloadColors(grid);
        }

        public void ReloadColors(int grid)
        {
            Config.LEVELS[grid].Grid.ClearSelection();
            for (int i = 0; i < Config.Y_COUNT; i++) // Loop all rows
                for (int j = 0; j < Config.X_COUNT; j++) // Loop all columns
                    SetCellBackColor(grid, i, j);
        }

        public void SetCellBackColor(int grid, int y, int x)
        {
            if (Config.LEVELS[grid].Grid.Rows[y].Cells[x].Value != null)
                switch ((int)Config.LEVELS[grid].Grid.Rows[y].Cells[x].Value)
                {
                    case 1: // wall
                        Config.LEVELS[grid].Grid.Rows[y].Cells[x].Style.BackColor = Color.Red;
                        break;
                    case 2: // bottom of stairs
                        // !!!If this color is changed, change on save & SQL export too (workaround)
                        Config.LEVELS[grid].Grid.Rows[y].Cells[x].Style.BackColor = Color.MediumAquamarine;
                        Config.LEVELS[grid].Grid.Rows[y].Cells[x].ReadOnly = true;
                        break;
                    case 3: // middle of stairs (object placed here)
                        Config.LEVELS[grid].Grid.Rows[y].Cells[x].Style.BackColor = Color.MediumTurquoise;
                        Config.LEVELS[grid].Grid.Rows[y].Cells[x].ReadOnly = true;
                        break;
                    case 4: // middle of stairs
                        Config.LEVELS[grid].Grid.Rows[y].Cells[x].Style.BackColor = Color.Aquamarine;
                        Config.LEVELS[grid].Grid.Rows[y].Cells[x].ReadOnly = true;
                        break;
                    case 5: // top of stairs
                        Config.LEVELS[grid].Grid.Rows[y].Cells[x].Style.BackColor = Color.Aqua;
                        Config.LEVELS[grid].Grid.Rows[y].Cells[x].ReadOnly = true;

                        // Show top of stairs on next level
                        if (Config.LEVELS.Count > grid + 1)
                        {
                            Config.LEVELS[grid + 1].Grid.Rows[y].Cells[x].Style.BackColor = Color.Aqua;
                            Config.LEVELS[grid + 1].Grid.Rows[y].Cells[x].ReadOnly = true;
                        }
                        break;
                    default: // 0
                        Config.LEVELS[grid].Grid.Rows[y].Cells[x].Style.BackColor = Color.Lime;
                        break;
                }
            else Config.LEVELS[grid].Grid.Rows[y].Cells[x].Style.BackColor = Color.Lime;
            
        }

        #region UI Handlers
        private void fillBordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.FillBorders();
        }

        private void exportToSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.fileHandler.Export();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/RStijn/MazeCreator");
        }

        private void fillWholeMazeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to fill this whole level with walls?", "Fill maze",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                Tools.FillMaze();
        }

        private void clearWholeMazeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to empty this whole level?", "Clear maze",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                Tools.FillMaze(empty: true);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.fileHandler.OpenFile();
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.fileHandler.SaveFile();
        }

        // Set Color
        public void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Stairs.stairsDirection == 0) // if not placing stairs
                SetCellBackColor(App.activeGrid, e.RowIndex, e.ColumnIndex);
        }

        // Selected cell looks
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Back color
            if (Config.LEVELS[App.activeGrid].Grid.SelectedCells.Count > 0)
                Config.LEVELS[App.activeGrid].Grid.DefaultCellStyle.SelectionBackColor =
                    Config.LEVELS[App.activeGrid].Grid.SelectedCells[0].Style.BackColor;

            // Border
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (Config.LEVELS[App.activeGrid].Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected == true)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
                    using (Pen p = new Pen(Color.Black, 1))
                    {
                        Rectangle rect = e.CellBounds;
                        rect.Width -= 2;
                        rect.Height -= 2;
                        e.Graphics.DrawRectangle(p, rect);
                    }
                    e.Handled = true;
                }
            }
        }

        private void Creator_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Creator_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changedSinceSave)
            {
                DialogResult result = MessageBox.Show(
                    "Would you like to save your changes before you close?",
                    "Save", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Yes:
                        if (!App.fileHandler.SaveFile()) 
                            e.Cancel = true;
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void changeConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.configForm.DisplayConfigForm();
            this.Hide();
        }

        private void insertColumnLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.AddLine(1);
        }

        private void insertColumnRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.AddLine(2);
        }

        private void insertRowTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.AddLine(3);
        }

        private void insertRowBottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.AddLine(4);
        }

        private void removeSelectedRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.RemoveLine(1);
        }
        private void removeSelectedColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.RemoveLine(2);
        }
        private void levelTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            App.activeGrid = levelTabControl.SelectedIndex;
            if (App.activeGrid == -1) App.activeGrid = 0;
        }
        private void add3DLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLevel();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Stairs.PlaceStairs(1);
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Stairs.PlaceStairs(2);
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Stairs.PlaceStairs(3);
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Stairs.PlaceStairs(4);
        }
        #endregion
    }
}