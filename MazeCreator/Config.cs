using System.Collections.Generic;

namespace MazeCreator
{
    class Config
    {
        // Levels
        public List<Level> LEVELS = new List<Level>();

        // Config
        public string[] MAZEDATA;
        public int GAMEOBJECT;
        public double SPACING;
        public int WALLHEIGHT;
        public int X_COUNT;
        public int Y_COUNT;
        public bool FLOOR;
        public bool ROOF;
        public double[] STARTCOORDS = new double[4]; // x,y,z,map
        public int LEVEL_COUNT;
    }
}
