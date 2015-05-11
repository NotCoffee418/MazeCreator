using System;
using System.Linq;
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
            for (int lev = 0; lev < App.config.LEVELS.Count; lev++)
            {
                for (int y = 0; y < App.config.Y_COUNT; y++) // Loop all rows
                {
                    for (int x = 0; x < App.config.X_COUNT; x++) // Loop all columns 
                    {
                        double z = 0;
                        double[] box = new double[6]; // x,y,z,map
                        // Floor
                        if (App.config.FLOOR && lev == 0 || lev > 0 && NeedsFloorBlock(lev, x, y))
                        {
                            box[0] = App.config.SPACING * x + App.config.STARTCOORDS[0];              // x
                            box[1] = App.config.SPACING * y + App.config.STARTCOORDS[1];              // y
                            box[2] = App.config.SPACING * (startZ + z) + App.config.STARTCOORDS[2];   // z
                            box[3] = App.config.STARTCOORDS[3];                            // map
                            box[4] = 0;                                          // orientation
                            box[5] = App.config.GAMEOBJECT;                                             // game object
                            boxList.Add(box);
                            z++;
                        }

                        // Walls
                        if (App.config.LEVELS[lev].Grid.Rows[y].Cells[x].Value != null && 
                            (int)App.config.LEVELS[lev].Grid.Rows[y].Cells[x].Value == 1)
                        {
                            // Create wall
                            for (int k = 0; k < App.config.WALLHEIGHT; k++)
                            {
                                box[0] = App.config.SPACING * x + App.config.STARTCOORDS[0];
                                box[1] = App.config.SPACING * y + App.config.STARTCOORDS[1];
                                box[2] = App.config.SPACING * (startZ + z + k) + App.config.STARTCOORDS[2];
                                box[3] = App.config.STARTCOORDS[3];
                                boxList.Add(box);
                                box = new double[4];
                            }
                        }
                        z += App.config.WALLHEIGHT;

                        // Roof
                        if (App.config.ROOF && lev == App.config.LEVELS.Count - 1) // Last level only
                        {
                            box[0] = App.config.SPACING * x + App.config.STARTCOORDS[0];
                            box[1] = App.config.SPACING * y + App.config.STARTCOORDS[1];
                            box[2] = App.config.SPACING * (startZ + z) + App.config.STARTCOORDS[2];
                            box[3] = App.config.STARTCOORDS[3];
                            boxList.Add(box);
                            box = new double[4];
                        }
                    }
                }

                //setss next startZ
                startZ += App.config.WALLHEIGHT;
                if (App.config.FLOOR && lev == 0 || lev > 0 && lev < App.config.LEVELS.Count - 1)
                    startZ++;
            }
            return boxList;
        }

        private bool NeedsFloorBlock(int lev, int x, int y)
        {
            // Return false when stairs below
            return true;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves current maze data to MAZEDATA
        /// </summary>
        public void StoreMazeData()
        {
            string[] d = GetMaze().Split('\n');
            App.config.MAZEDATA = new string[d.Count() + 1];
            for (int i = 0; i < d.Count(); i++)
                App.config.MAZEDATA[i + 1] = d[i];
        }

        /// <summary>
        /// Returns maze as string without config
        /// </summary>
        /// <returns></returns>
        public string GetMaze()
        {
            // config
            string content = String.Empty;
            for (int grid = 0; grid < App.config.LEVELS.Count; grid++) // Loop all levels
            {
                App.config.LEVELS[grid].Grid.EndEdit();
                for (int i = 0; i < App.config.Y_COUNT; i++) // Loop all rows
                {
                    for (int j = 0; j < App.config.X_COUNT; j++)// Loop all columns 
                        if (App.config.LEVELS[grid].Grid.Rows[i].Cells[j].Value == null)
                            content += 0;
                        else
                            content += (int)App.config.LEVELS[grid].Grid.Rows[i].Cells[j].Value;
                    if (i < (App.config.Y_COUNT * App.config.LEVELS.Count) - 1) content += "\n";
                }
            }
            return content;
        }
    }
}
