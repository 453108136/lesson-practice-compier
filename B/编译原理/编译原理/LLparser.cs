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
        static private LinkedList<string>[] Rules = new LinkedList<string>[28];
        static private Dictionary<string, Dictionary<string, int>> table = new Dictionary<string,Dictionary<string,int>>();
        static private Stack<string> stack = new Stack<string>();

        public static Stack<string> Stack
        {
            get { return LLparser.stack; }
            set { LLparser.stack = value; }
        }

        public static Dictionary<string, Dictionary<string, int>> Table
        {
            get { return LLparser.table; }
        }

        //static LLparser()
        public void make()
        {
            init();
            createTable();
        }

        private static void init()
        {
            Rules[0] = new LinkedList<string>(); Rules[0].AddLast("program"); Rules[0].AddLast("compoundstmt");
            Rules[1] = new LinkedList<string>(); Rules[1].AddLast("stmt"); Rules[1].AddLast("ifstmt");
            Rules[2] = new LinkedList<string>(); Rules[2].AddLast("stmt"); Rules[2].AddLast("whilestmt");
            Rules[3] = new LinkedList<string>(); Rules[3].AddLast("stmt"); Rules[3].AddLast("assgstmt");
            Rules[4] = new LinkedList<string>(); Rules[4].AddLast("stmt"); Rules[4].AddLast("compoundstmt");
            Rules[5] = new LinkedList<string>(); Rules[5].AddLast("compoundstmt"); Rules[5].AddLast("{"); Rules[5].AddLast("stmts"); Rules[5].AddLast("}");
            Rules[6] = new LinkedList<string>(); Rules[6].AddLast("stmts"); Rules[6].AddLast("stmt"); Rules[6].AddLast("stmts");
            Rules[7] = new LinkedList<string>(); Rules[7].AddLast("stmts"); Rules[7].AddLast("");
            Rules[8] = new LinkedList<string>(); Rules[8].AddLast("ifstmt"); Rules[8].AddLast("if"); Rules[8].AddLast("("); Rules[8].AddLast("boolexpr"); Rules[8].AddLast(")"); Rules[8].AddLast("then"); Rules[8].AddLast("stmt"); Rules[8].AddLast("else"); Rules[8].AddLast("stmt");
            Rules[9] = new LinkedList<string>(); Rules[9].AddLast("whilestmt"); Rules[9].AddLast("while"); Rules[9].AddLast("("); Rules[9].AddLast("boolexpr"); Rules[9].AddLast(")"); Rules[9].AddLast("stmt");
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
            stackReset();

        }

        static public void stackReset()
        {
            Stack.Push("$");
            Stack.Push("program");

        }

        static public void pushStack (int rulesNo)
        {
            pushNode(Rules[rulesNo].First.Next);
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
            for(int i=0;i<28;i++){
                if (type == Rules[i].First.Value)
                {
                    return false;
                }
            }
            return true;
        }

        static private void createTable(){
            for (int i = 0; i < 28; i++)
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
                for(int i = 0; i<28; i++)
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
               // if (node.Value == symbol)
                //{
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
                            //followSymbols.UnionWith(follow(Rules[i].First.Value));
                            break;
                        }
                    }                   
               // }
            }
            return followSymbols;            
        }        
    }
}
