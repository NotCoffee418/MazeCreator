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
        public bool     CREATE_GO_TEMPLATE;

        public Creator()
        {
            InitializeComponent();
        }

        public void OpenFile()
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            string saveData = String.Empty;
            if (result == DialogResult.OK) // Test result.
                saveData = File.ReadAllText(openFileDialog1.FileName);
            else return;

            string[] data = saveData.Split('\n');
            Program.configForm.LoadConfig(data[0]);
            Program.configForm.SetConfig();
            LoadMaze(saveData);
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

        public void LoadData(string[] rows = null)
        {
            // Clear datagrid
            while (mazeGrid.Columns.Count > 0)
                mazeGrid.Columns.RemoveAt(0);

            // Set columns & rows
            mazeGrid.RowCount = Y_COUNT;
            for (int i = 0; i < X_COUNT; i++)
            {
                DataGridViewColumn c = new DataGridViewCheckBoxColumn();
                c.Width = 20;
                c.HeaderText = (i + 1).ToString();
                mazeGrid.Columns.Add(c);
            }
            if (mazeGrid.Columns.Count == 1 + X_COUNT)
                mazeGrid.Columns.RemoveAt(0);

            // Load maze points
            if (rows == null)
                rows = MAZEDATA;
            if (rows[0] != "" && rows[1] != null)
            {
                for (int i = 1; i <= rows[1].Count(); i++)
                {
                    // -1 because config line
                    int row = i - 1;
                    int col = 0;
                    foreach (char c in rows[i])
                    {
                        if (c != '\r')
                        {
                            bool v = false;
                            if (c == '1') v = true;
                            mazeGrid.Rows[row].Cells[col].Value = v;
                            col++;
                        }
                    }
                }
            }
            ReloadColors();
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
                sql += "INSERT IGNORE INTO `gameobject_template` (`entry`, `type`, `displayId`, `name`, `IconName`, `castBarCaption`, `unk1`, `faction`, `flags`, `size`, `questItem1`, `questItem2`, `questItem3`, `questItem4`, `questItem5`, `questItem6`, `data0`, `data1`, `data2`, `data3`, `data4`, `data5`, `data6`, `data7`, `data8`, `data9`, `data10`, `data11`, `data12`, `data13`, `data14`, `data15`, `data16`, `data17`, `data18`, `data19`, `data20`, `data21`, `data22`, `data23`, `mingold`, `maxgold`, `ScriptName`) VALUES\n" +
                    "('745000', '5', '31', 'Maze Crate', '', '', '', '94', '0', '2', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '');";

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

        /// <summary>
        /// Saves current maze data to MAZEDATA
        /// </summary>
        internal void StoreMazeData()
        {
            string[] d = GetMazeData().Split('\n');
            MAZEDATA = new string[d.Count() + 1];
            for (int i = 0; i < d.Count(); i++)
                MAZEDATA[i + 1] = d[i];
        }

        /// <summary>
        /// Returns maze as string
        /// </summary>
        /// <returns></returns>
        public string GetMazeData()
        {
            // config
            string content = String.Empty;
            try
            {
                mazeGrid.EndEdit();
                // Maze
                for (int i = 0; i < Y_COUNT; i++) // Loop all rows
                {
                    for (int j = 0; j < X_COUNT; j++)// Loop all columns 
                    {
                        if (mazeGrid.Rows[i].Cells[j].Value == null || !(bool)mazeGrid.Rows[i].Cells[j].Value)
                            content += "0";
                        else content += "1";
                    }
                    if (i < Y_COUNT-1) content += "\n";
                }
            }
            catch (Exception e)  { }
            return content;
        }

        private List<double[]> GenerateMazeObjects()
        {
            // Generate object locations
            List<double[]> boxList = new List<double[]>();
            for (int i = 0; i < Y_COUNT; i++) // Loop all rows
            {
                for (int j = 0; j < X_COUNT; j++)// Loop all columns 
                {
                    double[] box = new double[4]; // x,y,z,map
                    // Floor
                    if (FLOOR)
                    {
                        box[0] = SPACING * i + STARTCOORDS[0];
                        box[1] = SPACING * j + STARTCOORDS[1];
                        box[2] = STARTCOORDS[2];
                        box[3] = STARTCOORDS[3];
                        boxList.Add(box);
                        box = new double[4];
                    }

                    // Walls
                    if (mazeGrid.Rows[i].Cells[j].Value != null && (bool)mazeGrid.Rows[i].Cells[j].Value)
                    {
                        // Create wall
                        for (int k = 1; k <= WALLHEIGHT; k++)
                        {
                            box[0] = SPACING * i + STARTCOORDS[0];
                            box[1] = SPACING * j + STARTCOORDS[1];
                            box[2] = SPACING * k + STARTCOORDS[2];
                            box[3] = STARTCOORDS[3];
                            boxList.Add(box);
                            box = new double[4];
                        }
                    }

                    // Roof
                    if (ROOF)
                    {
                        box[0] = SPACING * i + STARTCOORDS[0];
                        box[1] = SPACING * j + STARTCOORDS[1];
                        box[2] = SPACING * (WALLHEIGHT + 1) + STARTCOORDS[2];
                        box[3] = STARTCOORDS[3];
                        boxList.Add(box);
                        box = new double[4];
                    }
                }
            }

            return boxList;
        }

        private void ReloadColors()
        {
            mazeGrid.ClearSelection();
            for (int i = 0; i < Y_COUNT; i++) // Loop all rows
            {
                for (int j = 0; j < X_COUNT; j++)
                {// Loop all columns
                    SetCellBackColor(i, j);
                }
            }
        }

        private void SetCellBackColor(int i, int j)
        {
            if (mazeGrid.Rows[i].Cells[j].Value != null && (bool)mazeGrid.Rows[i].Cells[j].Value)
                mazeGrid.Rows[i].Cells[j].Style.BackColor = Color.Red;
            else
                mazeGrid.Rows[i].Cells[j].Style.BackColor = Color.Lime;
        }


        #region Tools

        private void FillBorders()
        {
            for (int i = 0; i < Y_COUNT; i++)
            {
                if (i == 0 || i == Y_COUNT - 1)
                {
                    for (int j = 0; j < X_COUNT; j++)
                        mazeGrid.Rows[i].Cells[j].Value = true;
                }
                else
                {
                    mazeGrid.Rows[i].Cells[0].Value = true;
                    mazeGrid.Rows[i].Cells[X_COUNT - 1].Value = true;
                }
            }
            ReloadColors();
        }
        private void FillMaze(bool empty=false)
        {
            for (int i = 0; i < Y_COUNT; i++) // Loop all rows
            {
                for (int j = 0; j < X_COUNT; j++)// Loop all columns 
                {
                    if (empty)
                        mazeGrid.Rows[i].Cells[j].Value = false;
                    else
                        mazeGrid.Rows[i].Cells[j].Value = true;
                }
            }
            ReloadColors();
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var sel = mazeGrid.SelectedCells[0];
                SetCellBackColor(sel.RowIndex, sel.ColumnIndex);
                changedSinceSave = true;
            }
            catch (Exception ex)
            { }
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
        #endregion        
    }
}
