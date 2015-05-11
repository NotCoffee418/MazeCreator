using System;
using System.Windows.Forms;

namespace MazeCreator
{
    static class App
    {
        // Public static classes
        public static ConfigForm configForm;
        public static Config config;
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

            configForm = new ConfigForm();
            config = new Config();
            creator = new Creator();
            fileHandler = new FileHandler();
            objectHandler = new ObjectHandler();
            Application.Run(configForm);
        }
    }
}
