using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Inputs;

namespace Inputs
{
    public class HardInputs
    {
        InputText inputText;
        PadInput padInput;
        BoardInput boardInput;


        public string GetInputDirection()
        {
            boardInput = new BoardInput();
            padInput = new PadInput();
            inputText = new InputText();
            string direction = inputText.NONE;

            if (padInput.GetInputDirection() != inputText.NONE)
                direction = padInput.GetInputDirection();
            if (boardInput.GetInputDirection() != inputText.NONE)
                direction = boardInput.GetInputDirection();

            return direction;
        }
    }
}
