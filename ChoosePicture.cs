using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace auto_game_playing
{

    public partial class ChoosePicture : Form
    {

        public ChoosePicture()
        {
            InitializeComponent();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Load(@".\picture\"+listBox1.SelectedItems[0].ToString());


        }

        private void get_file(string path)
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            foreach (FileInfo file in folder.GetFiles("*.png"))
            {
                listBox1.Items.Add(file.Name);
            }
            foreach (FileInfo file in folder.GetFiles("*.jpg"))
            {
                listBox1.Items.Add(file.Name);
            }

        }
        private void ChoosePicture_Load(object sender, EventArgs e)
        {
            get_file(@".\picture\");
            
        }
        


        
        private void button1_Click(object sender, EventArgs e) //确定
        {
            
            deliver.func = "pictureclick";

            if (deliver.if_pic)
            {
                deliver.ifval= listBox1.SelectedItem.ToString();
                deliver.if_pic = false;
            }
            else
            {
                deliver.val = listBox1.SelectedItem.ToString();
            }
            deliver.choose_open = false;
            if (deliver.if_open == false) { Compile.es.Close(); }
            
            Close();
      
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process p=Process.Start(@".\python\python.exe", @".\picturecut.pyc");

        }
    }
}
