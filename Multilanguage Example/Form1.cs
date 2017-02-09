using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multilanguage_Example
{
    public partial class Form1 : Form
    {

       string DefaultLanguage = Application.StartupPath + "\\Language\\Default.ini";

       INIClass ini;

       public ToolStripMenuItem LangToolStrip;


        public Form1()
        {
     
            ini = new INIClass(DefaultLanguage);

            InitializeComponent();

            DirectoryInfo LanguagesDir = new DirectoryInfo("Language");

            if (LanguagesDir.Exists)
            {
                foreach(FileInfo Lang in LanguagesDir.GetFiles("*.ini"))
                {
                    try
                    {
                        LangToolStrip = new ToolStripMenuItem
                        {
                            Text = System.IO.Path.GetFileNameWithoutExtension(Lang.FullName),
                            Image = Image.FromFile(Application.StartupPath + "\\Language\\LangIcons\\" + System.IO.Path.GetFileNameWithoutExtension(Lang.FullName) + ".Png")
                        };
                        languageToolStripMenuItem.DropDownItems.Add(LangToolStrip);
                        LangToolStrip.Click += LangToolStrip_Click;
                    }
                    catch(Exception ex)
                    {
                        
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }




        }

        private void LangToolStrip_Click(object sender, EventArgs e)
        {
            string langName = ((ToolStripMenuItem)sender).Text;
            string newLang = DefaultLanguage.Replace("Default", langName);
            ini = new INIClass(newLang);
            fileToolStripMenuItem.Text = GetValue("Menu","File");
            newToolStripMenuItem.Text = GetValue("Menu", "New");
            openToolStripMenuItem.Text = GetValue("Menu", "Open");
            saveToolStripMenuItem.Text = GetValue("Menu", "Save");
            saveAsToolStripMenuItem.Text = GetValue("Menu", "Save As");
            exitToolStripMenuItem.Text = GetValue("Menu", "Exit");
            editToolStripMenuItem.Text = GetValue("Menu", "Edit");
            undoToolStripMenuItem.Text = GetValue("Menu", "Undo");
            redoToolStripMenuItem.Text = GetValue("Menu", "Redo");
            cutToolStripMenuItem.Text = GetValue("Menu", "Cut");
            copyToolStripMenuItem.Text = GetValue("Menu", "Copy");
            pasteToolStripMenuItem.Text = GetValue("Menu", "Paste");
            selectAllToolStripMenuItem.Text = GetValue("Menu", "Select All");
            formatToolStripMenuItem.Text = GetValue("Menu", "Format");
            fontToolStripMenuItem.Text = GetValue("Menu", "Font");
            helpToolStripMenuItem.Text = GetValue("Menu", "Help");
            languageToolStripMenuItem.Text = GetValue("Menu", "Language");
            aboutToolStripMenuItem.Text = GetValue("Menu", "About");

            toolStripMenuItem1.Text = GetValue("Context", "Cut");
            toolStripMenuItem2.Text = GetValue("Context", "Copy");
            toolStripMenuItem3.Text = GetValue("Context", "Paste");

        }

        private string GetValue(string Sec,string Key)
        {
            return ini.ReadValueBetween(Sec, Key);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() {Filter="Text File (*.txt)|*.txt"})
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.Text = System.IO.File.ReadAllText(ofd.FileName);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog svd = new SaveFileDialog() { Filter = "Text File (*.txt)|*.txt"})
            {
                if (svd.ShowDialog() == DialogResult.OK)
                {
                     System.IO.File.WriteAllText(svd.FileName,richTextBox1.Text);
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog svd = new SaveFileDialog() { Filter = "Text File (*.txt)|*.txt" })
            {
                if (svd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(svd.FileName, richTextBox1.Text);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo)
            { 
                richTextBox1.Undo(); 
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanRedo)
            {
                richTextBox1.Redo();
            }            
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText !=null)
            {
                Clipboard.SetText(richTextBox1.SelectedText);
                richTextBox1.SelectedText = "";
            }
            else
            {
                Clipboard.SetText(richTextBox1.Text);
                richTextBox1.SelectedText = "";
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != null)
            {
                Clipboard.SetText(richTextBox1.SelectedText);              
            }
            else
            {
                Clipboard.SetText(richTextBox1.Text);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != null)
            {
                richTextBox1.SelectedText = Clipboard.GetText();
            }
            else
            {
                richTextBox1.Text = Clipboard.GetText();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanSelect)
            {
                richTextBox1.SelectAll();
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new FontDialog() {ShowHelp = true})
            {
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    if (richTextBox1.SelectedText != null)
                    {
                        richTextBox1.SelectionFont = fd.Font;
                    }
                    else
                    {
                        if (richTextBox1.Text != null)
                        {
                            richTextBox1.SelectAll();
                            richTextBox1.SelectionFont = fd.Font;
                        }
                        
                    }
                }
            }
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                contextMenuStrip1.Show(richTextBox1, new Point(e.X, e.Y));
        
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != null)
            {
                Clipboard.SetText(richTextBox1.SelectedText);
                richTextBox1.SelectedText = "";
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != null)
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
            else
            {
                Clipboard.SetText(richTextBox1.Text);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != null)
            {
                richTextBox1.SelectedText = Clipboard.GetText();
            }
            else
            {
                richTextBox1.SelectedText = Clipboard.GetText();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.Show();
        }


    }
}
