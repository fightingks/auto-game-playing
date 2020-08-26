using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace auto_game_playing

{


    public partial class Compile : Form
    {
        string xmlPath;
        XmlDocument xmlDoc = new XmlDocument();
        XmlDocument writenote = new XmlDocument();
        XmlElement writeroot;

        ArrayList ValueList = new ArrayList();
        public Compile()
        {
            InitializeComponent();
            ListBox1.Text = "";

        }


        XmlNodeList childlist;
        public void xmlview(string xmlPath)
        {

            xmlDoc.Load(xmlPath);
            //取根结点
            var root = xmlDoc.DocumentElement;
            //获取根节点的所有子节点列表 
            childlist = root.ChildNodes;
            ListBox1.Items.Clear();
            for (int i = 0; i < childlist.Count; i++)
            //遍历xml中所有的调用函数和对应值
            {
                XmlNode child = childlist[i];
                XmlNamedNodeMap childattr = child.Attributes;
                string funcvalue = childattr.GetNamedItem("function").Value.ToString();
                string textvalue = child.InnerText.ToString();
                string ifvalue = childattr.GetNamedItem("if_value").Value.ToString();
                string iffind = childattr.GetNamedItem("if_find").Value.ToString();
                ValueList.Add((funcvalue, textvalue, ifvalue, iffind));


                if (funcvalue == "pictureclick")
                {
                    ListBox1.Items.Add("点击"+textvalue);
                }
                if(funcvalue=="IF")
                {
                    ListBox1.Items.Add(string.Format("{0}{1} 时点击 {2}", iffind, textvalue, ifvalue));

                }

            }
        }


        public static Eventselect es;

        private void ListBox1_MouseDoubleClick(object sender, EventArgs e)
        {

            es = new Eventselect();
            es.ShowDialog();
            if (deliver.if_open == false&& deliver.choose_open == false)
            {
                try
                {
                    ValueList[ListBox1.SelectedIndex] = (deliver.func,deliver.val,
                        deliver.ifval, deliver.if_find);
                }
                catch {
                    ValueList.Add((deliver.func,deliver.val, 
                        deliver.ifval, deliver.if_find));
                }

                
                if (deliver.func == "pictureclick")
                {

                    try
                    {
                        ListBox1.Items.Add("");
                        ListBox1.Items.Insert(ListBox1.SelectedIndex, "点击" + deliver.val);
                        ListBox1.Items.Remove(ListBox1.Items[ListBox1.SelectedIndex]);
                    }
                    catch { ListBox1.Items.Add("点击" + deliver.val); }
                    

                    //更改listbox内容
                }
                if (deliver.func == "IF")
                {
                    ListBox1.Items.Add("");
                    try
                    {
                        ListBox1.Items.Insert(ListBox1.SelectedIndex, "点击" + deliver.val);
                        ListBox1.Items.Remove(ListBox1.Items[ListBox1.SelectedIndex]);
                    }
                    catch { ListBox1.Items.Add(string.Format("{0}{1}时点击{2}", deliver.if_find, deliver.val, deliver.ifval)); }
                    
                    //更改listbox内容
                }




            }




        }
        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateFile createFile = new CreateFile();
            createFile.ShowDialog();
            FileStream note = new FileStream(string.Format("{0}.xml",deliver.filename), FileMode.OpenOrCreate, FileAccess.ReadWrite);
            xmlPath = Path.GetFullPath(string.Format("{0}.xml", deliver.filename));
            Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");
            Match m = RegCHZN.Match(xmlPath);
            if (m.Success)
            {
                MessageBox.Show("路径不得包含中文，请重试！");

            }
            else
            {
                //向path.xml写入路径，供python调用
                XmlDocument namexml = new XmlDocument();
                //xml文档的声明部分
                XmlDeclaration declaration = namexml.CreateXmlDeclaration("1.0", "UTF-8", "");
                namexml.AppendChild(declaration);
                //创建Root
                XmlElement nameroot = namexml.CreateElement("note");
                namexml.AppendChild(nameroot);
                XmlElement name = namexml.CreateElement("name");
                name.InnerText = xmlPath.Split('\\')[xmlPath.Split('\\').Length - 1];
                nameroot.AppendChild(name);
                namexml.Save(Path.GetDirectoryName(xmlPath) + "\\" + "name.xml");
                ListBox1.Visible = true;
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
            }
            note.Close();
        }
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "xml文件|*.xml|C#文件|*.cs|所有文件|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Title = "请选择打开的文件";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.SafeFileName;
            }
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            xmlPath = Path.GetFullPath(openFileDialog1.FileName);
            Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");
            Match m = RegCHZN.Match(xmlPath);
            if (m.Success)
            {
                MessageBox.Show("路径不得包含中文，请重试！");

            }
            else
            {
                xmlview(xmlPath);
                //向path.xml写入路径，供python调用
                XmlDocument namexml = new XmlDocument();
                //xml文档的声明部分
                XmlDeclaration declaration = namexml.CreateXmlDeclaration("1.0", "UTF-8", "");
                namexml.AppendChild(declaration);
                //创建Root
                XmlElement nameroot = namexml.CreateElement("note");
                namexml.AppendChild(nameroot);
                XmlElement name = namexml.CreateElement("name");
                name.InnerText =  xmlPath.Split('\\')[xmlPath.Split('\\').Length-1];                
                nameroot.AppendChild(name);
                namexml.Save(Path.GetDirectoryName(openFileDialog1.FileName)+"\\"+"name.xml");
                ListBox1.Visible = true;
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
            }

        }
        //另存为
        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = Application.StartupPath;
            saveFileDialog1.Filter = "xml文件|*.xml|所有文件|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Title = "请选择保存的路径";
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
            }

        }
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

            //xml文档的声明部分
            XmlDeclaration declaration = writenote.CreateXmlDeclaration("1.0", "UTF-8", "");
            writenote.AppendChild(declaration);
            //创建Root
            writeroot = writenote.CreateElement("note");
            writenote.AppendChild(writeroot);
            foreach (object obj in ValueList)
            {
                string[] olist = obj.ToString().Replace("(", "").Replace(")", "").Split(',');
                Writexml(olist[0].Replace(" ",""), olist[1].Replace(" ", ""), olist[2].Replace(" ", "")
                    , olist[3].Replace(" ", "")); 


            }
            writenote.Save(Path.GetFullPath(saveFileDialog1.FileName));
            MessageBox.Show("保存完毕。");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void Writexml(string tag, string value, string ifval = null, string iffind = null)
        {
            //赋予子节点属性名及数据
            XmlAttribute function = writenote.CreateAttribute("function");
            function.Value = tag;
            XmlAttribute val1 = writenote.CreateAttribute("if_value");
            val1.Value = ifval;
            XmlAttribute if_find = writenote.CreateAttribute("if_find");
            if_find.Value = iffind;
            //创建子节点
            XmlElement code = writenote.CreateElement("code");
            code.InnerText = value;
            writeroot.AppendChild(code);
            code.Attributes.Append(function);
            code.Attributes.Append(val1);
            code.Attributes.Append(if_find);
        }
        //保存
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                //xml文档的声明部分
                XmlDeclaration declaration = writenote.CreateXmlDeclaration("1.0", "UTF-8", "");
                writenote.AppendChild(declaration);
                //创建Root
                writeroot = writenote.CreateElement("note");
                writenote.AppendChild(writeroot);
                foreach (object obj in ValueList)
                {
                    string[] olist = obj.ToString().Replace("(", "").Replace(")", "").Split(',');
                    Writexml(olist[0].Replace(" ", ""), olist[1].Replace(" ", ""), olist[2].Replace(" ", "")
    , olist[3].Replace(" ", ""));
                    MessageBox.Show(olist.ToString());

                }
                writenote.Save(xmlPath);
                MessageBox.Show(xmlPath);
                
                MessageBox.Show("保存完毕。");
            }
            catch { };

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //xml文档的声明部分
                XmlDeclaration declaration = writenote.CreateXmlDeclaration("1.0", "UTF-8", "");
                writenote.AppendChild(declaration);
                //创建Root
                writeroot = writenote.CreateElement("note");
                writenote.AppendChild(writeroot);
                foreach (object obj in ValueList)
                {
                    string[] olist = obj.ToString().Replace("(", "").Replace(")", "").Split(',');
                    Writexml(olist[0].Replace(" ", ""), olist[1].Replace(" ", ""), olist[2].Replace(" ", "")
    , olist[3].Replace(" ", ""));

                }
                writenote.Save(xmlPath);
                MessageBox.Show("保存完毕。");
            }
            catch { };

        
    }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(@".\python\pythonw.exe", @".\auto_game-playing.pyc");
        }

        private void 运行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@".\python\pythonw.exe", @".\auto_game-playing.pyc");
        }


    }
}
