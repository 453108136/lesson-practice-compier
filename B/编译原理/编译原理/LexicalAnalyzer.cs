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
        static public StreamWriter output;
        static public StreamWriter errorOutput;

        static public string[,] tokenList = new string[10000,4];
        public static int countColum = 0;
        static public string[,] erList = new string [10000,4];
        public static int countColumError = 0;

        public static void tokenListReset()
        {
            countColum = 0;
        }

        private bool isExistKeywords(string a)
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

        private bool isExistDelimiters(string a)
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

        private bool isLetter(char a)         
        { 
            if ((a >= 'a' && a <= 'z') || (a >= 'A' && a <= 'Z')) 
                return true;             
            else return false;         
        }

        private bool isDigit(char a)         
        { 
            if (a >= '0' && a <= '9') 
                return true;             
            else return false;         
        }

        public void isToken()
        {
            int line = 1, position = 0, col = 0;
            int state = 0;
            string str="";

            while( !input.EndOfStream)
            {
                str += input.ReadLine() + '\n';
            }

            string attrva = "";
            int coun = 0;
            Error.reset();
            for (int i =0;i<str.Length;i++)
            {
                switch (state)
                {
                    case 0:                
                        ch = str[position];
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
                            Token token = new Token("operators", ch.ToString(), line, col - coun);
                            output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\n');
                            tokenList[countColum,0] = "operators";
                            tokenList[countColum,1] = attrva;
                            tokenList[countColum,2] = line.ToString();
                            tokenList[countColum,3] = (col - coun).ToString();
                            countColum++;
                            state = 0;
                            coun = 0;
                            col += 1;
                            position += 1;
                            attrva = "";
                        }
                        else if (isExistDelimiters(ch.ToString()))
                        {
                            attrva = ch.ToString();
                            Token token = new Token("delimiters", ch.ToString(), line, col - coun);
                            output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\n');
                            tokenList[countColum,0] = "delimiters";
                            tokenList[countColum,1] = attrva;
                            tokenList[countColum,2] = line.ToString();
                            tokenList[countColum,3] = (col - coun).ToString();
                            countColum++;
                            state = 0;
                            coun = 0;
                            col += 1;
                            position += 1;
                            attrva = "";
                        }
                        else if (ch == ' ')
                        {
                            position += 1;
                            col += 1;
                            state = 0;
                            coun = 0;
                        }
                        else if (ch == '\n')
                        {
                            line += 1;
                            position += 1;
                            col = 0;
                            state = 0;
                            coun = 0;
                        }
                        else
                        {
                            Error a = new Error(line, col - coun, "illegal char input");
                            errorOutput.Write(a.ErrorNo + " line: "+line + "   position: " + col + " :'"+ ch +"'  "+a.Message + "\n");
                            errorList.Add(a);
                            erList[countColumError, 0] = a.ErrorNo.ToString();
                            erList[countColumError, 1] = a.Message;
                            erList[countColumError, 2] = line.ToString();
                            erList[countColumError, 3] = (col - coun).ToString();
                            countColumError++;
                            state = 0;
                            coun = 0;
                            position += 1;
                            col += 1;
                        }
                        break;

                    case 1:
                        ch = str[position];
                        if (ch == '=')
                        { state = 2; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                        else
                        {
                            Token token = new Token("operators", attrva, line, col - coun);
                            output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\n');
                            tokenList[countColum,0] = "operators";
                            tokenList[countColum,1] = attrva;
                            tokenList[countColum,2] = line.ToString();
                            tokenList[countColum,3] = (col - coun).ToString();
                            countColum++;
                            state = 0;
                            coun = 0;
                            attrva = "";
                            i--;
                        }
                        break;

                    case 2:
                        Token token1 = new Token("operators", attrva, line, col - coun);
                        output.Write(token1.Tokentype + " '" + token1.Attributevalue + "' linenumber:" + token1.Linenumber + " lineposition:" + token1.Lineposition + '\n');
                        tokenList[countColum,0] = "operators";
                        tokenList[countColum,1] = attrva;
                        tokenList[countColum,2] = line.ToString();
                        tokenList[countColum,3] = (col - coun).ToString();
                        countColum++;
                        state = 0;
                        coun = 0;
                        attrva = "";
                        i--;
                        break;

                    case 3:
                        ch = str[position];
                        if (ch == '=')
                        { state = 4; position += 1; coun += 1; attrva += ch.ToString(); }
                        else
                        {
                            Token token = new Token("operators", attrva, line, col - coun);
                            output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\n');
                            tokenList[countColum,0] = "operators";
                            tokenList[countColum,1] = attrva;
                            tokenList[countColum,2] = line.ToString();
                            tokenList[countColum,3] = (col - coun).ToString();
                            countColum++;
                            state = 0;
                            coun = 0;
                            attrva = "";
                            i--;
                        }
                        break;

                    case 4:
                        Token token2 = new Token("operators", attrva, line, col - coun);
                        output.Write(token2.Tokentype + " '" + token2.Attributevalue + "' linenumber:" + token2.Linenumber + " lineposition:" + token2.Lineposition + '\n');
                        tokenList[countColum,0] = "operators";
                        tokenList[countColum,1] = attrva;
                        tokenList[countColum,2] = line.ToString();
                        tokenList[countColum,3] = (col - coun).ToString();
                        countColum++;
                        state = 0;
                        coun = 0;
                        attrva = "";
                        i--;
                        break;

                    case 5:
                        ch = str[position];
                        if (ch == '=')
                        { state = 6; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                        else
                        {
                            Token token = new Token("operators", attrva, line, col - coun);
                            output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\n');
                            tokenList[countColum,0] = "operators";
                            tokenList[countColum,1] = attrva;
                            tokenList[countColum,2] = line.ToString();
                            tokenList[countColum,3] = (col - coun).ToString();
                            countColum++;
                            state = 0;
                            coun = 0;
                            attrva = "";
                            i--;
                        }
                        break;

                    case 6:
                        Token token3 = new Token("operators", attrva, line, col - coun);
                        output.Write(token3.Tokentype + " '" + token3.Attributevalue + "' linenumber:" + token3.Linenumber + " lineposition:" + token3.Lineposition + '\n');
                        tokenList[countColum,0] = "operators";
                        tokenList[countColum,1] = attrva;
                        tokenList[countColum,2] = line.ToString();
                        tokenList[countColum,3] = (col - coun).ToString();
                        countColum++;
                        state = 0;
                        coun = 0;
                        attrva = "";
                        i--;
                        break;

                    case 7:
                        ch = str[position];
                        if (ch == '=')
                        { state = 8; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                        else
                        {
                            Error a = new Error(line, col - coun, "illegal char input");
                            errorOutput.Write(a.ErrorNo + " line: " + line + "   position: " + col + " :'" + ch + "'  " + a.Message + "\n");
                            errorList.Add(a);
                            erList[countColumError, 0] = a.ErrorNo.ToString();
                            erList[countColumError, 1] = a.Message;
                            erList[countColumError, 2] = line.ToString();
                            erList[countColumError, 3] = (col - coun).ToString();
                            countColumError++;
                            state = 0;
                            coun = 0;
                            attrva = "";
                            i--;
                        }
                        break;

                    case 8:
                        Token token4 = new Token("operators", attrva, line, col - coun);
                        output.Write(token4.Tokentype + " '" + token4.Attributevalue + "' linenumber:" + token4.Linenumber + " lineposition:" + token4.Lineposition + '\n');
                        tokenList[countColum,0] = "operators";
                        tokenList[countColum,1] = attrva;
                        tokenList[countColum,2] = line.ToString();
                        tokenList[countColum,3] = (col - coun).ToString();
                        countColum++;
                        state = 0;
                        coun = 0;
                        attrva = "";
                        i--;
                        break;

                    case 9:
                        ch = str[position];
                        if (isLetter(ch)||isDigit(ch))
                        { state = 9; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                        else
                        {
                            if (isExistKeywords(attrva))
                            {
                                Token token = new Token("keywords", attrva, line, col - coun);
                                output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\n');
                                tokenList[countColum,0] = "keywords";
                                tokenList[countColum,1] = attrva;
                                tokenList[countColum,2] = line.ToString();
                                tokenList[countColum,3] = (col - coun).ToString();
                                countColum++;
                                state = 0;
                                coun = 0;
                                attrva = "";
                                i--;
                            }
                            else
                            {
                                Token token = new Token("identifiers", attrva, line, col - coun);
                                output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\n');
                                tokenList[countColum,0] = "identifiers";
                                tokenList[countColum,1] = attrva;
                                tokenList[countColum,2] = line.ToString();
                                tokenList[countColum,3] = (col - coun).ToString();
                                countColum++;
                                state = 0;
                                coun = 0;
                                attrva = "";
                                i--;
                            }
                        }
                        break;

                    case 10:
                        ch = str[position];
                        if (isDigit(ch))
                        { state = 10; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                        else if (ch == '.')
                        { state = 11; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                        else if (ch == 'E' || ch == 'e')
                        { state = 13; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                        else
                        {
                            Token token = new Token("numbers", attrva, line, col - coun);
                            output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\n');
                            tokenList[countColum,0] = "numbers";
                            tokenList[countColum,1] = attrva;
                            tokenList[countColum,2] = line.ToString();
                            tokenList[countColum,3] = (col - coun).ToString();
                            countColum++;
                            state = 0;
                            coun = 0;
                            attrva = "";
                            i--;
                        }
                        break;

                    case 11:
                        ch = str[position];
                        if (isDigit(ch))
                        { state = 12; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                        else
                        {
                            Error a = new Error(line, col - coun, "illegal char input");
                            errorOutput.Write(a.ErrorNo + " line: " + line + "   position: " + col + " :'" + ch + "'  " + a.Message + "\n");
                            errorList.Add(a);
                            erList[countColumError, 0] = a.ErrorNo.ToString();
                            erList[countColumError, 1] = a.Message;
                            erList[countColumError, 2] = line.ToString();
                            erList[countColumError, 3] = (col - coun).ToString();
                            countColumError++;
                            state = 0;
                            coun = 0;
                            attrva = "";
                            i--;
                        }
                        break;

                    case 12:
                        ch = str[position];
                        if (isDigit(ch))
                        { state = 12; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                        else if (ch == 'E' || ch == 'e')
                        { state = 13; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                        else
                        {
                            Token token = new Token("numbers", attrva, line, col - coun);
                            output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\n');
                            tokenList[countColum,0] = "numbers";
                            tokenList[countColum,1] = attrva;
                            tokenList[countColum,2] = line.ToString();
                            tokenList[countColum,3] = (col - coun).ToString();
                            countColum++;
                            state = 0;
                            coun = 0;
                            attrva = "";
                            i--;
                        }
                        break;

                    case 13:
                        ch = str[position];
                        if (isDigit(ch))
                        { state = 15; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                        else if (ch == '+'|| ch == '-')
                        { state = 14; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                        else
                        {
                            Error a = new Error(line, col - coun, "illegal char input");
                            errorOutput.Write(a.ErrorNo + " line: " + line + "   position: " + col + " :'" + ch + "'  " + a.Message + "\n");
                            errorList.Add(a);
                            erList[countColumError, 0] = a.ErrorNo.ToString();
                            erList[countColumError, 1] = a.Message;
                            erList[countColumError, 2] = line.ToString();
                            erList[countColumError, 3] = (col - coun).ToString();
                            countColumError++;
                            state = 0;
                            coun = 0;
                            attrva = "";
                            i--;
                        }
                        break;

                    case 14:
                        ch = str[position];
                        if (isDigit(ch))
                        { state = 15; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                        else
                        {
                            Error a = new Error(line, col - coun, "illegal char input");
                            errorOutput.Write(a.ErrorNo + " line: " + line + "   position: " + col + " :'" + ch + "'  " + a.Message + "\n");
                            errorList.Add(a);
                            erList[countColumError, 0] = a.ErrorNo.ToString();
                            erList[countColumError, 1] = a.Message;
                            erList[countColumError, 2] = line.ToString();
                            erList[countColumError, 3] = (col - coun).ToString();
                            countColumError++;
                            state = 0;
                            coun = 0;
                            attrva = "";
                            i--;
                        }
                        break;

                    case 15:
                        ch = str[position];
                        if (isDigit(ch))
                        { state = 15; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                        else
                        {
                            Token token = new Token("numbers", attrva, line, col - coun);
                            output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\n');
                            tokenList[countColum,0] = "numbers";
                            tokenList[countColum,1] = attrva;
                            tokenList[countColum,2] = line.ToString();
                            tokenList[countColum,3] = (col - coun).ToString();
                            countColum++;
                            state = 0;
                            coun = 0;
                            attrva = "";
                            i--;
                        }
                        break;

                    case 16:
                        ch = str[position];
                        if (ch == '/')
                        { state = 17; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                        else
                        {
                            Token token = new Token("operators", attrva, line, col - coun);
                            output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\n');
                            tokenList[countColum,0] = "operators";
                            tokenList[countColum,1] = attrva;
                            tokenList[countColum,2] = line.ToString();
                            tokenList[countColum,3] = (col - coun).ToString();
                            countColum++;
                            state = 0;
                            coun = 0;
                            attrva = "";
                            i--;
                        }
                        break;

                    case 17:
                        ch = str[position];
                        if (ch == '\n')
                        {
                            Token token = new Token("comments", attrva, line, col - coun);
                            output.Write(token.Tokentype + " '" + token.Attributevalue + "' linenumber:" + token.Linenumber + " lineposition:" + token.Lineposition + '\n');
                            tokenList[countColum,0] = "comments";
                            tokenList[countColum,1] = attrva;
                            tokenList[countColum,2] = line.ToString();
                            tokenList[countColum,3] = (col - coun).ToString();
                            countColum++;
                            state = 0;
                            coun = 0;
                            attrva = "";
                            i--;
                        }
                        else
                        { state = 17; position += 1; col += 1; coun += 1; attrva += ch.ToString(); }
                        break;
                }
            }
        }
    }
}
