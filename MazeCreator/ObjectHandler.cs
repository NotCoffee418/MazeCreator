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
            for (int lev = 0; lev < Config.LEVELS.Count; lev++)
            {
                for (int y = 0; y < Config.Y_COUNT; y++) // Loop all rows
                {
                    for (int x = 0; x < Config.X_COUNT; x++) // Loop all columns 
                    {
                        double z = 0;
                        double[] box = new double[6]; // x,y,z,map
                        // Floor
                        if (Config.FLOOR && lev == 0 || lev > 0 && NeedsFloorBlock(lev, x, y))
                        {
                            box[0] = Config.SPACING * x + Config.STARTCOORDS[0];              // x
                            box[1] = Config.SPACING * y + Config.STARTCOORDS[1];              // y
                            box[2] = Config.SPACING * (startZ + z) + Config.STARTCOORDS[2];   // z
                            box[3] = Config.STARTCOORDS[3];                            // map
                            box[4] = 0;                                          // orientation
                            box[5] = Config.GAMEOBJECT;                                             // game object
                            boxList.Add(box);
                            z++;
                        }

                        // Walls
                        if (Config.LEVELS[lev].Grid.Rows[y].Cells[x].Value != null && 
                            (int)Config.LEVELS[lev].Grid.Rows[y].Cells[x].Value == 1)
                        {
                            // Create wall
                            for (int k = 0; k < Config.WALLHEIGHT; k++)
                            {
                                box[0] = Config.SPACING * x + Config.STARTCOORDS[0];
                                box[1] = Config.SPACING * y + Config.STARTCOORDS[1];
                                box[2] = Config.SPACING * (startZ + z + k) + Config.STARTCOORDS[2];
                                box[3] = Config.STARTCOORDS[3];
                                boxList.Add(box);
                                box = new double[4];
                            }
                        }
                        z += Config.WALLHEIGHT;

                        // Roof
                        if (Config.ROOF && lev == Config.LEVELS.Count - 1) // Last level only
                        {
                            box[0] = Config.SPACING * x + Config.STARTCOORDS[0];
                            box[1] = Config.SPACING * y + Config.STARTCOORDS[1];
                            box[2] = Config.SPACING * (startZ + z) + Config.STARTCOORDS[2];
                            box[3] = Config.STARTCOORDS[3];
                            boxList.Add(box);
                            box = new double[4];
                        }
                    }
                }

                //setss next startZ
                startZ += Config.WALLHEIGHT;
                if (Config.FLOOR && lev == 0 || lev > 0 && lev < Config.LEVELS.Count - 1)
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
                        if (Config.LEVELS[grid].Grid.Rows[i].Cells[j].Value == null)
                            content += 0;
                        else
                            content += (int)Config.LEVELS[grid].Grid.Rows[i].Cells[j].Value;
                    if (i < (Config.Y_COUNT * Config.LEVELS.Count) - 1) content += "\n";
                }
            }
            return content;
        }
    }
}
