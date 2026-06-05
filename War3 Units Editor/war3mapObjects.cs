using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War3_Map_Editor_Tools
{
    public class war3mapObjects
    {
        public static Dictionary<string, ObjectsFiles> FilesList = new Dictionary<string, ObjectsFiles>();
        public class ObjectsFiles
        {
            public int version = 2;
            public Dictionary<int, Objects> OriginalTable = new Dictionary<int, Objects>();
            public Dictionary<int, Objects> CustomTable = new Dictionary<int, Objects>();
        }
        public class Objects
        {
            public string OriginalId;
            public string CustomId;
            public Dictionary<int, Mods> ModsList = new Dictionary<int, Mods>();
        }
        public class Mods
        {
            public string ModId;
            public int ModType;
            //public int Level;
            //public int Column;
            public string ModValue;
            public string ObjectId;
        }
        public static void Load(string fileName)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(Config.folderPath + "\\" + fileName, FileMode.Open)))
            {
                ObjectsFiles file = new ObjectsFiles();
                file.version = reader.ReadInt32();
                int OriginalTableCount = reader.ReadInt32();
                for (int i1 = 0; i1 < OriginalTableCount; i1++)
                {
                    Objects objects = new Objects();
                    objects.OriginalId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    objects.CustomId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    int ModCount = reader.ReadInt32();
                    for (int i2 = 0; i2 < ModCount; i2++)
                    {
                        Mods mod = new Mods();
                        mod.ModId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                        mod.ModType = reader.ReadInt32();//0=int, 1=real, 2=unreal, 3=string
                        //mod.Level = reader.ReadInt32();
                        //mod.Column = reader.ReadInt32();
                        mod.ModValue = ReadModificationValue(reader, mod.ModType);
                        mod.ObjectId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                        objects.ModsList.Add(objects.ModsList.Keys.LastOrDefault() + 1,mod);
                    }
                    file.OriginalTable.Add(file.OriginalTable.Keys.LastOrDefault() + 1, objects);
                }
                int CustomTableCount = reader.ReadInt32();
                for (int i3 = 0; i3 < CustomTableCount; i3++)
                {
                    Objects objects = new Objects();
                    objects.OriginalId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    objects.CustomId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    int ModCount = reader.ReadInt32();
                    for (int i4 = 0; i4 < ModCount; i4++)
                    {
                        Mods mod = new Mods();
                        mod.ModId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                        mod.ModType = reader.ReadInt32();//0=int, 1=real, 2=unreal, 3=string
                        //mod.Level = reader.ReadInt32();
                        //mod.Column = reader.ReadInt32();
                        mod.ModValue = ReadModificationValue(reader, mod.ModType);
                        mod.ObjectId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                        objects.ModsList.Add(objects.ModsList.Keys.LastOrDefault() + 1, mod);
                    }
                    file.CustomTable.Add(file.CustomTable.Keys.LastOrDefault() + 1, objects);
                }
                FilesList.Add(fileName, file);
            }
        }
        public static string ReadModificationValue(BinaryReader reader, int type)
        {
            string result = "";//0=int, 1=real, 2=unreal, 3=string
            switch (type)
            {
                case 0:
                    result = reader.ReadInt32().ToString();
                    break;
                case 1:
                    result = reader.ReadSingle().ToString();
                    break;
                case 2:
                    result = reader.ReadInt32().ToString();
                    break;
                case 3:
                    Dictionary<int, byte> Bytes = new Dictionary<int, byte>();
                    while (true)
                    {
                        byte v = reader.ReadByte();
                        if (v == 0x00)
                        {
                            break;
                        }
                        else
                        {
                            Bytes.Add(Bytes.Count, v);
                        }
                    }
                    byte[] bytes = new byte[Bytes.Count];
                    for (int i = 0; i < Bytes.Count; i++)
                    {
                        bytes[i] = Bytes[i];
                    }
                    result = Encoding.UTF8.GetString(bytes);
                    //result = reader.ReadString();//00 - end of strings
                    //MessageBox.Show(result);
                    break;
            }
            return result;
        }
    }
}