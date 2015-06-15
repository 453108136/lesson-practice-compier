﻿using System;
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
        static private Stack<SytaxNode> treeStack = new Stack<SytaxNode>();
        static private SytaxNode root;
        private Timer timer = new Timer();
        private bool fileBool = true;
        public char[,] filetxt = new char[50, 100];
        private int delay = 1000;
        private string oldStr;
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
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //lexicalView.Clear();
            string file = textBox1.Text;
            if (file != "")
            {
                StreamReader fileOpen = new StreamReader(file);
                fileBox.Text = fileOpen.ReadToEnd();
                fileOpen.Close();
            }
            else
            {
                MessageBox.Show("NO FILES SELECTED！");
            }
            //this.viewReset();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string file = textBox1.Text;
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
            LexicalAnalyzer.input = new StreamReader(fileName, fileBool);
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
            //button3_Click(sender, e);
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
                count = count + j - 1;
                fileBox.Focus();
                fileBox.Select(count, 1);
                fileBox.SelectionColor = Color.Red;
            }

            if (token != null && token.Tokentype != "error")
            {

                string symbol = LLparser.Stack.Peek();
                while (symbol == "///")
                {
                    LLparser.Stack.Pop();
                    treeStack.Pop();
                    symbol = LLparser.Stack.Peek();
                }
                if (symbol == "$")
                {
                    if (token.Tokentype == "$")
                    {
                        timer.Enabled = false;
                        MessageBox.Show("Complete!");
                        output();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Wrong token type!");
                    }
                }
                if (LLparser.isTerminator(symbol))
                {
                    SytaxNode newNode = new SytaxNode(symbol);
                    treeStack.Peek().Nodes.Add(newNode);
                    LLparser.Stack.Pop();
                    if (symbol != token.Tokentype)
                    {
                        //MessageBox.Show("Wrong token type!");
                        symbol = LLparser.Stack.Peek();
                        while (symbol != "stmts" && token.Tokentype != "$")
                        {
                            newNode = new SytaxNode(symbol);
                            treeStack.Peek().Nodes.Add(newNode);
                            LLparser.Stack.Pop();
                            symbol = LLparser.Stack.Peek();
                            while (symbol == "///")
                            {
                                LLparser.Stack.Pop();
                                treeStack.Pop();
                                symbol = LLparser.Stack.Peek();
                            }
                        }
                        if (token.Tokentype == "$")
                        {
                            fileBool = false;
                            timer.Enabled = false;
                            return;
                        }
                        return;
                    }
                }
                else
                {

                    while (!LLparser.isTerminator(symbol))
                    {
                        if (LLparser.Table[symbol].ContainsKey(token.Tokentype))
                        {
                            SytaxNode newNode = new SytaxNode(symbol);
                            if (treeStack.Count == 0)
                            {
                                treeStack.Push(newNode);
                                root = treeStack.Peek();
                            }
                            else
                            {
                                treeStack.Peek().Nodes.Add(newNode);
                                treeStack.Push(newNode);
                            }
                            LLparser.Stack.Pop();
                            LLparser.Stack.Push("///");
                            newNode.State = LLparser.Table[symbol][token.Tokentype];
                            LLparser.pushStack(LLparser.Table[symbol][token.Tokentype]);
                            symbol = LLparser.Stack.Peek();
                            if (symbol == "")
                            {
                                newNode = new SytaxNode(symbol);
                                treeStack.Peek().Nodes.Add(newNode);
                                LLparser.Stack.Pop();
                                symbol = LLparser.Stack.Peek();
                                while (symbol == "///")
                                {
                                    LLparser.Stack.Pop();
                                    treeStack.Pop();
                                    symbol = LLparser.Stack.Peek();
                                }
                            }
                            else
                            {
                                while (symbol == "///")
                                {
                                    LLparser.Stack.Pop();
                                    treeStack.Pop();
                                    symbol = LLparser.Stack.Peek();
                                }
                            }
                        }
                        else
                        {
                            //MessageBox.Show("Wrong token type!");
                            symbol = LLparser.Stack.Peek();
                            while (symbol != "stmts" && token.Tokentype != "$")
                            {
                                SytaxNode newNode = new SytaxNode(symbol);
                                treeStack.Peek().Nodes.Add(newNode);
                                LLparser.Stack.Pop();
                                symbol = LLparser.Stack.Peek();
                                while (symbol == "///")
                                {
                                    LLparser.Stack.Pop();
                                    treeStack.Pop();
                                    symbol = LLparser.Stack.Peek();
                                }
                                if(symbol == "$")
                                {
                                    fileBool = false;
                                    timer.Enabled = false;
                                    return;
                                }
                            }
                            if (token.Tokentype == "$")
                            {
                                fileBool = false;
                                timer.Enabled = false;
                                return;
                            }
                            return;
                        }
                    }
                    //while (symbol == "")
                    //{
                    // SytaxNode newNode = new SytaxNode(symbol);
                    //treeStack.Pop().Nodes.Add(newNode);
                    //symbol = LLparser.Stack.Pop();
                    // }
                    SytaxNode lastNode = new SytaxNode(symbol);
                    if (token.Tokentype == "number")
                    {
                        lastNode.Value = token.Attributevalue;
                    }
                    else
                    {
                        if (token.Tokentype == "identifier")
                        {
                            lastNode.Id = token.Attributevalue;
                        }
                    }
                    lastNode.Line = token.Linenumber;
                    lastNode.Position = token.Lineposition;
                    treeStack.Peek().Nodes.Add(lastNode);
                    treeStack.Push(lastNode);
                    LLparser.Stack.Pop();
                    treeStack.Pop();
                }
                syntaxTreeView.Nodes.Clear();
                syntaxTreeView.Nodes.Add(root);
                syntaxTreeView.ExpandAll();
            }
        }

        private void lexical_Click(object sender, EventArgs e)
        {
            if (lexical.Checked)
            {
                lexicalView.Visible = true;
                errorView.Visible = true;
                syntaxTreeView.Visible = false;
                sytaxBox.Visible = false;
            }
        }

        private void syntax_Click(object sender, EventArgs e)
        {
            if (syntax.Checked)
            {
                lexicalView.Visible = false;
                errorView.Visible = false;
                syntaxTreeView.Visible = true;
                sytaxBox.Visible = false;
            }
        }

        private void viewReset()
        {
            lexicalView.Columns.Add("tokenType", 70);
            lexicalView.Columns.Add("attributeValue", 70);
            lexicalView.Columns.Add("lineNumber", 70);
            lexicalView.Columns.Add("linePosition", 70);
            lexicalView.GridLines = true;
            lexicalView.View = View.Details;
            lexicalView.HeaderStyle = ColumnHeaderStyle.Clickable;
            lexicalView.FullRowSelect = true;
            errorView.Columns.Add("tokenType", 70);
            errorView.Columns.Add("message", 70);
            errorView.Columns.Add("lineNumber", 70);
            errorView.Columns.Add("linePosition", 70);
            errorView.GridLines = true;
            errorView.View = View.Details;
            errorView.HeaderStyle = ColumnHeaderStyle.Clickable;
            errorView.FullRowSelect = true;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if(oldStr != fileBox.Text)
            {
                LexicalAnalyzer.tokenClear();
                LexicalAnalyzer.erListClear();
                this.lexicalView.Clear();
                this.errorView.Clear();
                treeStack.Clear();
                LLparser.stackReset();
                this.viewReset();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void output()
        {
            ergodic(root);
            sytaxBox.Text = root.Code;
        }

        private void ergodic(SytaxNode node)
        {
            if(node.NextNode != null)
            {
                ((SytaxNode)node.NextNode).Before = node;
            }
            if (node.FirstNode != null)
            {
                node.NodeList = new List<SytaxNode>();
                SytaxNode childNode = (SytaxNode)node.FirstNode;
                while (childNode.NextNode != null)
                {
                    node.NodeList.Add(childNode);
                    childNode = (SytaxNode)childNode.NextNode;
                }
                node.NodeList.Add(childNode);
                LLparser.inherit(node, node.State);
                foreach (SytaxNode ergodicNode in node.NodeList)
                {
                    ergodic(ergodicNode);
                }
                LLparser.synthetical(node, node.State);
            }
            //else
            //{
            //    if(node.Text == "identifier" && ((SytaxNode)node.Parent).State == 25)
            //    {
            //        node.Place = SymbolTable.addSymbol(node.Id, "double", node.Line, node.Position);
            //    }
            //}
            return;
        }


        private void threeAddr_Click(object sender, EventArgs e)
        {
            errorView.Visible = false;
            lexicalView.Visible = false;
            syntaxTreeView.Visible = false;
            sytaxBox.Visible = true;
        }

        private void autoButton_Click(object sender, EventArgs e)
        {
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
            count = count + j-1;
            fileBox.Focus();
            fileBox.Select(count, 1);
        }
    }
}
