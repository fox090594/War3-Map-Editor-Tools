using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;

namespace War3_Units_Editor.UnitsEditor
{
    public class UnitData
    {
        public static Dictionary<int, Unit> List = new Dictionary<int, Unit>();
        public class Unit
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
            using (var reader = new StreamReader(path + "\\UnitData.slk", System.Text.Encoding.ASCII))
            {
                string line;
                Unit c = new Unit();
                bool Add = false;
                while ((line = reader.ReadLine()) != null)
                {
                    // SYLK records are semicolon-delimited
                    string[] fields = line.Split(';');

                    if (fields.Length > 0 && fields[0].StartsWith("C"))
                    {
                        if (line.Contains("C;Y"))
                        {
                            if (Add == true)
                            {
                                List.Add(c.id, c);
                                c = new Unit();
                            }
                            if (Add == false)
                            {
                                Add = true;
                            }
                            c.id = Int32.Parse(fields[1].Replace("Y", ""));
                            c.Index = fields[3].Replace("K", "").Replace('"', ' ').Replace(" ", "");
                            c.Name = GetFromStrings(c.Index, "Name");
                            c.Ubertip = GetFromStrings(c.Index, "Ubertip");
                            c.Researchtip = GetFromStrings(c.Index, "Researchtip");
                            c.Researchubertip = GetFromStrings(c.Index, "Researchubertip");
                            c.Tip = GetFromStrings(c.Index, "Tip");
                            c.Researchhotkey = GetFromStrings(c.Index, "Researchhotkey");
                            c.Hotkey = GetFromStrings(c.Index, "Hotkey");
                        }
                    }
                }
            }
        }

        public static string GetFromStrings(string index, string value)
        {
            string result = "";
            for(int i = 0; i  < strings.files.Length; i++) 
            {
                try
                {
                    string value1 = strings.Files[i][index][value];
                    if (value1.Length > 1)
                    {
                        result = value1;
                    }
                }
                catch { }
            }
            return result;
        }
    }
}
