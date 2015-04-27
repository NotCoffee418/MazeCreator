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
using System.Globalization;

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
            levelCountTextBox.Enabled = false;
            label6.Visible = true;
            label15.Visible = true;
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
            this.Hide();
            Program.creator.Show();
            Program.creator.OpenFile();
        }

       
        /// <summary>
        /// Loads config string to config form
        /// </summary>
        /// <param name="configString">Config string to load</param>
        /// <param name="change">index</param>
        /// <param name="value">new value</param>
        internal void LoadConfig(string configString = "", int change = -1, string value = "")
        {
            if (configString == "")
                configString = Program.creator.MAZEDATA[0];
            string[] config = configString.Split('|');

            // Apply any change
            if (change != -1)
                config[change] = value;

            // Load config to text boxes
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

            if (config.Count() == 12)
                levelCountTextBox.Text = config[11];

            // Set config in Creator
            Program.configForm.SetConfig();
        }

        /// <summary>
        /// Sets config form settings in Creator.
        /// </summary>
        public void SetConfig()
        {
            try
            {
                // Set creator data
                Program.creator.GAMEOBJECT = int.Parse(objectIdTextBox.Text);
                Program.creator.SPACING = SafeDouble(objectSpacingTextBox.Text);
                Program.creator.WALLHEIGHT = int.Parse(wallHeightTextBox.Text);
                Program.creator.X_COUNT = int.Parse(xCountTextBox.Text);
                Program.creator.Y_COUNT = int.Parse(yCountTextBox.Text);
                Program.creator.FLOOR = floorCheckBox.Checked;
                Program.creator.ROOF = roofCheckBox.Checked;
                Program.creator.STARTCOORDS[0] = SafeDouble(xTextBox.Text);
                Program.creator.STARTCOORDS[1] = SafeDouble(yTextBox.Text);
                Program.creator.STARTCOORDS[2] = SafeDouble(zTextBox.Text);
                Program.creator.STARTCOORDS[3] = int.Parse(mapTextBox.Text);
                Program.creator.LEVEL_COUNT = int.Parse(levelCountTextBox.Text);


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
                '|' + mapTextBox.Text +
                '|' + levelCountTextBox.Text;
            }
            catch
            {
                MessageBox.Show("Some information was incorrectly entered.");
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Makes sure input value doesn't become corrupted.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private double SafeDouble(string p)
        {
            return double.Parse(p, CultureInfo.InvariantCulture);
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
