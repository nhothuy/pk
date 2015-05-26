using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PKTool
{
    public class Key
    {
        private int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        private int level;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        private string data;

        public string Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
