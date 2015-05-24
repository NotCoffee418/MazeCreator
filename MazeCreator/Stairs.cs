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
        public static bool alreadyPlacing = false;
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
            alreadyPlacing = true;

            // Display instructions
            MessageBox.Show("Activate the block where the bottom of your stairs start.");

            // Let user select start of stairs
            App.GetLevel().CellMouseEnter += PlacingStairs;
            App.GetLevel().CellMouseDown += ConfirmPlaceStairs;
            App.GetLevel().KeyDown += CancelPlacing;
            App.GetLevel().Focus();
        }

        private void CancelPlacing(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                StopPlacing();
        }

        public void PlacingStairs(object sender, DataGridViewCellEventArgs e)
        {
            allowedLocation = true;
            int locX = e.ColumnIndex;
            int locY = e.RowIndex;

            Cell.ReloadAllInfo();
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
                        if (locY + reqBlocks > App.GetLevel().ColumnCount)
                            locY = App.GetLevel().RowCount - reqBlocks;
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
                        if (locX + reqBlocks > App.GetLevel().ColumnCount)
                            locX = App.GetLevel().ColumnCount - reqBlocks;
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
            allowedLocation = Cell.PlacementAllowed(x, y);
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
            Cell.Get(locX, locY).Style.BackColor = App.color[i + 2];

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
            if (e.Button == MouseButtons.Right)
            {
                StopPlacing();
                return;
            }
            else if (!allowedLocation)
            {
                MessageBox.Show("You can't place stairs here.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Remove remove handlers
            StopPlacing();

            // Only trigger when stairs should be placed
            if (stairsDirection == 0) return;
            stairsDirection = 0;

            // Set stairs location
            int next = 5; // Count down from 5 to 2
            for (int i = 0; i < 4; i++)
            {
                Cell.SetValue(next, newLocation[i, 1], newLocation[i, 0]);

                // Add top of stairs to next level
                if (i < 3 && App.activeGrid + 1 < App.GetLevelCount())
                    Cell.SetValue(6, newLocation[i, 1], newLocation[i, 0], App.activeGrid + 1);

                next--;
            }
        }

        /// <summary>
        /// Remove handlers for placing
        /// </summary>
        public void StopPlacing()
        {
            App.GetLevel().CellMouseEnter -= PlacingStairs;
            App.GetLevel().CellMouseDown -= ConfirmPlaceStairs;
            App.creator.KeyDown -= CancelPlacing;
            Cell.ReloadAllInfo();
            alreadyPlacing = false;
        }

        /// <summary>
        /// Attempt to remove stairs at selected location
        /// </summary>
        internal static void Remove(int newValue = 0)
        {
            var bottom = App.GetLevel().SelectedCells[0];

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

            int col = bottom.ColumnIndex;
            int row = bottom.RowIndex;

            // vars for spawn location check
            int left = col - 1;
            int right = col + 1;
            int above = row - 1;
            int below = row + 1;

            // Determine placement location & orientation
            if (above >= 0 && Cell.GetValue(col, above) == 3)
            {
                for (int offset = 0; offset < 4; offset++)
                {
                    Cell.SetValue(newValue, col, row - offset);
                    RemoveIndicator(col, row - offset);
                }
            }
            else if (below <= Config.Y_COUNT - 1 && Cell.GetValue(col, below) == 3)
            {
                for (int offset = 0; offset < 4; offset++)
                {
                    Cell.SetValue(newValue, col, row + offset);
                    RemoveIndicator(col, row + offset);
                }
            }
            else if (left >= 0 && Cell.GetValue(col, left) == 3)
            {
                for (int offset = 0; offset < 4; offset++)
                {
                    Cell.SetValue(newValue, col - offset, row);
                    RemoveIndicator(col - offset, row);
                }
            }
            else if (right <= Config.X_COUNT - 1 && Cell.GetValue(right, row) == 3)
            {
                for (int offset = 0; offset < 4; offset++)
                {
                    Cell.SetValue(newValue, col + offset, row);
                    RemoveIndicator(col + offset, row);
                }
            }

            Cell.ReloadAllInfo();
            if (App.activeGrid + 1 < App.LEVELS.Count)
                Cell.ReloadAllInfo(App.activeGrid + 1);
        }

        /// <summary>
        /// Remove stairs indicator blocks above stairs
        /// </summary>
        internal static void RemoveIndicator(int x, int y)
        {
            if (App.activeGrid + 1 < App.LEVELS.Count) // not max level
            {
                if (Cell.GetValue(x, y, App.activeGrid + 1) == 6) // above is stairs indicator
                    Cell.SetValue(0, x, y, App.activeGrid + 1);
            }

        }
    }
}
