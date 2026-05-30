using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War3_Map_Editor_Tools
{
    public class Config
    {
        public static string AppPath = System.Windows.Forms.Application.StartupPath;
        public static string folderPath = "";
        public static void Load()
        {
            if (File.Exists(AppPath + "\\editor.inf"))
            {
                IniData File = new FileIniDataParser().ReadFile(AppPath + "\\editor.inf", Encoding.UTF8);
                folderPath = File["Editor"]["MapFolderPath"];
            }
        }
        public static void Write()
        {
            var MyIni = new IniFile(AppPath + "\\editor.inf");
            MyIni.Write("MapFolderPath", folderPath, "Editor");
        }
    }
}
