using System;
using System.Windows.Forms;
using System.Drawing;

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
        public static Color[] color = new Color[10]; // All color values
        public static String[] tooltip = new String[10]; // All tooltip values

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Define colors per value
            InitColors();
            InitTooltips();

            // Load classes
            configForm = new ConfigForm();
            creator = new Creator();
            fileHandler = new FileHandler();
            objectHandler = new ObjectHandler();
            
            Application.Run(configForm);
        }

        private static void InitColors()
        {
            color[0] = Color.Lime;
            color[1] = Color.Red;
            color[2] = Color.MediumAquamarine; // bottom
            color[3] = Color.Aquamarine; // Placementblock
            color[4] = Color.Aquamarine; // middle high
            color[5] = Color.Aqua; // top
            color[6] = Color.Green;
            color[7] = Color.White;
            color[8] = Color.Yellow;
            color[9] = Color.Orange;
        }

        private static void InitTooltips()
        {
            tooltip[0] = "Empty";
            tooltip[1] = "Wall";
            tooltip[2] = "Bottom of stairs";
            tooltip[3] = "Stairs"; // Object placed here
            tooltip[4] = "Stairs";
            tooltip[5] = "Top of stairs";
            tooltip[6] = "Stairs below"; // Can't be edited
            tooltip[7] = "Floor trap";
            tooltip[8] = "Concealed floor trap";
            tooltip[9] = "Secret passage";
        }
    }
}
