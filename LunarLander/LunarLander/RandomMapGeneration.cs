using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Xna.Framework;

namespace LunarLander
{
    class RandomMapGeneration
    {
        #region Limits Variables
        //These variables can be changed to fix the map
        //Change these variables in the "RandomLimits.txt" file in the bin folder
        private float minY;
        private float maxY;
        private float terrainMinLength;
        private float terrainMaxLength;
        private float padsMinLength;
        private float padsMaxLength;

        //Min and max number of terrain lines before a pad
        private int minNumOfTerrain;
        private int maxNumOfTerrain;
        #endregion

        #region Function Variables
        //These variables should not be changed as they are used only for the method to function
        private Vector2 lineStart = new Vector2();
        private Vector2 lineEnd = new Vector2();
        private Random rnd = new Random();
        private List<Tuple<Vector2, Vector2, bool>> mapData;//Used to store the map data collected. lineStart, lineEnd, isTerrain

        private float currentXPos;
        private float currentYPos;

        private int xLimit = 1280;

        private int lineCount; //The number of terrain lines made since last pad line
        private int numTillPad; //Random number of terrain lines until the next pad line

        //The edges of the currently generated map
        private float mapXRightBound = 0.0f;
        private float mapXLeftBound = 0.0f;
        #endregion

        #region Other Variables
        private bool limitsLoaded;
        #endregion

        #region Initialization
        public RandomMapGeneration()
        {
            LoadLimits();
        }
        #endregion

        #region Function Methods [LoadLimits]
        private void LoadLimits()
        {
            if (!limitsLoaded)
            {
                if (File.Exists(@"...\...\...\RandomLimits.txt"))
                {
                    using (StreamReader reader = File.OpenText(@"...\...\...\RandomLimits.txt"))
                    {
                        List<string> limits = new List<string>();

                        #region Is Num Bools
                        bool minYisNum = false;
                        bool maxYisNum = false;
                        bool terrainMinLengthisNum = false;
                        bool terrainMaxLengthisNum = false;
                        bool padsMinLengthisNum = false;
                        bool padsMaxLengthisNum = false;
                        bool minNumOfTerrainisNum = false;
                        bool maxNumOfTerrainisNum = false;
                        #endregion

                        string line = null;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (!line.StartsWith("#"))
                            {
                                line = line.Trim();

                                limits.Add(line); //Remove this when done

                                #region Data Checks

                                #region Min Y
                                if (line.StartsWith("minY"))
                                {
                                    minYisNum = float.TryParse(line.Remove(0, "minY = ".Length), out float num);

                                    if (minYisNum)
                                    {
                                        minY = num;
                                    }
                                }
                                #endregion

                                #region Max Y
                                if (line.StartsWith("maxY"))
                                {
                                    maxYisNum = float.TryParse(line.Remove(0, "maxY = ".Length), out float num);

                                    if (maxYisNum)
                                    {
                                        maxY = num;
                                    }
                                }
                                #endregion

                                #region Terrain Min Length
                                if (line.StartsWith("terrainMinLength"))
                                {
                                    terrainMinLengthisNum = float.TryParse(line.Remove(0, "terrainMinLength = ".Length), out float num);

                                    if (terrainMinLengthisNum)
                                    {
                                        terrainMinLength = num;
                                    }
                                }
                                #endregion

                                #region Terrain Max Length
                                if (line.StartsWith("terrainMaxLength"))
                                {
                                    terrainMaxLengthisNum = float.TryParse(line.Remove(0, "terrainMaxLength = ".Length), out float num);

                                    if (terrainMaxLengthisNum)
                                    {
                                        terrainMaxLength = num;
                                    }
                                }
                                #endregion

                                #region Pads Min Length
                                if (line.StartsWith("padsMinLength"))
                                {
                                    padsMinLengthisNum = float.TryParse(line.Remove(0, "padsMinLength = ".Length), out float num);

                                    if (padsMinLengthisNum)
                                    {
                                        padsMinLength = num;
                                    }
                                }
                                #endregion

                                #region Pads Max Length
                                if (line.StartsWith("padsMaxLength"))
                                {
                                    padsMaxLengthisNum = float.TryParse(line.Remove(0, "padsMaxLength = ".Length), out float num);

                                    if (padsMaxLengthisNum)
                                    {
                                        padsMaxLength = num;
                                    }
                                }
                                #endregion

                                #region Min Num Of Terrain
                                if (line.StartsWith("minNumOfTerrain"))
                                {
                                    minNumOfTerrainisNum = int.TryParse(line.Remove(0, "minNumOfTerrain = ".Length), out int num);

                                    if (minNumOfTerrainisNum)
                                    {
                                        minNumOfTerrain = num;
                                    }
                                }
                                #endregion

                                #region Max Num Of Terrain
                                if (line.StartsWith("maxNumOfTerrain"))
                                {
                                    maxNumOfTerrainisNum = int.TryParse(line.Remove(0, "maxNumOfTerrain = ".Length), out int num);

                                    if (maxNumOfTerrainisNum)
                                    {
                                        maxNumOfTerrain = num;
                                    }
                                }
                                #endregion

                                #region Are All Nums
                                //Checks to make sure that all of the numbers are loaded properly
                                if (minYisNum && maxYisNum && terrainMinLengthisNum && terrainMaxLengthisNum && padsMinLengthisNum && padsMaxLengthisNum && minNumOfTerrainisNum && maxNumOfTerrainisNum)
                                {
                                    limitsLoaded = true;
                                }
                                #endregion

                                #endregion
                            }
                        }

                        reader.Close();
                    }
                }
            }
        }
        #endregion

