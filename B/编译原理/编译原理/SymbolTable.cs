using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class SymbolTable
    {
        private static Dictionary<string, LinkedList<Symbol>> table = new Dictionary<string,LinkedList<Symbol>>();
        private static int tempNum = 1;

        internal static Dictionary<string, LinkedList<Symbol>> Table
        {
            get { return table; }
        }

        public static void reset()
        {
            tempNum = 1;
            table = new Dictionary<string,LinkedList<Symbol>>();
        }

        public static Symbol addSymbol(string key, string type, int line, int postiton)
        {
            if (!SymbolTable.Table.ContainsKey(key))
            {
                LinkedList<Symbol> linkedList = new LinkedList<Symbol>();
                SymbolTable.Table.Add(key, linkedList);
            }
            if (table[key] == null)
            {
                table[key] = new LinkedList<Symbol>();
                table[key].AddFirst(new Symbol(key, type, line, postiton));
            }
            else
            {

                table[key].AddFirst(new Symbol(key, type, line, postiton));
            }
            return table[key].First.Value;
        }

        public static Symbol getSymbol(string key)
        {
            if (table[key] == null)
            {
                return null;
            }
            else
            {
                return table[key].First.Value;
            }
            
        }

        public static Symbol newtemp(string type)
        {
            string tempname = "t" + tempNum;
            table[tempname] = new LinkedList<Symbol>();
            Symbol tempSym = new Symbol(tempname, type);
            table[tempname].AddFirst(tempSym);
            tempNum++;
            return tempSym;
        }
    }

}
