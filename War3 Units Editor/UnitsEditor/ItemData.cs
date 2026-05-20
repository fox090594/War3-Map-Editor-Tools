using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static War3_Map_Editor_Tools.UnitsEditor.UnitData;

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
        public static void Load(string path)
        {
            using (var reader = new StreamReader(path + "\\ItemData.slk", System.Text.Encoding.ASCII))
            {
                string line;
                Item c = new Item();
                bool Add = false;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(';');

                    if (fields.Length > 0 && fields[0].StartsWith("C"))
                    {
                        if (line.Contains("C;Y"))
                        {
                            if (Add == true)
                            {
                                List.Add(c.id, c);
                                c = new Item();
                            }
                            if (Add == false)
                            {
                                Add = true;
                            }
                            c.id = Int32.Parse(fields[1].Replace("Y", ""));
                            c.Index = fields[3].Replace("K", "").Replace('"', ' ').Replace(" ", "");
                            c.Name = strings.GetFromStrings(c.Index, "Name");
                            //c.Ubertip = strings.GetFromStrings(c.Index, "Ubertip");
                            //c.Researchtip = strings.GetFromStrings(c.Index, "Researchtip");
                            //c.Researchubertip = strings.GetFromStrings(c.Index, "Researchubertip");
                            //c.Tip = strings.GetFromStrings(c.Index, "Tip");
                            //c.Researchhotkey = strings.GetFromStrings(c.Index, "Researchhotkey");
                            //c.Hotkey = strings.GetFromStrings(c.Index, "Hotkey");
                        }
                    }
                }
            }
        }
    }
}