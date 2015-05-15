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

            for (int lev = 0; lev < Config.LEVEL_COUNT; lev++)
                AddLevel(lev, rows);

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
            grid.CellValueChanged += dataGridView1_CellValueChanged;
            grid.CellPainting += dataGridView1_CellPainting;
            grid.CellMouseDown += PlaceWall;

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

            // register any stairs below
            if (id > 0)
            {
                int below = id - 1;
                for (int row = 0; row < Config.Y_COUNT; row++)
                    for (int col = 0; col < Config.X_COUNT; col++)
                    {
                        int value = (int)Config.LEVELS[below].Grid.Rows[row].Cells[col].Value;
                        if (value >= 3 && value <= 5) // lock upper three cells of stairs
                        {
                            Config.LEVELS[id].Grid.Rows[row].Cells[col].Value = 6;
                            SetCellInfo(col, row, id);
                        }
                    }
            }
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
            for (int col = 0; col < Config.X_COUNT; col++)
            {
                DataGridViewCheckBoxColumn c = new DataGridViewCheckBoxColumn();
                c.ValueType = typeof(int);
                c.Width = 15;
                c.HeaderText = (col + 1).ToString();
                Config.LEVELS[grid].Grid.Columns.Add(c);
                defaultRow[col] = 0;
            }

            // Remove extra column
            if (Config.LEVELS[grid].Grid.Columns.Count == 1 + Config.X_COUNT)
                Config.LEVELS[grid].Grid.Columns.RemoveAt(0);


            // Set rows
            for (int row = 0; row < Config.Y_COUNT; row++)
            {
                DataGridViewRow r = new DataGridViewRow();
                r.Height = 15;
                r.HeaderCell.Value = (row + 1).ToString();
                r.SetValues(defaultRow);
                Config.LEVELS[grid].Grid.Rows.Add(r);

                // Set integer value for all cells
                for (int col = 0; col < Config.X_COUNT; col++)
                {
                    Config.LEVELS[grid].Grid.Rows[row].Cells[col].Value = 0;
                }
            }

            // Sets rows if none given
            if (rows == null) // From existing if no rows were given
                rows = Config.MAZEDATA;

            // Loads data to grid
            for (int row = 0; row < Config.Y_COUNT; row++)
            {
                if (rows.Count() > lastRow + row && rows[lastRow + row] != "") // Check if any data were
                    for (int col = 0; col < Config.X_COUNT; col++)
                    {
                        int v = int.Parse(rows[lastRow + row][col].ToString()); 
                        Config.LEVELS[grid].Grid.Rows[row].Cells[col].Value = v;
                    }
            }
            ReloadColors(grid);
        }

        public void ReloadColors(int grid = -1)
        {
            if (grid == -1) 
                grid = App.activeGrid;

            // handle cell selection
            if (Config.LEVELS[grid].Grid.SelectedCells.Count == 0)
                Config.LEVELS[grid].Grid.Rows[0].Cells[0].Selected = true;
            var sel = Config.LEVELS[grid].Grid.SelectedCells[0];
            Config.LEVELS[grid].Grid.ClearSelection();

            // Reload colors
            for (int row = 0; row < Config.Y_COUNT; row++) // Loop all rows
                for (int col = 0; col < Config.X_COUNT; col++) // Loop all columns
                    SetCellInfo(col, row, grid);

            // Select original cell
            Config.LEVELS[grid].Grid.Rows[sel.RowIndex].Cells[sel.ColumnIndex].Selected = true;
        }

        public void SetCellInfo(int x, int y, int grid = -1)
        {
            if (grid == -1)
                grid = App.activeGrid;

            try
            {
                // Get cell value
                int value = (int)Config.LEVELS[grid].Grid.Rows[y].Cells[x].Value;

                // Set info
                Config.LEVELS[grid].Grid.Rows[y].Cells[x].Style.BackColor = App.color[value];
                Config.LEVELS[grid].Grid.Rows[y].Cells[x].ToolTipText = App.tooltip[value];

                // Make stairs read-only
                if (value >= 2 && value <= 6)
                    Config.LEVELS[grid].Grid.Rows[y].Cells[x].ReadOnly = true;
                else Config.LEVELS[grid].Grid.Rows[y].Cells[x].ReadOnly = false; // for removed stairs 
            }
            catch
            { // Occurs when editing too fast and changing >1 values
                Config.LEVELS[grid].Grid.Rows[y].Cells[x].Value = 0;
            }
        }

        /// <summary>
        /// Click to place wall
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaceWall(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                    return; // right mouse is used to cancel

                var cell = Config.LEVELS[App.activeGrid].Grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                int value = 0;

                if (cell.ReadOnly)
                    return;
                else if ((int)cell.Value == 0)
                    value = 1;

                cell.Value = value;
                SetCellInfo(e.ColumnIndex, e.RowIndex);
            }
            catch (ArgumentOutOfRangeException)
            { /* Mouse moved out of grid */}
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
            SetCellInfo(e.ColumnIndex, e.RowIndex);
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

        /// <summary>
        /// Ask to save before closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            new Stairs(1);
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            new Stairs(2);
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            new Stairs(3);
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            new Stairs(4);
        }
        private void removeStairsButton_Click(object sender, EventArgs e)
        {
            Stairs.Remove();
        }

        private void addTrapButton_Click(object sender, EventArgs e)
        {
            new Trap(Trap.Type.HoleTrap);
        }
        private void concealedFloorTrapButton_Click(object sender, EventArgs e)
        {
            new Trap(Trap.Type.ConcealedTrap);
        }
        private void secretPassageButton_Click(object sender, EventArgs e)
        {
            new Trap(Trap.Type.SecretPassage);
        }
        #endregion
    }
}