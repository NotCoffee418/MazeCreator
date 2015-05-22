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
            App.GetLevel().CellMouseEnter += PlacingTrap;
            App.GetLevel().CellMouseDown += ConfirmPlaceTrap;
            App.GetLevel().KeyDown += CancelPlacing;
            App.GetLevel().Focus();
        }

        private void CancelPlacing(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                StopPlacing();
        }

        public void PlacingTrap(object sender, DataGridViewCellEventArgs e)
        {
            int x = e.ColumnIndex;
            int y = e.RowIndex;

            try
            {
                Cell.ReloadAllInfo();
                Cell.Get(x, y).Style.BackColor = App.color[(int)type];
            }
            catch (ArgumentOutOfRangeException)
            { /* Mouse moved outside of grid */  }
        }

        public void ConfirmPlaceTrap(object sender, DataGridViewCellMouseEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;

            if (e.Button == MouseButtons.Right)
            {
                StopPlacing(col, row);
                return;
            }
            else if (!isAllowedHere(col, row))
                MessageBox.Show("You can't place a trap here.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else // Set value
            {
                Cell.SetValue((int)type, col, row);
                if (App.activeGrid > 0) // Place indicator
                    Cell.SetValue(10, col, row, App.activeGrid - 1);
                StopPlacing(col, row);
            }
        }

        /// <summary>
        /// True if a trap can be placed here
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool isAllowedHere(int col, int row)
        {
            int value = Cell.GetValue(col, row);
            int below = 0;
            if (App.activeGrid > 0)
                below = Cell.GetValue(col, row, App.activeGrid - 1);

            // not allowed when stairs here
            if (value >= 2 && value <= 6 || below == 1)
                return false;

            // Is allowed
            return true;
        }

        private void StopPlacing(int x = -1, int y = -1)
        {
            App.GetLevel().CellMouseEnter -= PlacingTrap;
            App.GetLevel().CellMouseDown -= ConfirmPlaceTrap;
            App.GetLevel().KeyDown -= CancelPlacing;

            if (x == -1 || y == -1)
                Cell.ReloadAllInfo();
            else
                Cell.SetInfo(x, y);
        }
    }
}
