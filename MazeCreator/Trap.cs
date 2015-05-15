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
                App.creator.ReloadColors();
                App.GetLevel().Rows[y].Cells[x].Style.BackColor = App.color[(int)type];
            }
            catch (ArgumentOutOfRangeException)
            { /* Mouse moved outside of grid */  }
        }

        public void ConfirmPlaceTrap(object sender, DataGridViewCellMouseEventArgs e)
        {
            int x = e.ColumnIndex;
            int y = e.RowIndex;

            if (e.Button == MouseButtons.Right)
            {
                StopPlacing(x, y);
                return;
            }
            else if (!isAllowedHere(x, y))
                MessageBox.Show("You can't place a trap here.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else // Set value
            {
                App.GetLevel().Rows[y].Cells[x].Value = (int)type;
                StopPlacing(x, y);
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
            int value = (int)App.GetLevel().Rows[y].Cells[x].Value;
            int below = 0;
            if (App.activeGrid > 0)
                below = (int)App.GetLevel(App.activeGrid - 1).Rows[y].Cells[x].Value;

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
                App.creator.ReloadColors();
            else
                App.creator.SetCellInfo(x, y);
        }
    }
}
