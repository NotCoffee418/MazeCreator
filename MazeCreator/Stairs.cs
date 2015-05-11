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
            // Remove wall handler
            App.config.LEVELS[App.activeGrid].Grid.CellValueChanged -= 
                new DataGridViewCellEventHandler(App.creator.dataGridView1_CellValueChanged);
            App.config.LEVELS[App.activeGrid].Grid.MultiSelect = true;

            // Display instructions
            MessageBox.Show("Activate the block where the bottom of your stairs start.");

            // Let user select start of maze
            App.config.LEVELS[App.activeGrid].Grid.SelectionChanged += new System.EventHandler(PlacingStairs);
        }
        public static void PlacingStairs(object sender, EventArgs e)
        {
            if (progSel) return;

            var loc = App.config.LEVELS[App.activeGrid].Grid.SelectedCells[0];
            int reqBlocks = 4;

            progSel = true;
            switch (stairsDirection)
            {
                case 1: // up
                    if (loc.RowIndex - reqBlocks + 1 >= 0)
                        for (int i = 0; i < reqBlocks; i++)
                            App.config.LEVELS[App.activeGrid].Grid.Rows[loc.RowIndex - i].Cells[loc.ColumnIndex].Selected = true;
                    else
                    {
                        progSel = false;
                        App.config.LEVELS[App.activeGrid].Grid.Rows[reqBlocks - 1].Cells[loc.ColumnIndex].Selected = true;
                    }
                    break;
                case 2: // down
                    if (loc.RowIndex + reqBlocks <= App.config.LEVELS[App.activeGrid].Grid.ColumnCount)
                        for (int i = 0; i < reqBlocks; i++)
                            App.config.LEVELS[App.activeGrid].Grid.Rows[loc.RowIndex + i].Cells[loc.ColumnIndex].Selected = true;
                    else
                    {
                        progSel = false;
                        App.config.LEVELS[App.activeGrid].Grid.Rows[App.config.LEVELS[App.activeGrid].Grid.RowCount - reqBlocks].Cells[loc.ColumnIndex].Selected = true;
                    }
                    break;
                case 3: // left
                    if (loc.ColumnIndex - reqBlocks + 1 >= 0)
                        for (int i = 0; i < reqBlocks; i++)
                            App.config.LEVELS[App.activeGrid].Grid.Rows[loc.RowIndex].Cells[loc.ColumnIndex - i].Selected = true;
                    else
                    {
                        progSel = false;
                        App.config.LEVELS[App.activeGrid].Grid.Rows[loc.RowIndex].Cells[reqBlocks - 1].Selected = true;
                    }
                    break;
                case 4: // right
                    if (loc.ColumnIndex + reqBlocks <= App.config.LEVELS[App.activeGrid].Grid.ColumnCount)
                        for (int i = 0; i < reqBlocks; i++)
                            App.config.LEVELS[App.activeGrid].Grid.Rows[loc.RowIndex].Cells[loc.ColumnIndex + i].Selected = true;
                    else
                    {
                        progSel = false;
                        App.config.LEVELS[App.activeGrid].Grid.Rows[loc.RowIndex].Cells[App.config.LEVELS[App.activeGrid].Grid.ColumnCount - reqBlocks].Selected = true;
                    }
                    break;
            }
            progSel = false;
        }
        public static void ConfirmPlaceStairs(object sender, DataGridViewCellEventArgs e)
        {
            // Only trigger when stairs should be placed
            if (stairsDirection == 0) return;
            stairsDirection = 0;

            // Remove placing stairs handler
            App.config.LEVELS[App.activeGrid].Grid.SelectionChanged -= new System.EventHandler(PlacingStairs);

            // Get selected cells in the correct direction/order
            var cells = App.config.LEVELS[App.activeGrid].Grid.SelectedCells;

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
                App.creator.SetCellBackColor(App.activeGrid, cells[i].RowIndex, cells[i].ColumnIndex);
            }

            // Add wall handler again
            App.config.LEVELS[App.activeGrid].Grid.CellValueChanged += 
                new DataGridViewCellEventHandler(App.creator.dataGridView1_CellValueChanged);
            App.config.LEVELS[App.activeGrid].Grid.MultiSelect = false;
        }
    }
}
