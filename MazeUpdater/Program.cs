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
            if (args.Length > 1)
                path = args[0];

            while (Process.GetProcessesByName("MazeCreator").Length != 0)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Waiting for Maze Creator to close...");
            }
            
            StartUpdate();
        }

        private static void StartUpdate()
        {
            Console.WriteLine("Downloading latest version of Maze Creator...");
            WebClient c = new WebClient();
            c.DownloadFile("https://github.com/RStijn/MazeCreator/blob/master/MazeCreator/bin/Release/MazeCreator.exe?raw=true", path);

            Console.WriteLine("Downloaded to {0}", path);
            Process.Start(path);
        }
    }
}
