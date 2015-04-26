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
            {
                // Create grid
                var grid = new DataGridView();
                grid.AllowUserToAddRows = false;
                grid.AllowUserToDeleteRows = false;
                grid.AllowUserToResizeColumns = false;
                grid.AllowUserToResizeRows = false;
                grid.Dock = DockStyle.Fill;
                grid.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            

                // Add tab
                var page = new TabPage();
                page.Location = new System.Drawing.Point(4, 22);
                page.Name = "levelPage" + (i+1);
                page.Padding = new System.Windows.Forms.Padding(3);
                page.Size = new System.Drawing.Size(968, 463);
                page.TabIndex = 0;
                page.Text = "Level " + (i+1);
                page.UseVisualStyleBackColor = true;
                page.Controls.Add(grid);

                // Add to LEVELS and levelControl
                Level l = new Level();
                l.Tab = page;
                l.Grid = grid;
                LEVELS.Add(l);
                levelTabControl.Controls.Add(page);
                LoadGrid(i, rows, 1+(Y_COUNT * i));
            }
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

            // Set columns & rows
            LEVELS[grid].Grid.RowCount = Y_COUNT;
            for (int i = 0; i < X_COUNT; i++)
            {
                DataGridViewColumn c = new DataGridViewCheckBoxColumn();
                c.Width = 20;
                c.HeaderText = (i + 1).ToString();
                LEVELS[grid].Grid.Columns.Add(c);
            }
            if (LEVELS[grid].Grid.Columns.Count == 1 + X_COUNT)
                LEVELS[grid].Grid.Columns.RemoveAt(0);

            // Sets rows if none given
            if (rows == null) // From existing if no rows were given
                rows = MAZEDATA;

            // Loads data to grid
            for (int i = 0; i < Y_COUNT; i++)
            {
                int col = 0;
                if (rows.Count() > 2 && rows[i] != "") // Check if any data were
                    for (int j = 0; j < X_COUNT; j++)
                    {
                        bool v = false;
                        if (rows[lastRow + i][j] == '1') v = true;
                        LEVELS[grid].Grid.Rows[i].Cells[col].Value = v;
                        col++;
                    }
            }
            ReloadColors(grid);
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
            if (LEVELS[grid].Grid.Rows[i].Cells[j].Value != null &&
                (bool)LEVELS[grid].Grid.Rows[i].Cells[j].Value)
                    LEVELS[grid].Grid.Rows[i].Cells[j].Style.BackColor = Color.Red;
            else    LEVELS[grid].Grid.Rows[i].Cells[j].Style.BackColor = Color.Lime;
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
            try
            {
                for (int grid = 0; grid < LEVELS.Count; grid++) // Loop all levels
                {
                    LEVELS[grid].Grid.EndEdit();
                    for (int i = 0; i < Y_COUNT; i++) // Loop all rows
                    {
                        for (int j = 0; j < X_COUNT; j++)// Loop all columns 
                        {
                            if (LEVELS[grid].Grid.Rows[i].Cells[j].Value == null ||
                                !(bool)LEVELS[grid].Grid.Rows[i].Cells[j].Value)
                                content += "0";
                            else content += "1";
                        }
                        if (i < (Y_COUNT * LEVELS.Count) - 1) content += "\n";
                    }
                }
            }
            catch { }
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
                        if (LEVELS[lev].Grid.Rows[y].Cells[x].Value != null && (bool)LEVELS[lev].Grid.Rows[y].Cells[x].Value)
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
                    "('745000', '5', '31', 'Maze Crate', '', '', '', '94', '0', '2', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '');";

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
                    var c = new DataGridViewCheckBoxColumn();
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
                        LEVELS[activeGrid].Grid.Rows[i].Cells[j].Value = true;
                }
                else
                {
                    LEVELS[activeGrid].Grid.Rows[i].Cells[0].Value = true;
                    LEVELS[activeGrid].Grid.Rows[i].Cells[X_COUNT - 1].Value = true;
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
                        LEVELS[activeGrid].Grid.Rows[i].Cells[j].Value = false;
                    else
                        LEVELS[activeGrid].Grid.Rows[i].Cells[j].Value = true;
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


        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var sel = LEVELS[activeGrid].Grid.SelectedCells[0];
                SetCellBackColor(activeGrid, sel.RowIndex, sel.ColumnIndex);
                changedSinceSave = true;
            }
            catch { }
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
        }
        #endregion
    }
}
