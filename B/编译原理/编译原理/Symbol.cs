using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class Symbol
    {
        private string key;     //符号的名称

        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        private string value;       //符号的值

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private string type;        //符号的类型

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private int line;       //符号所在行

        public int Line
        {
            get { return line; }
            set { line = value; }
        }
        private int positon;    //符号所在列

        public int Positon
        {
            get { return positon; }
            set { positon = value; }
        }

        public Symbol(string key, string type, int line, int postiton)
        {
            Key = key;
            Type = type;
            Line = line;
            Positon = positon;
        }

        public Symbol(string key, string type)      //用于临时变量的创建
        {
            Key = key;
            Type = type;
            Line = 0;
            Positon = 0;
        }
    }
}
