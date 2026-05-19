using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War3_Units_Editor.UnitsEditor
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
                    var parser = new FileIniDataParser();
                    Files[i] = new FileIniDataParser().ReadFile(path + "\\" + files[i] + ".txt");
                }
                catch { }
            }
        }
    }
}
