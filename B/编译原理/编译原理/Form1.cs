using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace compiler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog(); 
            fileDialog.Multiselect = true; 
            fileDialog.Title = "请选择文件"; 
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK) 
            {  
                string file = fileDialog.FileName;
                textBox1.Text = file;
                richTextBox1.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            string file = textBox1.Text;
            if (file != "")
            {
                StreamReader fileOpen = new StreamReader(file);
                richTextBox1.Text = fileOpen.ReadToEnd();
                fileOpen.Close();
            }
            else
            {
                MessageBox.Show("NO FILES SELECTED！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string file = textBox1.Text;
            StreamWriter fileSave = new StreamWriter(file);
            fileSave.Write(richTextBox1.Text);
            fileSave.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
            LexicalAnalyzer.errorList.Clear();
            string file = textBox1.Text;
            LexicalAnalyzer.input = new StreamReader(file);
            string fileOut = @"C:\text.txt";
            LexicalAnalyzer.output = new StreamWriter(fileOut);
            string errorOut = @"C:\error.txt";
            LexicalAnalyzer.errorOutput = new StreamWriter(errorOut);
            LexicalAnalyzer lex = new LexicalAnalyzer();
            LexicalAnalyzer.tokenListReset();
            LexicalAnalyzer.erListReset();
            lex.isToken();
            LexicalAnalyzer.input.Close();
            LexicalAnalyzer.output.Close();
            LexicalAnalyzer.errorOutput.Close();
            this.listView1.Clear();
            if(LexicalAnalyzer.errorList.Count == 0)
            {
                //StreamReader fil = new StreamReader(fileOut);
                //richTextBox2.Text = fil.ReadToEnd();
                //fil.Close();
                listView1.Columns.Add("tokenType", 70);
                listView1.Columns.Add("attributeValue", 70);
                listView1.Columns.Add("lineNumber", 70);
                listView1.Columns.Add("linePosition", 70);
                listView1.GridLines = true;
                listView1.View = View.Details;
                listView1.HeaderStyle = ColumnHeaderStyle.Clickable;
                listView1.FullRowSelect = true;
                for (int a = 0; a < LexicalAnalyzer.countColum; a++)
                {
                    ListViewItem li = new ListViewItem();
                    li.Text = LexicalAnalyzer.tokenList[a,0];
                    li.SubItems.Add(LexicalAnalyzer.tokenList[a,1]);
                    li.SubItems.Add(LexicalAnalyzer.tokenList[a,2]);
                    li.SubItems.Add(LexicalAnalyzer.tokenList[a,3]);
                    this.listView1.Items.Add(li);
                }
            }
            else
            {
                //StreamReader fil = new StreamReader(errorOut);
                //richTextBox2.Text = fil.ReadToEnd();
                //fil.Close();
                listView1.Columns.Add("errorNumber", 70);
                listView1.Columns.Add("message", 70);
                listView1.Columns.Add("lineNumber", 70);
                listView1.Columns.Add("linePosition", 70);
                listView1.GridLines = true;
                listView1.View = View.Details;
                listView1.HeaderStyle = ColumnHeaderStyle.Clickable;
                listView1.FullRowSelect = true;
                for (int a = 0; a < LexicalAnalyzer.countColum; a++)
                {
                    ListViewItem li = new ListViewItem();
                    li.Text = LexicalAnalyzer.erList[a, 0];
                    li.SubItems.Add(LexicalAnalyzer.erList[a, 1]);
                    li.SubItems.Add(LexicalAnalyzer.erList[a, 2]);
                    li.SubItems.Add(LexicalAnalyzer.erList[a, 3]);
                    this.listView1.Items.Add(li);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int c =  listView1.SelectedItems[0].Index;
            c = c;
        }
    }
}
