using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MazeCreator
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Change maze block count in creator after maze already started.
        /// </summary>
        /// <param name="hasData">true: disables editing counts</param>
        public void DisplayConfigForm()
        {
            xCountTextBox.Enabled = false;
            yCountTextBox.Enabled = false;
            label6.Visible = true;
            Show();
        }

        private void saveConfigButton_Click(object sender, EventArgs e)
        {
            Program.creator.StoreMazeData();
            SetConfig();
            Program.creator.LoadData(); // from stored
            this.Hide();
            Program.creator.Show();
        }

        private void openMazeButton_Click(object sender, EventArgs e)
        {
            Program.creator.OpenFile();
            this.Hide();
            Program.creator.Show();
        }

        /// <summary>
        /// Loads config string to config form
        /// </summary>
        /// <param name="p"></param>
        internal void LoadConfig(string configString)
        {
            string[] config = configString.Split('|');
            objectIdTextBox.Text = config[0];
            objectSpacingTextBox.Text = config[1];
            wallHeightTextBox.Text = config[2];
            xCountTextBox.Text = config[3];
            yCountTextBox.Text = config[4];
            floorCheckBox.Checked = bool.Parse(config[5]);
            roofCheckBox.Checked = bool.Parse(config[6]);
            xTextBox.Text = config[7];
            yTextBox.Text = config[8];
            zTextBox.Text = config[9];
            mapTextBox.Text = config[10];
        }

        /// <summary>
        /// Sets config form settings in Creator.
        /// </summary>
        public void SetConfig()
        {
            //try
            //{
                // Set creator data
                Program.creator.GAMEOBJECT = int.Parse(objectIdTextBox.Text);
                Program.creator.SPACING = double.Parse(SafeDouble(objectSpacingTextBox.Text));
                Program.creator.WALLHEIGHT = int.Parse(wallHeightTextBox.Text);
                Program.creator.X_COUNT = int.Parse(xCountTextBox.Text);
                Program.creator.Y_COUNT = int.Parse(yCountTextBox.Text);
                Program.creator.FLOOR = floorCheckBox.Checked;
                Program.creator.ROOF = roofCheckBox.Checked;
                Program.creator.STARTCOORDS[0] = double.Parse(SafeDouble(xTextBox.Text));
                Program.creator.STARTCOORDS[1] = double.Parse(SafeDouble(yTextBox.Text));
                Program.creator.STARTCOORDS[2] = double.Parse(SafeDouble(zTextBox.Text));
                Program.creator.STARTCOORDS[3] = int.Parse(mapTextBox.Text);

                // Set config text
                if (Program.creator.MAZEDATA == null)
                    Program.creator.MAZEDATA = new string[Program.creator.Y_COUNT + 1];
                Program.creator.MAZEDATA[0] = objectIdTextBox.Text +
                '|' + SafeDouble(objectSpacingTextBox.Text) +
                '|' + wallHeightTextBox.Text +
                '|' + xCountTextBox.Text +
                '|' + yCountTextBox.Text +
                '|' + floorCheckBox.Checked.ToString() +
                '|' + roofCheckBox.Checked.ToString() +
                '|' + SafeDouble(xTextBox.Text) +
                '|' + SafeDouble(yTextBox.Text) +
                '|' + SafeDouble(zTextBox.Text) +
                '|' + mapTextBox.Text;
            /*}
            catch (Exception ex)
            {
                MessageBox.Show("Some information was incorrectly entered.");
                Environment.Exit(0);
            }*/
        }

        /// <summary>
        /// Makes sure input value doesn't become corrupted.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string SafeDouble(string p)
        {
            // Split double into parts
            p = p.Replace('.', ',');
            string[] n = p.Split(',');

            int max = 8;
            if (n[0].StartsWith("-")) max = 9;
            if (n[0].Length > max) throw new Exception();

            switch(n.Count())
            {
                case 1: 
                    return n[0];
                case 2:
                    while (n[1].Length > 8) // Remove excessive numbers
                        n[1] = n[1].Remove(n[1].Length - 1);
                    return n[0] + ',' + n[1];
                default: 
                    throw new Exception();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (advancedCheckBox.Checked)
            {
                objectIdTextBox.Enabled = true;
                objectSpacingTextBox.Enabled = true;
            }
            else
            {
                objectIdTextBox.Text = "745000";
                objectIdTextBox.Enabled = false;

                objectSpacingTextBox.Text = "2.5";
                objectSpacingTextBox.Enabled = false;
            }
        }
    }
}
