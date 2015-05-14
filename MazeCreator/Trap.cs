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
        public Trap()
        {
            // Let user select start of maze
            Config.LEVELS[App.activeGrid].Grid.CellMouseEnter += PlacingTrap;
            Config.LEVELS[App.activeGrid].Grid.CellMouseDown += ConfirmPlaceTrap;
        }

        public void PlacingTrap(object sender, DataGridViewCellEventArgs e)
        {
            int x = e.ColumnIndex;
            int y = e.RowIndex;

            try
            {
                App.creator.ReloadColors(App.activeGrid);
                Config.LEVELS[App.activeGrid].Grid.Rows[y].Cells[x].Style.BackColor = App.color[6];
            }
            catch (ArgumentOutOfRangeException) // Mouse moved outside of grid 
            { }
        }

        public void ConfirmPlaceTrap(object sender, DataGridViewCellMouseEventArgs e)
        {
            int x = e.ColumnIndex;
            int y = e.RowIndex;

            if (!isAllowedHere(x, y))
            {
                MessageBox.Show("You can't place a trap here.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Set value
            Config.LEVELS[App.activeGrid].Grid.Rows[y].Cells[x].Value = 6;

            // Remove handlers
            Config.LEVELS[App.activeGrid].Grid.CellMouseEnter -= PlacingTrap;
            Config.LEVELS[App.activeGrid].Grid.CellMouseDown -= ConfirmPlaceTrap;
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

            // not allowed when stairs here
            if (value >= 2 && value <= 5)
                return false;

            // Is allowed
            return true;
        }
    }
}
