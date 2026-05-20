using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War3_Map_Editor_Tools.UnitsEditor
{
    public class strings
    {
        public static string[] files = 
        { 
            "CampaignAbilityStrings",
            "CampaignUnitStrings",
            "CampaignUpgradeStrings",
            "CommonAbilityStrings",
            "HumanAbilityStrings",
            "HumanUnitStrings",
            "HumanUpgradeStrings",
            "itemabilitystrings",
            "ItemStrings",
            "NeutralAbilityStrings",
            "NeutralUnitStrings",
            "NeutralUpgradeStrings",
            "NightElfAbilityStrings",
            "NightElfUnitStrings",
            "NightElfUpgradeStrings",
            "OrcAbilityStrings",
            "OrcUnitStrings",
            "OrcUpgradeStrings",
            "UndeadAbilityStrings",
            "UndeadUnitStrings",
            "UndeadUpgradeStrings"
        };
        public static IniData[] Files;
        public static void ReadFiles(string path)
        {
            Files = new IniData[files.Length];
            for (int i = 0; i < strings.files.Length; i++)
            {
                try
                {
                    Files[i] = new FileIniDataParser().ReadFile(path + "\\" + files[i] + ".txt",Encoding.UTF8);
                }
                catch { }
            }
        }
        public static string GetFromStrings(string index, string value)
        {
            string result = "";
            for (int i = 0; i < strings.files.Length; i++)
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
