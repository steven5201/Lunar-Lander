using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.GUIManagement
{
    public class MyFont
    {
        public static LinkedList<Vector2> GetWord(float scale, string word)
        {
            LinkedList<Vector2> outList = new LinkedList<Vector2>();
            char[] wordCharList = word.ToCharArray();
            List<string> splitWord = new List<string>();
            int relativePos = 0;

            foreach (char item in wordCharList)
            {
                splitWord.Add(item.ToString());
            }

            foreach (string item in splitWord)
            {
                LinkedList<Vector2> list2 = new LinkedList<Vector2>();
                list2 = GetLetter(scale, relativePos, item);
                relativePos += 6;

                foreach (Vector2 spot in list2)
                {
                    outList.AddLast(spot);
                }
            }

            return outList;
        }

        #region ShowEntireFont
        public static void ShowEntireFont(PrimitiveBatch pb)
        {
            int scaleForFontDisplay = 8;

            LinkedList<Vector2> row1 = new LinkedList<Vector2>();
            LinkedList<Vector2> row2 = new LinkedList<Vector2>();
            LinkedList<Vector2> row3 = new LinkedList<Vector2>();
            LinkedList<Vector2> row4 = new LinkedList<Vector2>();

            Vector2 row1Position = new Vector2(10, 50);
            Vector2 row2Position = new Vector2(10, 100);
            Vector2 row3Position = new Vector2(10, 150);
            Vector2 row4Position = new Vector2(10, 200);

            row1 = MyFont.GetWord(scaleForFontDisplay, "a b c d e f g h i j");
            row2 = MyFont.GetWord(scaleForFontDisplay, "k l m n o p q r s t");
            row3 = MyFont.GetWord(scaleForFontDisplay, "u v w x y z 1 2 3 4");
            row4 = MyFont.GetWord(scaleForFontDisplay, "5 6 7 8 9 0 .");

            pb.Begin(PrimitiveType.LineList);

            foreach (Vector2 v2 in row1)
            {
                pb.AddVertex(v2 + row1Position, Color.White);
            }

            foreach (Vector2 v2 in row2)
            {
                pb.AddVertex(v2 + row2Position, Color.White);
            }

            foreach (Vector2 v2 in row3)
            {
                pb.AddVertex(v2 + row3Position, Color.White);
            }

            foreach (Vector2 v2 in row4)
            {
                pb.AddVertex(v2 + row4Position, Color.White);
            }

            pb.End();
        }
        #endregion

        private static LinkedList<Vector2> GetLetter(float scale, int position, string letter)
        {
            LinkedList<Vector2> outLetter = new LinkedList<Vector2>();

            if (letter.ToLower() == "a")
            {
                outLetter = GetLetterA(scale, position);
            }
            else if (letter.ToLower() == "b")
            {
                outLetter = GetLetterB(scale, position);
            }
            else if (letter.ToLower() == "c")
            {
                outLetter = GetLetterC(scale, position);
            }
            else if (letter.ToLower() == "d")
            {
                outLetter = GetLetterD(scale, position);
            }
            else if (letter.ToLower() == "e")
            {
                outLetter = GetLetterE(scale, position);
            }
            else if (letter.ToLower() == "f")
            {
                outLetter = GetLetterF(scale, position);
            }
            else if (letter.ToLower() == "g")
            {
                outLetter = GetLetterG(scale, position);
            }
            else if (letter.ToLower() == "h")
            {
                outLetter = GetLetterH(scale, position);
            }
            else if (letter.ToLower() == "i")
            {
                outLetter = GetLetterI(scale, position);
            }
            else if (letter.ToLower() == "j")
            {
                outLetter = GetLetterJ(scale, position);
            }
            else if (letter.ToLower() == "k")
            {
                outLetter = GetLetterK(scale, position);
            }
            else if (letter.ToLower() == "l")
            {
                outLetter = GetLetterL(scale, position);
            }
            else if (letter.ToLower() == "m")
            {
                outLetter = GetLetterM(scale, position);
            }
            else if (letter.ToLower() == "n")
            {
                outLetter = GetLetterN(scale, position);
            }
            else if (letter.ToLower() == "o")
            {
                outLetter = GetLetterO(scale, position);
            }
            else if (letter.ToLower() == "p")
            {
                outLetter = GetLetterP(scale, position);
            }
            else if (letter.ToLower() == "q")
            {
                outLetter = GetLetterQ(scale, position);
            }
            else if (letter.ToLower() == "r")
            {
                outLetter = GetLetterR(scale, position);
            }
            else if (letter.ToLower() == "s")
            {
                outLetter = GetLetterS(scale, position);
            }
            else if (letter.ToLower() == "t")
            {
                outLetter = GetLetterT(scale, position);
            }
            else if (letter.ToLower() == "u")
            {
                outLetter = GetLetterU(scale, position);
            }
            else if (letter.ToLower() == "v")
            {
                outLetter = GetLetterV(scale, position);
            }
            else if (letter.ToLower() == "w")
            {
                outLetter = GetLetterW(scale, position);
            }
            else if (letter.ToLower() == "x")
            {
                outLetter = GetLetterX(scale, position);
            }
            else if (letter.ToLower() == "y")
            {
                outLetter = GetLetterY(scale, position);
            }
            else if (letter.ToLower() == "z")
            {
                outLetter = GetLetterZ(scale, position);
            }
            else if (letter.ToLower() == "1")
            {
                outLetter = GetNumber1(scale, position);
            }
            else if (letter.ToLower() == "2")
            {
                outLetter = GetNumber2(scale, position);
            }
            else if (letter.ToLower() == "3")
            {
                outLetter = GetNumber3(scale, position);
            }
            else if (letter.ToLower() == "4")
            {
                outLetter = GetNumber4(scale, position);
            }
            else if (letter.ToLower() == "5")
            {
                outLetter = GetNumber5(scale, position);
            }
            else if (letter.ToLower() == "6")
            {
                outLetter = GetNumber6(scale, position);
            }
            else if (letter.ToLower() == "7")
            {
                outLetter = GetNumber7(scale, position);
            }
            else if (letter.ToLower() == "8")
            {
                outLetter = GetNumber8(scale, position);
            }
            else if (letter.ToLower() == "9")
            {
                outLetter = GetNumber9(scale, position);
            }
            else if (letter.ToLower() == "0")
            {
                outLetter = GetNumber0(scale, position);
            }
            else if (letter.ToLower() == ".")
            {
                outLetter = GetDot(scale, position);
            }
            else if (letter.ToLower() == ":")
            {
                outLetter = GetSemiColon(scale, position);
            }

            return outLetter;
        }

        #region Draw Methods
        private static LinkedList<Vector2> GetLetterA(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));


            return list;
        }

        private static LinkedList<Vector2> GetLetterB(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((1 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((1 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((1 + position) * scale, 2 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterC(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterD(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 3 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 3 * scale));
            list.AddLast(new Vector2((3 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((3 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterE(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 2 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterF(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 2 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterG(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 2 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterH(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterI(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterJ(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterK(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterL(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterM(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterN(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterO(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 1 * scale));
            list.AddLast(new Vector2((1 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((1 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((4 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((4 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 1 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 1 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 4 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 4 * scale));
            list.AddLast(new Vector2((4 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((4 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((1 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((1 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 4 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 4 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 1 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterP(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterQ(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterR(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterS(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterT(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterU(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterV(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterW(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterX(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterY(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetLetterZ(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetNumber1(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((1 + position) * scale, 1 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetNumber2(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetNumber3(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetNumber4(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetNumber5(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetNumber6(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetNumber7(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetNumber8(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetNumber9(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetNumber0(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((0 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((5 + position) * scale, 0 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetDot(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((2 + position) * scale, 3 * scale));
            list.AddLast(new Vector2((4 + position) * scale, 3 * scale));

            list.AddLast(new Vector2((4 + position) * scale, 3 * scale));
            list.AddLast(new Vector2((4 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((4 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 3 * scale));

            return list;
        }

        private static LinkedList<Vector2> GetSemiColon(float scale, int position)
        {
            //Each of these lines in a point on a graph
            //So the first line is the starting x, y for the line we are drawing
            //The second line is the ending x, y for the line we are drawing

            LinkedList<Vector2> list = new LinkedList<Vector2>();

            list.AddLast(new Vector2((2 + position) * scale, 3 * scale));
            list.AddLast(new Vector2((4 + position) * scale, 3 * scale));

            list.AddLast(new Vector2((4 + position) * scale, 3 * scale));
            list.AddLast(new Vector2((4 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((4 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 5 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 5 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 3 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((4 + position) * scale, 0 * scale));

            list.AddLast(new Vector2((4 + position) * scale, 0 * scale));
            list.AddLast(new Vector2((4 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((4 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 2 * scale));

            list.AddLast(new Vector2((2 + position) * scale, 2 * scale));
            list.AddLast(new Vector2((2 + position) * scale, 0 * scale));

            return list;
        }
        #endregion
    }
}
