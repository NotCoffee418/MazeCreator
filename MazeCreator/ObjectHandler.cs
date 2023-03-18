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
            for (int lev = 0; lev <= App.GetLevelCount(); lev++)
            {
                for (int row = 0; row < Config.Y_COUNT; row++) // Loop all rows
                {
                    for (int col = 0; col < Config.X_COUNT; col++) // Loop all columns 
                    {
                        bool roof = (lev == App.GetLevelCount());
                        int value = 0;

                        if (!roof)
                            value = Cell.GetValue(col, row, lev);

                        double z = 0;
                        double[] box = new double[6];
                        // Floor, roof traps
                        if (NeedsFloorBlock(lev, col, row))
                        {
                            box[0] = Config.SPACING * col + Config.STARTCOORDS[0];                // x
                            box[1] = Config.SPACING * row + Config.STARTCOORDS[1];                // y
                            box[2] = Config.SPACING * (startZ + z) + Config.STARTCOORDS[2];     // z
                            box[3] = Config.STARTCOORDS[3];                                     // map
                            box[4] = 0;                                                         // orientation
                            if (value == (int)Trap.Type.ConcealedTrap)
                                box[5] = 745002;
                            else
                                box[5] = Config.GAMEOBJECT;                                         // game object
                            boxList.Add(box);
                            z++;
                        }

                        // Walls, stairs, secret passage
                        if (!roof) // don't run level checks on roof
                        {
                            if (value == 1 || value == (int)Trap.Type.SecretPassage) // wall
                            {
                                for (int k = 0; k < Config.WALLHEIGHT; k++)
                                {
                                    box = new double[6];
                                    box[0] = Config.SPACING * col + Config.STARTCOORDS[0];
                                    box[1] = Config.SPACING * row + Config.STARTCOORDS[1];
                                    box[2] = Config.SPACING * (startZ + z + k) + Config.STARTCOORDS[2];
                                    box[3] = Config.STARTCOORDS[3];
                                    box[4] = 0;
                                    if (value == (int)Trap.Type.SecretPassage)
                                        box[5] = 745002;
                                    else
                                        box[5] = Config.GAMEOBJECT;  
                                    boxList.Add(box);
                                    box = new double[6];
                                }
                            }

                            // Stairs
                            else if (value == 2) // bottom of stairs
                            {
                                double spawnZ = Config.SPACING * (startZ + z) + Config.STARTCOORDS[2];
                                box = GetStairsBox(lev, col, row, spawnZ);
                                boxList.Add(box);
                            }
                        }
                    }
                }

                // sets next startZ
                startZ += Config.WALLHEIGHT;
                if (Config.FLOOR && lev == 0 || lev > 0 && lev < App.GetLevelCount())
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
        private bool NeedsFloorBlock(int lev, int col, int row)
        {
            int current = 0;
            int below = 0;
            if (lev != 0)
                below = Cell.GetValue(col, row, lev - 1);
            if (lev != App.LEVELS.Count)
                current = Cell.GetValue(col, row, lev);

            // check for roof
            if (lev == App.GetLevelCount())
            {
                if (!Config.ROOF || (below >= 2 && below <= 5))
                    return false; // no Roof
                else return true;
            }
            else  // Not roof
            {
                // Check for trap
                if (current == (int)Trap.Type.HoleTrap)
                    return false;
                else if (current == (int)Trap.Type.ConcealedTrap)
                    return true; // Needs (different) floor block
                // Check if floor is configured at bottom level
                else if (lev == 0)
                {
                    if (Config.FLOOR)
                        return true;
                    else return false;
                }
                // Check of block should be removed
                else if (below >= 2 && below <= 5)
                    return false;
                else return true; // floor below upper level
            }
        }

        /// <summary>
        /// Returns stairs object box
        /// </summary>
        /// <param name="lev">level</param>
        /// <param name="col">bottom of stairs X</param>
        /// <param name="row">bottom of stairs Y</param>
        /// <returns></returns>
        private double[] GetStairsBox(int lev, int col, int row, double spawnZ)
        {
            // Determine object placement location relative to the given coords
            double placementX = col; // It also spawns a block next to the bottom location
            double placementY = row;
            double quarter = 6.28318 / 4; // max orientation = pi * 2
            double orientation = 0.0; // 0 - Pi

            // vars for spawn location check
            int left = col - 1;
            int right = col + 1;
            int above = row - 1;
            int below = row + 1;

            // Determine placement location & orientation
            if (left >= 0 && Cell.GetValue(left, row, lev) == 3)
            {
                placementX = left - 0.25;
                orientation = quarter * 3;
            }
            else if (right <= Config.Y_COUNT - 1 && Cell.GetValue(right, row, lev) == 3)
            {
                placementX = right + 0.25;
                orientation = quarter;
            }
            else if (above >= 0 && Cell.GetValue(col, above, lev) == 3)
            {
                placementY = above - 0.25;
                orientation = 0;
            }
            else if (below <= Config.X_COUNT - 1 && Cell.GetValue(col, below, lev) == 3)
            {
                placementY = below + 0.25;
                orientation = quarter * 2;
            }

            // Create the box
            double[] box = new double[6];
            box[0] = Config.SPACING * placementX + Config.STARTCOORDS[0];
            box[1] = Config.SPACING * placementY + Config.STARTCOORDS[1];
            box[2] = spawnZ + (Config.WALLHEIGHT / 1.9); // Dranei can fit
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
            for (int grid = 0; grid < App.GetLevelCount(); grid++) // Loop all levels
            {
                App.GetLevel(grid).EndEdit();
                for (int row = 0; row < Config.Y_COUNT; row++) // Loop all rows
                {
                    for (int col = 0; col < Config.X_COUNT; col++)// Loop all columns 
                    {
                        content += Cell.GetValue(col, row, grid);
                        if (col < Config.X_COUNT - 1) // not last in column
                            content += ',';
                    }
                    if (row < (Config.Y_COUNT * App.GetLevelCount()) - 1)
                        content += "\n";
                }
            }
            return content;
        }
    }
}
