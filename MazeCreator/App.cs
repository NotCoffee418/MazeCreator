using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace MazeCreator
{
    static class App
    {
        // Public static classes
        public static ConfigForm configForm;
        public static Creator creator;
        public static FileHandler fileHandler;
        public static ObjectHandler objectHandler;

        // Global variables
        public static int activeGrid = 0;   // Grid currently being viewed
        public static Color[] color = new Color[11]; // All color values
        public static String[] tooltip = new String[11]; // All tooltip values
        public static bool[] readOnly = new bool[11]; // All readonly cell types

        // Levels
        public static List<DataGridView> LEVELS = new List<DataGridView>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InitCellInfo();

            // Load classes
            configForm = new ConfigForm();
            creator = new Creator();
            fileHandler = new FileHandler();
            objectHandler = new ObjectHandler();
            
            Application.Run(configForm);
        }

        private static void InitCellInfo()
        {
            // Set all readonly to false by default
            for (int i = 0; i < readOnly.Length; i++)
                readOnly[i] = false;

            color[0] = Color.Lime;
            tooltip[0] = "Empty";

            color[1] = Color.Red;
            tooltip[1] = "Wall";

            color[2] = Color.MediumAquamarine; // bottom
            tooltip[2] = "Bottom of stairs";
            readOnly[2] = true;

            color[3] = Color.Aquamarine; // Object placed here
            tooltip[3] = "Stairs";
            readOnly[3] = true;

            color[4] = Color.Aquamarine; // middle high
            tooltip[4] = "Stairs";
            readOnly[4] = true;

            color[5] = Color.Aqua; // top
            tooltip[5] = "Top of stairs";
            readOnly[5] = true;

            // Indicator of stairs on level below
            color[6] = Color.Green;
            tooltip[6] = "Stairs below";
            readOnly[6] = true;

            // No floor block
            color[7] = Color.White;
            tooltip[7] = "Floor trap";

            // Visible floor block you can fall through
            color[8] = Color.Yellow;
            tooltip[8] = "Concealed floor trap";

            // Visible walls with no collision you can walk through
            color[9] = Color.Orange;
            tooltip[9] = "Secret passage";

            // Indicator that there's a fall trap above
            color[10] = Color.LightGoldenrodYellow;
            tooltip[10] = "Trap Above";
        }

        public static DataGridView GetLevel(int lev = -1)
        {
            if (lev == -1)
                lev = activeGrid; 
            return LEVELS[lev];
        }

        public static int GetLevelCount()
        {
            return LEVELS.Count;
        }
    }
}