        #region Map Generation Methods [CompleteMap, NewSection]

        #region Complete Map
        public void CompleteMap()
        {
            currentXPos = 0.0f;
            currentYPos = rnd.Next((int)minY, (int)maxY);
            lineCount = 0;
            numTillPad = 0;
            mapData = new List<Tuple<Vector2, Vector2, bool>>();

            //Generates the entire map
            if (limitsLoaded)
            {
                numTillPad = rnd.Next(minNumOfTerrain, maxNumOfTerrain);

                #region Math
                while (currentXPos < xLimit)
                {
                    //Variables for the current line being created
                    lineStart = new Vector2(currentXPos, currentYPos); //Sets the start line at the spot where the last one ended
                    float xDistance = 0;
                    bool isTerrain = false;

                    if (lineCount <= numTillPad) //Making a terrain line
                    {
                        isTerrain = true;
                        xDistance = rnd.Next((int)terrainMinLength, (int)terrainMaxLength);
                        float newYPos = rnd.Next((int)minY, (int)maxY);

                        currentYPos = newYPos;

                        lineCount++;
                    }
                    else //Making a pad line
                    {
                        isTerrain = false;
                        xDistance = rnd.Next((int)padsMinLength, (int)padsMaxLength);

                        lineCount = 0;
                        numTillPad = rnd.Next(minNumOfTerrain, maxNumOfTerrain);
                    }

                    mapXRightBound += xDistance;
                    currentXPos += xDistance;
                    lineEnd = new Vector2(currentXPos, currentYPos);
                    mapData.Add(new Tuple<Vector2, Vector2, bool>(lineStart, lineEnd, isTerrain));
                }
                #endregion
            }

        }
        #endregion

        #region New Section
        public void NewSection(bool goingLeft, float shiftPosition)
        {
            //Generates a new section

            #region Going Left
            if (goingLeft && ((0 - shiftPosition) < mapXLeftBound))
            {
                currentXPos = mapData[0].Item1.X;
                currentYPos = mapData[0].Item1.Y;
                numTillPad = 0;

                //Generates the entire map
                if (limitsLoaded)
                {
                    numTillPad = rnd.Next(minNumOfTerrain, maxNumOfTerrain);

                    #region Math
                    while (currentXPos > (0 - shiftPosition))
                    {
                        //Variables for the current line being created
                        lineEnd = new Vector2(currentXPos, currentYPos); //Sets the start line at the spot where the last one ended
                        float xDistance = 0;
                        bool isTerrain = false;

                        if (lineCount <= numTillPad) //Making a terrain line
                        {
                            isTerrain = true;
                            xDistance = rnd.Next((int)terrainMinLength, (int)terrainMaxLength);
                            float newYPos = rnd.Next((int)minY, (int)maxY);

                            currentYPos = newYPos;

                            lineCount++;
                        }
                        else //Making a pad line
                        {
                            isTerrain = false;
                            xDistance = rnd.Next((int)padsMinLength, (int)padsMaxLength);

                            lineCount = 0;
                            numTillPad = rnd.Next(minNumOfTerrain, maxNumOfTerrain);
                        }

                        mapXLeftBound -= xDistance;
                        currentXPos -= xDistance;
                        lineStart = new Vector2(currentXPos, currentYPos);
                        mapData.Insert(0, new Tuple<Vector2, Vector2, bool>(lineStart, lineEnd, isTerrain));
                    }
                    #endregion
                }
            }
            #endregion

            #region Going Right
            if (!goingLeft && ((xLimit - shiftPosition) > mapXRightBound))
            {
                currentXPos = mapData[mapData.Count - 1].Item2.X;
                currentYPos = mapData[mapData.Count - 1].Item2.Y;
                numTillPad = 0;

                //Generates the entire map
                if (limitsLoaded)
                {
                    numTillPad = rnd.Next(minNumOfTerrain, maxNumOfTerrain);

                    #region Math
                    while (currentXPos < (xLimit - shiftPosition))
                    {
                        //Variables for the current line being created
                        lineStart = new Vector2(currentXPos, currentYPos); //Sets the start line at the spot where the last one ended
                        float xDistance = 0;
                        bool isTerrain = false;

                        if (lineCount <= numTillPad) //Making a terrain line
                        {
                            isTerrain = true;
                            xDistance = rnd.Next((int)terrainMinLength, (int)terrainMaxLength);
                            float newYPos = rnd.Next((int)minY, (int)maxY);

                            currentYPos = newYPos;

                            lineCount++;
                        }
                        else //Making a pad line
                        {
                            isTerrain = false;
                            xDistance = rnd.Next((int)padsMinLength, (int)padsMaxLength);

                            lineCount = 0;
                            numTillPad = rnd.Next(minNumOfTerrain, maxNumOfTerrain);
                        }

                        mapXRightBound += xDistance;
                        currentXPos += xDistance;
                        lineEnd = new Vector2(currentXPos, currentYPos);
                        mapData.Add(new Tuple<Vector2, Vector2, bool>(lineStart, lineEnd, isTerrain));
                    }
                    #endregion
                }
            }
            #endregion
        }
        #endregion

        #endregion

        #region Get Methods
        public List<Tuple<Vector2, Vector2, bool>> GetMapData()
        {
            return mapData;
        }
        #endregion
    }
}
