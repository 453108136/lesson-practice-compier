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
using System.Threading;

namespace compiler
{
    public partial class Form1 : Form
    {
        static private Stack<SytaxNode> treeStack = new Stack<SytaxNode>();
        static private SytaxNode root;
        public Form1()
        {
            InitializeComponent();
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
                richTextBox1.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lexicalView.Clear();
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
            lexicalView.Columns.Add("tokenType", 70);
            lexicalView.Columns.Add("attributeValue", 70);
            lexicalView.Columns.Add("lineNumber", 70);
            lexicalView.Columns.Add("linePosition", 70);
            lexicalView.GridLines = true;
            lexicalView.View = View.Details;
            lexicalView.HeaderStyle = ColumnHeaderStyle.Clickable;
            lexicalView.FullRowSelect = true;
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
            if(!lexical.Checked || !syntax.Checked || !radioButton3.Checked )
            {
                lexicalView.Visible = true;
            }
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
            Token token = lex.nextToken();
            string str = "";
            while (!LexicalAnalyzer.input.EndOfStream)
            {
                str += LexicalAnalyzer.input.ReadLine() + '\n';
            }
            LexicalAnalyzer.input.Close();
            LexicalAnalyzer.output.Close();
            LexicalAnalyzer.errorOutput.Close();
            if (LexicalAnalyzer.code == -1)
            {
                MessageBox.Show("There are no more Tokens!");
                return ;
            }
            if (LexicalAnalyzer.errorList.Count == 0)
            {
                //StreamReader fil = new StreamReader(fileOut);
                //richTextBox2.Text = fil.ReadToEnd();
                //fil.Close();
                    ListViewItem li = new ListViewItem();
                    li.Text = LexicalAnalyzer.tokenList[0, 0];
                    li.SubItems.Add(LexicalAnalyzer.tokenList[0, 1]);
                    li.SubItems.Add(LexicalAnalyzer.tokenList[0, 2]);
                    li.SubItems.Add(LexicalAnalyzer.tokenList[0, 3]);
                    this.lexicalView.Items.Add(li);
            }
            else
            {
                //StreamReader fil = new StreamReader(errorOut);
                //richTextBox2.Text = fil.ReadToEnd();
                //fil.Close();
                this.lexicalView.Clear();
                lexicalView.Columns.Add("errorNumber", 70);
                lexicalView.Columns.Add("message", 70);
                lexicalView.Columns.Add("lineNumber", 70);
                lexicalView.Columns.Add("linePosition", 70);
                lexicalView.GridLines = true;
                lexicalView.View = View.Details;
                lexicalView.HeaderStyle = ColumnHeaderStyle.Clickable;
                lexicalView.FullRowSelect = true;
                for (int a = 0; a < LexicalAnalyzer.countColum; a++)
                {
                    ListViewItem li = new ListViewItem();
                    li.Text = LexicalAnalyzer.erList[a, 0];
                    li.SubItems.Add(LexicalAnalyzer.erList[a, 1]);
                    li.SubItems.Add(LexicalAnalyzer.erList[a, 2]);
                    li.SubItems.Add(LexicalAnalyzer.erList[a, 3]);
                    this.lexicalView.Items.Add(li);
                }
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
                        MessageBox.Show("Complete!");
                        output();
                        return ;
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
                    if (symbol != token.Tokentype)
                    {
                        MessageBox.Show("Wrong token type!");
                    }
                    LLparser.Stack.Pop();
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
                            MessageBox.Show("Wrong token type!");
                            break;
                        }
                    }
                    //while (symbol == "")
                    //{
                    // SytaxNode newNode = new SytaxNode(symbol);
                    //treeStack.Pop().Nodes.Add(newNode);
                    //symbol = LLparser.Stack.Pop();
                    // }
                    SytaxNode lastNode = new SytaxNode(symbol);
                    lastNode.Value = token.Attributevalue;
                    treeStack.Peek().Nodes.Add(lastNode);
                    treeStack.Push(lastNode);
                    LLparser.Stack.Pop();
                    treeStack.Pop();
                }
                syntaxTreeView.Nodes.Clear();
                syntaxTreeView.Nodes.Add(root);
                syntaxTreeView.ExpandAll();
            }
            if (token!=null &&  token.Tokentype != "$")
            {
                //Thread.Sleep(1000);
                button4_Click(sender, e);

            }
        }

        private void lexical_Click(object sender, EventArgs e)
        {
            if (lexical.Checked)
            {
                lexicalView.Visible = true;
                syntaxTreeView.Visible = false;
                richTextBox3.Visible = false;
            }
        }

        private void syntax_Click(object sender, EventArgs e)
        {
            if (syntax.Checked)
            {
                lexicalView.Visible = false;
                syntaxTreeView.Visible = true;
                richTextBox3.Visible = false;
            }
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                lexicalView.Visible = false;
                syntaxTreeView.Visible = true;
                richTextBox3.Visible = false;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            LexicalAnalyzer.tokenListClear();
            LexicalAnalyzer.tokenClear();
            this.lexicalView.Clear();
            treeStack.Clear();
            LLparser.stackReset();
            lexicalView.Columns.Add("tokenType", 70);
            lexicalView.Columns.Add("attributeValue", 70);
            lexicalView.Columns.Add("lineNumber", 70);
            lexicalView.Columns.Add("linePosition", 70);
            lexicalView.GridLines = true;
            lexicalView.View = View.Details;
            lexicalView.HeaderStyle = ColumnHeaderStyle.Clickable;
            lexicalView.FullRowSelect = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void output()
        {
            ergodic(root);
        }

        private void ergodic(SytaxNode node)
        {
            if (node.FirstNode != null)
            {
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
            return;
        }

        /*private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int c =  listView1.SelectedItems[0].Index;
            c = c;
        }*/
    }
}
