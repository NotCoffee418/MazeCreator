using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeCreator
{
    class Stairs
    {
        public static int stairsDirection = 0;
        private static bool progSel = false;

        /// <summary>
        /// Prepare to place stairs
        /// </summary>
        /// <param name="direction"></param>
        public static void PlaceStairs(int direction)
        {
            stairsDirection = direction;
            Config.LEVELS[App.activeGrid].Grid.MultiSelect = true;

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
            if (progSel) return;

            var locX = Config.LEVELS[App.activeGrid].Grid.SelectedCells[0].ColumnIndex;
            var locY = Config.LEVELS[App.activeGrid].Grid.SelectedCells[0].RowIndex;
            int reqBlocks = 4;

            progSel = true;
            switch (stairsDirection)
            {
                case 1: // up
                    if (locY - reqBlocks + 1 >= 0)
                        for (int i = 0; i < reqBlocks; i++)
                            Config.LEVELS[App.activeGrid].Grid.Rows[locY - i].Cells[locX].Selected = true;
                    else
                    {
                        progSel = false;
                        Config.LEVELS[App.activeGrid].Grid.Rows[reqBlocks - 1].Cells[locX].Selected = true;
                    }
                    break;
                case 2: // down
                    if (locY + reqBlocks <= Config.LEVELS[App.activeGrid].Grid.ColumnCount)
                        for (int i = 0; i < reqBlocks; i++)
                            Config.LEVELS[App.activeGrid].Grid.Rows[locY + i].Cells[locX].Selected = true;
                    else
                    {
                        progSel = false;
                        Config.LEVELS[App.activeGrid].Grid.Rows[Config.LEVELS[App.activeGrid].Grid.RowCount - reqBlocks].Cells[locX].Selected = true;
                    }
                    break;
                case 3: // left
                    if (locX - reqBlocks + 1 >= 0)
                        for (int i = 0; i < reqBlocks; i++)
                            Config.LEVELS[App.activeGrid].Grid.Rows[locY].Cells[locX - i].Selected = true;
                    else
                    {
                        progSel = false;
                        Config.LEVELS[App.activeGrid].Grid.Rows[locY].Cells[reqBlocks - 1].Selected = true;
                    }
                    break;
                case 4: // right
                    if (locX + reqBlocks <= Config.LEVELS[App.activeGrid].Grid.ColumnCount)
                        for (int i = 0; i < reqBlocks; i++)
                            Config.LEVELS[App.activeGrid].Grid.Rows[locY].Cells[locX + i].Selected = true;
                    else
                    {
                        progSel = false;
                        Config.LEVELS[App.activeGrid].Grid.Rows[locY].Cells[Config.LEVELS[App.activeGrid].Grid.ColumnCount - reqBlocks].Selected = true;
                    }
                    break;
            }
            progSel = false;
        }
        public static void ConfirmPlaceStairs(object sender, DataGridViewCellEventArgs e)
        {
            Config.LEVELS[App.activeGrid].Grid.CellValueChanged -=
                new DataGridViewCellEventHandler(ConfirmPlaceStairs);
            // Only trigger when stairs should be placed
            if (stairsDirection == 0) return;
            stairsDirection = 0;

            // Remove placing stairs handler
            Config.LEVELS[App.activeGrid].Grid.SelectionChanged -= new System.EventHandler(PlacingStairs);

            // Get selected cells in the correct direction/order
            var cells = Config.LEVELS[App.activeGrid].Grid.SelectedCells;

            // Set stairs location
            int next = 5; // Count down from 5 to 2
            for (int i = 0; i < cells.Count; i++ )
            {
                cells[i].Value = next;
                App.creator.SetCellBackColor(App.activeGrid, cells[i].RowIndex, cells[i].ColumnIndex);
                next--;
            }

            // Add wall handler again
            Config.LEVELS[App.activeGrid].Grid.CellValueChanged += 
                new DataGridViewCellEventHandler(App.creator.dataGridView1_CellValueChanged);
            Config.LEVELS[App.activeGrid].Grid.MultiSelect = false;
        }
    }
}
