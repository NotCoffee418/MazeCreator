using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeCreator
{
    class Trap
    {
        public enum Type
        {
            HoleTrap = 7,
            ConcealedTrap = 8,
            SecretPassage = 9,
        }

        Type type;

        public Trap(Type t)
        {
            type = t;
            Config.LEVELS[App.activeGrid].Grid.CellMouseEnter += PlacingTrap;
            Config.LEVELS[App.activeGrid].Grid.CellMouseDown += ConfirmPlaceTrap;
        }

        public void PlacingTrap(object sender, DataGridViewCellEventArgs e)
        {
            int x = e.ColumnIndex;
            int y = e.RowIndex;

            try
            {
                App.creator.ReloadColors();
                Config.LEVELS[App.activeGrid].Grid.Rows[y].Cells[x].Style.BackColor = App.color[(int)type];
            }
            catch (ArgumentOutOfRangeException)
            { /* Mouse moved outside of grid */  }
        }

        public void ConfirmPlaceTrap(object sender, DataGridViewCellMouseEventArgs e)
        {
            int x = e.ColumnIndex;
            int y = e.RowIndex;

            if (!isAllowedHere(x, y))
                MessageBox.Show("You can't place a trap here.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else // Set value
            {
                Config.LEVELS[App.activeGrid].Grid.Rows[y].Cells[x].Value = (int)type;

                // Remove handlers
                Config.LEVELS[App.activeGrid].Grid.CellMouseEnter -= PlacingTrap;
                Config.LEVELS[App.activeGrid].Grid.CellMouseDown -= ConfirmPlaceTrap;
            }

        }

        /// <summary>
        /// True if a trap can be placed here
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool isAllowedHere(int x, int y)
        {
            int value = (int)Config.LEVELS[App.activeGrid].Grid.Rows[y].Cells[x].Value;
            int below = 0;
            if (App.activeGrid > 0)
                below = (int)Config.LEVELS[App.activeGrid - 1].Grid.Rows[y].Cells[x].Value;

            // not allowed when stairs here
            if (value >= 2 && value <= 6 || below == 1)
                return false;

            // Is allowed
            return true;
        }
    }
}
