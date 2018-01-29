#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GameLibrary;
#endregion

namespace LunarLander
{
    public class Lander : DrawableGameComponent
    {
        #region Variables
        private PrimitiveBatch pb;

        #region Screen Variables
        private int screenWidth;
        private int screenHeight;
        #endregion

        #region Lander Variables
        private Vector2 position;
        private Vector2 center;
        private float scale;
        private List<Vector2> landerCords;
        private bool showRocket1;
        private bool showRocket2;
        private bool isThrusting;
        #endregion

        #region TimeSpan Variables
        private TimeSpan elapsedTime = TimeSpan.Zero;
        private TimeSpan elapsedTime2 = TimeSpan.Zero;
        private TimeSpan gameRunTime = TimeSpan.Zero;
        private TimeSpan gravityTime = TimeSpan.Zero;
        private TimeSpan landerRocketTime = TimeSpan.Zero;
        #endregion

        #region Collision Variables
        private bool isCrashing = false;
        private bool landedCorrectly = false;
        private bool landerCrashed = false;
        private bool justCrashed = false;

        private int rotationLimit = 5;
        private float speedLimit = 500;

        //Explosion Variables
        private float topRotation = 0.0f;
        private float capsuleRotation = 0.0f;
        private float leftLegRotation = 0.0f;
        private float rightLegRotation = 0.0f;

        private Vector2 topCenter = new Vector2(5.5f, 1.0f);
        private Vector2 capsuleCenter = new Vector2(5.5f, 4.5f);
        private Vector2 leftLegCenter = new Vector2(2.25f, 8.5f);
        private Vector2 rightLegCenter = new Vector2(8.75f, 8.5f);

        private Vector2 topPosition = new Vector2();
        private Vector2 capsulePosition = new Vector2();
        private Vector2 leftLegPosition = new Vector2();
        private Vector2 rightLegPosition = new Vector2();

        private Vector2 collisionVelocity = new Vector2();
        #endregion

        #region Physics Variables
        const float MOON_GRAVITY = 1.62f;
        const float EARTH_GRAVITY = 9.80f;
        const float JUPITER_GRAVITY = 25.0f;

        float particleConstant;
        Matrix rotationMatrix;
        Vector2 currentLanderDirection;

        float thrustPower;
        float thrustPowerIncrement;
        float gravityConstant;
        float gravityPull;

        const float MAX_THRUST_POWER = 2.0f;

        Vector2 UP = new Vector2(0, -1);
        Vector2 DOWN = new Vector2(0, 1);

        Random RND = new Random();
        #endregion

        float rotation;
        #endregion

        #region Initialization Components

        public Lander(Game game) : base(game)
        {
            RandomLanderPos();
            landerCords = LanderValues.FillLanderLines();

            this.pb = GlobalValues.PrimitiveBatch;

            showRocket1 = false;
            showRocket2 = false;
            rotationMatrix = new Matrix();
            currentLanderDirection = new Vector2();
            isThrusting = false;

            scale = 2.0f;
            center = new Vector2(5.5f, 4.5f);
            particleConstant = LanderValues.ParticleConstant;

            thrustPower = 0.0f;
            thrustPowerIncrement = 0.1f; //Needs adjustment
            gravityConstant = MOON_GRAVITY;
            gravityPull = gravityConstant;
            LanderValues.Velocity = new Vector2();
        }

        public override void Initialize()
        {
            screenWidth = GraphicsDevice.PresentationParameters.Bounds.Width;
            screenHeight = GraphicsDevice.PresentationParameters.Bounds.Height;

            base.Initialize();
        }

        public void RandomLanderPos()
        {
            position = new Vector2(RND.Next(55, 250), 135.0f);
        }

        #endregion

