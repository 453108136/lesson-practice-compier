using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace compiler
{
    class LLparser
    {
        static private LinkedList<string>[] Rules = new LinkedList<string>[30];
        static private int loopNum = 1;
        static private bool beforeIsWrong = false;
        static private List<List<string>> errorList = new List<List<string>>();

        public static List<List<string>> ErrorList
        {
            get { return LLparser.errorList; }
            set { LLparser.errorList = value; }
        }
        static private Stack<SytaxNode> treeStack = new Stack<SytaxNode>();

        internal static Stack<SytaxNode> TreeStack
        {
            get { return LLparser.treeStack; }
            set { LLparser.treeStack = value; }
        }
        static private Token token;

        internal static Token Token
        {
            get { return LLparser.token; }
            set { LLparser.token = value; }
        }

        static private Stack<string> stack = new Stack<string>();

        public static Stack<string> Stack
        {
            get { return LLparser.stack; }
            set { LLparser.stack = value; }
        }

        static private Dictionary<string, Dictionary<string, int>> table = new Dictionary<string,Dictionary<string,int>>();

        public static Dictionary<string, Dictionary<string, int>> Table
        {
            get { return LLparser.table; }
        }

        static private SytaxNode root;

        internal static SytaxNode Root
        {
            get { return LLparser.root; }
            set { LLparser.root = value; }
        }

        public void make()
        {
            init();
            createTable();
        }

        private static void init()
        {
            Rules[0]  = new LinkedList<string>(); Rules[0] .AddLast("program"); Rules[0].AddLast("compoundstmt");
            Rules[1]  = new LinkedList<string>(); Rules[1] .AddLast("stmt"); Rules[1].AddLast("ifstmt");
            Rules[2]  = new LinkedList<string>(); Rules[2] .AddLast("stmt"); Rules[2].AddLast("whilestmt");
            Rules[3]  = new LinkedList<string>(); Rules[3] .AddLast("stmt"); Rules[3].AddLast("assgstmt");
            Rules[4]  = new LinkedList<string>(); Rules[4] .AddLast("stmt"); Rules[4].AddLast("compoundstmt");
            Rules[5]  = new LinkedList<string>(); Rules[5] .AddLast("compoundstmt"); Rules[5].AddLast("{"); Rules[5].AddLast("stmts"); Rules[5].AddLast("}");
            Rules[6]  = new LinkedList<string>(); Rules[6] .AddLast("stmts"); Rules[6].AddLast("stmt"); Rules[6].AddLast("stmts");
            Rules[7]  = new LinkedList<string>(); Rules[7] .AddLast("stmts"); Rules[7].AddLast("");
            Rules[8]  = new LinkedList<string>(); Rules[8] .AddLast("ifstmt"); Rules[8].AddLast("if"); Rules[8].AddLast("("); Rules[8].AddLast("boolexpr"); Rules[8].AddLast(")"); Rules[8].AddLast("then"); Rules[8].AddLast("stmt"); Rules[8].AddLast("else"); Rules[8].AddLast("stmt");
            Rules[9]  = new LinkedList<string>(); Rules[9] .AddLast("whilestmt"); Rules[9].AddLast("while"); Rules[9].AddLast("("); Rules[9].AddLast("boolexpr"); Rules[9].AddLast(")"); Rules[9].AddLast("stmt");
            Rules[10] = new LinkedList<string>(); Rules[10].AddLast("assgstmt"); Rules[10].AddLast("identifier"); Rules[10].AddLast("="); Rules[10].AddLast("arithexpr"); Rules[10].AddLast(";");
            Rules[11] = new LinkedList<string>(); Rules[11].AddLast("boolexpr"); Rules[11].AddLast("arithexpr"); Rules[11].AddLast("boolop"); Rules[11].AddLast("arithexpr");
            Rules[12] = new LinkedList<string>(); Rules[12].AddLast("boolop"); Rules[12].AddLast("<");
            Rules[13] = new LinkedList<string>(); Rules[13].AddLast("boolop"); Rules[13].AddLast(">");
            Rules[14] = new LinkedList<string>(); Rules[14].AddLast("boolop"); Rules[14].AddLast("<=");
            Rules[15] = new LinkedList<string>(); Rules[15].AddLast("boolop"); Rules[15].AddLast(">=");
            Rules[16] = new LinkedList<string>(); Rules[16].AddLast("boolop"); Rules[16].AddLast("==");
            Rules[17] = new LinkedList<string>(); Rules[17].AddLast("arithexpr"); Rules[17].AddLast("multexpr"); Rules[17].AddLast("arithexprprime");
            Rules[18] = new LinkedList<string>(); Rules[18].AddLast("arithexprprime"); Rules[18].AddLast("+"); Rules[18].AddLast("multexpr"); Rules[18].AddLast("arithexprprime");
            Rules[19] = new LinkedList<string>(); Rules[19].AddLast("arithexprprime"); Rules[19].AddLast("-"); Rules[19].AddLast("multexpr"); Rules[19].AddLast("arithexprprime");
            Rules[20] = new LinkedList<string>(); Rules[20].AddLast("arithexprprime"); Rules[20].AddLast("");
            Rules[21] = new LinkedList<string>(); Rules[21].AddLast("multexpr"); Rules[21].AddLast("simpleexpr");Rules[21].AddLast("multexprprime");
            Rules[22] = new LinkedList<string>(); Rules[22].AddLast("multexprprime"); Rules[22].AddLast("*"); Rules[22].AddLast("simpleexpr"); Rules[22].AddLast("multexprprime");
            Rules[23] = new LinkedList<string>(); Rules[23].AddLast("multexprprime"); Rules[23].AddLast("/"); Rules[23].AddLast("simpleexpr"); Rules[23].AddLast("multexprprime");
            Rules[24] = new LinkedList<string>(); Rules[24].AddLast("multexprprime"); Rules[24].AddLast("");
            Rules[25] = new LinkedList<string>(); Rules[25].AddLast("simpleexpr"); Rules[25].AddLast("identifier");
            Rules[26] = new LinkedList<string>(); Rules[26].AddLast("simpleexpr"); Rules[26].AddLast("number");
            Rules[27] = new LinkedList<string>(); Rules[27].AddLast("simpleexpr"); Rules[27].AddLast("("); Rules[27].AddLast("arithexpr"); Rules[27].AddLast(")");
            Rules[28] = new LinkedList<string>(); Rules[28].AddLast("number"); Rules[28].AddLast("int");
            Rules[29] = new LinkedList<string>(); Rules[29].AddLast("number"); Rules[29].AddLast("real");
            stackReset();
        }

        static private void stackReset()
        {
            Stack.Clear();
            Stack.Push("$");
            Stack.Push("program");

        }

        
        static public void reset()
        {
            stackReset();
            loopNum = 1;
            treeStack.Clear();
            errorList.Clear();
            token = null;
            root = null;
        }

        static public void pushStack (int rulesNo)
        {
            pushNode(Rules[rulesNo].First.Next);
        }

        static private double stringValue(string str)
        {
            return Convert.ToDouble(str);
        }

        static private string newLabel()
        {
            return "L" + loopNum++;
        }

        private static string gen(string op, string result, string para1, string para2)
        {
            return "   " + op + "\t" + result + "," + para1 + "," + para2 + "\r";
        }

        private static string gen(string lable)
        {
            return lable + ":\r";
        }

        private static string numberType(string type1, string type2)
        {
            if(type1 == "int" && type2 == "int")
            {
                return "int";
            }
            else
            {
                if(type1 == "real" && type2 == "real" || type1 == "int" && type2 == "real" || type1 == "real" && type2 == "int")
                {
                    return "real";
                }
            }
            return "";
        }


        public static void ergodic(SytaxNode node)
        {
            if (node.NextNode != null)
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
            return;
        }

        static public void inherit(SytaxNode node, int state)
        {
            switch(state)
            {
                case 0 :
                    break;
                case 1 :
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;
                case 16:
                    break;
                case 17:
                    break;
                case 18:
                    break;
                case 19:
                    break;
                case 20:
                    break;
                case 21:
                    break;
                case 22:
                    break;
                case 23:
                    break;
                case 24:
                    break;
                case 25:
                    break;
                case 26:
                    break;
                case 27:
                    break;
            }
        }

        static public void synthetical(SytaxNode node, int state)
        {
            switch (state)
            {
                case 0:
                    node.Code = node.NodeList[0].Code;
                    break;
                case 1:
                    node.Code = node.NodeList[0].Code;
                    break;
                case 2:
                    node.Code = node.NodeList[0].Code;
                    break;
                case 3:
                    node.Code = node.NodeList[0].Code;
                    break;
                case 4:
                    node.Code = node.NodeList[0].Code;
                    break;
                case 5:
                    node.Code = node.NodeList[1].Code;
                    break;
                case 6:
                    node.Code = node.NodeList[0].Code + node.NodeList[1].Code;
                    break;
                case 7:
                    node.Code = "";
                    break;
                case 8:
                    node.ElseAddr = newLabel();
                    node.AfterAddr = newLabel();
                    node.Code = node.NodeList[2].Code + gen("jmpf", node.NodeList[2].Place.Key, "", node.ElseAddr);
                    node.Code += node.NodeList[5].Code + gen("jmp", "", "", node.AfterAddr);
                    node.Code += gen(node.ElseAddr) + node.NodeList[7].Code + gen(node.AfterAddr);
                    break;
                case 9:
                    node.BeginAddr = newLabel();
                    node.AfterAddr = newLabel();
                    node.Code = gen(node.BeginAddr) + node.NodeList[2].Code;
                    node.Code += gen("jmpf", node.NodeList[2].Place.Key, "", node.AfterAddr) + node.NodeList[4].Code;
                    node.Code += gen("jmp", "", "", node.BeginAddr) + gen(node.AfterAddr);
                    break;
                case 10:
                    node.NodeList[0].Type = node.NodeList[2].Type;
                    node.NodeList[0].Place = SymbolTable.addSymbol(node.NodeList[0].Id, node.NodeList[0].Type, node.NodeList[0].Line, node.NodeList[0].Position);
                    node.Code = node.NodeList[2].Code + gen("mov", node.NodeList[0].Place.Key, "", node.NodeList[2].Place.Key);
                    break;
                case 11:
                    node.Type = "bool";
                    node.Place = SymbolTable.newtemp(node.Type);
                    node.Code = node.NodeList[0].Code + node.NodeList[2].Code;
                    switch (node.NodeList[1].Value)
                    {
                        case "<": 
                            node.Code += gen("lt", node.Place.Key, node.NodeList[0].Place.Key, node.NodeList[2].Place.Key);
                            break;
                        case ">":
                            node.Code += gen("gt", node.Place.Key, node.NodeList[0].Place.Key, node.NodeList[2].Place.Key);
                            break;
                        case "<=":
                            node.Code += gen("le", node.Place.Key, node.NodeList[0].Place.Key, node.NodeList[2].Place.Key);
                            break;
                        case ">=":
                            node.Code += gen("ge", node.Place.Key, node.NodeList[0].Place.Key, node.NodeList[2].Place.Key);
                            break;
                        case "==":
                            node.Code += gen("eq", node.Place.Key, node.NodeList[0].Place.Key, node.NodeList[2].Place.Key);
                            break;
                    }
                    break;
                case 12:
                    node.Value = "<";
                    break;
                case 13:
                    node.Value = ">";
                    break;
                case 14:
                    node.Value = "<=";
                    break;
                case 15:
                    node.Value = ">=";
                    break;
                case 16:
                    node.Value = "==";
                    break;
                case 17:
                    node.Code = node.NodeList[0].Code + node.NodeList[1].Code;
                    if(node.NodeList[1].Value == null)
                    {
                        node.Type = node.NodeList[0].Type;
                        node.Place = node.NodeList[0].Place;
                        node.Value = node.NodeList[0].Value;
                    }
                    else
                    {
                        node.Type = node.NodeList[1].Type;
                        node.Place = node.NodeList[1].Place;
                        node.Value = node.NodeList[1].Value;
                    }
                    break;
                case 18:
                    node.Type = numberType(node.Before.Place.Type, node.NodeList[1].Place.Type);
                    node.Place = SymbolTable.newtemp(node.Type);
                    node.Code = node.NodeList[1].Code + node.NodeList[2].Code;
                    node.Code += gen("add", node.Place.Key, node.Before.Place.Key,node.NodeList[1].Place.Key);
                    node.NodeList[1].Value = (stringValue(node.NodeList[1].Value) + stringValue(node.Before.Value)).ToString();
                    if(node.NodeList[2].Value == null)
                    {
                        node.Value = node.NodeList[1].Value;
                    }
                    else
                    {
                        node.Value = node.NodeList[2].Value;
                    }
                    break;
                case 19:
                    node.Type = numberType(node.Before.Place.Type, node.NodeList[1].Place.Type);
                    node.Place = SymbolTable.newtemp(node.Type);
                    node.Code = node.NodeList[1].Code + node.NodeList[2].Code;
                    node.Code += gen("sub", node.Place.Key, node.Before.Place.Key, node.NodeList[1].Place.Key);
                    node.NodeList[1].Value = (stringValue(node.Before.Value) - stringValue(node.NodeList[1].Value)).ToString() ;
                    if (node.NodeList[2].Value == null)
                    {
                        node.Value = node.NodeList[1].Value;
                    }
                    else
                    {
                        node.Value = node.NodeList[2].Value;
                    }
                    break;
                case 20:
                    node.Code = "";
                    node.Value = null;
                    break;
                case 21:
                    node.Code = node.NodeList[0].Code + node.NodeList[1].Code;
                    if (node.NodeList[1].Value == null)
                    {
                        node.Type = node.NodeList[0].Type;
                        node.Place = node.NodeList[0].Place;
                        node.Value = node.NodeList[0].Value;
                    }
                    else
                    {
                        node.Type = node.NodeList[1].Type;
                        node.Place = node.NodeList[1].Place;
                        node.Value = node.NodeList[1].Value;
                    }
                    break;
                case 22:
                    node.Type = numberType(node.Before.Place.Type, node.NodeList[1].Place.Type);
                    node.Place = SymbolTable.newtemp(node.Type);
                    node.Code = node.NodeList[1].Code + node.NodeList[2].Code;
                    node.Code += gen("sub", node.NodeList[1].Place.Key, node.Before.Place.Key, node.NodeList[1].Place.Key);
                    node.NodeList[1].Value = (stringValue(node.Before.Value) - stringValue(node.NodeList[1].Value)).ToString();
                    if (node.NodeList[2].Value == null)
                    {
                        node.Value = node.NodeList[1].Value;
                    }
                    else
                    {
                        node.Value = node.NodeList[2].Value;
                    }
                    break;
                case 23:
                    node.Type = numberType(node.Before.Place.Type, node.NodeList[1].Place.Type);
                    node.Place = SymbolTable.newtemp(node.Type);
                    node.Code = node.NodeList[1].Code + node.NodeList[2].Code;
                    node.Code += gen("sub", node.NodeList[1].Place.Key, node.Before.Place.Key, node.NodeList[1].Place.Key);
                    node.NodeList[1].Value = (stringValue(node.Before.Value) - stringValue(node.NodeList[1].Value)).ToString();
                    if (node.NodeList[2].Value == null)
                    {
                        node.Value = node.NodeList[1].Value;
                    }
                    else
                    {
                        node.Value = node.NodeList[2].Value;
                    }
                    break;
                case 24:
                    node.Code = "";
                    node.Value = null;
                    break;
                case 25:
                    node.Type = node.NodeList[0].Type;
                    node.Place = SymbolTable.newtemp(node.Type);
                    node.Value = node.NodeList[0].Value;
                    node.NodeList[0].Place = SymbolTable.getSymbol(node.NodeList[0].Id);
                    node.Code = gen("mov", node.Place.Key, "", node.NodeList[0].Place.Key);
                    break;
                case 26:
                    node.Code = node.NodeList[0].Code;
                    node.Type = node.NodeList[0].Type;
                    node.Place = node.NodeList[0].Place;
                    break;
                case 27:
                    node.Value = node.NodeList[1].Value;
                    node.Place = node.NodeList[1].Place;
                    node.Type = node.NodeList[1].Type;
                    break;
                case 28:
                    node.Type = "int";
                    node.Place = SymbolTable.newtemp(node.Type);
                    node.Value = node.NodeList[0].Value;
                    node.Code = gen("mov", node.Place.Key, "", node.NodeList[0].Value);
                    break;
                case 29:
                    node.Type = "real";
                    node.Place = SymbolTable.newtemp(node.Type);
                    node.Value = node.NodeList[0].Value;
                    node.Code = gen("mov", node.Place.Key, "", node.NodeList[0].Value);
                    break;
            }

        }

        static private void pushNode(LinkedListNode<string> node)
        {
            if (node.Next != null)
            {
                pushNode(node.Next);
            }
            stack.Push(node.Value);
        }

        static public bool isTerminator(string type)
        {
            for(int i=0;i<Rules.Length;i++){
                if (type == Rules[i].First.Value)
                {
                    return false;
                }
            }
            return true;
        }

        static private void createTable(){
            for (int i = 0; i < Rules.Length; i++)
            {
                string terminator = Rules[i].First.Value;
                if (!table.ContainsKey(terminator))
                {
                    table.Add(terminator,new Dictionary<string,int>());
                }
                HashSet<string> firstSymbols = first(Rules[i].First.Next.Value);
                foreach(string symbol in firstSymbols){
                    if (symbol != "")
                    {
                        if(!table[terminator].ContainsKey(symbol))
                        { 
                            table[terminator].Add(symbol, i);
                        }
                    }
                    else
                    {
                        HashSet<string> followSymbols = follow(terminator);
                        foreach(string followSymbol in followSymbols)
                        {
                            if (!table[terminator].ContainsKey(followSymbol))
                            {
                                table[terminator].Add(followSymbol, i);
                            }
                        }
                    }
                }
            }
        }

        static private HashSet<string> first(string symbol)
        {
            HashSet<string> firstSymbols = new HashSet<string>();
            if (isTerminator(symbol))
            {
                firstSymbols.Add(symbol);
                return firstSymbols;
            }
            else
            {
                for(int i = 0; i<Rules.Length; i++)
                {
                    string terminator = Rules[i].First.Value;
                    if (terminator == symbol)
                    {
                        LinkedListNode<string> node = Rules[i].First;
                        HashSet<string> nodeFirstSymbols = null;
                        while (node.Next != null && (nodeFirstSymbols == null || nodeFirstSymbols.Contains("") ))
                        {
                            node = node.Next;
                            nodeFirstSymbols = first(node.Value);
                            firstSymbols.UnionWith(nodeFirstSymbols);
                        }
                    }
                }
            }
            return firstSymbols;
        }

        static private HashSet<string> follow(string symbol)
        {
            HashSet<string> followSymbols = new HashSet<string>();
            if (symbol == "program")
            {
                followSymbols.Add("$");
            }
            for (int i = 0; i < 28; i++)
            {
                LinkedListNode<string> node = Rules[i].First.Next;
                while (true)
                {
                    if (node.Value == symbol)
                    {
                        LinkedListNode<string> tempNode = node;
                        HashSet<string> nextFirst = null;
                        do
                        {
                            if (tempNode.Next == null)
                            {
                                if (symbol != Rules[i].First.Value)
                                {
                                    followSymbols.UnionWith(follow(Rules[i].First.Value));
                                }
                                break;
                            }
                            tempNode = tempNode.Next;
                            nextFirst = first(tempNode.Value);
                            followSymbols.UnionWith(nextFirst);
                            if (followSymbols.Contains(""))
                            {
                                followSymbols.Remove("");
                            }
                        }
                        while (nextFirst.Contains(""));
                    }
                    if (node.Next != null)
                    {
                        node = node.Next;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return followSymbols;            
        }

        static public int sytaxAnalyse(object sender, EventArgs e)//语法分析 返回分析结果 -1：错误 -2：错误并分析结束 0：正确 1：正确的结尾
        {
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
                        beforeIsWrong = false;
                        return 1;
                    }
                    else
                    {
                        MessageBox.Show("Wrong token type!");
                    }
                }
                if (LLparser.isTerminator(symbol))
                {
                    SytaxNode newNode = new SytaxNode(symbol);
                    if (token.Tokentype == "int" || token.Tokentype == "real")
                    {
                        newNode.Value = token.Attributevalue;
                    }
                    else
                    {
                        if (token.Tokentype == "identifier")
                        {
                            newNode.Id = token.Attributevalue;
                        }
                    }
                    treeStack.Peek().Nodes.Add(newNode);
                    LLparser.Stack.Pop();
                    if (symbol != token.Tokentype)
                    {
                        List<string> error = new List<string>();
                        error.Add(token.Linenumber.ToString());
                        error.Add(token.Lineposition.ToString());
                        error.Add(token.Tokentype);
                        error.Add(symbol);
                        error.Add(token.Attributevalue.Length.ToString());
                        error.Add(beforeIsWrong.ToString());
                        errorList.Add(error);
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
                            beforeIsWrong = true;
                            return -2;
                        }
                        beforeIsWrong = true;
                        return -1;
                    }
                }
                else
                {

                    while (!isTerminator(symbol))
                    {
                        if (Table[symbol].ContainsKey(token.Tokentype))
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
                            List<string> error = new List<string>();
                            error.Add(token.Linenumber.ToString());
                            error.Add(token.Lineposition.ToString());
                            error.Add(token.Tokentype);
                            error.Add(Table[symbol].ToString());
                            error.Add(token.Attributevalue.Length.ToString());
                            error.Add(beforeIsWrong.ToString());
                            errorList.Add(error);
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
                                if (symbol == "$")
                                {
                                    beforeIsWrong = true;
                                    return -2;
                                }
                            }
                            if (token.Tokentype == "$")
                            {
                                beforeIsWrong = true;
                                return -2;
                            }
                            beforeIsWrong = true;
                            return -1;
                        }
                    }
                    SytaxNode lastNode = new SytaxNode(symbol);
                    if (token.Tokentype == "int" || token.Tokentype == "real")
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
            }
            beforeIsWrong = false;
            return 0;
        }
    }

}
