using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace War3_Map_Editor_Tools.ScriptParser
{
    public class MapUnits
    {
        //public static Dictionary<int, UnitsCls> List = new Dictionary<int, UnitsCls>();
        public class UnitsCls
        {
            public int id;
            public string OriginalId;
            public float PositionX;
            public float PositionY;
            public float PositionZ;
            public int PlayerNum;
        }
        public static Dictionary<int, UnitsCls> LoadList(string path)
        {
            Dictionary<int, UnitsCls> List = new Dictionary<int, UnitsCls>();
            string[] lines = File.ReadAllLines(path);
            int playernum = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Replace("'", "").Replace("$", "0").Replace("0A", "10").Replace("0B", "11").Replace("0C", "12").Replace("0D", "13").Replace("0E", "14").Replace("0F", "15");
                if (lines[i].Contains("set"))
                {
                    if (lines[i].Contains("=Player"))
                    {
                        try
                        {
                            string Match = Regex.Match(lines[i], @"\(([^)]+)\)").Groups[1].Value;
                            playernum = Int32.Parse(Match);
                        }
                        catch
                        {
                            playernum = 0;
                        }
                    }
                    if (lines[i].Contains("=CreateUnit"))
                    {
                        try
                        {
                            string Match = Regex.Match(lines[i], @"\(([^)]+)\)").Groups[1].Value;
                            //MessageBox.Show(Match);
                            string[] values = Match.Replace(" ", "").Split(',');
                            string id = values[1];
                            if (id.Length != 4)
                            {
                                uint dwordValue = UInt32.Parse(id);
                                id = Converters.HexToAscii(dwordValue.ToString("X"));
                            }

                            string x = values[2];
                            if (x.Split('.')[1].Length < 1)
                            {
                                x = x + "00";
                            }

                            string y = values[3];
                            if (y.Split('.')[1].Length < 1)
                            {
                                y = y + "00";
                            }

                            string z = values[4];
                            if (z.Split('.')[1].Length < 1)
                            {
                                z = z + "00";
                            }

                            UnitsCls newUnit = new UnitsCls();
                            newUnit.OriginalId = id;
                            newUnit.PlayerNum = playernum;
                            newUnit.PositionX = float.Parse(x, CultureInfo.InvariantCulture); //Single.Parse(x);
                            newUnit.PositionY = float.Parse(y, CultureInfo.InvariantCulture); //Single.Parse(y);
                            newUnit.PositionZ = float.Parse(z, CultureInfo.InvariantCulture); //Single.Parse(z);
                            List.Add(List.Count, newUnit);
                        }
                        catch
                        {

                        }
                    }
                }
            }
            MessageBox.Show("Parsed and loaded: " + List.Count.ToString() + " rows.");
            return List;
        }
    }
}
