using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War3_Map_Editor_Tools
{
    public class war3mapUnits
    {
        public static string fileID = "W3do";
        public static int version = 8;
        public static int subversion = 11;
        public class UnitsDoo
        {

        }
        public class DropTable
        {
            public string DropItemId;
            public int DropChance;
        }
        public class InvItem
        {
            public int InvSlot;
            public string InvItemId;
        }
        public class Ability
        {
            public string AbilityId;
            public int Autocast;
            public int Abilitylevel;
        }
        public static void Load(string path)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                string fileID = Encoding.ASCII.GetString(reader.ReadBytes(4));
                int version = reader.ReadInt32();
                int subversion = reader.ReadInt32();
                int countTable = reader.ReadInt32();
                for (int i1 = 0; i1 < countTable; i1++)
                {
                    string OriginalId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    int variation = reader.ReadInt32();
                    float PositionX = reader.ReadSingle();
                    float PositionY = reader.ReadSingle();
                    float PositionZ = reader.ReadSingle();
                    float Rotation = reader.ReadSingle();
                    float ScaleX = reader.ReadSingle();
                    float ScaleY = reader.ReadSingle();
                    float ScaleZ = reader.ReadSingle();
                    //string OriginalId2 = Encoding.ASCII.GetString(reader.ReadBytes(4));//Reforged?
                    byte Flags = reader.ReadByte();
                    int PlayerNum = reader.ReadInt32();
                    byte b1 = reader.ReadByte();
                    byte b2 = reader.ReadByte();
                    int Hit = reader.ReadInt32();
                    int Mana = reader.ReadInt32();
                    int DroppedItemSetPointer = reader.ReadInt32();
                    int DropTableCount = reader.ReadInt32();
                    //item.SubItems.Add(OriginalId2);
                    for (int i2 = 0; i2 < DropTableCount; i2++)
                    {
                        string DropItemId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                        int DropChance = reader.ReadInt32();
                    }
                    int Gold = reader.ReadInt32();
                    float TargetAcquisition = reader.ReadSingle();
                    int HeroLevel = reader.ReadInt32();
                    int Strength = reader.ReadInt32();
                    int Agility = reader.ReadInt32();
                    int Intelligence = reader.ReadInt32();
                    int InvCount = reader.ReadInt32();
                    for (int i3 = 0; i3 < InvCount; i3++)
                    {
                        int InvSlot = reader.ReadInt32();
                        string InvItemId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    }
                    int ModAbilityCount = reader.ReadInt32();
                    for (int i4 = 0; i4 < ModAbilityCount; i4++)
                    {
                        string AbilityId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                        int Autocast = reader.ReadInt32();
                        int Abilitylevel = reader.ReadInt32();
                    }
                    int RandomFlag = reader.ReadInt32();
                    reader.ReadBytes(4);//
                    int UnitColor = reader.ReadInt32();
                    int Waygate = reader.ReadInt32();
                    int UnitId = reader.ReadInt32();
                }
            }
        }
    }
}
