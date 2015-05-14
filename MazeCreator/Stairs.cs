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
        private bool allowedLocation = true;
        public int stairsDirection = 0;
        private int[,] newLocation = new int[4,2]; 

        /// <summary>
        /// Prepare to place stairs
        /// </summary>
        /// <param name="direction"></param>
        public Stairs(int direction)
        {
            stairsDirection = direction;

            // Display instructions
            MessageBox.Show("Activate the block where the bottom of your stairs start.");

            // Let user select start of maze
            Config.LEVELS[App.activeGrid].Grid.CellMouseEnter += PlacingStairs;
            Config.LEVELS[App.activeGrid].Grid.CellMouseDown += ConfirmPlaceStairs;
        }

        public void PlacingStairs(object sender, DataGridViewCellEventArgs e)
        {
            allowedLocation = true;
            int locX = e.ColumnIndex;
            int locY = e.RowIndex;
            
            App.creator.ReloadColors(App.activeGrid);
            int reqBlocks = 4;
            
            try
            {
                switch (stairsDirection)
                {
                    case 1: // up
                        if (locY - reqBlocks + 1 < 0)
                            locY = reqBlocks - 1;
                        for (int i = 0; i < reqBlocks; i++)
                        {
                            AddStairsPos(i, locX, locY - i);
                            CheckAllowedLocation(locX, locY - i);
                        }
                        break;
                    case 2: // down
                        if (locY + reqBlocks > Config.LEVELS[App.activeGrid].Grid.ColumnCount)
                            locY = Config.LEVELS[App.activeGrid].Grid.RowCount - reqBlocks;
                        for (int i = 0; i < reqBlocks; i++)
                        {
                            AddStairsPos(i, locX, locY + i);
                            CheckAllowedLocation(locX, locY + i);
                        }
                        break;
                    case 3: // left
                        if (locX - reqBlocks + 1 < 0)
                            locX = reqBlocks - 1;
                        for (int i = 0; i < reqBlocks; i++)
                        {
                            AddStairsPos(i, locX - i, locY);
                            CheckAllowedLocation(locX - i, locY);
                        }
                        break;
                    case 4: // right
                        if (locX + reqBlocks > Config.LEVELS[App.activeGrid].Grid.ColumnCount)
                            locX = Config.LEVELS[App.activeGrid].Grid.ColumnCount - reqBlocks;
                        for (int i = 0; i < reqBlocks; i++)
                        {
                            AddStairsPos(i, locX + i, locY);
                            CheckAllowedLocation(locX + i, locY);
                        }
                        break;
                }
            }
            catch (ArgumentOutOfRangeException) // Mouse moved outside of grid 
            {
                allowedLocation = false;
            }
        }

        /// <summary>
        /// Sets allowedLocation to false if stairs already exist here
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void CheckAllowedLocation(int x, int y)
        {
            int value = (int)Config.LEVELS[App.activeGrid].Grid.Rows[y].Cells[x].Value;
            if (value >= 2 && value <= 5)
                allowedLocation = false;
        }

        /// <summary>
        /// Sets color for temporary placement
        /// Fills newLocation to be used by ConfirmPlaceStairs
        /// </summary>
        /// <param name="i">part of stairs</param>
        /// <param name="locX">X location of part</param>
        /// <param name="locY">Y location of part</param>
        private void AddStairsPos(int i, int locX, int locY)
        {
            // Set temporary color
            Config.LEVELS[App.activeGrid].Grid.Rows[locY].Cells[locX].Style.BackColor = App.color[i + 2];

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
        public void ConfirmPlaceStairs(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!allowedLocation)
            {
                MessageBox.Show("You can't place stairs here.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Remove remove handlers
            Config.LEVELS[App.activeGrid].Grid.CellMouseEnter -= PlacingStairs;
            Config.LEVELS[App.activeGrid].Grid.CellMouseDown -= ConfirmPlaceStairs;

            // Only trigger when stairs should be placed
            if (stairsDirection == 0) return;
            stairsDirection = 0;

            // Set stairs location
            int next = 5; // Count down from 5 to 2
            for (int i = 0; i < 4; i++)
            {
                Config.LEVELS[App.activeGrid].Grid.Rows[newLocation[i, 0]].Cells[newLocation[i, 1]].Value = next;
                App.creator.SetCellInfo(App.activeGrid, newLocation[i, 0], newLocation[i, 1]);

                // Add top of stairs to next level
                if (i == 0 && App.activeGrid + 1 < Config.LEVELS.Count)
                {
                    Config.LEVELS[App.activeGrid + 1].Grid.Rows[newLocation[i, 0]].Cells[newLocation[i, 1]].Value = next;
                    App.creator.SetCellInfo(App.activeGrid + 1, newLocation[i, 0], newLocation[i, 1]);
                }

                next--;
            }
        }

        /// <summary>
        /// Attempt to remove stairs at selected location
        /// </summary>
        internal static void Remove()
        {
            var grid = Config.LEVELS[App.activeGrid].Grid;
            var bottom = grid.SelectedCells[0];

            if ((int)bottom.Value != 2) // No stairs selected
            {
                MessageBox.Show("Select the bottom of the stairs you want to remove and click Remove Stairs again.", 
                    "No stairs selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                var result = MessageBox.Show("Are you sure you want to remove the selected stairs?", "Remove stairs",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes) return;
            }

            int x = bottom.ColumnIndex;
            int y = bottom.RowIndex;

            // vars for spawn location check
            int left = x - 1;
            int right = x + 1;
            int above = y - 1;
            int below = y + 1;

            // Determine placement location & orientation
            if (above >= 0 && (int)grid.Rows[above].Cells[x].Value == 3)
            {
                for (int i = 0; i < 4; i++)
                    grid.Rows[y - i].Cells[x].Value = 0;
            }
            else if (below <= grid.Columns.Count - 1 && (int)grid.Rows[below].Cells[x].Value == 3)
            {
                for (int i = 0; i < 4; i++)
                    grid.Rows[y + i].Cells[x].Value = 0;
            }
            else if (left >= 0 && (int)grid.Rows[y].Cells[left].Value == 3)
            {
                for (int i = 0; i < 4; i++)
                    grid.Rows[y].Cells[x - i].Value = 0;
            }
            else if (right <= grid.Rows.Count - 1 && (int)grid.Rows[y].Cells[right].Value == 3)
            {
                for (int i = 0; i < 4; i++)
                    grid.Rows[y].Cells[x + i].Value = 0;
            }

            App.creator.ReloadColors(App.activeGrid);
        }
    }
}
