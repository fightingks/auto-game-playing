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
    public partial class Eventselect : Form
    {
        public Eventselect()
        {
            InitializeComponent();

        }
        //点击图片
        private void button1_Click(object sender, EventArgs e)
        {
            new ChoosePicture().Show();
            deliver.choose_open = true;


        }
        //分支
        private void button2_Click(object sender, EventArgs e)
        {
            If_event IF= new If_event();
            IF.Show();
            deliver.if_open = true;


        }


    }
}
