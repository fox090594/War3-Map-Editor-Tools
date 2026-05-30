using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War3_Map_Editor_Tools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SelectFolder()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select the output folder";
                //dialog.UseDescriptionForTitle = true;
                if(Config.folderPath.Length > 3)
                {
                    dialog.SelectedPath = Config.folderPath;
                }
                else
                {
                    dialog.SelectedPath = @"C:\";
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Config.folderPath = dialog.SelectedPath;
                    textBox1.Text = Config.folderPath;
                    textBox7.Text = Config.folderPath;
                    Config.Write();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectFolder();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            listView3.Items.Clear();
            listView4.Items.Clear();
            listView5.Items.Clear();
            listView6.Items.Clear();
            listView7.Items.Clear();
            listView8.Items.Clear();
            listView9.Items.Clear();
            UnitsEditor.strings.ReadFiles(Config.folderPath + "\\Units");
            UnitsEditor.UnitData.Load(Config.folderPath + "\\Units");
            UnitsEditor.ItemData.Load(Config.folderPath + "\\Units");
            UnitsEditor.UpgradeData.Load(Config.folderPath + "\\Units");
            UnitsEditor.UnitAbilities.Load(Config.folderPath + "\\Units");
            UnitsEditor.AbilityBuffData.Load(Config.folderPath + "\\Units");
            UnitsEditor.UnitWeapons.Load(Config.folderPath + "\\Units");
            UnitsEditor.UnitUI.Load(Config.folderPath + "\\Units");
            UnitsEditor.UnitBalance.Load(Config.folderPath + "\\Units");
            UnitsEditor.AbilityData.Load(Config.folderPath + "\\Units");
            foreach (int i1 in UnitsEditor.UnitData.List.Keys)
            //for (int i1 = 1; i1 < UnitsEditor.UnitData.List.Count + 1; i1++)
            {
                ListViewItem item = new ListViewItem(UnitsEditor.UnitData.List[i1].id.ToString());
                item.SubItems.Add(UnitsEditor.UnitData.List[i1].Index);
                item.SubItems.Add(UnitsEditor.UnitData.List[i1].Name);
                listView1.Items.Add(item);
            }
            foreach (int i2 in UnitsEditor.ItemData.List.Keys)
            //for (int i2 = 1; i2 < UnitsEditor.ItemData.List.Count + 1; i2++)
            {
                ListViewItem item = new ListViewItem(UnitsEditor.ItemData.List[i2].id.ToString());
                item.SubItems.Add(UnitsEditor.ItemData.List[i2].Index);
                item.SubItems.Add(UnitsEditor.ItemData.List[i2].Name);
                listView2.Items.Add(item);
            }
            foreach (int i3 in UnitsEditor.UpgradeData.List.Keys)//for (int i3 = 1; i3 < UnitsEditor.UpgradeData.List.Count + 1; i3++)
            {
                ListViewItem item = new ListViewItem(UnitsEditor.UpgradeData.List[i3].id.ToString());
                item.SubItems.Add(UnitsEditor.UpgradeData.List[i3].Index);
                item.SubItems.Add(UnitsEditor.UpgradeData.List[i3].Name);
                listView3.Items.Add(item);
            }
            foreach (int i4 in UnitsEditor.UnitAbilities.List.Keys)//for (int i4 = 1; i4 < UnitsEditor.UnitAbilities.List.Count + 1; i4++)
            {
                ListViewItem item = new ListViewItem(UnitsEditor.UnitAbilities.List[i4].id.ToString());
                item.SubItems.Add(UnitsEditor.UnitAbilities.List[i4].Index);
                item.SubItems.Add(UnitsEditor.UnitAbilities.List[i4].Name);
                listView4.Items.Add(item);
            }
            foreach (int i5 in UnitsEditor.AbilityBuffData.List.Keys) //for (int i5 = 1; i5 < UnitsEditor.AbilityBuffData.List.Count + 1; i5++)
            { 
                ListViewItem item = new ListViewItem(UnitsEditor.AbilityBuffData.List[i5].id.ToString());
                item.SubItems.Add(UnitsEditor.AbilityBuffData.List[i5].Index);
                item.SubItems.Add(UnitsEditor.AbilityBuffData.List[i5].Bufftip);
                listView5.Items.Add(item);
            }
            foreach (int i6 in UnitsEditor.UnitWeapons.List.Keys) //for (int i6 = 1; i6 < UnitsEditor.UnitWeapons.List.Count + 1; i6++)
            {
                ListViewItem item = new ListViewItem(UnitsEditor.UnitWeapons.List[i6].id.ToString());
                item.SubItems.Add(UnitsEditor.UnitWeapons.List[i6].Index);
                item.SubItems.Add(UnitsEditor.UnitWeapons.List[i6].Name);
                listView6.Items.Add(item);
            }
            foreach (int i7 in UnitsEditor.UnitUI.List.Keys) //for (int i7 = 1; i7 < UnitsEditor.UnitUI.List.Count + 1; i7++)
            {
                ListViewItem item = new ListViewItem(UnitsEditor.UnitUI.List[i7].id.ToString());
                item.SubItems.Add(UnitsEditor.UnitUI.List[i7].Index);
                item.SubItems.Add(UnitsEditor.UnitUI.List[i7].Name);
                listView7.Items.Add(item);
            }
            foreach (int i8 in UnitsEditor.UnitBalance.List.Keys) //for (int i8 = 1; i8 < UnitsEditor.UnitBalance.List.Count + 1; i8++)
            {
                ListViewItem item = new ListViewItem(UnitsEditor.UnitBalance.List[i8].id.ToString());
                item.SubItems.Add(UnitsEditor.UnitBalance.List[i8].Index);
                item.SubItems.Add(UnitsEditor.UnitBalance.List[i8].Name);
                listView8.Items.Add(item);
            }
            foreach (int i9 in UnitsEditor.AbilityData.List.Keys) //for (int i9 = 1; i9 < UnitsEditor.AbilityData.List.Count + 1; i9++)
            {
                ListViewItem item = new ListViewItem(UnitsEditor.AbilityData.List[i9].id.ToString());
                item.SubItems.Add(UnitsEditor.AbilityData.List[i9].Index);
                item.SubItems.Add(UnitsEditor.AbilityData.List[i9].Name);
                listView9.Items.Add(item);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool NotError = true;
            for (int i = 0; i < UnitsEditor.strings.files.Length; i++)
            {
                var parser = new FileIniDataParser();
                //IniData data = parser.ReadFile(folderPath + "\\" + strings.files[i] + ".txt");
                //string value1 = data["xxxx"]["xxxx"];// IniParser.Exceptions.ParsingException
                try
                {
                    IniData data = parser.ReadFile(Config.folderPath + "\\Units\\" + UnitsEditor.strings.files[i] + ".txt");
                    string value1 = data["xxxx"]["xxxx"];
                }
                catch (Exception ex)
                {
                    NotError = false;
                    // Displays just the error description
                    MessageBox.Show(ex.ToString(), "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (NotError)
            {
                MessageBox.Show("String files are OK!");
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.Items.Count != 0)
            {
                if (this.listView1.SelectedItems.Count != 0)
                {
                    int id = Int32.Parse(listView1.Items[this.listView1.SelectedItems[0].Index].SubItems[0].Text);
                    richTextBox1.Text = UnitsEditor.UnitData.List[id].Name;
                    richTextBox2.Text = UnitsEditor.UnitData.List[id].Ubertip;
                    richTextBox3.Text = UnitsEditor.UnitData.List[id].Researchtip;
                    richTextBox4.Text = UnitsEditor.UnitData.List[id].Researchubertip;
                    richTextBox5.Text = UnitsEditor.UnitData.List[id].Tip;
                    richTextBox6.Text = UnitsEditor.UnitData.List[id].Researchhotkey;
                    richTextBox7.Text = UnitsEditor.UnitData.List[id].Hotkey;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                textBox2.Text = file;
                using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
                {
                    byte[] bytes = reader.ReadBytes(512);
                    int lastb = IO.LastBytes(bytes);
                    string type = Encoding.ASCII.GetString(IO.GetBytes(bytes, 0, 4));
                    byte[] unknown = IO.GetBytes(bytes, 4, 4);
                    string Name = Encoding.ASCII.GetString(IO.GetBytes(bytes, 8, lastb - 9));
                    int mapFlags = BitConverter.ToInt32(bytes, lastb - 5);
                    int PlayersCount = BitConverter.ToInt32(bytes, lastb - 1);
                    textBox3.Text = Name;
                    textBox4.Text = PlayersCount.ToString();
                    textBox5.Text = mapFlags.ToString();
                    textBox6.Text = type;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string path = textBox2.Text;
            if (File.Exists(path))
            {
                string extension = Path.GetExtension(path);
                string file = (path).Replace(extension, "") + "_renamed" + extension;
                File.Copy(path, file);
                if (File.Exists(file))
                {
                    using (BinaryWriter writer = new BinaryWriter(File.Open(file, FileMode.OpenOrCreate)))
                    {
                        for (int i = 0; i < 512; i++)
                        {
                            writer.Write((byte)0x00);
                        }
                    }
                    using (BinaryWriter writer = new BinaryWriter(File.Open(file, FileMode.OpenOrCreate)))
                    {
                        writer.Write(Encoding.ASCII.GetBytes(textBox6.Text));
                        writer.Write(0x00);
                        byte[] newname = Encoding.ASCII.GetBytes(textBox3.Text);
                        writer.Write(newname);
                        if (newname[newname.Length - 1] != (byte)0x00)
                        {
                            writer.Write((byte)0x00);
                        }
                        writer.Write(BitConverter.GetBytes(Int32.Parse(textBox5.Text)));
                        writer.Write(BitConverter.GetBytes(Int32.Parse(textBox4.Text)));
                    }
                    MessageBox.Show("Done. Renamed w3x name is: " + Path.GetFileName(file));
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UnitsEditor.strings.MergeFiles(Config.folderPath + "\\Units");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SelectFolder();
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(Config.folderPath + "\\war3map.w3a", FileMode.Open)))
            {
                int version = reader.ReadInt32();
                int countTable = reader.ReadInt32();
                for (int i1 = 0; i1 < countTable; i1++)
                {
                    string OriginalId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    ListViewItem item = new ListViewItem(OriginalId.ToString());
                    string CustomId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    int ModCount = reader.ReadInt32();
                    item.SubItems.Add(ModCount.ToString());
                    for (int i2 = 0; i2 < ModCount; i2++)
                    {
                        string ModId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                        int ModType = reader.ReadInt32();//0=int, 1=real, 2=unreal, 3=string
                        int Level = reader.ReadInt32();
                        int Column = reader.ReadInt32();
                        string ModValue = ReadModificationValue(reader, ModType);
                        string ObjectId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    }
                    listView10.Items.Add(item);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            uint dwordValue = UInt32.Parse(textBox9.Text); // Max DWORD value
            string hexValue = dwordValue.ToString("X");
            //byte[] bytes = Convert.FromHexString(hexValue);
            //string asciiString = Encoding.ASCII.GetString(bytes);
            textBox8.Text = HexToAscii(hexValue);//Reverse(HexToAscii(hexValue));
        }

        public static string ReadModificationValue(BinaryReader reader,int type)
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
                    result = reader.ReadString();
                    break;
            }
            return result;
        }
        public static string GetIDfromwar3map(int input)
        {
            string hexValue = input.ToString("X");
            return Reverse(HexToAscii(hexValue));
        }
        public static string HexToAscii(string hexString)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hexString.Length; i += 2)
            {
                // Get two characters, convert to base 16 (hex) byte, then to char
                string hs = hexString.Substring(i, 2);
                sb.Append(Convert.ToChar(Convert.ToUInt32(hs, 16)));
            }
            return sb.ToString();
        }
        public static string Reverse(string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Config.Load();
            textBox1.Text = Config.folderPath;
            textBox7.Text = Config.folderPath;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            listView11.Items.Clear();
            war3mapUnits.Load(Config.folderPath + "\\war3mapUnits.doo");
            using (BinaryReader reader = new BinaryReader(File.Open(Config.folderPath + "\\war3mapUnits.doo", FileMode.Open)))
            {
                string fileID = Encoding.ASCII.GetString(reader.ReadBytes(4));
                int version = reader.ReadInt32();
                int subversion = reader.ReadInt32();
                int countTable = reader.ReadInt32();
                for (int i1 = 0; i1 < countTable; i1++)
                {
                    string OriginalId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    MessageBox.Show(OriginalId); 
                    ListViewItem item = new ListViewItem(OriginalId.ToString());
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
                    for (int i3 = 0; i3 < DropTableCount; i3++)
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
                    MessageBox.Show(Gold.ToString());
                    listView11.Items.Add(item);
                }
            }
        }
    }
}
