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
                int newCount = App.config.X_COUNT + 1;
                App.configForm.LoadConfig(String.Empty, 3, newCount.ToString());
                App.config.X_COUNT = newCount;

                // Change in creator
                for (int i = 0; i < App.config.LEVELS.Count; i++)
                {
                    var c = new DataGridViewColumn();
                    c.Width = 20;
                    if (loc == 1) // left
                        App.config.LEVELS[i].Grid.Columns.Insert(0, c);
                    else // 2 - right
                        App.config.LEVELS[i].Grid.Columns.Add(c);
                    App.creator.ReloadColors(i);
                }
            }
            else if (loc == 3 || loc == 4)
            {
                // Change in config
                int newCount = App.config.Y_COUNT + 1;
                App.configForm.LoadConfig(String.Empty, 4, newCount.ToString());
                App.config.Y_COUNT = newCount;

                // Change in creator
                for (int i = 0; i < App.config.LEVELS.Count; i++)
                {
                    if (loc == 3) // left
                        App.config.LEVELS[i].Grid.Rows.Insert(0);
                    else // 2 - right
                        App.config.LEVELS[i].Grid.Rows.Add();
                    App.creator.ReloadColors(i);
                }
            }
        }

        /// <summary>
        /// Removes the selected row or column
        /// </summary>
        /// <param name="type">1: Row - 2: Column</param>
        public static void RemoveLine(int type)
        {
            var selected = App.config.LEVELS[App.activeGrid].Grid.CurrentCell;
            if (type == 1) // row
            {
                // Select in grid
                int i = selected.RowIndex;
                App.config.LEVELS[App.activeGrid].Grid.Rows[i].Selected = true;

                var result = MessageBox.Show("Are you sure you want to remove this row on all levels?",
                    "Remove row", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Change in config
                    int newCount = App.config.Y_COUNT - 1;
                    App.configForm.LoadConfig(String.Empty, 4, newCount.ToString());
                    App.config.Y_COUNT = newCount;

                    // Change in grid
                    for (int j = 0; j < App.config.LEVELS.Count; j++)
                        App.config.LEVELS[j].Grid.Rows.RemoveAt(i);
                }
            }
            else if (type == 2) // column
            {
                // Select in grid
                int i = selected.ColumnIndex;
                for (int r = 0; r < App.config.LEVELS[App.activeGrid].Grid.RowCount; r++)
                    App.config.LEVELS[App.activeGrid].Grid[i, r].Selected = true;

                var result = MessageBox.Show("Are you sure you want to remove this column on all levels?",
                    "Remove column", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Change in config
                    int newCount = App.config.X_COUNT - 1;
                    App.configForm.LoadConfig(String.Empty, 3, newCount.ToString());
                    App.config.X_COUNT = newCount;

                    // Change in grid
                    for (int j = 0; j < App.config.LEVELS.Count; j++)
                        App.config.LEVELS[j].Grid.Columns.RemoveAt(i);
                }
            }
        }

        public static void FillBorders()
        {
            for (int i = 0; i < App.config.Y_COUNT; i++)
            {
                if (i == 0 || i == App.config.Y_COUNT - 1)
                {
                    for (int j = 0; j < App.config.X_COUNT; j++)
                        App.config.LEVELS[App.activeGrid].Grid.Rows[i].Cells[j].Value = 1;
                }
                else
                {
                    App.config.LEVELS[App.activeGrid].Grid.Rows[i].Cells[0].Value = 1;
                    App.config.LEVELS[App.activeGrid].Grid.Rows[i].Cells[App.config.X_COUNT - 1].Value = 1;
                }
            }
            App.creator.ReloadColors(App.activeGrid);
        }
        public static void FillMaze(bool empty = false)
        {
            for (int i = 0; i < App.config.Y_COUNT; i++) // Loop all rows
            {
                for (int j = 0; j < App.config.X_COUNT; j++)// Loop all columns 
                {
                    if (empty)
                        App.config.LEVELS[App.activeGrid].Grid.Rows[i].Cells[j].Value = 0;
                    else
                        App.config.LEVELS[App.activeGrid].Grid.Rows[i].Cells[j].Value = 1;
                }
            }
            App.creator.ReloadColors(App.activeGrid);
        }
    }
}
