using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Inputs
{
    public class PadInput
    {
        public InputText inputText;
        GamePadCapabilities capabilities = GamePad.GetCapabilities(
                                       PlayerIndex.One);

        public string GetInputDirection()
        {
            string direction = "NONE";
            inputText = new InputText();
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
                        direction = inputText.RIGHT;
                    if (state.ThumbSticks.Left.X > 0.5f)
                        direction = inputText.LEFT;
                    if (state.ThumbSticks.Left.Y < -0.5f)
                        direction = inputText.UP;
                    if (state.ThumbSticks.Left.Y > 0.5f)
                        direction = inputText.DOWN;
                }
            }
            // You can also check the controllers "type"
            /*
            if (capabilities.GamePadType == GamePadType.GamePad)
            {
                if (state.IsButtonDown(Buttons.A))
                    Exit();
            }
            */
            return direction;
        }
    }
}