        #region Update
        public override void Update(GameTime gameTime)
        {
            #region Scale
            //Changes the scale
            //Changes the size of the ship
            if (Keyboard.GetState().IsKeyDown(Keys.Subtract))
            {
                if (scale >= 2)
                {
                    scale--;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Add))
            {
                if (scale <= 10)
                {
                    scale++;
                }
            }
            #endregion

            #region Thrust
            if ((Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Space)) && LanderValues.FuelRemaining > 0 && !isCrashing)
            {
                isThrusting = true;
            }
            else
            {
                isThrusting = false;
            }

            if (isThrusting)
            {
                //Power increases as the up movement key is held down.
                thrustPower += thrustPowerIncrement;

                if (thrustPower > MAX_THRUST_POWER)
                {
                    thrustPower = MAX_THRUST_POWER;
                }

                elapsedTime += gameTime.ElapsedGameTime;

                if (elapsedTime > TimeSpan.FromSeconds(1))
                {
                    elapsedTime -= TimeSpan.FromSeconds(1);
                    LanderValues.FuelRemaining -= 1; //The amount of fuel being taken from the ship
                }

                landerRocketTime += gameTime.ElapsedGameTime;

                if (landerRocketTime > TimeSpan.FromMilliseconds(100))
                {
                    landerRocketTime -= TimeSpan.FromMilliseconds(100);

                    if (showRocket1)
                    {
                        showRocket1 = false;
                        showRocket2 = true;
                    }
                    else
                    {
                        showRocket1 = true;
                        showRocket2 = false;
                    }
                }

                Sound.PlayThrust();
            }
            else
            {
                //When the up movement key is not down then decrease power
                thrustPower -= thrustPowerIncrement / 3.5f; //change this to fit the game better

                if (thrustPower < 0)
                {
                    thrustPower = 0;
                }

                showRocket1 = false;
                showRocket2 = false;

                Sound.StopThrust();
            }
            #endregion

            #region Rotation: Left and Right
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                LanderValues.Rotation -= 0.1f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                LanderValues.Rotation += 0.1f;
            }

            rotationMatrix = Matrix.CreateRotationZ(LanderValues.Rotation); //Is this the angle or radians :: The type says radians
            currentLanderDirection = Vector2.Transform(UP, rotationMatrix);
            currentLanderDirection *= thrustPower;
            #endregion

            #region Movement
            if (GlobalValues.LevelLoaded == "Random")
            {
                if ((position + LanderValues.Velocity).X >= (StateManager.graphicsDevice.PresentationParameters.BackBufferWidth - GlobalValues.ScreenBuffer) && LanderValues.Velocity.X != 0)
                {
                    LanderValues.RightBoundHit = true;
                }
                else if ((position + LanderValues.Velocity).X <= GlobalValues.ScreenBuffer && LanderValues.Velocity.X != 0)
                {
                    LanderValues.LeftBoundHit = true;
                }
                else
                {
                    LanderValues.RightBoundHit = false;
                    LanderValues.LeftBoundHit = false;
                }

                LanderValues.Velocity += currentLanderDirection * particleConstant;
            }
            else
            {
                LanderValues.Velocity += currentLanderDirection * particleConstant;
            }
            #endregion

            elapsedTime2 += gameTime.ElapsedGameTime;

            if (elapsedTime2 > TimeSpan.FromSeconds(1))
            {
                elapsedTime2 -= TimeSpan.FromSeconds(1);
                gameRunTime = gameRunTime + TimeSpan.FromSeconds(1);
            }

            Gravity(gameTime);

            MoveShip();

            #region Collision
            if (!isCrashing && !justCrashed)
            {
                CheckCollision();
            }

            if (justCrashed)
            {
                //Set peices position
                topRotation = 0.0f;
                capsuleRotation = 0.0f;
                leftLegRotation = 0.0f;
                rightLegRotation = 0.0f;

                topPosition = position;
                capsulePosition = position;
                leftLegPosition = position;
                rightLegPosition = position;

                collisionVelocity = ((-1 * LanderValues.Velocity) / LanderValues.Velocity) * (LanderValues.ParticleConstant * 15);

                Sound.PlayExplosion();
                isCrashing = true;
                justCrashed = false;
            }

