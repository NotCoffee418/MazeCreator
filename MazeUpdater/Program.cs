using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Threading;

namespace MazeUpdater
{
    class Program
    {
        static string path = String.Empty;

        static void Main(string[] args)
        {
            Console.WriteLine("- Maze Creator Updater -");
            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\MazeCreator.exe";
            if (args.Length > 0)
                path = args[0];

            while (Process.GetProcessesByName("MazeCreator").Length != 0)
            {
                Thread.Sleep(2000);
                Console.WriteLine("Waiting for Maze Creator to close...");
            }
            
            StartUpdate();
            Thread.Sleep(1000);
            Environment.Exit(0);
        }

        private static void StartUpdate()
        {
            Console.WriteLine("Downloading latest version of Maze Creator...");
            WebClient c = new WebClient();
            c.DownloadFile("https://github.com/RStijn/MazeCreator/blob/master/MazeCreator/bin/Release/MazeCreator.exe?raw=true", path);

            Console.WriteLine("Downloaded to {0}", path);
            Console.WriteLine("Opening Maze Creator...");
            Process.Start(path);
        }
    }
}
