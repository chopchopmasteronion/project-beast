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
            speed = 20;
            walkDown = new Animation();
            walkDown.AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkDown.AddFrame(new Rectangle(16, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkDown.AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkDown.AddFrame(new Rectangle(32, 0, 16, 16), TimeSpan.FromSeconds(.25));
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

            // temporary - we'll replace this with logic based off of which way the
            // character is moving when we add movement logic
            currentAnimation = walkDown;

            currentAnimation.Update(gameTime);
        }
    }
}