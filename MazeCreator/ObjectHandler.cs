using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace MazeCreator
{
    class ObjectHandler
    {
        public List<double[]> GenerateMazeObjects()
        {
            // Generate object locations
            List<double[]> boxList = new List<double[]>();
            int startZ = 0;
            for (int lev = 0; lev <= Config.LEVELS.Count; lev++)
            {
                for (int y = 0; y < Config.Y_COUNT; y++) // Loop all rows
                {
                    for (int x = 0; x < Config.X_COUNT; x++) // Loop all columns 
                    {
                        double z = 0;
                        double[] box = new double[6];
                        // Floor, roof traps
                        if (Config.FLOOR && lev == 0 || NeedsFloorBlock(lev, x, y))
                        {
                            box[0] = Config.SPACING * x + Config.STARTCOORDS[0];                // x
                            box[1] = Config.SPACING * y + Config.STARTCOORDS[1];                // y
                            box[2] = Config.SPACING * (startZ + z) + Config.STARTCOORDS[2];     // z
                            box[3] = Config.STARTCOORDS[3];                                     // map
                            box[4] = 0;                                                         // orientation
                            box[5] = Config.GAMEOBJECT;                                         // game object
                            boxList.Add(box);
                            z++;
                        }

                        // Walls, stairs, secret passage
                        if (lev != Config.LEVELS.Count) // only run floor check on roof
                        {
                            int value = (int)Config.LEVELS[lev].Grid.Rows[y].Cells[x].Value;

                            if (value == 1) // wall
                            {
                                for (int k = 0; k < Config.WALLHEIGHT; k++)
                                {
                                    box = new double[6];
                                    box[0] = Config.SPACING * x + Config.STARTCOORDS[0];
                                    box[1] = Config.SPACING * y + Config.STARTCOORDS[1];
                                    box[2] = Config.SPACING * (startZ + z + k) + Config.STARTCOORDS[2];
                                    box[3] = Config.STARTCOORDS[3];
                                    box[4] = 0;
                                    box[5] = Config.GAMEOBJECT;
                                    boxList.Add(box);
                                    box = new double[6];
                                }
                            }

                            // Stairs
                            else if (value == 2) // bottom of stairs
                            {
                                double spawnZ = Config.SPACING * (startZ + z) + Config.STARTCOORDS[2];
                                box = GetStairsBox(lev, x, y, spawnZ);
                                boxList.Add(box);
                            }
                        }
                    }
                }

                // sets next startZ
                startZ += Config.WALLHEIGHT;
                if (Config.FLOOR && lev == 0 || lev > 0 && lev < Config.LEVELS.Count)
                    startZ++;
            }
            return boxList;
        }

        /// <summary>
        /// Disables floor block when there are stairs below & allows roof
        /// Disables on floor trap
        /// </summary>
        /// <param name="lev">Current level</param>
        /// <param name="x">Column</param>
        /// <param name="y">Row</param>
        /// <returns>true if there's no stairs below</returns>
        private bool NeedsFloorBlock(int lev, int x, int y)
        {
            if ((int)Config.LEVELS[lev].Grid.Rows[y].Cells[x].Value == 6)
                return false;// Floor trap (hole)
            else if (lev == 0) 
                return true; // no stairs below
            else if (!Config.ROOF && lev == Config.LEVELS.Count)
                return false; // Roof

            // Block below 
            int below = (int)Config.LEVELS[lev - 1].Grid.Rows[y].Cells[x].Value;

            // Check of block should be removed
            if (below == 3 || below == 4 || below == 5)
                return false;
            else return true;
        }

        /// <summary>
        /// Returns stairs object box
        /// </summary>
        /// <param name="lev">level</param>
        /// <param name="x">bottom of stairs X</param>
        /// <param name="y">bottom of stairs Y</param>
        /// <returns></returns>
        private double[] GetStairsBox(int lev, int x, int y, double spawnZ)
        {
            // Determine object placement location relative to the given coords
            var grid = Config.LEVELS[lev].Grid; // clearer to work with
            double placementX = x; // It also spawns a block next to the bottom location
            double placementY = y;
            double quarter = 6.28318 / 4; // max orientation = pi * 2
            double orientation = 0.0; // 0 - Pi

            // vars for spawn location check
            int left = x - 1;
            int right = x + 1;
            int above = y - 1;
            int below = y + 1;

            // Determine placement location & orientation
            if (left >= 0 && (int)grid.Rows[y].Cells[left].Value == 3)
            {
                placementX = left - 0.25;
                orientation = quarter * 3;
            }
            else if (right <= grid.Rows.Count - 1 && (int)grid.Rows[y].Cells[right].Value == 3)
            {
                placementX = right + 0.25;
                orientation = quarter;
            }
            else if (above >= 0 && (int)grid.Rows[above].Cells[x].Value == 3)
            {
                placementY = above - 0.25;
                orientation = 0;
            }
            else if (below <= grid.Columns.Count - 1 && (int)grid.Rows[below].Cells[x].Value == 3)
            {
                placementY = below + 0.25;
                orientation = quarter * 2;
            }

            // Create the box
            double[] box = new double[6];
            box[0] = Config.SPACING * placementX + Config.STARTCOORDS[0];
            box[1] = Config.SPACING * placementY + Config.STARTCOORDS[1];
            box[2] = spawnZ + (Config.WALLHEIGHT / 1.9); // Makes the stairs walkable without jumping
            box[3] = Config.STARTCOORDS[3];
            box[4] = orientation;
            box[5] = 745001; // Stairs object
            return box;
        }

        /// <summary>
        /// Saves current maze data to MAZEDATA
        /// </summary>
        public void StoreMazeData()
        {
            string[] d = GetMaze().Split('\n');
            Config.MAZEDATA = new string[d.Count() + 1];
            for (int i = 0; i < d.Count(); i++)
                Config.MAZEDATA[i + 1] = d[i];
        }

        /// <summary>
        /// Returns maze as string without config
        /// </summary>
        /// <returns></returns>
        public string GetMaze()
        {
            // config
            string content = String.Empty;
            for (int grid = 0; grid < Config.LEVELS.Count; grid++) // Loop all levels
            {
                Config.LEVELS[grid].Grid.EndEdit();
                for (int i = 0; i < Config.Y_COUNT; i++) // Loop all rows
                {
                    for (int j = 0; j < Config.X_COUNT; j++)// Loop all columns 
                            content += (int)Config.LEVELS[grid].Grid.Rows[i].Cells[j].Value;
                    if (i < (Config.Y_COUNT * Config.LEVELS.Count) - 1) content += "\n";
                }
            }
            return content;
        }
    }
}
