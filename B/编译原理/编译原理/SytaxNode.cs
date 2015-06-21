using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace compiler
{
    class SytaxNode : TreeNode      //记录各种信息的树节点
    {
        public SytaxNode(string str):base(str)
        {
        }
        public SytaxNode():base()
        {

        }

        List<SytaxNode> nodeList;   //子节点的列表

        internal List<SytaxNode> NodeList
        {
            get { return nodeList; }
            set { nodeList = value; }
        }

        string id;      //符号

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        int state;

        public int State    //状态，根据哪条文法展开的
        {
            get { return state; }
            set { state = value; }
        }

        SytaxNode before;   //前一个节点

        internal SytaxNode Before
        {
            get { return before; }
            set { before = value; }
        }

        string value;   //值

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        Symbol place;   //符号表中的地址

        internal Symbol Place
        {
            get { return place; }
            set { place = value; }
        }
        string type;  //类型

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        string code;  //生成的三地址代码

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        int line;     //所在行

        public int Line
        {
            get { return line; }
            set { line = value; }
        }
        int position;     //所在列

        public int Position
        {
            get { return position; }
            set { position = value; }
        }
        string elseAddr;      //else跳转标签

        public string ElseAddr
        {
            get { return elseAddr; }
            set { elseAddr = value; }
        }
        string afterAddr;     //then结束跳转标签

        public string AfterAddr 
        {
            get { return afterAddr; }
            set { afterAddr = value; }
        }

        string beginAddr;      //while开始跳转标签

        public string BeginAddr
        {
            get { return beginAddr; }
            set { beginAddr = value; }
        }
    }

}
