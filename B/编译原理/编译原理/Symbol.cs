using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class Symbol
    {
        private string key;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        private string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private int line;

        public int Line
        {
            get { return line; }
            set { line = value; }
        }
        private int positon;

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
    }
}
