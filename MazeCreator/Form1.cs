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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string mazeConfig = textBox1.Text+
                '|' + textBox2.Text + // .Replace('.', ',') +
                '|' + textBox3.Text +
                '|' + textBox4.Text +
                '|' + textBox5.Text +
                '|' + checkBox1.Checked.ToString() +
                '|' + checkBox2.Checked.ToString() +
                '|' + textBox6.Text + // .Replace('.', ',') + 
                '|' + textBox7.Text + // .Replace('.', ',') +
                '|' + textBox8.Text + // .Replace('.', ',') +
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
