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
        string      mazeConfigText; // for saving
        int         GAMEOBJECT;
        double      SPACING;
        int         WALLHEIGHT;
        int         X_COUNT;
        int         Y_COUNT;
        bool        FLOOR;
        bool        ROOF;
        double[]    STARTCOORDS = new double[4]; // x,y,z,map
        bool        CREATE_GO_TEMPLATE;

        public Creator(string data)
        {
            InitializeComponent();
            LoadMaze(data);
            Show();
        }

        private void OpenFile()
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            string saveData = String.Empty;
            if (result == DialogResult.OK) // Test result.
                saveData = File.ReadAllText(openFileDialog1.FileName);
            else return;
            LoadMaze(saveData);
        }

        private void LoadMaze(string data)
        {
            String[] rows = data.Split('\n');
            LoadConfig(rows[0]);

            if (rows.Count() > 1)
                LoadSaveData(rows);
        }

        private void LoadSaveData(string[] rows)
        {
            // Load maze points
            for (int i = 1; i < rows[1].Count(); i++)
            {
                // -1 because config line
                int row = i - 1;
                int col = 0;
                foreach (char c in rows[i])
                {
                    bool v = false;
                    if (c == '1') v = true;
                    dataGridView1.Rows[row].Cells[col].Value = v;
                    col++;
                }
            }
            ReloadColors();
        }

        private void LoadConfig(string configString)
        {
            string[] mazeConfig = configString.Split('|');

            // set config
            mazeConfigText = configString;
            GAMEOBJECT = int.Parse(mazeConfig[0]);
            SPACING = double.Parse(mazeConfig[1]);
            WALLHEIGHT = int.Parse(mazeConfig[2]);
            X_COUNT = int.Parse(mazeConfig[3]);
            Y_COUNT = int.Parse(mazeConfig[4]);
            FLOOR = bool.Parse(mazeConfig[5]);
            ROOF = bool.Parse(mazeConfig[6]);
            STARTCOORDS[0] = double.Parse(mazeConfig[7]);
            STARTCOORDS[1] = double.Parse(mazeConfig[8]);
            STARTCOORDS[2] = double.Parse(mazeConfig[9]);
            STARTCOORDS[3] = double.Parse(mazeConfig[10]);
            LoadTemplate();
        }

        private void LoadTemplate()
        {
            // Clear datagrid
            while (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns.RemoveAt(0);
            }

            // Set columns & rows
            dataGridView1.RowCount = Y_COUNT;
            for (int i = 0; i < X_COUNT; i++)
            {
                DataGridViewColumn c = new DataGridViewCheckBoxColumn();
                c.Width = 20;
                c.HeaderText = (i+1).ToString();
                dataGridView1.Columns.Add(c);
            }
            dataGridView1.Columns.RemoveAt(0);
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
                // config
                string content = mazeConfigText;

                // Maze
                for (int i = 0; i < Y_COUNT; i++) // Loop all rows
                {
                    content += "\n";
                    for (int j = 0; j < X_COUNT; j++)// Loop all columns 
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value == null || !(bool)dataGridView1.Rows[i].Cells[j].Value)
                            content += "0";
                        else content += "1";
                    }
                }

                System.IO.File.WriteAllText(saveToFileDialog.FileName, content);
                changedSinceSave = false;
                return true;
            }
            else return false;
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
                    if (dataGridView1.Rows[i].Cells[j].Value != null && (bool)dataGridView1.Rows[i].Cells[j].Value)
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
            dataGridView1.ClearSelection();
            for (int i = 0; i < Y_COUNT; i++) // Loop all rows
            {
                for (int j = 0; j < X_COUNT; j++)  {// Loop all columns
                    SetCellBackColor(i, j);
                }
            }
        }

        private void SetCellBackColor(int i, int j)
        {
            if (dataGridView1.Rows[i].Cells[j].Value != null && (bool)dataGridView1.Rows[i].Cells[j].Value)
                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Red;
            else
                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Lime;
        }


        #region Tools

        private void FillBorders()
        {
            for (int i = 0; i < Y_COUNT; i++)
            {
                if (i == 0 || i == Y_COUNT - 1)
                {
                    for (int j = 0; j < X_COUNT; j++)
                        dataGridView1.Rows[i].Cells[j].Value = true;
                }
                else
                {
                    dataGridView1.Rows[i].Cells[0].Value = true;
                    dataGridView1.Rows[i].Cells[X_COUNT - 1].Value = true;
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
                        dataGridView1.Rows[i].Cells[j].Value = false;
                    else
                        dataGridView1.Rows[i].Cells[j].Value = true;
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
                var sel = dataGridView1.SelectedCells[0];
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
        #endregion
    }
}
