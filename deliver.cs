using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auto_game_playing
{

    class deliver
    {
        public static bool if_open = false;
        public static bool choose_open = false;
        public static bool random_open = false;
        public static bool python_open = false;
        public static bool if_pic=false;
        public deliver()
        {
            val = "";
            func = "";
            ifval = "";
            if_find = "";
        }
        public static string filename;
        public string FileName
        {
            get { return filename; }
            set { filename = value; }
        }
        public static string val;
        public string Values
        {get{return val;}
        set{val = value;}}

        public static string func;
        public string Funcs
        {get { return func; }
        set { func = value; }}

        public static string ifval;
        public string Ifval
        {get { return ifval; }
        set { ifval = value; }}

        public static string if_find;
        public string If_Find
        {
            get { return if_find; }
            set { if_find = value; }
        }
    }
}
