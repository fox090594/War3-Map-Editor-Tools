using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War3_Map_Editor_Tools.UnitsEditor
{
    public class ItemData
    {
        public static Dictionary<int, Item> List = new Dictionary<int, Item>();
        public class Item
        {
            public int id;
            public string Index;
            public string Name;
            public string Ubertip;
            public string Researchtip;
            public string Researchubertip;
            public string Tip;
            public string Researchhotkey;
            public string Hotkey;
        }
    }
}
