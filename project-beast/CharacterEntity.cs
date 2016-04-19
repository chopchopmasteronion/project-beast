using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WalkingGame;


namespace Player
{
    public class CharacterEntity
    {
        static Texture2D characterSheetTexture;
        Animation walkDown;
        Animation walkUp;
        Animation walkLeft;
        Animation walkRight;

        Animation standDown;
        Animation standUp;
        Animation standLeft;
        Animation standRight;

        Animation currentAnimation;
        float speed;

        public float X
        {
            get;
            set;
        }

        public float Y
        {
            get;
            set;
        }

        public CharacterEntity(GraphicsDevice graphicsDevice)
        {
            if (characterSheetTexture == null)
            {
                using (var stream = TitleContainer.OpenStream("Content/charactersheet.png"))
                {
                    characterSheetTexture = Texture2D.FromStream(graphicsDevice, stream);
                }
            }

            speed = 40;

            walkDown = new Animation();
            walkDown.AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkDown.AddFrame(new Rectangle(16, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkDown.AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkDown.AddFrame(new Rectangle(32, 0, 16, 16), TimeSpan.FromSeconds(.25));

            walkUp = new Animation();
            walkUp.AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkUp.AddFrame(new Rectangle(160, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkUp.AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkUp.AddFrame(new Rectangle(176, 0, 16, 16), TimeSpan.FromSeconds(.25));

            walkLeft = new Animation();
            walkLeft.AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkLeft.AddFrame(new Rectangle(64, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkLeft.AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkLeft.AddFrame(new Rectangle(80, 0, 16, 16), TimeSpan.FromSeconds(.25));

            walkRight = new Animation();
            walkRight.AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkRight.AddFrame(new Rectangle(112, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkRight.AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkRight.AddFrame(new Rectangle(128, 0, 16, 16), TimeSpan.FromSeconds(.25));

            // Standing animations only have a single frame of animation:
            standDown = new Animation();
            standDown.AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));

            standUp = new Animation();
            standUp.AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));

            standLeft = new Animation();
            standLeft.AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));

            standRight = new Animation();
            standRight.AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));
        }

        Vector2 GetDesiredVelocityFromInput()
        {
            Vector2 desiredVelocity = new Vector2();

            GamePadCapabilities capabilities = GamePad.GetCapabilities(
                                               PlayerIndex.One);
            // If there a controller attached, handle it
            if (capabilities.IsConnected)
            {
                // Get the current state of Controller1
                GamePadState state = GamePad.GetState(PlayerIndex.One);

                // You can check explicitly if a gamepad has support for a certain feature
                if (capabilities.HasLeftXThumbStick)
                {
                    // Check teh direction in X axis of left analog stick
                    if (state.ThumbSticks.Left.X < -0.5f)
                        desiredVelocity.X -= speed;
                    if (state.ThumbSticks.Left.X > 0.5f)
                        desiredVelocity.X += speed;
                    if (state.ThumbSticks.Left.Y < -0.5f)
                        desiredVelocity.Y += speed;
                    if (state.ThumbSticks.Left.Y > 0.5f)
                        desiredVelocity.Y -= speed;
                }

                // You can also check the controllers "type"
                /*
                if (capabilities.GamePadType == GamePadType.GamePad)
                {
                    if (state.IsButtonDown(Buttons.A))
                        Exit();
                }
                */
            }
            return desiredVelocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 topLeftOfSprite = new Vector2(this.X, this.Y);
            Color tintColor = Color.White;
            var sourceRectangle = currentAnimation.CurrentRectangle;

            spriteBatch.Draw(characterSheetTexture, topLeftOfSprite, sourceRectangle, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            var velocity = GetDesiredVelocityFromInput();

            this.X += velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.Y += velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (velocity != Vector2.Zero)
            {
                bool movingHorizontally = Math.Abs(velocity.X) > Math.Abs(velocity.Y);
                if (movingHorizontally)
                {
                    if (velocity.X > 0)
                    {
                        currentAnimation = walkRight;
                    }
                    else
                    {
                        currentAnimation = walkLeft;
                    }
                }
                else
                {
                    if (velocity.Y > 0)
                    {
                        currentAnimation = walkDown;
                    }
                    else
                    {
                        currentAnimation = walkUp;
                    }
                }
            }
            else
            {
                // If the character was walking, we can set the standing animation
                // according to the walking animation that is playing:
                if (currentAnimation == walkRight)
                {
                    currentAnimation = standRight;
                }
                else if (currentAnimation == walkLeft)
                {
                    currentAnimation = standLeft;
                }
                else if (currentAnimation == walkUp)
                {
                    currentAnimation = standUp;
                }
                else if (currentAnimation == walkDown)
                {
                    currentAnimation = standDown;
                }
                else if (currentAnimation == null)
                {
                    currentAnimation = standDown;
                }

                // if none of the above code hit then the character
                // is already standing, so no need to change the animation.
            }

            currentAnimation.Update(gameTime);
        }
    }
}