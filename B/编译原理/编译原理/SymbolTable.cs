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
        private static int tempNum = 1;     //临时变量命名所需的临时数，如t1
        private static Dictionary<string, LinkedList<Symbol>> table = new Dictionary<string,LinkedList<Symbol>>();      //二维符号表，对值加链表实现

        internal static Dictionary<string, LinkedList<Symbol>> Table
        {
            get { return table; }
        }

        public static void reset()      //清空符号表
        {
            tempNum = 1;
            table = new Dictionary<string,LinkedList<Symbol>>();
        }

        public static Symbol addSymbol(string key, string type, int line, int postiton)     //向符号表中添加符号
        {
            if (!table.ContainsKey(key))    //如果表中存在该符号则加入链表中
            {
                LinkedList<Symbol> linkedList = new LinkedList<Symbol>();
                SymbolTable.Table.Add(key, linkedList);
                table[key].AddFirst(new Symbol(key, type, line, postiton));
            }
            //if (table[key] == null)     //如果不存在，新建该符号的链表加入符号表
            //{
            //    table[key] = new LinkedList<Symbol>();
            //    table[key].AddFirst(new Symbol(key, type, line, postiton));
            //}
            else     //如果不存在，新建该符号的链表加入符号表
            {
                table[key].AddFirst(new Symbol(key, type, line, postiton));
            }
            return table[key].First.Value;      //返回符号表地址，对应的符号，Symbol类对象
        }

        public static Symbol getSymbol(string key)  //获取符号表地址，对应的符号，Symbol类对象
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

        public static Symbol newtemp(string type)   //添加临时变量在符号表中，返回符号表地址，对应的符号，Symbol类对象
        {
            string tempname = "t" + tempNum;        //变量名tn
            table[tempname] = new LinkedList<Symbol>();
            Symbol tempSym = new Symbol(tempname, type);
            table[tempname].AddFirst(tempSym);
            tempNum++;
            return tempSym;
        }
    }

}
