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

            string createSql = "INSERT INTO `gameobject`(`id`, `map`, `spawnMask`, `phaseMask`, `position_x`, `position_y`, `position_z`, `orientation`, `rotation0`, `rotation1`, `rotation2`, `rotation3`, `spawntimesecs`, `animprogress`, `state`) VALUES\n";
            string removeSql = String.Empty;
            string endLine = String.Empty;
            int curr = 0;
            List<double[]> maze = App.objectHandler.GenerateMazeObjects();
            foreach (double[] box in maze)
            {
                curr++;
                if (curr < maze.Count)
                    endLine = ",\n";
                else endLine = ";\n";
                createSql += "(" + box[5] + "," + box[3] + ",1,1," + box[0].ToString().Replace(',', '.') + "," + box[1].ToString().Replace(',', '.') + "," + box[2].ToString().Replace(',', '.') + "," + box[4].ToString().Replace(',', '.') + ",0,0,0,0,0,0,0)" + endLine;
                removeSql += "DELETE FROM `gameobject` WHERE `id`=" + box[5].ToString().Replace(',', '.') + " AND `map`=" + box[3].ToString().Replace(',', '.') + " AND `position_x`=" + box[0].ToString().Replace(',', '.') + " AND `position_y`=" + box[1].ToString().Replace(',', '.') + " AND `position_z`=" + box[2].ToString().Replace(',', '.') + ";\n";
            }

            // Add gameobject_template for Maze Crate
            if (Config.GAMEOBJECT == 745000)
                createSql += "INSERT IGNORE INTO `gameobject_template` (`entry`, `type`, `displayId`, `name`, `IconName`, `castBarCaption`, `unk1`, `size`, `Data0`, `Data1`,`Data2`, `Data3`,`Data4`, `Data5`, `Data6`, `Data7`,	`Data8`, `Data9`, `Data10`, `Data11`, `Data12`, `Data13`, `Data14`, `Data15`, `Data16`, `Data17`, `Data18`, `Data19`, `Data20`,`Data21`, `Data22`, `Data23`, `AIName`) VALUES\n" + "('745003', '5', '7593', 'Maze Stairs', '', '', '', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '', '');\n";

            // Write SQL files
            File.WriteAllText(path, createSql);
            WriteRemoveSqlFile(path, removeSql);
        }



        /// <summary>
        /// Writes the remove maze file without overwriting a previous one
        /// </summary>
        /// <param name="createPath">File path of creation SQL file</param>
        /// <param name="removeSql">SQL queries</param>
        private void WriteRemoveSqlFile(string createPath, string removeSql)
        {
            // Determine file name of remove SQL file
            string fileName = Path.GetFileNameWithoutExtension(createPath) + "-remove";
            string dir = Path.GetDirectoryName(createPath);
            string fullPath = dir + "\\" + fileName + ".sql";

            int count = 2;
            while (File.Exists(fullPath))
            {
                fullPath = dir + "\\" + fileName + "-" + count + ".sql";
                count++;
            }

            File.WriteAllText(fullPath, removeSql);
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
            ConvertOldSaveData(ref saveData);
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

        /// <summary>
        /// Turns old save data into new save data
        /// </summary>
        /// <param name="saveData"></param>
        private void ConvertOldSaveData(ref string saveData)
        {
            String[] lines = saveData.Split('\n');

            // Already new savedata
            if (lines.Length > 1 && lines[1].Contains(","))
                return;

            string newSaveData = String.Empty;
            for (int i = 0; i < lines.Length; i++)
            {
                string saveLine = String.Empty;
                foreach (char c in lines[i])
                    if (c != '\r')
                    {
                        saveLine += c.ToString() + ',';
                    }
                if (saveLine != "")
                {
                    saveLine = saveLine.Remove(saveLine.Length - 1); // remove final comma
                    newSaveData += saveLine + '\n';
                }
            }
            saveData = newSaveData;
        }
    }
}