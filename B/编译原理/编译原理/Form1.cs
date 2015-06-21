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
        //static private Stack<SytaxNode> treeStack = new Stack<SytaxNode>();
        static private SytaxNode root;
        private Timer timer = new Timer();
        //private bool fileBool = true;
        public char[,] filetxt = new char[50, 100];
        private int delay = 1000;
        private string oldStr;
        private int oldBegin = 0;
        private int oldEnd = 0;
        private Color oldColor;

        public Form1()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(sytaxAnalyse);
            timer.Interval = delay;
            timer.Enabled = false;
            System.Threading.Thread.Sleep(delay);
        }
        public int size;

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
                fileBox.Text = "";
                LexicalAnalyzer.Str = "";
                string file1 = textBox1.Text;
                this.fileOpen(file1);
                if (file1 != "")
                {
                    StreamReader fileOpenReader = new StreamReader(file);
                    fileBox.Text = fileOpenReader.ReadToEnd();
                    fileOpenReader.Close();
                }
                else
                {
                    MessageBox.Show("NO FILES SELECTED！");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //lexicalView.Clear();
            LexicalAnalyzer.Str = "";
            filetxt = new char[50, 100];
            string file = textBox1.Text;
            this.fileOpen(file);
            if (file != "")
            {
                StreamReader fileOpenReader = new StreamReader(file);
                fileBox.Text = fileOpenReader.ReadToEnd();
                fileOpenReader.Close();
            }
            else
            {
                MessageBox.Show("NO FILES SELECTED！");
            }
            //this.viewReset();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LexicalAnalyzer.Str = "";
            string file = textBox1.Text;
            this.fileOpen(file);
            StreamWriter fileSave = new StreamWriter(file);
            fileSave.Write(fileBox.Text);
            fileSave.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string file = textBox1.Text;
            fileOpen(file);
            timer.Enabled = false;
            sytaxAnalyse(sender, e);
        }

        private void fileOpen(string fileName)
        {
            LexicalAnalyzer.input = new StreamReader(fileName);
            oldStr = fileBox.Text;
            int filei = 0;            
            int i = 0;
            while (!LexicalAnalyzer.input.EndOfStream)
            {
                string strrr = LexicalAnalyzer.input.ReadLine();
                LexicalAnalyzer.Str += strrr + '\r';
                for (i = 0; i < strrr.Length; i++)
                {
                    filetxt[filei, i] = strrr.ToCharArray()[i];
                }
                filetxt[filei, i + 1] = '\r';
                filei++;
            }
            LexicalAnalyzer.Str += '$';
            LexicalAnalyzer.input.Close();
        }

        private void sytaxAnalyse(object sender, EventArgs e)
        {
            if (!lexical.Checked && !syntax.Checked && !threeAddr.Checked)
            {
                lexicalView.Visible = true;
                errorView.Visible = true;
            }
            button3_Click(sender, e);
            LexicalAnalyzer.errorList.Clear();      
            LexicalAnalyzer lex = new LexicalAnalyzer();
            Token token = lex.nextToken();
            //LexicalAnalyzer.input = new StreamReader(file, fileBool);
            if (LexicalAnalyzer.code == -1)
            {
                MessageBox.Show("There are no more Tokens!");
                return;
            }
            if (LexicalAnalyzer.errorList.Count == 0)
            {
                //StreamReader fil = new StreamReader(fileOut);
                //richTextBox2.Text = fil.ReadToEnd();
                //fil.Close();
                ListViewItem li = new ListViewItem();
                li.Text = token.Tokentype;
                li.SubItems.Add(token.Attributevalue);
                li.SubItems.Add(token.Linenumber.ToString());
                li.SubItems.Add(token.Lineposition.ToString());
                this.lexicalView.Items.Add(li);
            }
            else
            {
                //StreamReader fil = new StreamReader(errorOut);
                //richTextBox2.Text = fil.ReadToEnd();
                //fil.Close();
                ListViewItem li = new ListViewItem();
                li.Text = token.Tokentype;
                li.SubItems.Add(token.Attributevalue);
                li.SubItems.Add(token.Linenumber.ToString());
                li.SubItems.Add(token.Lineposition.ToString());
                this.errorView.Items.Add(li);
                fileBox.Focus();
                int x = token.Linenumber;
                int j = token.Lineposition;
                int count = 0;
                for (int a = 0; a < x - 1; a++)
                {
                    int b = 0;
                    while (filetxt[a, b] != '\r')
                    {
                        count++;
                        b++;
                    }
                }
                count = count + j;
                fileBox.Focus();
                fileBox.Select(count, 1);
                fileBox.SelectionColor = Color.Red;
            }
            LLparser.Token = token;
            switch(LLparser.sytaxAnalyse(sender, e))
            {
                case -1:
                    if (LLparser.ErrorList[LLparser.ErrorList.Count - 1][2] != "$")
                    {
                        int bef = 2;
                        if (LLparser.ErrorList[LLparser.ErrorList.Count - 1][5] == "False")
                        {
                            int x = Convert.ToInt32(LLparser.ErrorList[LLparser.ErrorList.Count - 1][0]);
                            int j = Convert.ToInt32(LLparser.ErrorList[LLparser.ErrorList.Count - 1][1]);
                            int count = 0;
                            for (int a = 0; a < x - 1; a++)
                            {
                                int b = 0;
                                while (filetxt[a, b] != '\r')
                                {
                                    count++;
                                    b++;
                                }
                            }
                            count = count + j;
                            fileBox.Focus();
                            fileBox.Select(count, Convert.ToInt32(LLparser.ErrorList[LLparser.ErrorList.Count - 1][4]));
                            fileBox.SelectionBackColor = Color.Yellow;
                            ListViewItem li = new ListViewItem();
                            li.Text = LLparser.ErrorList[LLparser.ErrorList.Count - 1][0];
                            li.SubItems.Add(LLparser.ErrorList[LLparser.ErrorList.Count - 1][1]);
                            li.SubItems.Add(LLparser.ErrorList[LLparser.ErrorList.Count - 1][2]);
                            li.SubItems.Add(LLparser.ErrorList[LLparser.ErrorList.Count - 1][3]);
                            this.LLerrorView.Items.Add(li);
                        }
                        else
                        {
                            if (LLparser.ErrorList.Count > 1)
                            {
                                while (LLparser.ErrorList[LLparser.ErrorList.Count - bef][5] == "True")
                                {
                                    bef++;
                                }
                                int x = Convert.ToInt32(LLparser.ErrorList[LLparser.ErrorList.Count - bef][0]);
                                int j = Convert.ToInt32(LLparser.ErrorList[LLparser.ErrorList.Count - bef][1]);
                                int count = 0;
                                for (int a = 0; a < x - 1; a++)
                                {
                                    int b = 0;
                                    while (filetxt[a, b] != '\r')
                                    {
                                        count++;
                                        b++;
                                    }
                                }
                                count = count + j;
                                int z = Convert.ToInt32(LLparser.ErrorList[LLparser.ErrorList.Count - 1][0]);
                                int y = Convert.ToInt32(LLparser.ErrorList[LLparser.ErrorList.Count - 1][1]);
                                int count1 = 0;
                                for (int a = 0; a < z - 1; a++)
                                {
                                    int b = 0;
                                    while (filetxt[a, b] != '\r')
                                    {
                                        count1++;
                                        b++;
                                    }
                                }
                                count1 = count1 + y;
                                fileBox.Focus();
                                fileBox.Select(count, count1 - count + Convert.ToInt32(LLparser.ErrorList[LLparser.ErrorList.Count - 1][4]));
                                fileBox.SelectionBackColor = Color.Yellow;
                            }
                            ListViewItem li = new ListViewItem();
                            li.Text = LLparser.ErrorList[LLparser.ErrorList.Count - 1][0];
                            li.SubItems.Add(LLparser.ErrorList[LLparser.ErrorList.Count - 1][1]);
                            li.SubItems.Add(LLparser.ErrorList[LLparser.ErrorList.Count - 1][2]);
                            li.SubItems.Add(LLparser.ErrorList[LLparser.ErrorList.Count - 1][3]);
                            this.LLerrorView.Items.Add(li);
                        }
                    }
                    break;
                case -2:
                    //fileBool = false;
                    timer.Enabled = false;
                    MessageBox.Show("There's something wrong with sytax!");
                    //if (LLparser.ErrorList[LLparser.ErrorList.Count - 1][2] != "$")
                    //{
                    //    int bef = 2;
                    //    if (LLparser.ErrorList[LLparser.ErrorList.Count - 1][5] == "False")
                    //    {
                    //        int x = Convert.ToInt32(LLparser.ErrorList[LLparser.ErrorList.Count - 1][0]);
                    //        int j = Convert.ToInt32(LLparser.ErrorList[LLparser.ErrorList.Count - 1][1]);
                    //        int count = 0;
                    //        for (int a = 0; a < x - 1; a++)
                    //        {
                    //            int b = 0;
                    //            while (filetxt[a, b] != '\r')
                    //            {
                    //                count++;
                    //                b++;
                    //            }
                    //        }
                    //        count = count + j;
                    //        fileBox.Focus();
                    //        fileBox.Select(count, Convert.ToInt32(LLparser.ErrorList[LLparser.ErrorList.Count - 1][4]));
                    //        fileBox.SelectionBackColor = Color.Yellow;
                    //        ListViewItem li = new ListViewItem();
                    //        li.Text = LLparser.ErrorList[LLparser.ErrorList.Count - 1][0];
                    //        li.SubItems.Add(LLparser.ErrorList[LLparser.ErrorList.Count - 1][1]);
                    //        li.SubItems.Add(LLparser.ErrorList[LLparser.ErrorList.Count - 1][2]);
                    //        li.SubItems.Add(LLparser.ErrorList[LLparser.ErrorList.Count - 1][3]);
                    //        this.LLerrorView.Items.Add(li);
                    //    }
                    //    else
                    //    {
                    //        if (LLparser.ErrorList.Count > 1)
                    //        {
                    //            while (LLparser.ErrorList[LLparser.ErrorList.Count - bef][5] == "True")
                    //            {
                    //                bef++;
                    //            }
                    //            int x = Convert.ToInt32(LLparser.ErrorList[LLparser.ErrorList.Count - bef][0]);
                    //            int j = Convert.ToInt32(LLparser.ErrorList[LLparser.ErrorList.Count - bef][1]);
                    //            int count = 0;
                    //            for (int a = 0; a < x - 1; a++)
                    //            {
                    //                int b = 0;
                    //                while (filetxt[a, b] != '\r')
                    //                {
                    //                    count++;
                    //                    b++;
                    //                }
                    //            }
                    //            count = count + j;
                    //            int z = Convert.ToInt32(LLparser.ErrorList[LLparser.ErrorList.Count - 1][0]);
                    //            int y = Convert.ToInt32(LLparser.ErrorList[LLparser.ErrorList.Count - 1][1]);
                    //            int count1 = 0;
                    //            for (int a = 0; a < z - 1; a++)
                    //            {
                    //                int b = 0;
                    //                while (filetxt[a, b] != '\r')
                    //                {
                    //                    count++;
                    //                    b++;
                    //                }
                    //            }
                    //            count1 = count1 + y;
                    //            fileBox.Focus();
                    //            fileBox.Select(count, count1 - count + Convert.ToInt32(LLparser.ErrorList[LLparser.ErrorList.Count - 1][4]));
                    //            fileBox.SelectionBackColor = Color.Yellow;
                    //        }
                    //        ListViewItem li = new ListViewItem();
                    //        li.Text = LLparser.ErrorList[LLparser.ErrorList.Count - 1][0];
                    //        li.SubItems.Add(LLparser.ErrorList[LLparser.ErrorList.Count - 1][1]);
                    //        li.SubItems.Add(LLparser.ErrorList[LLparser.ErrorList.Count - 1][2]);
                    //        li.SubItems.Add(LLparser.ErrorList[LLparser.ErrorList.Count - 1][3]);
                    //        this.LLerrorView.Items.Add(li);
                    //    }
                    //}
                    break;
                case 0:
                    break;
                case 1:
                    timer.Enabled = false;
                    MessageBox.Show("Complete!");
                    LLparser.ergodic(root);
                    sytaxBox.Text = root.Code;
                    SymbolTable.reset();
                    LLparser.loopReset();
                    break;
            }
            root = LLparser.Root;
            syntaxTreeView.Nodes.Clear();
            syntaxTreeView.Nodes.Add(root);
            syntaxTreeView.ExpandAll();

        }

        private void lexical_Click(object sender, EventArgs e)
        {
                lexicalView.Visible = true;
                errorView.Visible = true;
                syntaxTreeView.Visible = false;
                sytaxBox.Visible = false;
                LLerrorView.Visible = false;
        }

        private void syntax_Click(object sender, EventArgs e)
        {
                lexicalView.Visible = false;
                errorView.Visible = false;
                syntaxTreeView.Visible = true;
                sytaxBox.Visible = false;
                LLerrorView.Visible = true;
        }

        private void sytaxReset()
        {
            SymbolTable.reset();
            LLparser.reset();
        }

        private void viewReset()
        {
            lexicalView.Columns.Add("tokenType", 90);
            lexicalView.Columns.Add("attributeValue", 90);
            lexicalView.Columns.Add("lineNumber", 90);
            lexicalView.Columns.Add("linePosition", 90);
            lexicalView.GridLines = true;
            lexicalView.View = View.Details;
            lexicalView.HeaderStyle = ColumnHeaderStyle.Clickable;
            lexicalView.FullRowSelect = true;
            errorView.Columns.Add("tokenType", 90);
            errorView.Columns.Add("message", 90);
            errorView.Columns.Add("lineNumber", 90);
            errorView.Columns.Add("linePosition", 90);
            errorView.GridLines = true;
            errorView.View = View.Details;
            errorView.HeaderStyle = ColumnHeaderStyle.Clickable;
            errorView.FullRowSelect = true;
            LLerrorView.Columns.Add("lineNumber", 90);
            LLerrorView.Columns.Add("linePosition", 90);
            LLerrorView.Columns.Add("tokenType", 90);
            LLerrorView.Columns.Add("expectTokenType", 90);
            LLerrorView.GridLines = true;
            LLerrorView.View = View.Details;
            LLerrorView.HeaderStyle = ColumnHeaderStyle.Clickable;
            LLerrorView.FullRowSelect = true;
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (oldStr != fileBox.Text)
            {
                LexicalAnalyzer.tokenClear();
                LexicalAnalyzer.erListClear();
                sytaxBox.Clear();
                lexicalView.Clear();
                errorView.Clear();
                LLerrorView.Clear();
                sytaxReset();
                viewReset();
                fileBox.Focus();
                fileBox.SelectAll();
                fileBox.SelectionBackColor = Color.White;
                fileBox.SelectionColor = Color.Black;
                fileBox.Select(0, 0);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void threeAddr_Click(object sender, EventArgs e)
        {
            errorView.Visible = false;
            lexicalView.Visible = false;
            syntaxTreeView.Visible = false;
            sytaxBox.Visible = true;
            LLerrorView.Visible = false;
        }

        private void autoButton_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
            string file = textBox1.Text;
            fileOpen(file);
            timer.Enabled = true;
            delay = Convert.ToInt32(delayBox.Text);
            timer.Interval = delay;
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
        }

        /*private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int c =  listView1.SelectedItems[0].Index;
            c = c;
        }*/

        private void Ranks()
        {
            int index = fileBox.GetFirstCharIndexOfCurrentLine();
            int line = fileBox.GetLineFromCharIndex(index) + 1;
            int column = fileBox.SelectionStart - index;
            this.label1.Text = string.Format("第：{0}行 {1}列", line.ToString(), column.ToString());
        }

        private void fileBox_MouseClick(object sender, MouseEventArgs e)
        {
            this.Ranks(); 
        }

        private void fileBox_KeyUp(object sender, KeyEventArgs e)
        {
            this.Ranks(); 
        }

        private void errorView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (oldColor != null)
            {
                fileBox.Focus();
                fileBox.Select(oldBegin, oldEnd);
                fileBox.SelectionBackColor = oldColor;
            }
            int x = Convert.ToInt32(errorView.FocusedItem.SubItems[2].Text);
            int j = Convert.ToInt32(errorView.FocusedItem.SubItems[3].Text);
            int count = 0;
            for (int a = 0; a < x-1; a++)
            {
                int b = 0;
                while (filetxt[a, b] != '\r')
                {
                    count++;
                    b++;
                }
            }
            count = count + j;
            oldBegin = count;
            oldEnd = 1;
            fileBox.Focus();
            fileBox.Select(count, 1);
            oldColor = fileBox.SelectionBackColor;
            fileBox.SelectionBackColor = Color.BlueViolet;
        }

        private void LLerrorView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (oldColor != null)
            {
                fileBox.Focus();
                fileBox.Select(oldBegin, oldEnd);
                fileBox.SelectionBackColor = oldColor;
            }
            int x = Convert.ToInt32(LLerrorView.FocusedItem.SubItems[0].Text);
            int j = Convert.ToInt32(LLerrorView.FocusedItem.SubItems[1].Text);
            int count = 0;
            for (int a = 0; a < x - 1; a++)
            {
                int b = 0;
                while (filetxt[a, b] != '\r')
                {
                    count++;
                    b++;
                }
            }
            count = count + j;
            oldBegin = count;
            oldEnd = 1;
            fileBox.Focus();
            fileBox.Select(count, 1);
            oldColor = fileBox.SelectionBackColor;
            fileBox.SelectionBackColor = Color.BlueViolet;
        }
    }
}
