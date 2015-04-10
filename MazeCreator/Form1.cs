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
            String[] mazeConfig = new String[7];
            mazeConfig[0] = textBox1.Text;
            mazeConfig[1] = textBox2.Text;
            mazeConfig[2] = textBox3.Text;
            mazeConfig[3] = textBox4.Text;
            mazeConfig[4] = textBox5.Text;
            mazeConfig[5] = checkBox1.Checked.ToString();
            mazeConfig[6] = checkBox2.Checked.ToString();
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
