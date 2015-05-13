using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MazeCreator
{
    class Stairs
    {

        public static Color[] stairsColor = new Color[4];
        public static int stairsDirection = 0;
        private static bool progSel = false;
        private static int[,] newLocation = new int[4,2]; 


        // Loads stairs colors
        public static void InitStairs()
        {
            // Set stairsColor
            stairsColor[0] = Color.MediumAquamarine; // bottom
            stairsColor[1] = Color.MediumTurquoise; // Placementblock
            stairsColor[2] = Color.Aquamarine; // middle high
            stairsColor[3] = Color.Aqua; // top
        }

        /// <summary>
        /// Prepare to place stairs
        /// </summary>
        /// <param name="direction"></param>
        public static void PlaceStairs(int direction)
        {
            stairsDirection = direction;

            // Display instructions
            MessageBox.Show("Activate the block where the bottom of your stairs start.");

            // Remove wall handler
            Config.LEVELS[App.activeGrid].Grid.CellValueChanged -=
                new DataGridViewCellEventHandler(App.creator.dataGridView1_CellValueChanged);

            // Let user select start of maze
            Config.LEVELS[App.activeGrid].Grid.SelectionChanged += new System.EventHandler(PlacingStairs);
            Config.LEVELS[App.activeGrid].Grid.CellValueChanged +=
                new DataGridViewCellEventHandler(ConfirmPlaceStairs);
        }
        public static void PlacingStairs(object sender, EventArgs e)
        {
            if (progSel || Config.LEVELS[App.activeGrid].Grid.SelectedCells.Count == 0) 
                return;
            progSel = true;

            App.creator.ReloadColors(App.activeGrid);
            int locX = Config.LEVELS[App.activeGrid].Grid.SelectedCells[0].ColumnIndex;
            int locY = Config.LEVELS[App.activeGrid].Grid.SelectedCells[0].RowIndex;
            int reqBlocks = 4;

            switch (stairsDirection)
            {
                case 1: // up
                    if (locY - reqBlocks + 1 >= 0)
                        for (int i = 0; i < reqBlocks; i++)
                            AddStairsPos(i, locX, locY - i);
                    else
                    {
                        progSel = false;
                        Config.LEVELS[App.activeGrid].Grid.Rows[reqBlocks - 1].Cells[locX].Selected = true;
                    }
                    break;
                case 2: // down
                    if (locY + reqBlocks <= Config.LEVELS[App.activeGrid].Grid.ColumnCount)
                        for (int i = 0; i < reqBlocks; i++)
                            AddStairsPos(i, locX, locY + i);
                    else
                    {
                        progSel = false;
                        Config.LEVELS[App.activeGrid].Grid.Rows[Config.LEVELS[App.activeGrid].Grid.RowCount - reqBlocks].Cells[locX].Selected = true;
                    }
                    break;
                case 3: // left
                    if (locX - reqBlocks + 1 >= 0)
                        for (int i = 0; i < reqBlocks; i++)
                            AddStairsPos(i, locX - i, locY);
                    else
                    {
                        progSel = false;
                        Config.LEVELS[App.activeGrid].Grid.Rows[locY].Cells[reqBlocks - 1].Selected = true;
                    }
                    break;
                case 4: // right
                    if (locX + reqBlocks <= Config.LEVELS[App.activeGrid].Grid.ColumnCount)
                        for (int i = 0; i < reqBlocks; i++)
                            AddStairsPos(i, locX + i, locY);
                    else
                    {
                        progSel = false;
                        Config.LEVELS[App.activeGrid].Grid.Rows[locY].Cells[Config.LEVELS[App.activeGrid].Grid.ColumnCount - reqBlocks].Selected = true;
                    }
                    break;
            }
            progSel = false;
        }

        /// <summary>
        /// Sets color for temporary placement
        /// Fills newLocation to be used by ConfirmPlaceStairs
        /// </summary>
        /// <param name="i">part of stairs</param>
        /// <param name="locX">X location of part</param>
        /// <param name="locY">Y location of part</param>
        private static void AddStairsPos(int i, int locX, int locY)
        {
            // Set temporary color
            Config.LEVELS[App.activeGrid].Grid.Rows[locY].Cells[locX].Style.BackColor = stairsColor[i]; ;

            // Add part
            i = 3 - i; // Inverted
            newLocation[i, 0] = locY;
            newLocation[i, 1] = locX;
        }

        /// <summary>
        /// Confirms the stairs location & places it permanently
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ConfirmPlaceStairs(object sender, DataGridViewCellEventArgs e)
        {
            Config.LEVELS[App.activeGrid].Grid.CellValueChanged -=
                new DataGridViewCellEventHandler(ConfirmPlaceStairs);
            // Only trigger when stairs should be placed
            if (stairsDirection == 0) return;
            stairsDirection = 0;

            // Remove placing stairs handler
            Config.LEVELS[App.activeGrid].Grid.SelectionChanged -= new System.EventHandler(PlacingStairs);

            // Set stairs location
            int next = 5; // Count down from 5 to 2
            for (int i = 0; i < 4; i++ )
            {
                Config.LEVELS[App.activeGrid].Grid.Rows[newLocation[i, 0]].Cells[newLocation[i, 1]].Value = next;
                next--;
            }

            // Add wall handler again
            Config.LEVELS[App.activeGrid].Grid.CellValueChanged += 
                new DataGridViewCellEventHandler(App.creator.dataGridView1_CellValueChanged);
        }
    }
}
