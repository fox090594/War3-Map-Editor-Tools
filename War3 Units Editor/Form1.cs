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

        private string folderPath = "";

        private void button1_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select the output folder";
                //dialog.UseDescriptionForTitle = true;
                dialog.SelectedPath = @"C:\"; // Sets the initial directory

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    folderPath = dialog.SelectedPath;
                    textBox1.Text = folderPath;
                }
            }
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
            UnitsEditor.strings.ReadFiles(folderPath);
            UnitsEditor.UnitData.Load(folderPath);
            UnitsEditor.ItemData.Load(folderPath);
            UnitsEditor.UpgradeData.Load(folderPath);
            UnitsEditor.UnitAbilities.Load(folderPath);
            UnitsEditor.AbilityBuffData.Load(folderPath);
            UnitsEditor.UnitWeapons.Load(folderPath);
            UnitsEditor.UnitUI.Load(folderPath);
            UnitsEditor.UnitBalance.Load(folderPath);
            UnitsEditor.AbilityData.Load(folderPath);
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
            for (int i = 0; i < UnitsEditor.strings.files.Length; i++)
            {
                var parser = new FileIniDataParser();
                //IniData data = parser.ReadFile(folderPath + "\\" + strings.files[i] + ".txt");
                //string value1 = data["xxxx"]["xxxx"];// IniParser.Exceptions.ParsingException
                try
                {
                    IniData data = parser.ReadFile(folderPath + "\\" + UnitsEditor.strings.files[i] + ".txt");
                    string value1 = data["xxxx"]["xxxx"];
                }
                catch (Exception ex)
                {
                    // Displays just the error description
                    MessageBox.Show(ex.ToString(), "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
    }
}
