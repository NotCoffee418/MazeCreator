using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeCreator
{
    class Tools
    {
        /// <summary>
        /// Adds an row or column in a specific location
        /// </summary>
        /// <param name="loc">1: Left - 2: Right - 3: Top - 4: Bottom</param>
        public static void AddLine(int loc)
        {
            if (loc == 1 || loc == 2)
            {
                // Change in config
                int newCount = Config.X_COUNT + 1;
                App.configForm.LoadConfig(String.Empty, 3, newCount.ToString());
                Config.X_COUNT = newCount;

                // Change in creator
                for (int lev = 0; lev < Config.LEVELS.Count; lev++)
                {
                    var c = new DataGridViewCheckBoxColumn();
                    c.ValueType = typeof(int);
                    c.Width = 15;
                    if (loc == 1) // left
                        Config.LEVELS[lev].Grid.Columns.Insert(0, c);
                    else // 2 - right
                        Config.LEVELS[lev].Grid.Columns.Add(c);
                    App.creator.ReloadColors(lev);
                }
            }
            else if (loc == 3 || loc == 4)
            {
                // Change in config
                int newCount = Config.Y_COUNT + 1;
                App.configForm.LoadConfig(String.Empty, 4, newCount.ToString());
                Config.Y_COUNT = newCount;

                // Change in creator
                for (int lev = 0; lev < Config.LEVELS.Count; lev++)
                {
                    if (loc == 3) // left
                    {
                        Config.LEVELS[lev].Grid.Rows.Insert(0);
                        Config.LEVELS[lev].Grid.Rows[0].Height = 15;
                    }
                    else // 4 - right
                    {
                        Config.LEVELS[lev].Grid.Rows.Add();
                        Config.LEVELS[lev].Grid.Rows[Config.LEVELS[lev].Grid.Rows.Count - 1].Height = 15;
                    }
                    App.creator.ReloadColors(lev);
                }
            }
        }

        /// <summary>
        /// Removes the selected row or column
        /// </summary>
        /// <param name="type">1: Row - 2: Column</param>
        public static void RemoveLine(int type)
        {
            var selected = Config.LEVELS[App.activeGrid].Grid.CurrentCell;
            if (type == 1) // row
            {
                // Select in grid
                int row = selected.RowIndex;
                Config.LEVELS[App.activeGrid].Grid.Rows[row].Selected = true;

                var result = MessageBox.Show("Are you sure you want to remove this row on all levels?",
                    "Remove row", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Change in config
                    int newCount = Config.Y_COUNT - 1;
                    App.configForm.LoadConfig(String.Empty, 4, newCount.ToString());
                    Config.Y_COUNT = newCount;

                    // Change in grid
                    for (int lev = 0; lev < Config.LEVELS.Count; lev++)
                        Config.LEVELS[lev].Grid.Rows.RemoveAt(row);
                }
            }
            else if (type == 2) // column
            {
                // Select in grid
                int col = selected.ColumnIndex;
                for (int r = 0; r < Config.LEVELS[App.activeGrid].Grid.RowCount; r++)
                    Config.LEVELS[App.activeGrid].Grid[col, r].Selected = true;

                var result = MessageBox.Show("Are you sure you want to remove this column on all levels?",
                    "Remove column", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Change in config
                    int newCount = Config.X_COUNT - 1;
                    App.configForm.LoadConfig(String.Empty, 3, newCount.ToString());
                    Config.X_COUNT = newCount;

                    // Change in grid
                    for (int lev = 0; lev < Config.LEVELS.Count; lev++)
                        Config.LEVELS[lev].Grid.Columns.RemoveAt(col);
                }
            }
        }

        public static void FillBorders()
        {
            for (int row = 0; row < Config.Y_COUNT; row++)
            {
                if (row == 0 || row == Config.Y_COUNT - 1)
                {
                    for (int col = 0; col < Config.X_COUNT; col++)
                        Config.LEVELS[App.activeGrid].Grid.Rows[row].Cells[col].Value = 1;
                }
                else
                {
                    Config.LEVELS[App.activeGrid].Grid.Rows[row].Cells[0].Value = 1;
                    Config.LEVELS[App.activeGrid].Grid.Rows[row].Cells[Config.X_COUNT - 1].Value = 1;
                }
            }
            App.creator.ReloadColors();
        }
        public static void FillMaze(bool empty = false)
        {
            for (int row = 0; row < Config.Y_COUNT; row++) // Loop all rows
            {
                for (int col = 0; col < Config.X_COUNT; col++)// Loop all columns 
                {
                    if (empty)
                        Config.LEVELS[App.activeGrid].Grid.Rows[row].Cells[col].Value = 0;
                    else
                        Config.LEVELS[App.activeGrid].Grid.Rows[row].Cells[col].Value = 1;
                }
            }
            App.creator.ReloadColors();
        }
    }
}
