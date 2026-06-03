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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static War3_Map_Editor_Tools.war3mapUnits;

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
                if (Config.folderPath.Length > 3)
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
            using (BinaryReader reader = new BinaryReader(File.Open(Config.folderPath + "\\war3map.w3u", FileMode.Open)))
            {
                int version = reader.ReadInt32();
                int countTable = reader.ReadInt32();
                for (int i1 = 0; i1 < countTable; i1++)
                {
                    string OriginalId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    ListViewItem item = new ListViewItem(OriginalId.ToString());
                    string CustomId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    int ModCount = reader.ReadInt32();
                    item.SubItems.Add(CustomId.ToString());
                    for (int i2 = 0; i2 < ModCount; i2++)
                    {
                        string ModId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                        
                        int ModType = reader.ReadInt32();//0=int, 1=real, 2=unreal, 3=string
                        //MessageBox.Show(ModId + " Type:" + ModType);
                        //int Level = reader.ReadInt32();
                        //int Column = reader.ReadInt32();
                        string ModValue = ReadModificationValue(reader, ModType);
                        string ObjectId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    }
                    listView10.Items.Add(item);
                }
                int countTable2 = reader.ReadInt32();
                for (int i3 = 0; i3 < countTable2; i3++)
                {
                    string OriginalId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    ListViewItem item = new ListViewItem(OriginalId.ToString());
                    string CustomId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    int ModCount = reader.ReadInt32();
                    item.SubItems.Add(CustomId.ToString());
                    for (int i4 = 0; i4 < ModCount; i4++)
                    {
                        string ModId = Encoding.ASCII.GetString(reader.ReadBytes(4));

                        int ModType = reader.ReadInt32();//0=int, 1=real, 2=unreal, 3=string
                        //MessageBox.Show(ModId + " Type:" + ModType);
                        //int Level = reader.ReadInt32();
                        //int Column = reader.ReadInt32();
                        string ModValue = ReadModificationValue(reader, ModType);
                        string ObjectId = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    }
                    listView10.Items.Add(item);
                }
                label42.Text = "Counts: " + "Standart: " + countTable + " Modded: " + countTable2;
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
            war3mapUnits.List.Clear();
            war3mapUnits.Load(Config.folderPath + "\\war3mapUnits.doo");
            foreach (int i1 in war3mapUnits.List.Keys)
            {
                ListViewItem item = new ListViewItem(i1.ToString());
                item.SubItems.Add(war3mapUnits.List[i1].OriginalId);
                listView11.Items.Add(item);
            }
            listView12.Items.Clear();
            listView13.Items.Clear();
            listView14.Items.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(Reverse(textBox8.Text));
            Int32 v = BitConverter.ToInt32(bytes, 0);
            textBox9.Text = v.ToString();
        }

        private void listView11_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView12.Items.Clear();
            listView13.Items.Clear();
            listView14.Items.Clear();
            if (this.listView11.Items.Count != 0)
            {
                if (this.listView11.SelectedItems.Count != 0)
                {
                    int id = Int32.Parse(listView11.Items[this.listView11.SelectedItems[0].Index].SubItems[0].Text);
                    //int id = this.listView11.SelectedItems[0].Index + 1;
                    textBox10.Text = war3mapUnits.List[id].OriginalId;
                    textBox11.Text = war3mapUnits.List[id].variation.ToString();
                    textBox12.Text = war3mapUnits.List[id].PositionX.ToString();
                    textBox13.Text = war3mapUnits.List[id].PositionY.ToString();
                    textBox14.Text = war3mapUnits.List[id].PositionZ.ToString();
                    textBox15.Text = war3mapUnits.List[id].Rotation.ToString();
                    textBox16.Text = war3mapUnits.List[id].ScaleX.ToString();
                    textBox17.Text = war3mapUnits.List[id].ScaleY.ToString();
                    textBox18.Text = war3mapUnits.List[id].ScaleZ.ToString();
                    textBox19.Text = war3mapUnits.List[id].Flags.ToString();
                    textBox20.Text = war3mapUnits.List[id].PlayerNum.ToString();
                    textBox21.Text = war3mapUnits.List[id].Hit.ToString();
                    textBox22.Text = war3mapUnits.List[id].Mana.ToString();
                    textBox29.Text = war3mapUnits.List[id].RandomFlag.ToString();
                    textBox30.Text = war3mapUnits.List[id].UnitColor.ToString();
                    textBox23.Text = war3mapUnits.List[id].Gold.ToString();
                    textBox24.Text = war3mapUnits.List[id].TargetAcquisition.ToString();
                    textBox25.Text = war3mapUnits.List[id].HeroLevel.ToString();
                    textBox26.Text = war3mapUnits.List[id].Strength.ToString();
                    textBox27.Text = war3mapUnits.List[id].Agility.ToString();
                    textBox28.Text = war3mapUnits.List[id].Intelligence.ToString();
                    textBox31.Text = war3mapUnits.List[id].Waygate.ToString();
                    foreach (int i1 in war3mapUnits.List[id].DropList.Keys)
                    {
                        ListViewItem item = new ListViewItem(war3mapUnits.List[id].DropList[i1].DropItemId);
                        item.SubItems.Add(war3mapUnits.List[id].DropList[i1].DropChance.ToString());
                        listView12.Items.Add(item);
                    }
                    foreach (int i2 in war3mapUnits.List[id].InvList.Keys)
                    {
                        ListViewItem item = new ListViewItem(war3mapUnits.List[id].InvList[i2].InvSlot.ToString());
                        item.SubItems.Add(war3mapUnits.List[id].InvList[i2].InvItemId.ToString());
                        listView13.Items.Add(item);
                    }
                    foreach (int i3 in war3mapUnits.List[id].AbilityList.Keys)
                    {
                        ListViewItem item = new ListViewItem(war3mapUnits.List[id].AbilityList[i3].AbilityId);
                        item.SubItems.Add(war3mapUnits.List[id].AbilityList[i3].Abilitylevel.ToString());
                        listView14.Items.Add(item);
                    }
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (this.listView11.Items.Count != 0)
            {
                if (this.listView11.SelectedItems.Count != 0)
                {
                    int id = Int32.Parse(listView11.Items[this.listView11.SelectedItems[0].Index].SubItems[0].Text);
                    war3mapUnits.List.Remove(id);
                }
            }
            listView11.Items.Clear();
            listView12.Items.Clear();
            listView13.Items.Clear();
            listView14.Items.Clear();
            foreach (int i1 in war3mapUnits.List.Keys)
            {
                ListViewItem item = new ListViewItem(i1.ToString());
                item.SubItems.Add(war3mapUnits.List[i1].OriginalId);
                listView11.Items.Add(item);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            war3mapUnits.Write(Config.folderPath + "\\war3mapUnits.doo");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            war3mapUnits.Add();
            listView11.Items.Clear();
            foreach (int i1 in war3mapUnits.List.Keys)
            {
                ListViewItem item = new ListViewItem(i1.ToString());
                item.SubItems.Add(war3mapUnits.List[i1].OriginalId);
                listView11.Items.Add(item);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (this.listView11.Items.Count != 0)
            {
                if (this.listView11.SelectedItems.Count != 0)
                {
                    int SelIndex = this.listView11.SelectedItems[0].Index;
                    int id = Int32.Parse(listView11.Items[SelIndex].SubItems[0].Text);
                    war3mapUnits.List[id].OriginalId = textBox10.Text;
                    war3mapUnits.List[id].variation = Int32.Parse(textBox11.Text);
                    war3mapUnits.List[id].PositionX = Single.Parse(textBox12.Text);
                    war3mapUnits.List[id].PositionY = Single.Parse(textBox13.Text);
                    war3mapUnits.List[id].PositionZ = Single.Parse(textBox14.Text);
                    war3mapUnits.List[id].Rotation = Single.Parse(textBox15.Text);
                    war3mapUnits.List[id].ScaleX = Single.Parse(textBox16.Text);
                    war3mapUnits.List[id].ScaleY = Single.Parse(textBox17.Text);
                    war3mapUnits.List[id].ScaleZ = Single.Parse(textBox18.Text);
                    war3mapUnits.List[id].Flags = Byte.Parse(textBox19.Text);
                    war3mapUnits.List[id].PlayerNum = Int32.Parse(textBox20.Text);
                    war3mapUnits.List[id].Hit = Int32.Parse(textBox21.Text);
                    war3mapUnits.List[id].Mana = Int32.Parse(textBox22.Text);
                    war3mapUnits.List[id].RandomFlag = Int32.Parse(textBox29.Text);
                    war3mapUnits.List[id].UnitColor = Int32.Parse(textBox30.Text);
                    war3mapUnits.List[id].Gold = Int32.Parse(textBox23.Text);
                    war3mapUnits.List[id].TargetAcquisition = Int32.Parse(textBox24.Text);
                    war3mapUnits.List[id].HeroLevel = Int32.Parse(textBox25.Text);
                    war3mapUnits.List[id].Strength = Int32.Parse(textBox26.Text);
                    war3mapUnits.List[id].Agility = Int32.Parse(textBox27.Text);
                    war3mapUnits.List[id].Intelligence = Int32.Parse(textBox28.Text);
                    war3mapUnits.List[id].Waygate = Int32.Parse(textBox31.Text);
                    listView11.Items.Clear();
                    foreach (int i1 in war3mapUnits.List.Keys)
                    {
                        ListViewItem item = new ListViewItem(i1.ToString());
                        item.SubItems.Add(war3mapUnits.List[i1].OriginalId);
                        listView11.Items.Add(item);
                    }
                    listView11.Items[SelIndex].Selected = true;
                    listView11.Items[SelIndex].Focused = true;
                    listView11.Items[SelIndex].EnsureVisible();
                }
            }
        }

    }
}