            if (isCrashing)
            {
                LanderExplosion(gameTime);
                Crashed();
            }
            else if (landedCorrectly)
            {
                Landed();
            }
            #endregion

            UpdateLanderStaticValues(gameRunTime);
            base.Update(gameTime);
        }

        #region Ship Movement
        private void MoveShip()
        {
            if (!landerCrashed && !isCrashing && !landedCorrectly)
            {
                //Only change position if the ship isnt hitting the sides
                if (LanderValues.LeftBoundHit)
                {
                    if (LanderValues.Velocity.X > 0)
                    {
                        position.X += LanderValues.Velocity.X;
                    }
                }
                else if (LanderValues.RightBoundHit)
                {
                    if (LanderValues.Velocity.X < 0)
                    {
                        position.X += LanderValues.Velocity.X;
                    }
                }
                else
                {
                    position.X += LanderValues.Velocity.X;
                }

                if ((position.Y + LanderValues.Velocity.Y) > GlobalValues.ScreenBuffer)
                {
                    position.Y += LanderValues.Velocity.Y;
                }
            }
        }
        #endregion

        #region Collision Detection
        private void CheckCollision()
        {
            List<Vector2> landerLinePositions = new List<Vector2>();
            List<Line2D> landerLine2D = new List<Line2D>();

            foreach (Vector2 line in landerCords)
            {
                float Xrotated = center.X + (line.X - center.X) *
                  (float)Math.Cos(LanderValues.Rotation) - (line.Y - center.Y) *
                  (float)Math.Sin(LanderValues.Rotation);

                float Yrotated = center.Y + (line.X - center.X) *
                    (float)Math.Sin(LanderValues.Rotation) + (line.Y - center.Y) *
                    (float)Math.Cos(LanderValues.Rotation);

                landerLinePositions.Add((new Vector2(Xrotated, Yrotated) * scale) + position);
            }

            for (int i = 0; i < (landerLinePositions.Count - 1); i++)
            {
                landerLine2D.Add(new Line2D(landerLinePositions[i].X, landerLinePositions[i].Y, landerLinePositions[i + 1].X, landerLinePositions[i + 1].Y));

                i++;
            }

            //Check for intersection with terrain
            foreach (Line2D landerLine in landerLine2D)
            {
                foreach (Line2D terrainLine in Terrain.TerrainLines)
                {
                    if (!justCrashed)
                    {
                        justCrashed = Line2D.Intersects(landerLine, terrainLine);
                    }
                }

                foreach (Line2D padLine in Terrain.PadsLines)
                {
                    if (!landedCorrectly)
                    {
                        rotation = MathHelper.ToDegrees(LanderValues.Rotation);

                        if (rotation < 0)
                        {
                            rotation *= -1;
                        }

                        if (rotation >= 360 || rotation <= -360)
                        {
                            int numOf360 = (int)rotation / 360;
                            rotation -= (float)360 * numOf360;
                        }

                        float xVelocity = LanderValues.Velocity.X * LanderValues.VelocityScale;
                        float yVelocity = LanderValues.Velocity.Y * LanderValues.VelocityScale;

                        if (xVelocity < 0)
                        {
                            xVelocity *= -1;
                        }

                        if (yVelocity < 0)
                        {
                            yVelocity *= -1;
                        }

                        bool touchingPad = Line2D.Intersects(landerLine, padLine);

                        if (touchingPad
                            && ((rotation <= rotationLimit && rotation >= 0) || (rotation >= (360 - rotationLimit) && rotation <= 360))
                            && (/*(xVelocity < speedLimit * 5) && */(yVelocity < speedLimit)))
                        {
                            landedCorrectly = true;

                            bool neverBeforeLanded = true;

                            //Checks to see if the player has not already landed on that line
                            foreach (Line2D previouslyLandedLine in LanderValues.LandedLines)
                            {
                                if (previouslyLandedLine.StartX == padLine.StartX && previouslyLandedLine.StartY == padLine.StartY
                                    && previouslyLandedLine.EndX == padLine.EndX && previouslyLandedLine.EndY == padLine.EndY)
                                {
                                    neverBeforeLanded = false;
                                }
                            }

                            if (neverBeforeLanded)
                            {
                                LanderValues.FuelRemaining += 10;
                                LanderValues.LandedLines.Add(padLine);
                            }
                        }
                        else if (touchingPad)
                        {
                            landedCorrectly = false;
                            justCrashed = true;
                        }
                    }
                }
            }
        }
        #endregion

        #region Collision Events [Crashed, Landed, LanderExplosion]
        private void Crashed()
        {
            Sound.StopThrust();

            RandomLanderPos();
        }

        private void Landed()
        {
            Sound.StopThrust();
            Sound.PlayLanding();

            LanderValues.PlayerScore += 100;

            RandomLanderPos();
        }

        private void LanderExplosion(GameTime gameTime)
        {
            //Moves each peice seperately
            //Moves each peices rotation
            //Moves each peices position

            topRotation += .1f;
            capsuleRotation -= .1f;
            leftLegRotation += .1f;
            rightLegRotation -= .1f;

            topPosition.Y += collisionVelocity.Y;
            topPosition.X += collisionVelocity.X;

            capsulePosition.Y += collisionVelocity.Y;
            capsulePosition.X -= collisionVelocity.X;

            leftLegPosition.Y += collisionVelocity.Y;
            leftLegPosition.X += collisionVelocity.X;

            rightLegPosition.Y += collisionVelocity.Y;
            rightLegPosition.X -= collisionVelocity.X;
        }
        #endregion

        #region Gravity
        private void Gravity(GameTime gameTime)
        {
            //Gravity
            gravityTime += gameTime.ElapsedGameTime;

            if (gravityTime > TimeSpan.FromSeconds(1))
            {
                gravityTime -= TimeSpan.FromSeconds(1);

                if (isThrusting)
                {
                    gravityPull = 0;
                }
                else
                {
                    gravityPull += gravityConstant;
                }
            }

            LanderValues.Velocity += (DOWN * gravityPull * particleConstant);
        }
        #endregion

        #region Lander Value Updates [GameTime]
        private void UpdateLanderStaticValues(TimeSpan gameRunTime)
        {
            LanderValues.GameTime = gameRunTime;
        }
        #endregion

        #endregion

        #region Draw
        public override void Draw(GameTime gameTime)
        {
            pb.Begin(PrimitiveType.LineList);

            if (!isCrashing)
            {
                DrawLander();

                if (showRocket1 || showRocket2)
                {
                    DrawLanderRocket();
                }
            }
            else
            {
                DrawLanderExplosion();
            }

            pb.End();

            base.Draw(gameTime);
        }

        #region Lander Parts
        private void DrawLander()
        {
            foreach (Vector2 v2 in landerCords)
            {
                float Xrotated = center.X + (v2.X - center.X) *
                    (float)Math.Cos(LanderValues.Rotation) - (v2.Y - center.Y) *
                    (float)Math.Sin(LanderValues.Rotation);

                float Yrotated = center.Y + (v2.X - center.X) *
                    (float)Math.Sin(LanderValues.Rotation) + (v2.Y - center.Y) *
                    (float)Math.Cos(LanderValues.Rotation);

                pb.AddVertex((new Vector2(Xrotated, Yrotated) * scale) + position, Color.White);//Color of the lander
            }
        }

        private void DrawLanderRocket()
        {
            foreach (Vector2 v2 in LanderValues.GetRocketCords(showRocket1))
            {
                float Xrotated = center.X + (v2.X - center.X) *
                    (float)Math.Cos(LanderValues.Rotation) - (v2.Y - center.Y) *
                    (float)Math.Sin(LanderValues.Rotation);

                float Yrotated = center.Y + (v2.X - center.X) *
                    (float)Math.Sin(LanderValues.Rotation) + (v2.Y - center.Y) *
                    (float)Math.Cos(LanderValues.Rotation);

                pb.AddVertex((new Vector2(Xrotated, Yrotated) * scale) + position, Color.Red);
            }
        }
        #endregion

        #region Lander Explosion
        private void DrawLanderExplosion()
        {
            //The draws each section of the lander
            foreach (Vector2 v2 in LanderValues.TopLines())
            {
                float Xrotated = topCenter.X + (v2.X - topCenter.X) *
                    (float)Math.Cos(topRotation) - (v2.Y - topCenter.Y) *
                    (float)Math.Sin(topRotation);

                float Yrotated = topCenter.Y + (v2.X - topCenter.X) *
                    (float)Math.Sin(topRotation) + (v2.Y - topCenter.Y) *
                    (float)Math.Cos(topRotation);

                pb.AddVertex((new Vector2(Xrotated, Yrotated) * scale) + topPosition, Color.White);//Color of the lander
            }

            foreach (Vector2 v2 in LanderValues.CapsuleLines())
            {
                float Xrotated = capsuleCenter.X + (v2.X - capsuleCenter.X) *
                    (float)Math.Cos(capsuleRotation) - (v2.Y - capsuleCenter.Y) *
                    (float)Math.Sin(capsuleRotation);

                float Yrotated = capsuleCenter.Y + (v2.X - capsuleCenter.X) *
                    (float)Math.Sin(capsuleRotation) + (v2.Y - capsuleCenter.Y) *
                    (float)Math.Cos(capsuleRotation);

                pb.AddVertex((new Vector2(Xrotated, Yrotated) * scale) + capsulePosition, Color.White);//Color of the lander
            }

            foreach (Vector2 v2 in LanderValues.LeftLegLines())
            {
                float Xrotated = leftLegCenter.X + (v2.X - leftLegCenter.X) *
                    (float)Math.Cos(leftLegRotation) - (v2.Y - leftLegCenter.Y) *
                    (float)Math.Sin(leftLegRotation);

                float Yrotated = leftLegCenter.Y + (v2.X - leftLegCenter.X) *
                    (float)Math.Sin(leftLegRotation) + (v2.Y - leftLegCenter.Y) *
                    (float)Math.Cos(leftLegRotation);

                pb.AddVertex((new Vector2(Xrotated, Yrotated) * scale) + leftLegPosition, Color.White);//Color of the lander
            }

            foreach (Vector2 v2 in LanderValues.RightLegLines())
            {
                float Xrotated = rightLegCenter.X + (v2.X - rightLegCenter.X) *
                    (float)Math.Cos(rightLegRotation) - (v2.Y - rightLegCenter.Y) *
                    (float)Math.Sin(rightLegRotation);

                float Yrotated = rightLegCenter.Y + (v2.X - rightLegCenter.X) *
                    (float)Math.Sin(rightLegRotation) + (v2.Y - rightLegCenter.Y) *
                    (float)Math.Cos(rightLegRotation);

                pb.AddVertex((new Vector2(Xrotated, Yrotated) * scale) + rightLegPosition, Color.White);//Color of the lander
            }

            if (Sound.isExplosionFinished()) //Stop Crashing
            {
                landerCrashed = true;
            }
        }
        #endregion

        #endregion

        #region Get and Set Methods
        public Vector2 GetLanderPos()
        {
            return position;
        }

        public TimeSpan GetGameTime()
        {
            return gameRunTime;
        }

        public void SetGameTime(TimeSpan newGameRunTime)
        {
            gameRunTime = newGameRunTime;
        }

        public bool DidWin()
        {
            return landedCorrectly;
        }

        public bool DidLose()
        {
            return landerCrashed;
        }
        #endregion
    }
}
