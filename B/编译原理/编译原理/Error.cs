using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class Error
    {
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

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        private static int count = 0;
        
        private int errorNo;
        public int ErrorNo
        {
            get { return errorNo; }
            set { errorNo = value; }
        }

        public Error(int linenumber, int lineposition, string message)
        {
            this.linenumber = linenumber;
            this.lineposition = lineposition;
            this.message = message;
            errorNo = ++count;
        }

        public static void reset()
        {
            count = 0;
        }
    }
}
