#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using GameLibrary;
#endregion

namespace LunarLander
{
    public static class LanderValues
    {
        #region Variables
        private static int playerScore = 0;
        private static TimeSpan gameTime = TimeSpan.Zero;
        private static int fuelRemaining = 0;
        private static float rotation = 0;
        private static Vector2 velocity = new Vector2();
        private const float particleConstant = .005f;
        private const float velocityScale = 50.0f;

        private static bool leftBoundHit = false;
        private static bool rightBoundHit = false;

        private static List<Line2D> landedLines = new List<Line2D>();
        #endregion

        #region Get and Set Methods
        public static int PlayerScore
        {
            get => playerScore;
            set => playerScore = value;
        }

        public static TimeSpan GameTime
        {
            get => gameTime;
            set => gameTime = value;
        }

        public static int FuelRemaining
        {
            get => fuelRemaining;
            set
            {
                if (value <= 100 && value >= 0)
                {
                    fuelRemaining = value;
                }
                else if (value >= 100)
                {
                    fuelRemaining = 100;
                }
                else if (value <= 0)
                {
                    fuelRemaining = 0;
                }
            }
        }

        public static float Rotation
        {
            get => rotation;
            set => rotation = value;
        }

        public static Vector2 Velocity
        {
            get => velocity;
            set
            {
                if (value.Y < 1.5f && value.Y > -1.5f)
                {
                    velocity.Y = value.Y;
                }

                if (value.X < 5f && value.X > -5f)
                {
                    velocity.X = value.X;
                }
            }
        }

        public static bool LeftBoundHit
        {
            get => leftBoundHit;
            set => leftBoundHit = value;
        }

        public static bool RightBoundHit
        {
            get => rightBoundHit;
            set => rightBoundHit = value;
        }

        public static List<Line2D> LandedLines
        {
            get => landedLines;
            set => landedLines = value;
        }

        public static float ParticleConstant => particleConstant;

        public static float VelocityScale => velocityScale;
        #endregion

        #region Lander Lines
        public static List<Vector2> FillLanderLines()
        {
            List<Vector2> landerCords = new List<Vector2>();

            #region Example
            //landerCords.Add(new Vector2(0, 0));
            //landerCords.Add(new Vector2(1, 0));
            //landerCords.Add(new Vector2(1, 1));
            //landerCords.Add(new Vector2(0, 1));
            //landerCords.Add(new Vector2(0, 0));

            /* In class example of a lander
            //Capsule
            landerCords.Add(new Vector2(1, 1));
            landerCords.Add(new Vector2(3, 1));

            landerCords.Add(new Vector2(1, 1));
            landerCords.Add(new Vector2(1, 2));

            landerCords.Add(new Vector2(3, 1));
            landerCords.Add(new Vector2(3, 2));


            //Body
            landerCords.Add(new Vector2(0, 2));
            landerCords.Add(new Vector2(4, 2));

            landerCords.Add(new Vector2(0, 2));
            landerCords.Add(new Vector2(0, 4));

            landerCords.Add(new Vector2(4, 2));
            landerCords.Add(new Vector2(4, 4));

            landerCords.Add(new Vector2(0, 4));
            landerCords.Add(new Vector2(4, 4));


            //Legs
            landerCords.Add(new Vector2(1, 4));
            landerCords.Add(new Vector2(1, 6));

            landerCords.Add(new Vector2(3, 4));
            landerCords.Add(new Vector2(3, 6));

            landerCords.Add(new Vector2(1, 6));
            landerCords.Add(new Vector2(0, 6));

            landerCords.Add(new Vector2(3, 6));
            landerCords.Add(new Vector2(4, 6));
            */
            #endregion

            //My Lander

            landerCords.AddRange(LeftLegLines());
            landerCords.AddRange(RightLegLines());
            landerCords.AddRange(CapsuleLines());
            landerCords.AddRange(TopLines());

            return landerCords;
        }

        #region Left Leg
        public static List<Vector2> LeftLegLines()
        {
            List<Vector2> list = new List<Vector2>();

            list.Add(new Vector2(0, 9));
            list.Add(new Vector2(2, 9));

            list.Add(new Vector2(2, 9));
            list.Add(new Vector2(3, 6));

            return list;
        }
        #endregion

        #region Right Leg
        public static List<Vector2> RightLegLines()
        {
            List<Vector2> list = new List<Vector2>();

            list.Add(new Vector2(11, 9));
            list.Add(new Vector2(9, 9));

            list.Add(new Vector2(9, 9));
            list.Add(new Vector2(8, 6));

            return list;
        }
        #endregion

        #region Capsule
        public static List<Vector2> CapsuleLines()
        {
            List<Vector2> list = new List<Vector2>();

            list.Add(new Vector2(4, 7));
            list.Add(new Vector2(7, 7));

            list.Add(new Vector2(7, 7));
            list.Add(new Vector2(8, 6));

            list.Add(new Vector2(8, 6));
            list.Add(new Vector2(8, 3));

            list.Add(new Vector2(8, 3));
            list.Add(new Vector2(7, 2));

            list.Add(new Vector2(7, 2));
            list.Add(new Vector2(4, 2));

            list.Add(new Vector2(4, 2));
            list.Add(new Vector2(3, 3));

            list.Add(new Vector2(3, 3));
            list.Add(new Vector2(3, 6));

            list.Add(new Vector2(3, 6));
            list.Add(new Vector2(4, 7));

            return list;
        }
        #endregion

        #region Top
        public static List<Vector2> TopLines()
        {
            List<Vector2> list = new List<Vector2>();

            list.Add(new Vector2(4, 2));
            list.Add(new Vector2(7, 2));

            list.Add(new Vector2(7, 2));
            list.Add(new Vector2(7, 1));

            list.Add(new Vector2(7, 1));
            list.Add(new Vector2(6, 0));

            list.Add(new Vector2(6, 0));
            list.Add(new Vector2(5, 0));

            list.Add(new Vector2(5, 0));
            list.Add(new Vector2(4, 1));

            list.Add(new Vector2(4, 1));
            list.Add(new Vector2(4, 2));

            return list;
        }
        #endregion

        public static List<Vector2> GetRocketCords(bool showRocket1)
        {
            List<Vector2> list = new List<Vector2>();

            if (showRocket1)
            {
                list.Add(new Vector2(4, 7));
                list.Add(new Vector2(6, 13));

                list.Add(new Vector2(7, 7));
                list.Add(new Vector2(6, 13));
            }
            else
            {
                list.Add(new Vector2(4, 7));
                list.Add(new Vector2(5, 13));

                list.Add(new Vector2(7, 7));
                list.Add(new Vector2(5, 13));
            }

            return list;
        }
        #endregion
    }
}
