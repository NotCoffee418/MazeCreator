using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MazeCreator
{
    public partial class Creator : Form
    {
        // vars
        bool changedSinceSave = false;
        int activeGrid = 0;
        List<Level> LEVELS = new List<Level>();

        // Config
        public string[] MAZEDATA;
        public int      GAMEOBJECT;
        public double   SPACING;
        public int      WALLHEIGHT;
        public int      X_COUNT;
        public int      Y_COUNT;
        public bool     FLOOR;
        public bool     ROOF;
        public double[] STARTCOORDS = new double[4]; // x,y,z,map
        public int      LEVEL_COUNT;
        public bool     CREATE_GO_TEMPLATE;

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
            LEVELS = new List<Level>();
            levelTabControl.Controls.Clear();

            for (int i = 0; i < LEVEL_COUNT; i++)
                AddLevel(i, rows);

            activeGrid = 0;
        }

        private void AddLevel(int id = -1, string[] rows = null)
        {
            if (id == -1)
                id = levelTabControl.Controls.Count;

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
            grid.CellValueChanged += new DataGridViewCellEventHandler(ConfirmPlaceStairs);


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
            LEVELS.Add(l);
            levelTabControl.Controls.Add(page);
            LoadGrid(id, rows, 1 + (Y_COUNT * id));
        }

        /// <summary>
        /// Load any maze data
        /// </summary>
        /// <param name="rows"></param>
        private void LoadGrid(int grid, string[] rows = null, int lastRow = 0)
        {
            // Clear datagrid
            while (LEVELS[grid].Grid.Columns.Count > 0)
                LEVELS[grid].Grid.Columns.RemoveAt(0);

            // set columns
            int[] defaultRow = new int[X_COUNT];
            for (int i = 0; i < X_COUNT; i++)
            {
                DataGridViewColumn c = new DataGridViewCheckBoxColumn();
                c.ValueType = typeof(int);
                c.Width = 15;
                c.HeaderText = (i + 1).ToString();
                LEVELS[grid].Grid.Columns.Add(c);
                defaultRow[i] = 0;
            }

            // Remove extra column
            if (LEVELS[grid].Grid.Columns.Count == 1 + X_COUNT)
                LEVELS[grid].Grid.Columns.RemoveAt(0);


            // Set rows
            for (int i = 0; i < Y_COUNT; i++)
            {
                DataGridViewRow r = new DataGridViewRow();
                r.Height = 15;
                r.HeaderCell.Value = (i + 1).ToString();
                r.SetValues(defaultRow);
                LEVELS[grid].Grid.Rows.Add(r);
            }

            // Sets rows if none given
            if (rows == null) // From existing if no rows were given
                rows = MAZEDATA;

            // Loads data to grid
            for (int i = 0; i < Y_COUNT; i++)
            {
                int col = 0;
                if (rows.Count() > lastRow + i && rows[lastRow + i] != "") // Check if any data were
                    for (int j = 0; j < X_COUNT; j++)
                    {
                        int v = 0;
                        if (rows[lastRow + i][j] != '0') 
                            v = int.Parse(rows[lastRow + i][j].ToString()); // Convert char>String>int, else it uses key of char
                        LEVELS[grid].Grid.Rows[i].Cells[col].Value = v;
                        col++;
                    }
            }

            ReloadColors(grid);
        }

        /// <summary>
        /// Prepare to place stairs
        /// </summary>
        /// <param name="direction"></param>
        int stairsDirection = 0;
        bool progSel = false;
        private void PlaceStairs(int direction)
        {
            stairsDirection = direction;
            // Remove wall handler
            LEVELS[activeGrid].Grid.CellValueChanged -= new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            LEVELS[activeGrid].Grid.MultiSelect = true;
            
            // Display instructions
            MessageBox.Show("Activate the block where the bottom of your stairs start.");

            // Let user select start of maze
            LEVELS[activeGrid].Grid.SelectionChanged += new System.EventHandler(PlacingStairs);
        }
        private void PlacingStairs(object sender, EventArgs e)
        {
            if (progSel) return;

            var loc = LEVELS[activeGrid].Grid.SelectedCells[0];
            int reqBlocks = 4;

            progSel = true;
            switch (stairsDirection)
            {
                case 1: // up
                    if (loc.RowIndex - reqBlocks + 1 >= 0)
                        for (int i = 0; i < reqBlocks; i++)
                            LEVELS[activeGrid].Grid.Rows[loc.RowIndex - i].Cells[loc.ColumnIndex].Selected = true;
                    else
                    {
                        progSel = false;
                        LEVELS[activeGrid].Grid.Rows[reqBlocks - 1].Cells[loc.ColumnIndex].Selected = true;
                    }
                    break;
                case 2: // down
                    if (loc.RowIndex + reqBlocks <= LEVELS[activeGrid].Grid.ColumnCount)
                        for (int i = 0; i < reqBlocks; i++)
                            LEVELS[activeGrid].Grid.Rows[loc.RowIndex + i].Cells[loc.ColumnIndex].Selected = true;
                    else 
                    {
                        progSel = false;
                        LEVELS[activeGrid].Grid.Rows[LEVELS[activeGrid].Grid.RowCount - reqBlocks].Cells[loc.ColumnIndex].Selected = true;
                    }
                    break;
                case 3: // left
                    if (loc.ColumnIndex - reqBlocks + 1 >= 0)
                        for (int i = 0; i < reqBlocks; i++)
                            LEVELS[activeGrid].Grid.Rows[loc.RowIndex].Cells[loc.ColumnIndex -i].Selected = true;
                    else 
                    {
                        progSel = false;
                        LEVELS[activeGrid].Grid.Rows[loc.RowIndex].Cells[reqBlocks - 1].Selected = true;
                    }
                    break;
                case 4: // right
                    if (loc.ColumnIndex + reqBlocks <= LEVELS[activeGrid].Grid.ColumnCount)
                        for (int i = 0; i < reqBlocks; i++)
                            LEVELS[activeGrid].Grid.Rows[loc.RowIndex].Cells[loc.ColumnIndex + i].Selected = true;
                    else 
                    {
                        progSel = false;
                        LEVELS[activeGrid].Grid.Rows[loc.RowIndex].Cells[LEVELS[activeGrid].Grid.ColumnCount - reqBlocks].Selected = true;
                    }
                    break;
            }
            progSel = false;
        }
        private void ConfirmPlaceStairs(object sender, DataGridViewCellEventArgs e)
        {
            // Only trigger when stairs should be placed
            if (stairsDirection == 0) return;
            stairsDirection = 0;

            // Remove placing stairs handler
            LEVELS[activeGrid].Grid.SelectionChanged -= new System.EventHandler(PlacingStairs);

            // Get selected cells in the correct direction/order
            var cells = LEVELS[activeGrid].Grid.SelectedCells;

            // Set stairs location
            for (int i = 0; i < cells.Count; i++)
            {
                switch (i)
                {
                    case 3: // bottom
                        cells[i].Value = 2;
                        break;
                    case 2: // place location
                        cells[i].Value = 3;
                        break;
                    case 1: // padding
                        cells[i].Value = 4;
                        break;
                    case 0: // top
                        cells[i].Value = 5;
                        break;
                }
                SetCellBackColor(activeGrid, cells[i].RowIndex, cells[i].ColumnIndex);
            }

            // Add wall handler again
            LEVELS[activeGrid].Grid.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            LEVELS[activeGrid].Grid.MultiSelect = false;
        }

        private void ReloadColors(int grid)
        {
            LEVELS[grid].Grid.ClearSelection();
            for (int i = 0; i < Y_COUNT; i++) // Loop all rows
                for (int j = 0; j < X_COUNT; j++) // Loop all columns
                    SetCellBackColor(grid, i, j);
        }

        private void SetCellBackColor(int grid, int i, int j)
        {
            try
            {
                if (LEVELS[grid].Grid.Rows[i].Cells[j].Value != null)
                    switch ((int)LEVELS[grid].Grid.Rows[i].Cells[j].Value)
                    {
                        case 1: // wall
                            LEVELS[grid].Grid.Rows[i].Cells[j].Style.BackColor = Color.Red;
                            break;
                        case 2: // bottom of stairs
                            LEVELS[grid].Grid.Rows[i].Cells[j].Style.BackColor = Color.MediumTurquoise;
                            LEVELS[grid].Grid.Rows[i].Cells[j].ReadOnly = true;
                            break;
                        case 3: // middle of stairs (object placed here)
                            LEVELS[grid].Grid.Rows[i].Cells[j].Style.BackColor = Color.MediumAquamarine;
                            LEVELS[grid].Grid.Rows[i].Cells[j].ReadOnly = true;
                            break;
                        case 4: // middle of stairs
                            LEVELS[grid].Grid.Rows[i].Cells[j].Style.BackColor = Color.Aquamarine;
                            LEVELS[grid].Grid.Rows[i].Cells[j].ReadOnly = true;
                            break;
                        case 5: // top of stairs
                            LEVELS[grid].Grid.Rows[i].Cells[j].Style.BackColor = Color.Aqua;
                            LEVELS[grid].Grid.Rows[i].Cells[j].ReadOnly = true;

                            // Show top of stairs on next level
                            if (LEVELS.Count > grid + 1)
                            {
                                LEVELS[grid + 1].Grid.Rows[i].Cells[j].Style.BackColor = Color.Aqua;
                                LEVELS[grid + 1].Grid.Rows[i].Cells[j].ReadOnly = true;
                            }
                            break;
                        default: // 0
                            LEVELS[grid].Grid.Rows[i].Cells[j].Style.BackColor = Color.Lime;
                            break;

                    }
                else LEVELS[grid].Grid.Rows[i].Cells[j].Style.BackColor = Color.Lime;
            }
            catch { /* Exception when editing too fast */ }
        }

        /// <summary>
        /// Saves current maze data to MAZEDATA
        /// </summary>
        public void StoreMazeData()
        {
            string[] d = GetMaze().Split('\n');
            MAZEDATA = new string[d.Count() + 1];
            for (int i = 0; i < d.Count(); i++)
                MAZEDATA[i + 1] = d[i];
        }

        /// <summary>
        /// Returns maze as string without config
        /// </summary>
        /// <returns></returns>
        public string GetMaze()
        {
            // config
            string content = String.Empty;
            for (int grid = 0; grid < LEVELS.Count; grid++) // Loop all levels
            {
                LEVELS[grid].Grid.EndEdit();
                for (int i = 0; i < Y_COUNT; i++) // Loop all rows
                {
                    for (int j = 0; j < X_COUNT; j++)// Loop all columns 
                        if (LEVELS[grid].Grid.Rows[i].Cells[j].Value == null)
                            content += 0;
                        else
                            content += (int)LEVELS[grid].Grid.Rows[i].Cells[j].Value;
                    if (i < (Y_COUNT * LEVELS.Count) - 1) content += "\n";
                }
            }
            return content;
        }

        private List<double[]> GenerateMazeObjects()
        {
            // Generate object locations
            List<double[]> boxList = new List<double[]>();
            int startZ = 0;
            for (int lev = 0; lev < LEVELS.Count; lev++)
            {
                for (int y = 0; y < Y_COUNT; y++) // Loop all rows
                {
                    for (int x = 0; x < X_COUNT; x++) // Loop all columns 
                    {
                        double z = 0;
                        double[] box = new double[4]; // x,y,z,map
                        // Floor
                        if (FLOOR && lev==0 || lev > 0)
                        {
                            box[0] = SPACING * x + STARTCOORDS[0];
                            box[1] = SPACING * y + STARTCOORDS[1];
                            box[2] = SPACING * (startZ + z) + STARTCOORDS[2];
                            box[3] = STARTCOORDS[3];
                            boxList.Add(box);
                            box = new double[4];
                            z++;
                        }

                        // Walls
                        if (LEVELS[lev].Grid.Rows[y].Cells[x].Value != null && (int)LEVELS[lev].Grid.Rows[y].Cells[x].Value == 1)
                        {
                            // Create wall
                            for (int k = 0; k < WALLHEIGHT; k++)
                            {
                                box[0] = SPACING * x + STARTCOORDS[0];
                                box[1] = SPACING * y + STARTCOORDS[1];
                                box[2] = SPACING * (startZ + z + k) + STARTCOORDS[2];
                                box[3] = STARTCOORDS[3];
                                boxList.Add(box);
                                box = new double[4];
                            }
                        }
                        z += WALLHEIGHT;

                        // Roof
                        if (ROOF && lev == LEVELS.Count - 1) // Last level only
                        {
                            box[0] = SPACING * x + STARTCOORDS[0];
                            box[1] = SPACING * y + STARTCOORDS[1];
                            box[2] = SPACING * (startZ + z) + STARTCOORDS[2];
                            box[3] = STARTCOORDS[3];
                            boxList.Add(box);
                            box = new double[4];
                        }
                    }
                }

                //setss next startZ
                startZ += WALLHEIGHT;
                if (FLOOR && lev == 0 || lev > 0 && lev < LEVELS.Count - 1)
                    startZ++;
            }
            return boxList;
        }

        #region File menu
        public void OpenFile()
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            string saveData = String.Empty;
            if (result == DialogResult.OK) // Test result.
                saveData = File.ReadAllText(openFileDialog1.FileName);
            else return;

            // Opens and cleans file
            string[] temp = saveData.Split('\n');
            List<string> cleanData = new List<string>();
            foreach (string s in temp)
            {
                string v = s.Replace("\r", "");
                if (v != String.Empty)
                    cleanData.Add(v);
            }

            Program.configForm.LoadConfig(cleanData[0]);
            LoadMaze(saveData);
        }

        private void Export(string path)
        {
            string sql = "INSERT INTO `gameobject`(`id`, `map`, `spawnMask`, `phaseMask`, `position_x`, `position_y`, `position_z`, `orientation`, `rotation0`, `rotation1`, `rotation2`, `rotation3`, `spawntimesecs`, `animprogress`, `state`) VALUES\n";
            string endLine = String.Empty;
            int curr = 0;
            List<double[]> maze = GenerateMazeObjects();
            foreach (double[] box in maze)
            {
                curr++;
                if (curr < maze.Count)
                    endLine = ",\n";
                else endLine = ";\n";
                sql += "(" + GAMEOBJECT + "," + box[3] + ",1,1," + box[0].ToString().Replace(',', '.') + "," + box[1].ToString().Replace(',', '.') + "," + box[2].ToString().Replace(',', '.') + ",0,0,0,0,0,0,0,0)" + endLine;
            }

            // Add gameobject_template for Maze Crate
            if (GAMEOBJECT == 745000)
                sql += "INSERT IGNORE INTO `gameobject_template` (`entry`, `type`, `displayId`, `name`, `IconName`, `castBarCaption`, `unk1`, `faction`, `flags`, `size`, `questItem1`, `questItem2`, `questItem3`, `questItem4`, `questItem5`, `questItem6`, `data0`, `data1`, `data2`, `data3`, `data4`, `data5`, `data6`, `data7`, `data8`, `data9`, `data10`, `data11`, `data12`, `data13`, `data14`, `data15`, `data16`, `data17`, `data18`, `data19`, `data20`, `data21`, `data22`, `data23`, `ScriptName`) VALUES\n" +
                    "('745000', '5', '31', 'Maze Crate', '', '', '', '94', '0', '2', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '');\n";

            // Add gameobject_template for Maze Stairs
            sql += "INSERT IGNORE INTO `gameobject_template` (`entry`, `type`, `displayId`, `name`, `IconName`, `castBarCaption`, `unk1`, `faction`, `flags`, `size`, `questItem1`, `questItem2`, `questItem3`, `questItem4`, `questItem5`, `questItem6`, `data0`, `data1`, `data2`, `data3`, `data4`, `data5`, `data6`, `data7`, `data8`, `data9`, `data10`, `data11`, `data12`, `data13`, `data14`, `data15`, `data16`, `data17`, `data18`, `data19`, `data20`, `data21`, `data22`, `data23`, `ScriptName`) VALUES\n" +
                "('745001', '5', '7593', 'Maze Stairs', '', '', '', '94', '0', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '');\n";

            File.WriteAllText(path, sql);
        }

        // Save file content
        private bool SaveFile()
        {
            var save = saveToFileDialog.ShowDialog();
            if (save == DialogResult.OK)
            {
                Program.creator.StoreMazeData();
                Program.configForm.SetConfig();

                System.IO.File.WriteAllLines(saveToFileDialog.FileName, MAZEDATA);
                changedSinceSave = false;
                return true;
            }
            else return false;
        }
        #endregion

        #region Edit menu
        /// <summary>
        /// Adds an row or column in a specific location
        /// </summary>
        /// <param name="loc">1: Left - 2: Right - 3: Top - 4: Bottom</param>
        private void AddLine(int loc)
        {
            if (loc == 1 || loc == 2)
            {
                // Change in config
                int newCount = X_COUNT + 1;
                Program.configForm.LoadConfig(String.Empty, 3, newCount.ToString());
                X_COUNT = newCount;

                // Change in creator
                for (int i = 0; i < LEVELS.Count; i++)
                {
                    var c = new DataGridViewColumn();
                    c.Width = 20;
                    if (loc == 1) // left
                        LEVELS[i].Grid.Columns.Insert(0, c);
                    else // 2 - right
                        LEVELS[i].Grid.Columns.Add(c);
                    ReloadColors(i);
                }
            }
            else if (loc == 3 || loc == 4)
            {
                // Change in config
                int newCount = Y_COUNT + 1;
                Program.configForm.LoadConfig(String.Empty, 4, newCount.ToString());
                Y_COUNT = newCount;

                // Change in creator
                for (int i = 0; i < LEVELS.Count; i++)
                {
                    if (loc == 3) // left
                        LEVELS[i].Grid.Rows.Insert(0);
                    else // 2 - right
                        LEVELS[i].Grid.Rows.Add();
                    ReloadColors(i);
                }
            }
        }

        /// <summary>
        /// Removes the selected row or column
        /// </summary>
        /// <param name="type">1: Row - 2: Column</param>
        private void RemoveLine(int type)
        {
            var selected = LEVELS[activeGrid].Grid.CurrentCell;
            if (type == 1) // row
            {
                // Select in grid
                int i = selected.RowIndex;
                LEVELS[activeGrid].Grid.Rows[i].Selected = true;

                var result = MessageBox.Show("Are you sure you want to remove this row on all levels?",
                    "Remove row", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Change in config
                    int newCount = Y_COUNT - 1;
                    Program.configForm.LoadConfig(String.Empty, 4, newCount.ToString());
                    Y_COUNT = newCount;

                    // Change in grid
                    for (int j = 0; j < LEVELS.Count; j++)
                        LEVELS[j].Grid.Rows.RemoveAt(i);
                }
            }
            else if (type == 2) // column
            {
                // Select in grid
                int i = selected.ColumnIndex;
                for (int r = 0; r < LEVELS[activeGrid].Grid.RowCount; r++)
                    LEVELS[activeGrid].Grid[i, r].Selected = true;

                var result = MessageBox.Show("Are you sure you want to remove this column on all levels?",
                    "Remove column", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Change in config
                    int newCount = X_COUNT - 1;
                    Program.configForm.LoadConfig(String.Empty, 3, newCount.ToString());
                    X_COUNT = newCount;

                    // Change in grid
                    for (int j = 0; j < LEVELS.Count; j++)
                        LEVELS[j].Grid.Columns.RemoveAt(i);
                }
            }
        }
        #endregion

        #region Tools menu

        private void FillBorders()
        {
            for (int i = 0; i < Y_COUNT; i++)
            {
                if (i == 0 || i == Y_COUNT - 1)
                {
                    for (int j = 0; j < X_COUNT; j++)
                        LEVELS[activeGrid].Grid.Rows[i].Cells[j].Value = 1;
                }
                else
                {
                    LEVELS[activeGrid].Grid.Rows[i].Cells[0].Value = 1;
                    LEVELS[activeGrid].Grid.Rows[i].Cells[X_COUNT - 1].Value = 1;
                }
            }
            ReloadColors(activeGrid);
        }
        private void FillMaze(bool empty=false)
        {
            for (int i = 0; i < Y_COUNT; i++) // Loop all rows
            {
                for (int j = 0; j < X_COUNT; j++)// Loop all columns 
                {
                    if (empty)
                        LEVELS[activeGrid].Grid.Rows[i].Cells[j].Value = 0;
                    else
                        LEVELS[activeGrid].Grid.Rows[i].Cells[j].Value = 1;
                }
            }
            ReloadColors(activeGrid);
        }
        #endregion

        #region UI Handlers
        private void fillBordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillBorders();
        }

        private void exportToSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var save = ExportSqlDialog.ShowDialog();
            if (save == DialogResult.OK) 
                Export(ExportSqlDialog.FileName);
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/RStijn/MazeCreator");
        }

        private void fillWholeMazeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillMaze();
        }

        private void clearWholeMazeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillMaze(empty:true);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        // Set Color
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (stairsDirection == 0) // if not placing stairs
                SetCellBackColor(activeGrid, e.RowIndex, e.ColumnIndex);
        }

        // Selected cell looks
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Back color
            if (LEVELS[activeGrid].Grid.SelectedCells.Count > 0)
                LEVELS[activeGrid].Grid.DefaultCellStyle.SelectionBackColor = LEVELS[activeGrid].Grid.SelectedCells[0].Style.BackColor;

            // Border
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (LEVELS[activeGrid].Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected == true)
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
                        if (!SaveFile()) 
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
            Program.configForm.DisplayConfigForm();
            this.Hide();
        }

        private void insertColumnLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLine(1);
        }

        private void insertColumnRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLine(2);
        }

        private void insertRowTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLine(3);
        }

        private void insertRowBottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLine(4);
        }

        private void removeSelectedRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveLine(1);
        }
        private void removeSelectedColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveLine(2);
        }
        private void levelTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            activeGrid = levelTabControl.SelectedIndex;
            if (activeGrid == -1) activeGrid = 0;
        }
        private void add3DLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLevel();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            PlaceStairs(1);
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            PlaceStairs(2);
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            PlaceStairs(3);
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            PlaceStairs(4);
        }
        #endregion
    }
}