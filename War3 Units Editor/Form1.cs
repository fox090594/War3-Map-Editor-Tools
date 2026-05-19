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
using War3_Units_Editor.UnitsEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net;

namespace War3_Units_Editor
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
            UnitsEditor.strings.ReadFiles(folderPath);
            UnitsEditor.UnitData.Load(folderPath);
            for (int i = 1; i < UnitsEditor.UnitData.List.Count + 1; i++)
            {
                ListViewItem item = new ListViewItem(UnitsEditor.UnitData.List[i].id.ToString());
                item.SubItems.Add(UnitsEditor.UnitData.List[i].Index);
                item.SubItems.Add(UnitsEditor.UnitData.List[i].Name);
                listView1.Items.Add(item);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < strings.files.Length; i++)
            {
                var parser = new FileIniDataParser();
                //IniData data = parser.ReadFile(folderPath + "\\" + strings.files[i] + ".txt");
                //string value1 = data["xxxx"]["xxxx"];// IniParser.Exceptions.ParsingException
                try
                {
                    IniData data = parser.ReadFile(folderPath + "\\" + strings.files[i] + ".txt");
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
                    richTextBox1.Text = UnitData.List[id].Name;
                    richTextBox2.Text = UnitData.List[id].Ubertip;
                    richTextBox3.Text = UnitData.List[id].Researchtip;
                    richTextBox4.Text = UnitData.List[id].Researchubertip;
                    richTextBox5.Text = UnitData.List[id].Tip;
                    richTextBox6.Text = UnitData.List[id].Researchhotkey;
                    richTextBox7.Text = UnitData.List[id].Hotkey;
                }
            }
        }
    }
}
