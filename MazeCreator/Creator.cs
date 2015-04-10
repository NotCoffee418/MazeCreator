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
        int     GAMEOBJECT;
        double  SPACING;
        int     WALLHEIGHT;
        int     X_COUNT;
        int     Y_COUNT;
        bool    FLOOR;
        bool    ROOF;

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
            GenerateMaze();
        }

        private List<double[]> GenerateMaze()
        {
            List<double[]> boxList = new List<double[]>();
            for (int i = 0; i < Y_COUNT; i++) // Loop all rows
            {
                for (int j = 0; j < X_COUNT; j++)// Loop all columns 
                {
                    double[] box = new double[3];
                    // Floor
                    if (FLOOR)
                    {
                        box[0] = i * SPACING;
                        box[1] = j * SPACING;
                        box[2] = 0;
                        boxList.Add(box);
                    }

                    // Walls
                    if ((bool)dataGridView1.Rows[i].Cells[j].Value)
                    {
                        // Create wall
                        for (int k = 1; k <= WALLHEIGHT; k++)
                        {
                            box[0] = i * SPACING;
                            box[1] = j * SPACING;
                            box[2] = k * SPACING;
                            boxList.Add(box);
                        }
                    }

                    // Roof
                    if (ROOF)
                    {
                        box[0] = i * SPACING;
                        box[1] = j * SPACING;
                        box[2] = WALLHEIGHT + 1 * SPACING;
                        boxList.Add(box);
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
