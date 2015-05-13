using System;
using System.Windows.Forms;

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
        public static int activeGrid = 0;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Load classes
            configForm = new ConfigForm();
            creator = new Creator();
            fileHandler = new FileHandler();
            objectHandler = new ObjectHandler();

            // Init Stairs colors
            Stairs.InitStairs();

            Application.Run(configForm);
        }
    }
}
