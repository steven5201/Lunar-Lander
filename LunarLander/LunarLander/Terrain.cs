#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GameLibrary;
#endregion

namespace LunarLander
{
    class Terrain
    {
        #region Variables
        private static LinkedList<Vector2> pads = new LinkedList<Vector2>();
        private static LinkedList<Vector2> terrain = new LinkedList<Vector2>();

        private static List<Line2D> padsLines = new List<Line2D>();
        private static List<Line2D> terrainLines = new List<Line2D>();

        private static RandomMapGeneration randomMapGenerator = new RandomMapGeneration();
        #endregion

        #region Loading Methods

        #region MapNames
        public static List<string> LoadMapsNames()
        {
            StreamReader reader = File.OpenText($@".\Content\LevelNames.txt");
            List<string> mapNames = new List<string>();

            string line = null;

            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();

                mapNames = line.Split(',').ToList();
            }

            reader.Close();

            return mapNames;
        }
        #endregion

        #region Terrain
        public static void LoadTerrain(string levelName)
        {
            if (levelName == "Random")
            {
                InitializeRandomMap();
            }
            else
            {
                if (File.Exists($@".\Content\{levelName}_terrain.txt"))
                {
                    StreamReader reader = File.OpenText($@".\Content\{levelName}_terrain.txt");

                    string line = null;

                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();

                        if (!line.StartsWith("#"))
                        {
                            string[] s = line.Split(',');

                            Vector2 p1 = new Vector2(int.Parse(s[0]), int.Parse(s[1]));
                            Vector2 p2 = new Vector2(int.Parse(s[2]), int.Parse(s[3]));

                            terrainLines.Add(new Line2D(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3])));

                            terrain.AddLast(p1);
                            terrain.AddLast(p2);
                        }
                    }

                    reader.Close();
                }
            }
        }
        #endregion

        #region Pads
        public static void LoadPads(string levelName)
        {
            if (levelName != "Random")
            {
                if (File.Exists($@".\Content\{levelName}_pads.txt"))
                {
                    StreamReader reader = File.OpenText($@".\Content\{levelName}_pads.txt");

                    string line = null;

                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();

                        if (!line.StartsWith("#"))
                        {
                            string[] s = line.Split(',');

                            Vector2 p1 = new Vector2(int.Parse(s[0]), int.Parse(s[1]));
                            Vector2 p2 = new Vector2(int.Parse(s[2]), int.Parse(s[3]));

                            PadsLines.Add(new Line2D(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3])));

                            pads.AddLast(p1);
                            pads.AddLast(p2);
                        }
                    }

                    reader.Close();
                }
            }
        }
        #endregion

        #region Clear
        public static void ClearLevelValues()
        {
            if (pads.Count != 0)
            {
                pads.Clear();
            }

            if (terrain.Count != 0)
            {
                terrain.Clear();
            }

            if (PadsLines.Count != 0)
            {
                PadsLines.Clear();
            }

            if (terrainLines.Count != 0)
            {
                terrainLines.Clear();
            }
        }
        #endregion

        #endregion

        #region Building Methods

        #region Terrain
        public static void BuildTerrain(Vector2 position)
        {
            PrimitiveBatch pb = GlobalValues.PrimitiveBatch;

            pb.Begin(PrimitiveType.LineList);

            foreach (Vector2 v2 in terrain)
            {
                pb.AddVertex(v2 + position, Color.White);
            }

            pb.End();
        }
        #endregion

        #region Pads
        public static void BuildPads(Vector2 position)
        {
            PrimitiveBatch pb = GlobalValues.PrimitiveBatch;

            pb.Begin(PrimitiveType.LineList);

            foreach (Vector2 v2 in pads)
            {
                pb.AddVertex(v2 + position, Color.Green);
            }

            pb.End();
        }
        #endregion

        #endregion

        #region Get Lines
        public static List<Line2D> PadsLines { get => padsLines; }
        public static List<Line2D> TerrainLines { get => terrainLines; }
        #endregion

        #region Fix Hitboxes
        public static void FixPadHitboxes(Vector2 position)
        {
            List<Vector2> padLinePositions = new List<Vector2>();
            List<Line2D> padLine2D = new List<Line2D>();

            foreach (Vector2 line in pads)
            {
                padLinePositions.Add(line + position);
            }

            for (int i = 0; i < (padLinePositions.Count - 1); i++)
            {
                padLine2D.Add(new Line2D(padLinePositions[i].X, padLinePositions[i].Y, padLinePositions[i + 1].X, padLinePositions[i + 1].Y));

                i++;
            }

            padsLines = padLine2D;
        }

        public static void FixTerrainHitboxes(Vector2 position)
        {
            List<Vector2> terrainLinePositions = new List<Vector2>();
            List<Line2D> terrainLine2D = new List<Line2D>();

            foreach (Vector2 line in terrain)
            {
                terrainLinePositions.Add(line + position);
            }

            for (int i = 0; i < (terrainLinePositions.Count - 1); i++)
            {
                terrainLine2D.Add(new Line2D(terrainLinePositions[i].X, terrainLinePositions[i].Y, terrainLinePositions[i + 1].X, terrainLinePositions[i + 1].Y));

                i++;
            }

            terrainLines = terrainLine2D;
        }
        #endregion

        #region Random Map Generation

        #region Initialize
        private static void InitializeRandomMap()
        {
            //Generates the complete map at the beginning of the game when random map is choosen
            randomMapGenerator = new RandomMapGeneration();
            randomMapGenerator.CompleteMap();

            FillLines();
        }
        #endregion

        #region New Line
        public static void NewRandomLine(bool goingLeft, float shiftPosition)
        {
            randomMapGenerator.NewSection(goingLeft, shiftPosition);

            ClearLevelValues();

            FillLines();
        }
        #endregion

        #region Fill Lines
        private static void FillLines()
        {
            //Fills the lines to build the map data

            foreach (Tuple<Vector2, Vector2, bool> line in randomMapGenerator.GetMapData())
            {
                if (line.Item3) //isTerrain is true
                {
                    terrainLines.Add(new Line2D(line.Item1.X, line.Item1.Y, line.Item2.X, line.Item2.Y));

                    terrain.AddLast(line.Item1);
                    terrain.AddLast(line.Item2);
                }
                else //isTerrain is false
                {
                    PadsLines.Add(new Line2D(line.Item1.X, line.Item1.Y, line.Item2.X, line.Item2.Y));

                    pads.AddLast(line.Item1);
                    pads.AddLast(line.Item2);
                }
            }
        }
        #endregion

        #endregion
    }
}
