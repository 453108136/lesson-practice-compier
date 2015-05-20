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

        internal static Dictionary<string, LinkedList<Symbol>> Table
        {
            get { return table; }
            set { table = value; }
        }
    }

}
