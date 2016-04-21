using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Inputs
{
    class BoardInput
    {
        public InputText inputText;
        GamePadCapabilities capabilities = GamePad.GetCapabilities(
                                       PlayerIndex.One);

        public string GetInputDirection()
        {
            inputText = new InputText();

            string direction = inputText.NONE;
            
            // Poll for current keyboard state
            KeyboardState state = Keyboard.GetState();

            // Move our sprite based on arrow keys being pressed:
            if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
                direction = inputText.LEFT;
            if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A))
                direction = inputText.RIGHT;
            if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W))
                direction = inputText.DOWN;
            if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
                direction = inputText.UP;
            
            return direction;
        }
    }
}
