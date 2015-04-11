using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            String[] mazeConfig = new String[11];
            mazeConfig[0] = textBox1.Text;
            mazeConfig[1] = textBox2.Text.Replace('.',',');
            mazeConfig[2] = textBox3.Text;
            mazeConfig[3] = textBox4.Text;
            mazeConfig[4] = textBox5.Text;
            mazeConfig[5] = checkBox1.Checked.ToString();
            mazeConfig[6] = checkBox2.Checked.ToString();
            mazeConfig[7] = textBox6.Text.Replace('.', ',');
            mazeConfig[8] = textBox7.Text.Replace('.', ',');
            mazeConfig[9] = textBox8.Text.Replace('.', ',');
            mazeConfig[10] = textBox9.Text;
            this.Hide();
            Creator c = new Creator(mazeConfig);
            try
            {
            } 
            catch (Exception ex)
            {
                MessageBox.Show("Some information was incorrectly entered.");
                Environment.Exit(0);
            }
        }
    }
}
