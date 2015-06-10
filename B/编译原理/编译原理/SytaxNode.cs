using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace compiler
{
    class SytaxNode : TreeNode
    {
        public SytaxNode(string str):base(str)
        {
        }
        public SytaxNode():base()
        {

        }

        List<SytaxNode> nodeList;

        internal List<SytaxNode> NodeList
        {
            get { return nodeList; }
            set { nodeList = value; }
        }

        string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        int state;

        public int State
        {
            get { return state; }
            set { state = value; }
        }

        SytaxNode before;

        internal SytaxNode Before
        {
            get { return before; }
            set { before = value; }
        }

        string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        Symbol place;

        internal Symbol Place
        {
            get { return place; }
            set { place = value; }
        }
        string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        int line;

        public int Line
        {
            get { return line; }
            set { line = value; }
        }
        int position;

        public int Position
        {
            get { return position; }
            set { position = value; }
        }
        string elseAddr;

        public string ElseAddr
        {
            get { return elseAddr; }
            set { elseAddr = value; }
        }
        string afterAddr;

        public string AfterAddr
        {
            get { return afterAddr; }
            set { afterAddr = value; }
        }

        string beginAddr;

        public string BeginAddr
        {
            get { return beginAddr; }
            set { beginAddr = value; }
        }
    }

}
