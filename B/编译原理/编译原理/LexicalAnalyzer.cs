using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class LexicalAnalyzer
    {
        static public ArrayList errorList = new ArrayList();
        string[] keywords = {"int", "real", "if", "then", "else", "while"};
        string[] delimiters = { "(", ")", ";", "{", "}" };
        private char ch;
        static public StreamReader input;
        static private string str;

        public static string Str
        {
            get { return LexicalAnalyzer.str; }
            set { LexicalAnalyzer.str = value; }
        }

        static public int position, line = 1, col = 0, code=0; //position用来记录字符位于整个文档的位置，line为行数，col为列数


        static public string[,] erList = new string [1000,4];
        public static int countColumError = 0;

        public static void erListClear() //清空erList
        {
            erList.Initialize();
            countColumError = 0;
        }

        public static void tokenClear() //清空词法的编译，将位置置为0
        {
            position = 0;
            line = 1;
            col = 0;
            code = 0;
        }

        private bool isExistKeywords(string a) //判断是否为关键字
        {
            int i;
            bool result = false;
            for ( i=0; i < 6; i++)
            {
                if (a == keywords[i])
                {
                    result = true;
                }
            }
            return result;
        }

        private bool isExistDelimiters(string a) //判断是否为括号及分号
        {
            int j;
            bool result = false;
            for (j = 0; j < 5; j++)
            {
                if (a == delimiters[j])
                {
                    result = true;
                }
            }
            return result;
        }

        private bool isLetter(char a) //判断是否为字母
        { 
            if ((a >= 'a' && a <= 'z') || (a >= 'A' && a <= 'Z')) 
                return true;             
            else return false;         
        }

        private bool isDigit(char a)  //判断是否为数字
        { 
            if (a >= '0' && a <= '9') 
                return true;             
            else return false;         
        }

        public Token nextToken() //对文档的下一个token进行分析
        {
            int state = 0;  //开始时为0状态
            code = 0;

            string attrva = ""; //保存token的值，逐步加入
            int coun = 0; //保存token的长度
           
            for (int i =0;i < Str.Length;i++) //进行状态跳转，读入文档的下一个字符，分析完成后返回一个token的类型值。
            {
                if (position < Str.Length) 
                {
                    switch (state)
                    {
                        case 0:
                            ch = Str[position];
                            if (ch == '<')
                            { state = 1; position += 1; col += 1; coun += 1; attrva += ch.ToString(); } 
                            else if (ch == '=')
                            { state = 3; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else if (ch == '>')
                            { state = 5; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else if (ch == '!')
                            { state = 7; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else if (isLetter(ch))
                            { state = 9; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else if (isDigit(ch))
                            { state = 10; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else if (ch == '/')
                            { state = 16; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else if (ch == '+' || ch == '-' || ch == '*') 
                            {
                                attrva = ch.ToString();
                                Token token = new Token(ch.ToString(), ch.ToString(), line, col - coun);
                                //output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\r');
                                state = -1;
                                coun = 0;
                                col += 1;
                                position += 1;
                                attrva = "";
                                return token;
                            }
                            else if (isExistDelimiters(ch.ToString()))
                            {
                                attrva = ch.ToString();
                                Token token = new Token(ch.ToString(), ch.ToString(), line, col - coun);
                                //output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\r');
                                state = -1;
                                coun = 0;
                                col += 1;
                                position += 1;
                                attrva = "";
                                return token;
                            }
                            else if (ch == ' ')
                            {
                                position += 1;
                                col += 1;
                                state = 0;
                                coun = 0;
                            }
                            else if (ch == '\r')
                            {
                                line += 1;
                                position += 1;
                                col = 0;
                                state = 0;
                                coun = 0;
                            }
                            else if (ch == '$')
                            {
                                Token a = new Token('$'.ToString(),'$'.ToString(),line,col);
                                //output.Write(a.Tokentype + " '" + a.Attributevalue + "' linenumber:" + a.Linenumber + " lineposition:" + a.Lineposition + '\r');
                                return a;
                            }
                            else
                            {
                                Token b = new Token("error", "illegal char input", line, col - coun);
                                //errorOutput.Write(a.ErrorNo + " line: " + line + "   position: " + col + " :'" + ch + "'  " + a.Message + "\r");
                                errorList.Add(b);
                                state = -1;
                                coun = 0;
                                position += 1;
                                col += 1;
                                return b;
                            }
                            break;

                        case 1:
                            ch = Str[position];
                            if (ch == '=')
                            { state = 2; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else
                            {
                                Token token = new Token(attrva, attrva, line, col - coun);
                                //output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\r');
                                state = -1;
                                coun = 0;
                                attrva = "";
                                i--;
                                return token;
                            }
                            break;

                        case 2:
                            Token token1 = new Token(attrva, attrva, line, col - coun);
                            //output.Write(token1.Tokentype + " '" + token1.Attributevalue + "' linenumber:" + token1.Linenumber + " lineposition:" + token1.Lineposition + '\r');
                            state = -1;
                            coun = 0;
                            attrva = "";
                            i--;
                            return token1;

                        case 3:
                            ch = Str[position];
                            if (ch == '=')
                            { state = 4; position += 1; coun += 1; attrva += ch.ToString(); }
                            else
                            {
                                Token token = new Token(attrva, attrva, line, col - coun);
                                //output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\r');
                                state = -1;
                                coun = 0;
                                attrva = "";
                                i--;
                                return token;
                            }
                            break;

                        case 4:
                            Token token2 = new Token(attrva, attrva, line, col - coun);
                            //output.Write(token2.Tokentype + " '" + token2.Attributevalue + "' linenumber:" + token2.Linenumber + " lineposition:" + token2.Lineposition + '\r');
                            state = -1;
                            coun = 0;
                            attrva = "";
                            i--;
                            return token2;

                        case 5:
                            ch = Str[position];
                            if (ch == '=')
                            { state = 6; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else
                            {
                                Token token = new Token(attrva, attrva, line, col - coun);
                                //output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\r');
                                state = -1;
                                coun = 0;
                                attrva = "";
                                i--;
                                return token;
                            }
                            break;

                        case 6:
                            Token token3 = new Token(attrva, attrva, line, col - coun);
                            //output.Write(token3.Tokentype + " '" + token3.Attributevalue + "' linenumber:" + token3.Linenumber + " lineposition:" + token3.Lineposition + '\r');
                            state = -1;
                            coun = 0;
                            attrva = "";
                            i--;
                            return token3;

                        case 7:
                            ch = Str[position];
                            if (ch == '=')
                            { state = 8; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else
                            {
                                Token b = new Token("error", "illegal char input", line, col - coun);
                                //errorOutput.Write(a.ErrorNo + " line: " + line + "   position: " + col + " :'" + ch + "'  " + a.Message + "\r");
                                errorList.Add(b);
                                state = -1;
                                coun = 0;
                                attrva = "";
                                i--;                                
                                return b;
                            }
                            break;

                        case 8:
                            Token token4 = new Token(attrva, attrva, line, col - coun);
                            //output.Write(token4.Tokentype + " '" + token4.Attributevalue + "' linenumber:" + token4.Linenumber + " lineposition:" + token4.Lineposition + '\r');
                            state = -1;
                            coun = 0;
                            attrva = "";
                            i--;
                            return token4;

                        case 9:
                            ch = Str[position];
                            if (isLetter(ch) || isDigit(ch))
                            { state = 9; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else
                            {
                                if (isExistKeywords(attrva))
                                {
                                    Token token = new Token(attrva, attrva, line, col - coun);
                                    //output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\r');
                                    state = -1;
                                    coun = 0;
                                    attrva = "";
                                    i--;
                                    return token;
                                }
                                else
                                {
                                    Token token = new Token("identifier", attrva, line, col - coun);
                                    //output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\r');
                                    //if (SymbolTable.Table.ContainsKey(attrva))
                                    //{
                                    //    SymbolTable.Table[attrva].AddFirst(new Symbol(attrva, "identifier", line, col - coun));
                                    //}
                                    //else
                                    //{
                                    //    LinkedList<Symbol> linkedList = new LinkedList<Symbol>();
                                    //    linkedList.AddFirst(new Symbol(attrva, "identifier", line, col - coun));
                                    //    SymbolTable.Table.Add(attrva, linkedList);
                                    //}
                                    state = -1;
                                    coun = 0;
                                    attrva = "";
                                    i--;
                                    return token;
                                }
                            }
                            break;

                        case 10:
                            ch = Str[position];
                            if (isDigit(ch))
                            { state = 10; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else if (ch == '.')
                            { state = 11; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else if (ch == 'E' || ch == 'e')
                            { state = 13; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else
                            {
                                Token token = new Token("int", attrva, line, col - coun);
                                //output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\r');
                                state = -1;
                                coun = 0;
                                attrva = "";
                                i--;
                                return token;
                            }
                            break;

                        case 11:
                            ch = Str[position];
                            if (isDigit(ch))
                            { state = 12; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else
                            {
                                Token b = new Token("error", "illegal char input", line, col - coun);
                                //errorOutput.Write(a.ErrorNo + " line: " + line + "   position: " + col + " :'" + ch + "'  " + a.Message + "\r");
                                errorList.Add(b);
                                state = -1;
                                coun = 0;
                                attrva = "";
                                i--;
                                return b;
                            }
                            break;

                        case 12:
                            ch = Str[position];
                            if (isDigit(ch))
                            { state = 12; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else if (ch == 'E' || ch == 'e')
                            { state = 13; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else
                            {
                                Token token = new Token("real", attrva, line, col - coun);
                                //output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\r');
                                state = -1;
                                coun = 0;
                                attrva = "";
                                i--;
                                return token;
                            }
                            break;

                        case 13:
                            ch = Str[position];
                            if (isDigit(ch))
                            { state = 15; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else if (ch == '+' || ch == '-')
                            { state = 14; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else
                            {
                                Token b = new Token("error", "illegal char input", line, col - coun);
                                //errorOutput.Write(a.ErrorNo + " line: " + line + "   position: " + col + " :'" + ch + "'  " + a.Message + "\r");
                                errorList.Add(b);
                                state = -1;
                                coun = 0;
                                attrva = "";
                                i--;
                                return b;
                            }
                            break;

                        case 14:
                            ch = Str[position];
                            if (isDigit(ch))
                            { state = 15; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else
                            {
                                Token b = new Token("error", "illegal char input", line, col - coun);
                                //errorOutput.Write(a.ErrorNo + " line: " + line + "   position: " + col + " :'" + ch + "'  " + a.Message + "\r");
                                errorList.Add(b);
                                state = -1;
                                coun = 0;
                                attrva = "";
                                i--;
                                return b;
                            }
                            break;

                        case 15:
                            ch = Str[position];
                            if (isDigit(ch))
                            { state = 15; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else
                            {
                                Token token = new Token("real", attrva, line, col - coun);
                                //output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\r');
                                state = -1;
                                coun = 0;
                                attrva = "";
                                i--;
                                return token;
                            }
                            break;

                        case 16:
                            ch = Str[position];
                            if (ch == '/')
                            { state = 17; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            else
                            {
                                Token token = new Token("/", attrva, line, col - coun);
                                //output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\r');
                                state = -1;
                                coun = 0;
                                attrva = "";
                                i--;
                                return token;
                            }
                            break;

                        case 17:
                            ch = Str[position];
                            if (ch == '\r')
                            {
                                Token token = new Token("comments", attrva, line, col - coun);
                                //output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\r');
                                state = -1;
                                coun = 0;
                                attrva = "";
                                i--;
                                return token;
                            }
                            else
                            { state = 17; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                            break;
                    }
                }
                else
                {
                    code = -1;
                    break;
                }
            }
            return null;
        }
    }
}
