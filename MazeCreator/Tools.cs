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
                for (int i = 0; i < Config.LEVELS.Count; i++)
                {
                    var c = new DataGridViewColumn();
                    c.Width = 20;
                    if (loc == 1) // left
                        Config.LEVELS[i].Grid.Columns.Insert(0, c);
                    else // 2 - right
                        Config.LEVELS[i].Grid.Columns.Add(c);
                    App.creator.ReloadColors(i);
                }
            }
            else if (loc == 3 || loc == 4)
            {
                // Change in config
                int newCount = Config.Y_COUNT + 1;
                App.configForm.LoadConfig(String.Empty, 4, newCount.ToString());
                Config.Y_COUNT = newCount;

                // Change in creator
                for (int i = 0; i < Config.LEVELS.Count; i++)
                {
                    if (loc == 3) // left
                        Config.LEVELS[i].Grid.Rows.Insert(0);
                    else // 2 - right
                        Config.LEVELS[i].Grid.Rows.Add();
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
            var selected = Config.LEVELS[App.activeGrid].Grid.CurrentCell;
            if (type == 1) // row
            {
                // Select in grid
                int i = selected.RowIndex;
                Config.LEVELS[App.activeGrid].Grid.Rows[i].Selected = true;

                var result = MessageBox.Show("Are you sure you want to remove this row on all levels?",
                    "Remove row", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Change in config
                    int newCount = Config.Y_COUNT - 1;
                    App.configForm.LoadConfig(String.Empty, 4, newCount.ToString());
                    Config.Y_COUNT = newCount;

                    // Change in grid
                    for (int j = 0; j < Config.LEVELS.Count; j++)
                        Config.LEVELS[j].Grid.Rows.RemoveAt(i);
                }
            }
            else if (type == 2) // column
            {
                // Select in grid
                int i = selected.ColumnIndex;
                for (int r = 0; r < Config.LEVELS[App.activeGrid].Grid.RowCount; r++)
                    Config.LEVELS[App.activeGrid].Grid[i, r].Selected = true;

                var result = MessageBox.Show("Are you sure you want to remove this column on all levels?",
                    "Remove column", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Change in config
                    int newCount = Config.X_COUNT - 1;
                    App.configForm.LoadConfig(String.Empty, 3, newCount.ToString());
                    Config.X_COUNT = newCount;

                    // Change in grid
                    for (int j = 0; j < Config.LEVELS.Count; j++)
                        Config.LEVELS[j].Grid.Columns.RemoveAt(i);
                }
            }
        }

        public static void FillBorders()
        {
            for (int i = 0; i < Config.Y_COUNT; i++)
            {
                if (i == 0 || i == Config.Y_COUNT - 1)
                {
                    for (int j = 0; j < Config.X_COUNT; j++)
                        Config.LEVELS[App.activeGrid].Grid.Rows[i].Cells[j].Value = 1;
                }
                else
                {
                    Config.LEVELS[App.activeGrid].Grid.Rows[i].Cells[0].Value = 1;
                    Config.LEVELS[App.activeGrid].Grid.Rows[i].Cells[Config.X_COUNT - 1].Value = 1;
                }
            }
            App.creator.ReloadColors();
        }
        public static void FillMaze(bool empty = false)
        {
            for (int i = 0; i < Config.Y_COUNT; i++) // Loop all rows
            {
                for (int j = 0; j < Config.X_COUNT; j++)// Loop all columns 
                {
                    if (empty)
                        Config.LEVELS[App.activeGrid].Grid.Rows[i].Cells[j].Value = 0;
                    else
                        Config.LEVELS[App.activeGrid].Grid.Rows[i].Cells[j].Value = 1;
                }
            }
            App.creator.ReloadColors();
        }
    }
}
