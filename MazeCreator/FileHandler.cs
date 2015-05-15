using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace MazeCreator
{
    class FileHandler
    {
        private SaveFileDialog ExportSqlDialog = new SaveFileDialog();
        private OpenFileDialog openFileDialog1 = new OpenFileDialog();
        private SaveFileDialog saveToFileDialog = new SaveFileDialog();

        public FileHandler()
        {
            // ExportSqlDialog
            ExportSqlDialog.DefaultExt = "sql";
            ExportSqlDialog.FileName = "Maze.sql";
            ExportSqlDialog.Filter = "SQL File|*.sql";

            // openFileDialog1
            openFileDialog1.DefaultExt = "maze";
            openFileDialog1.FileName = "MyMaze.maze";
            openFileDialog1.Filter = "Maze File|*.maze";

            // saveToFileDialog
            saveToFileDialog.DefaultExt = "sql";
            saveToFileDialog.FileName = "MyMaze";
            saveToFileDialog.Filter = "Maze File|*.maze";
        }

        public void Export()
        {
            var save = ExportSqlDialog.ShowDialog();
            if (save != DialogResult.OK) return;
            string path = ExportSqlDialog.FileName;

            string sql = "INSERT INTO `gameobject`(`id`, `map`, `spawnMask`, `phaseMask`, `position_x`, `position_y`, `position_z`, `orientation`, `rotation0`, `rotation1`, `rotation2`, `rotation3`, `spawntimesecs`, `animprogress`, `state`) VALUES\n";
            string endLine = String.Empty;
            int curr = 0;
            List<double[]> maze = App.objectHandler.GenerateMazeObjects();
            foreach (double[] box in maze)
            {
                curr++;
                if (curr < maze.Count)
                    endLine = ",\n";
                else endLine = ";\n";
                sql += "(" + box[5] + "," + box[3] + ",1,1," + box[0].ToString().Replace(',', '.') + "," + box[1].ToString().Replace(',', '.') + "," + box[2].ToString().Replace(',', '.') + "," + box[4].ToString().Replace(',', '.') + ",0,0,0,0,0,0,0)" + endLine;
            }

            // Add gameobject_template for Maze Crate
            if (Config.GAMEOBJECT == 745000)
                sql += "INSERT IGNORE INTO `gameobject_template` (`entry`, `type`, `displayId`, `name`, `IconName`, `castBarCaption`, `unk1`, `faction`, `flags`, `size`, `questItem1`, `questItem2`, `questItem3`, `questItem4`, `questItem5`, `questItem6`, `data0`, `data1`, `data2`, `data3`, `data4`, `data5`, `data6`, `data7`, `data8`, `data9`, `data10`, `data11`, `data12`, `data13`, `data14`, `data15`, `data16`, `data17`, `data18`, `data19`, `data20`, `data21`, `data22`, `data23`, `ScriptName`) VALUES\n" +
                    "('745000', '5', '31', 'Maze Crate', '', '', '', '94', '0', '2', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', ''),\n" +
                    "('745001', '5', '7593', 'Maze Stairs', '', '', '', '94', '0', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', ''),\n" +
                    "('745002', '0', '31', 'Maze Crate (no collision)', '', '', '', '94', '4', '2', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '');\n";

            File.WriteAllText(path, sql);
        }

        public void OpenFile()
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            string saveData = String.Empty;
            if (result == DialogResult.OK) // Test result.
                saveData = File.ReadAllText(openFileDialog1.FileName);
            else return;

            // Opens and cleans file
            string[] temp = saveData.Split('\n');
            List<string> cleanData = new List<string>();
            foreach (string s in temp)
            {
                string v = s.Replace("\r", "");
                if (v != String.Empty)
                    cleanData.Add(v);
            }

            App.configForm.LoadConfig(cleanData[0]);
            App.creator.LoadMaze(saveData);
        }

        // Save file content
        public bool SaveFile()
        {
            var save = saveToFileDialog.ShowDialog();
            if (save == DialogResult.OK)
            {
                App.objectHandler.StoreMazeData();
                App.configForm.SetConfig();

                System.IO.File.WriteAllLines(saveToFileDialog.FileName, Config.MAZEDATA);
                App.creator.changedSinceSave = false;
                return true;
            }
            else return false;
        }
    }
}