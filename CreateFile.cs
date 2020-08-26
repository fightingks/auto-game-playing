using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace auto_game_playing
{
    public partial class CreateFile : Form
    {
        public CreateFile()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            deliver.filename = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
