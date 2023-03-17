using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeCreator
{
    class Cell
    {
        /// <summary>
        /// Returns the DataGridViewCell
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="grid">optional using activeGrid</param>
        /// <returns></returns>
        public static DataGridViewCell Get(int col, int row, int grid = -1)
        {
            if (grid == -1) grid = App.activeGrid;
            return App.GetLevel(grid).Rows[row].Cells[col];
        }

        /// <summary>
        /// Returns the value of a cell
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="grid">optional using activeGrid</param>
        /// <returns></returns>
        public static int GetValue(int col, int row, int grid = -1)
        {
            if (grid == -1) grid = App.activeGrid;
            return (int)Get(col, row, grid).Value;
        }

        /// <summary>
        /// Sets the value of a cell & sets info
        /// </summary>
        /// <param name="value"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="grid">optional using activeGrid</param>
        public static void SetValue(int value, int col, int row, int grid = -1)
        {
            if (grid == -1) grid = App.activeGrid;
            Get(col, row, grid).Value = value;
            SetInfo(col, row, grid);
        }

        /// <summary>
        /// Sets the color, tooltip & readonly info of a cell
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="grid"></param>
        public static void SetInfo(int col, int row, int grid = -1)
        {
            if (grid == -1) grid = App.activeGrid;

            try
            {
                // Get cell value
                DataGridViewCell cell = Get(col, row, grid);
                if (cell != null && cell.Value != null)
                {
                    int value = (int)cell.Value;

                    // Set info
                    cell.Style.BackColor = App.color[value];
                    cell.ToolTipText = App.tooltip[value];
                    cell.ReadOnly = App.readOnly[value];
                }
            }
            catch
            { // Occurs when editing too fast and changing >1 values
                Get(col, row, grid).Value = 0;
            }
        }


        /// <summary>
        /// Reloads info of all cells on a grid
        /// </summary>
        /// <param name="grid"></param>
        public static void ReloadAllInfo(int grid = -1)
        {
            if (grid == -1) grid = App.activeGrid;

            // handle cell selection
            if (App.GetLevel(grid).SelectedCells.Count == 0)
                App.GetLevel(grid).Rows[0].Cells[0].Selected = true;
            var sel = App.GetLevel(grid).SelectedCells[0];
            App.GetLevel(grid).ClearSelection();

            // Reload colors
            for (int row = 0; row < Config.Y_COUNT; row++) // Loop all rows
                for (int col = 0; col < Config.X_COUNT; col++) // Loop all columns
                    SetInfo(col, row, grid);

            // Select original cell
            App.GetLevel(grid).Rows[sel.RowIndex].Cells[sel.ColumnIndex].Selected = true;
        }

        public static bool PlacementAllowed(int col, int row, int grid = -1)
        {
            if (grid == -1) grid = App.activeGrid;
            return !Get(col, row, grid).ReadOnly;
        }
    }
}
