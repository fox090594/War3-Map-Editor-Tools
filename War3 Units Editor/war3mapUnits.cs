using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static War3_Map_Editor_Tools.UnitsEditor.AbilityBuffData;
using static War3_Map_Editor_Tools.UnitsEditor.UnitData;

namespace War3_Map_Editor_Tools
{
    public class war3mapUnits
    {
        public static string fileID = "W3do";
        public static int version = 8;
        public static int subversion = 11;
        public static Dictionary<int, UnitsDoo> List = new Dictionary<int, UnitsDoo>();
        public class UnitsDoo
        {
            public string OriginalId;
            public int variation;
            public float PositionX;
            public float PositionY;
            public float PositionZ;
            public float Rotation;
            public float ScaleX;
            public float ScaleY;
            public float ScaleZ;
            //string OriginalId2;//Reforged?
            public byte Flags;
            public int PlayerNum;
            public byte b1;
            public byte b2;
            public int Hit;
            public int Mana;
            public int DroppedItemSetPointer;
            public Dictionary<int, DropTable> DropList = new Dictionary<int, DropTable>();
            public int Gold;
            public float TargetAcquisition;
            public int HeroLevel;
            public int Strength;
            public int Agility;
            public int Intelligence;
            public int InvCount;
            public Dictionary<int, InvItem> InvList = new Dictionary<int, InvItem>();
            public Dictionary<int, Ability> AbilityList = new Dictionary<int, Ability>();
            public int RandomFlag;
            public byte[] b3;//
            public int UnitColor;
            public int Waygate;
            public int UnitId;
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
        public static void Write(string path)
        {
            int startindex = 1;
            File.Move(path, path + ".backup");
            using (BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                bw.Write(Encoding.ASCII.GetBytes(fileID));
                bw.Write((int)version);
                bw.Write((int)subversion);
                bw.Write((int)List.Count);
                foreach (int i1 in war3mapUnits.List.Keys)
                {
                    bw.Write(Encoding.ASCII.GetBytes(List[i1].OriginalId));
                    bw.Write((int)List[i1].variation);
                    bw.Write((float)List[i1].PositionX);
                    bw.Write((float)List[i1].PositionY);
                    bw.Write((float)List[i1].PositionZ);
                    bw.Write((float)List[i1].Rotation);
                    bw.Write((float)List[i1].ScaleX);
                    bw.Write((float)List[i1].ScaleY);
                    bw.Write((float)List[i1].ScaleZ);
                    bw.Write((byte)List[i1].Flags);
                    bw.Write((int)List[i1].PlayerNum);
                    bw.Write((byte)List[i1].b1);
                    bw.Write((byte)List[i1].b2);
                    bw.Write((int)List[i1].Hit);
                    bw.Write((int)List[i1].Mana);
                    bw.Write((int)List[i1].DroppedItemSetPointer);

                    bw.Write((int)List[i1].DropList.Count);
                    foreach (int i2 in List[i1].DropList.Keys)
                    {
                        bw.Write(Encoding.ASCII.GetBytes(List[i1].DropList[i2].DropItemId));
                        bw.Write((int)List[i1].DropList[i2].DropChance);
                    }

                    bw.Write((int)List[i1].Gold);
                    bw.Write((float)List[i1].TargetAcquisition);
                    bw.Write((int)List[i1].HeroLevel);
                    bw.Write((int)List[i1].Strength);
                    bw.Write((int)List[i1].Agility);
                    bw.Write((int)List[i1].Intelligence);

                    bw.Write((int)List[i1].InvList.Count);
                    foreach (int i3 in List[i1].InvList.Keys)
                    {
                        bw.Write((int)List[i1].InvList[i3].InvSlot);
                        bw.Write(Encoding.ASCII.GetBytes(List[i1].InvList[i3].InvItemId));
                    }

                    bw.Write((int)List[i1].AbilityList.Count);
                    foreach (int i3 in List[i1].AbilityList.Keys)
                    {
                        bw.Write(Encoding.ASCII.GetBytes(List[i1].AbilityList[i3].AbilityId));
                        bw.Write((int)List[i1].AbilityList[i3].Autocast);
                        bw.Write((int)List[i1].AbilityList[i3].Abilitylevel);
                    }

                    bw.Write((int)List[i1].RandomFlag);
                    bw.Write(List[i1].b3);
                    bw.Write((int)List[i1].UnitColor);
                    bw.Write((int)List[i1].Waygate);
                    bw.Write((int)startindex);//UnitId
                    startindex = startindex + 1;
                }
                //bw.Write((byte)(startindex-1));
            }
            MessageBox.Show("Done.");
        }
        public static void Add(int player, string id, float x, float y, float z)
        {
            UnitsDoo unit = new UnitsDoo();
            unit.OriginalId = id;
            unit.variation = 0;
            unit.PositionX = x;
            unit.PositionY = y;
            unit.PositionZ = z;
            unit.Rotation = 0.0f;
            unit.ScaleX = 1.0f;
            unit.ScaleY = 1.0f;
            unit.ScaleZ = 1.0f;
            unit.Flags = 2;
            unit.PlayerNum = player;
            unit.b1 = 0;
            unit.b2 = 0;
            unit.Hit = -1;
            unit.Mana = -1;
            unit.DroppedItemSetPointer = -1;

            unit.Gold = 12500;
            unit.TargetAcquisition = -1;
            unit.HeroLevel = 1;
            unit.Strength = 0;
            unit.Agility = 0;
            unit.Intelligence = 0;

            unit.RandomFlag = 0;
            unit.b3 = new byte[] { 0x01, 0x00, 0x00, 0x00 };
            unit.UnitColor = -1;
            unit.Waygate = -1;
            unit.UnitId = List.Keys.LastOrDefault() + 1;//List index 1+
            List.Add(List.Keys.LastOrDefault() + 1, unit);
        }
        public static void Add()
        {
            UnitsDoo unit = new UnitsDoo();
            unit.OriginalId = "XXXX";
            unit.variation = 0;
            unit.PositionX = 0.0f;
            unit.PositionY = 0.0f;
            unit.PositionZ = 0.0f;
            unit.Rotation = 0.0f;
            unit.ScaleX = 1.0f;
            unit.ScaleY = 1.0f;
            unit.ScaleZ = 1.0f;
            unit.Flags = 2;
            unit.PlayerNum = 15;
            unit.b1 = 0;
            unit.b2 = 0;
            unit.Hit = -1;
            unit.Mana = -1;
            unit.DroppedItemSetPointer = -1;

            unit.Gold = 12500;
            unit.TargetAcquisition = -1;
            unit.HeroLevel = 1;
            unit.Strength = 0;
            unit.Agility = 0;
            unit.Intelligence = 0;

            unit.RandomFlag = 0;
            unit.b3 = new byte[] { 0x01, 0x00, 0x00, 0x00 };
            unit.UnitColor = -1;
            unit.Waygate = -1;
            unit.UnitId = List.Keys.LastOrDefault() + 1;//List index 1+
            List.Add(List.Keys.LastOrDefault() + 1, unit);
        }
        public static void Load(string path)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                fileID = Encoding.ASCII.GetString(reader.ReadBytes(4));
                version = reader.ReadInt32();
                subversion = reader.ReadInt32();
                int countTable = reader.ReadInt32();
                for (int i1 = 0; i1 < countTable; i1++)
                {
                    UnitsDoo unit = new UnitsDoo();
                    unit.OriginalId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    unit.variation = reader.ReadInt32();
                    unit.PositionX = reader.ReadSingle();
                    unit.PositionY = reader.ReadSingle();
                    unit.PositionZ = reader.ReadSingle();
                    unit.Rotation = reader.ReadSingle();
                    unit.ScaleX = reader.ReadSingle();
                    unit.ScaleY = reader.ReadSingle();
                    unit.ScaleZ = reader.ReadSingle();
                    //string OriginalId2 = Encoding.ASCII.GetString(reader.ReadBytes(4));//Reforged?
                    unit.Flags = reader.ReadByte();
                    unit.PlayerNum = reader.ReadInt32();
                    unit.b1 = reader.ReadByte();
                    unit.b2 = reader.ReadByte();
                    unit.Hit = reader.ReadInt32();
                    unit.Mana = reader.ReadInt32();
                    unit.DroppedItemSetPointer = reader.ReadInt32();
                    int DropTableCount = reader.ReadInt32();
                    for (int i2 = 0; i2 < DropTableCount; i2++)
                    {
                        DropTable drop = new DropTable();
                        drop.DropItemId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                        drop.DropChance = reader.ReadInt32();
                        unit.DropList.Add(unit.DropList.Keys.LastOrDefault() + 1, drop);
                    }
                    unit.Gold = reader.ReadInt32();
                    unit.TargetAcquisition = reader.ReadSingle();
                    unit.HeroLevel = reader.ReadInt32();
                    unit.Strength = reader.ReadInt32();
                    unit.Agility = reader.ReadInt32();
                    unit.Intelligence = reader.ReadInt32();
                    int InvCount = reader.ReadInt32();
                    for (int i3 = 0; i3 < InvCount; i3++)
                    {
                        InvItem inv = new InvItem();
                        inv.InvSlot = reader.ReadInt32();
                        inv.InvItemId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                        unit.InvList.Add(unit.InvList.Keys.LastOrDefault() + 1, inv);
                    }
                    int ModAbilityCount = reader.ReadInt32();
                    for (int i4 = 0; i4 < ModAbilityCount; i4++)
                    {
                        Ability ability = new Ability();
                        ability.AbilityId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                        ability.Autocast = reader.ReadInt32();
                        ability.Abilitylevel = reader.ReadInt32();
                        unit.AbilityList.Add(unit.AbilityList.Keys.LastOrDefault() + 1, ability);
                    }
                    unit.RandomFlag = reader.ReadInt32();
                    unit.b3 = reader.ReadBytes(4);//
                    unit.UnitColor = reader.ReadInt32();
                    unit.Waygate = reader.ReadInt32();
                    unit.UnitId = reader.ReadInt32();//List index 1+
                    List.Add(List.Keys.LastOrDefault() + 1, unit);
                }
            }
        }
    }
}
