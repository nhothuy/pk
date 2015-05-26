using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PKTool
{
    public class Item
    {
        private String name;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        private bool isdamaged = false;
        public bool Isdamaged
        {
            get { return isdamaged; }
            set { isdamaged = value; }
        }
        private int level = 1;
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
    }
}
