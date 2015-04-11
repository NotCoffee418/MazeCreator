using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeCreator
{
    public partial class Creator : Form
    {
        int         GAMEOBJECT;
        double      SPACING;
        int         WALLHEIGHT;
        int         X_COUNT;
        int         Y_COUNT;
        bool        FLOOR;
        bool        ROOF;
        double[]    STARTCOORDS = new double[4]; // x,y,z,map

        public Creator(string[] mazeConfig)
        {
            InitializeComponent();

            // TODO: Complete member initialization
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
            Show();
        }

        private void LoadTemplate()
        {
            dataGridView1.RowCount = Y_COUNT;
            for (int i = 0; i < X_COUNT; i++)
            {
                DataGridViewColumn c = new DataGridViewCheckBoxColumn();
                c.Width = 20;
                c.HeaderText = (i+1).ToString();
                dataGridView1.Columns.Add(c);
            }
            dataGridView1.Columns.RemoveAt(0);
        }

        private void Export(string path)
        {
            string sql = String.Empty;
            List<double[]> maze = GenerateMaze();
            foreach (double[] box in maze)
            {
                sql += "INSERT INTO `gameobject`(`id`, `map`, `spawnMask`, `phaseMask`, `position_x`, `position_y`, `position_z`, `orientation`, `rotation0`, `rotation1`, `rotation2`, `rotation3`, `spawntimesecs`, `animprogress`, `state`) "+
                    "VALUES (" + GAMEOBJECT + "," + box[3] + ",1,1," + box[0].ToString().Replace(',', '.') + "," + box[1].ToString().Replace(',', '.') + "," + box[2].ToString().Replace(',', '.') + ",0,0,0,0,0,0,0,0);\n";

            }

            System.IO.File.WriteAllText(path, sql);
        }

        private List<double[]> GenerateMaze()
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
        }
#endregion



#region UI Handlers
        private void fillBordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillBorders();
        }

#endregion

        private void exportToSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var save = saveFileDialog1.ShowDialog();
            if (save == DialogResult.OK) 
                Export(saveFileDialog1.FileName);
        }

        
    }
}
