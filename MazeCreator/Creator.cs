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
            App.LEVELS = new List<DataGridView>();
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
            App.LEVELS.Add(grid);
            levelTabControl.Controls.Add(page);
            LoadGrid(id, rows, 1 + (Config.Y_COUNT * id));

            // register any stairs below
            if (id > 0)
            {
                int below = id - 1;
                for (int row = 0; row < Config.Y_COUNT; row++)
                    for (int col = 0; col < Config.X_COUNT; col++)
                    {
                        int value = Cell.GetValue(col, row);
                        if (value >= 3 && value <= 5) // lock upper three cells of stairs
                            Cell.SetValue(6, col, row, id);
                    }
            }
        }

        private void RemoveLevel()
        {
            var result = MessageBox.Show("Are you sure you want to remove this level?", "Remove Level",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                App.LEVELS.RemoveAt(App.activeGrid);
                levelTabControl.Controls.RemoveAt(App.activeGrid);
                Config.LEVEL_COUNT -= 1;
                App.configForm.levelCountTextBox.Text = Config.LEVEL_COUNT.ToString();
            }
        }

        /// <summary>
        /// Load any maze data
        /// </summary>
        /// <param name="rows"></param>
        private void LoadGrid(int grid, string[] rows = null, int startRow = 0)
        {
            // Clear datagrid
            while (App.GetLevel(grid).Columns.Count > 0)
                App.GetLevel(grid).Columns.RemoveAt(0);

            // set columns
            int[] defaultRow = new int[Config.X_COUNT];
            for (int col = 0; col < Config.X_COUNT; col++)
            {
                DataGridViewCheckBoxColumn c = new DataGridViewCheckBoxColumn();
                c.ValueType = typeof(int);
                c.Width = 15;
                c.HeaderText = (col + 1).ToString();
                App.GetLevel(grid).Columns.Add(c);
                defaultRow[col] = 0;
            }

            // Remove extra column
            if (App.GetLevel(grid).Columns.Count == 1 + Config.X_COUNT)
                App.GetLevel(grid).Columns.RemoveAt(0);

            // Set rows
            for (int row = 0; row < Config.Y_COUNT; row++)
            {
                DataGridViewRow r = new DataGridViewRow();
                r.Height = 15;
                r.HeaderCell.Value = (row + 1).ToString();
                r.SetValues(defaultRow);
                App.GetLevel(grid).Rows.Add(r);

                // Set integer value for all cells
                for (int col = 0; col < Config.X_COUNT; col++)
                    Cell.SetValue(0, col, row, grid);
            }

            // Sets rows if none given
            if (rows == null) // From existing if no rows were given
                rows = Config.MAZEDATA;

            // Loads data to grid
            for (int row = 0; row < Config.Y_COUNT; row++)
            {
                if (rows.Count() > startRow + row && rows[startRow + row] != "") // Check if row contains data
                {
                    String[] rowValues = rows[startRow + row].Split(',');
                    for (int col = 0; col < Config.X_COUNT; col++)
                        Cell.SetValue(int.Parse(rowValues[col]), col, row, grid);
                }
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

                var cell = Cell.Get(e.ColumnIndex, e.RowIndex);
                int value = 0;

                if (cell.ReadOnly)
                    return;
                else if ((int)cell.Value == 0)
                    value = 1;

                Cell.SetValue(value, e.ColumnIndex, e.RowIndex);
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
            Cell.SetInfo(e.ColumnIndex, e.RowIndex);
        }

        // Selected cell looks
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Back color
            if (App.GetLevel().SelectedCells.Count > 0)
                App.GetLevel().DefaultCellStyle.SelectionBackColor =
                    App.GetLevel().SelectedCells[0].Style.BackColor;

            // Border
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (Cell.Get(e.ColumnIndex, e.RowIndex).Selected == true)
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
            Config.LEVEL_COUNT += 1;
            App.configForm.levelCountTextBox.Text = Config.LEVEL_COUNT.ToString();
            AddLevel();
        }
        private void remove3DLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveLevel();
        }


        Stairs stairs;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (Stairs.alreadyPlacing)
                stairs.StopPlacing();
            stairs = new Stairs(1);
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (Stairs.alreadyPlacing)
                stairs.StopPlacing();
            stairs = new Stairs(2);
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (Stairs.alreadyPlacing)
                stairs.StopPlacing();
            stairs = new Stairs(3);
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (Stairs.alreadyPlacing)
                stairs.StopPlacing();
            stairs = new Stairs(4);
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

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Updater.CheckLatestVersion();
        }

        
    }
}