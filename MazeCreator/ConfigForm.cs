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
            SetConfig();
        }

        private void openMazeButton_Click(object sender, EventArgs e)
        {

            Program.creator.OpenFile();
            this.Hide();
            Program.creator.Show();
        }

        public void SetConfig(string mazeData = "")
        {
            //try
            //{
                // Set config text
                Program.creator.mazeConfigText = objectIdTextBox.Text +
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

                // store any maze data
                if (mazeData == "") mazeData = Program.creator.GetMazeData();
                string loadData = Program.creator.mazeConfigText + '\n' + mazeData;


                // Switch to creator
                this.Hide();
                Program.creator.LoadMaze(loadData);
                Program.creator.Show();
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
