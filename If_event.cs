using System;
using System.Windows.Forms;


namespace auto_game_playing
{
    public partial class If_event : Form
    {

            public If_event()
        {
            InitializeComponent();
            comboBox1.Items.Add("找到");
            comboBox1.Items.Add("没找到");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Focus();
            new ChoosePicture().ShowDialog();
            textBox1.Text = deliver.val;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deliver.if_pic = true;
            Focus();
            new ChoosePicture().ShowDialog();
            textBox2.Text= deliver.ifval;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            deliver.if_find = comboBox1.SelectedItem.ToString().Replace(" ","");
            deliver.func = "IF";
            deliver.if_open = false;
            Compile.es.Close();
            Close();
        }
    }
}
