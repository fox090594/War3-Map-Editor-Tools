using IniParser.Model;
using IniParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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
            UnitsEditor.strings.ReadFiles(folderPath);
            UnitsEditor.UnitData.Load(folderPath);
            UnitsEditor.ItemData.Load(folderPath);
            for (int i1 = 1; i1 < UnitsEditor.UnitData.List.Count + 1; i1++)
            {
                ListViewItem item = new ListViewItem(UnitsEditor.UnitData.List[i1].id.ToString());
                item.SubItems.Add(UnitsEditor.UnitData.List[i1].Index);
                item.SubItems.Add(UnitsEditor.UnitData.List[i1].Name);
                listView1.Items.Add(item);
            }
            for (int i2 = 1; i2 < UnitsEditor.ItemData.List.Count + 1; i2++)
            {
                ListViewItem item = new ListViewItem(UnitsEditor.ItemData.List[i2].id.ToString());
                item.SubItems.Add(UnitsEditor.ItemData.List[i2].Index);
                item.SubItems.Add(UnitsEditor.ItemData.List[i2].Name);
                listView2.Items.Add(item);
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
