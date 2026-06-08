using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
            public int Level;
            public int Column;
            public string ModValue;
            public string ObjectId;
        }
        public static bool EnableLevel(string fileName)
        {
            bool result = false;
            if (fileName.Contains("w3d"))
            {
                result = true;
            }
            if (fileName.Contains("w3a"))
            {
                result = true;
            }
            if (fileName.Contains("w3q"))
            {
                result = true;
            }
            return result;
        }
        public static int RemoveEmptyMods(string fileName)
        {
            int removed = 0;
            foreach (int i1 in FilesList[fileName].OriginalTable.Keys)
            {
                List<int> keysToRemove = new List<int>();
                foreach (int i2 in FilesList[fileName].OriginalTable[i1].ModsList.Keys)
                {
                    if (FilesList[fileName].OriginalTable[i1].ModsList[i2].ModType == 3)
                    {
                        if (FilesList[fileName].OriginalTable[i1].ModsList[i2].ModValue.Length < 1)
                        {
                            keysToRemove.Add(i2);
                        }
                    }
                }
                foreach (int KeyToRemove in keysToRemove)
                {
                    FilesList[fileName].OriginalTable[i1].ModsList.Remove(KeyToRemove);
                    removed++;
                }
            }
            foreach (int i3 in FilesList[fileName].CustomTable.Keys)
            {
                List<int> keysToRemove = new List<int>();
                foreach (int i4 in FilesList[fileName].CustomTable[i3].ModsList.Keys)
                {
                    if (FilesList[fileName].CustomTable[i3].ModsList[i4].ModType == 3)
                    {
                        if (FilesList[fileName].CustomTable[i3].ModsList[i4].ModValue.Length < 1)
                        {
                            keysToRemove.Add(i4);
                        }
                    }
                }
                foreach (int KeyToRemove in keysToRemove)
                {
                    FilesList[fileName].CustomTable[i3].ModsList.Remove(KeyToRemove);
                    removed++;
                }
            }
            return removed;
        }
        public static void Write(string fileName, bool SaveEmptyMods)
        {
            string path = Config.folderPath + "\\" + fileName;
            File.Move(path, path + ".backup");
            bool EnLevel = EnableLevel(fileName);
            string remove = "";
            if(SaveEmptyMods == false)
            {
                int removed = RemoveEmptyMods(fileName);
                remove = Environment.NewLine + "Removed " + removed + " empty mods fields.";
            }
            using (BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                bw.Write((int)FilesList[fileName].version);
                bw.Write((int)FilesList[fileName].OriginalTable.Count);
                foreach (int i1 in FilesList[fileName].OriginalTable.Keys)
                {
                    bw.Write(Encoding.UTF8.GetBytes(FilesList[fileName].OriginalTable[i1].OriginalId));
                    bw.Write(Encoding.UTF8.GetBytes(FilesList[fileName].OriginalTable[i1].CustomId));
                    bw.Write((int)FilesList[fileName].OriginalTable[i1].ModsList.Count);
                    foreach (int i2 in FilesList[fileName].OriginalTable[i1].ModsList.Keys)
                    {
                        bw.Write(Encoding.UTF8.GetBytes(FilesList[fileName].OriginalTable[i1].ModsList[i2].ModId));
                        bw.Write((int)FilesList[fileName].OriginalTable[i1].ModsList[i2].ModType);
                        if (EnLevel)
                        {
                            bw.Write((int)FilesList[fileName].OriginalTable[i1].ModsList[i2].Level);
                            bw.Write((int)FilesList[fileName].OriginalTable[i1].ModsList[i2].Column);
                        }
                        WriteModificationValue(bw, FilesList[fileName].OriginalTable[i1].ModsList[i2].ModType, FilesList[fileName].OriginalTable[i1].ModsList[i2].ModValue);
                        bw.Write(Encoding.UTF8.GetBytes(FilesList[fileName].OriginalTable[i1].ModsList[i2].ObjectId));
                    }
                }
                bw.Write((int)FilesList[fileName].CustomTable.Count);
                foreach (int i3 in FilesList[fileName].CustomTable.Keys)
                {
                    bw.Write(Encoding.UTF8.GetBytes(FilesList[fileName].CustomTable[i3].OriginalId));
                    bw.Write(Encoding.UTF8.GetBytes(FilesList[fileName].CustomTable[i3].CustomId));
                    bw.Write((int)FilesList[fileName].CustomTable[i3].ModsList.Count);
                    foreach (int i4 in FilesList[fileName].CustomTable[i3].ModsList.Keys)
                    {
                        bw.Write(Encoding.UTF8.GetBytes(FilesList[fileName].CustomTable[i3].ModsList[i4].ModId));
                        bw.Write((int)FilesList[fileName].CustomTable[i3].ModsList[i4].ModType);
                        if (EnLevel)
                        {
                            bw.Write((int)FilesList[fileName].CustomTable[i3].ModsList[i4].Level);
                            bw.Write((int)FilesList[fileName].CustomTable[i3].ModsList[i4].Column);
                        }
                        WriteModificationValue(bw, FilesList[fileName].CustomTable[i3].ModsList[i4].ModType, FilesList[fileName].CustomTable[i3].ModsList[i4].ModValue);
                        bw.Write(Encoding.UTF8.GetBytes(FilesList[fileName].CustomTable[i3].ModsList[i4].ObjectId));

                    }
                }
            }
            MessageBox.Show("Write Done! " + Environment.NewLine + "Old File move to: " + path + ".backup" + Environment.NewLine + "New File saved to: " + path + remove);
        }

        public static void Load(string fileName)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(Config.folderPath + "\\" + fileName, FileMode.Open)))
            {
                bool EnLevel = EnableLevel(fileName);

                ObjectsFiles file = new ObjectsFiles();
                file.version = reader.ReadInt32();
                int OriginalTableCount = reader.ReadInt32();
                for (int i1 = 0; i1 < OriginalTableCount; i1++)
                {
                    Objects objects = new Objects();
                    objects.OriginalId = Encoding.UTF8.GetString(reader.ReadBytes(4));
                    objects.CustomId = Encoding.UTF8.GetString(reader.ReadBytes(4));
                    int ModCount = reader.ReadInt32();
                    for (int i2 = 0; i2 < ModCount; i2++)
                    {
                        Mods mod = new Mods();
                        mod.ModId = Encoding.UTF8.GetString(reader.ReadBytes(4));
                        mod.ModType = reader.ReadInt32();//0=int, 1=real, 2=unreal, 3=string
                        if(EnLevel)
                        {
                            mod.Level = reader.ReadInt32();
                            mod.Column = reader.ReadInt32();
                        }
                        mod.ModValue = ReadModificationValue(reader, mod.ModType);
                        mod.ObjectId = Encoding.UTF8.GetString(reader.ReadBytes(4));
                        objects.ModsList.Add(objects.ModsList.Keys.LastOrDefault() + 1,mod);
                    }
                    file.OriginalTable.Add(file.OriginalTable.Keys.LastOrDefault() + 1, objects);
                }
                int CustomTableCount = reader.ReadInt32();
                for (int i3 = 0; i3 < CustomTableCount; i3++)
                {
                    Objects objects = new Objects();
                    objects.OriginalId = Encoding.UTF8.GetString(reader.ReadBytes(4));
                    objects.CustomId = Encoding.UTF8.GetString(reader.ReadBytes(4));
                    int ModCount = reader.ReadInt32();
                    for (int i4 = 0; i4 < ModCount; i4++)
                    {
                        Mods mod = new Mods();
                        mod.ModId = Encoding.UTF8.GetString(reader.ReadBytes(4));
                        mod.ModType = reader.ReadInt32();//0=int, 1=real, 2=unreal, 3=string
                        if (EnLevel)
                        {
                            mod.Level = reader.ReadInt32();
                            mod.Column = reader.ReadInt32();
                        }
                        mod.ModValue = ReadModificationValue(reader, mod.ModType);
                        mod.ObjectId = Encoding.UTF8.GetString(reader.ReadBytes(4));
                        objects.ModsList.Add(objects.ModsList.Keys.LastOrDefault() + 1, mod);
                    }
                    file.CustomTable.Add(file.CustomTable.Keys.LastOrDefault() + 1, objects);
                }
                FilesList.Add(fileName, file);
            }
        }
        public static void WriteModificationValue(BinaryWriter bw, int type, string value)
        {
            //0=int, 1=real, 2=unreal, 3=string
            switch (type)
            {
                case 0:
                    bw.Write((int)Int32.Parse(value));
                    break;
                case 1:
                    //MessageBox.Show(value);
                    bw.Write((float)Single.Parse(value));
                    break;
                case 2:
                    bw.Write((int)Int32.Parse(value));
                    break;
                case 3:
                    if (value.Length > 0)
                    {
                        bw.Write(Encoding.UTF8.GetBytes(value));
                    }
                    bw.Write((byte)0x00);
                    break;
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