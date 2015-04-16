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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string mazeConfig = textBox1.Text+
                '|' + SafeDouble(textBox2.Text) +
                '|' + textBox3.Text +
                '|' + textBox4.Text +
                '|' + textBox5.Text +
                '|' + checkBox1.Checked.ToString() +
                '|' + checkBox2.Checked.ToString() +
                '|' + SafeDouble(textBox6.Text) +
                '|' + SafeDouble(textBox7.Text) +
                '|' + SafeDouble(textBox8.Text) +
                '|' + textBox9.Text;
                this.Hide();
                Creator c = new Creator(mazeConfig);
            } 
            catch (Exception ex)
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
            if (checkBox3.Checked)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
            }
            else
            {
                textBox1.Text = "745000";
                textBox1.Enabled = false;

                textBox2.Text = "2.5";
                textBox2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            string saveData = String.Empty;
            if (result == DialogResult.OK) // Test result.
                saveData = File.ReadAllText(openFileDialog1.FileName);
            else return;
            this.Hide();
            Creator c = new Creator(saveData);
        }
    }
}
