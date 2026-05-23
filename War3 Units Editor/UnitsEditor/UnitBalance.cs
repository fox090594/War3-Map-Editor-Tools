using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War3_Map_Editor_Tools.UnitsEditor
{
    public class UnitBalance
    {
        public static Dictionary<int, Balance> List = new Dictionary<int, Balance>();
        public class Balance
        {
            public int id;
            public string Index;
            public string Name;
            public string Ubertip;
            public string Description;
        }
        public static void Load(string path)
        {
            using (var reader = new StreamReader(path + "\\UnitBalance.slk", System.Text.Encoding.ASCII))
            {
                string line;
                Balance c = new Balance();
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
                                c = new Balance();
                            }
                            if (Add == false)
                            {
                                Add = true;
                            }
                            c.id = Int32.Parse(fields[1].Replace("Y", ""));
                            c.Index = fields[3].Replace("K", "").Replace('"', ' ').Replace(" ", "");
                            c.Name = strings.GetFromStrings(c.Index, "Name");
                            //c.Ubertip = strings.GetFromStrings(c.Index, "Ubertip");
                            //c.Description = strings.GetFromStrings(c.Index, "Description");
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
