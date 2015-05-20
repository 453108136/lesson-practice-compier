using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class Token
    {
        private string tokentype;
        public string Tokentype
        {
            get { return tokentype; }
            set { tokentype = value; }
        }

        private string attributevalue;
        public string Attributevalue
        {
            get { return attributevalue; }
            set { attributevalue = value; }
        }

        private int linenumber;
        public int Linenumber
        {
            get { return linenumber; }
            set { linenumber = value; }
        }

        private int lineposition;
        public int Lineposition
        {
            get { return lineposition; }
            set { lineposition = value; }
        }

        public Token(string tokentype, string attributevalue, int linenumber, int lineposition)
        {
            this.tokentype = tokentype;
            this.attributevalue=attributevalue;
            this.linenumber = linenumber;
            this.lineposition = lineposition;
        }

    }
}
