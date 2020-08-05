using Microsoft.Xna.Framework.Input;
using System;

namespace ButtonMash
{
    public class ButtonControl
    {
        public override void Update(double dt)
        {
            var keyboardState = Keyboard.GetState();

            foreach(var buttonComponentEntity in ReadEntities<ButtonComponent>())
            {
                var buttonComponent = GetComponent<ButtonComponent>(buttonComponentEntity);

                var playerIndex = Microsoft.Xna.Framework.PlayerIndex.One;
                switch(buttonComponent.Player)
                {
                    case Player.One:
                        playerIndex = Microsoft.Xna.Framework.PlayerIndex.One;
                        break;
                    case Player.Two:
                        playerIndex = Microsoft.Xna.Framework.PlayerIndex.Two;
                        break;
                    case Player.Three:
                        playerIndex = Microsoft.Xna.Framework.PlayerIndex.Three;
                        break;
                }
                var gamePadState = GamePad.GetState(playerIndex);

                var new_ActionState = buttonComponent.ActionState;
                var new_TimeHeld = buttonComponent.TimeHeld;

                var buttonDown = false;

                foreach(var button in buttonComponent.Buttons)
                {
                    if (gamePadState.IsButtonDown(button))
                    {
                        buttonDown = true;
                        GamePad.SetVibration(playerIndex, 1, 1);
                        GamePad.SetLightBarEXT(playerIndex, new Microsoft.Xna.Framework.Color((float)MathHelper.RandomDouble(0, 1), (float)MathHelper.RandomDouble(0, 1), (float)MathHelper.RandomDouble(0, 1)));
                    }
                }
                foreach(var key in buttonComponent.Keys)
                {
                    if (keyboardState.IsKeyDown(key))
                    {
                        buttonDown = true;
                    }
                }

                if (buttonDown)
                {
                    new_ActionState = PressButton(new_ActionState);
                    new_TimeHeld += (float)dt;
                }
                else
                {
                    new_ActionState = ReleaseButton(new_ActionState);
                    new_TimeHeld = 0;
                }
                
                if (new_ActionState != ActionState.None)
                {
                    if(new_ActionState == ActionState.Pressed)
                    {
                        var datevalue = DateTime.Now;
                        //System.Console.WriteLine("{0} seconds input", datevalue.ToString("s.ffffff"));
                    }
                    //System.Console.WriteLine(ButtonName.ToString() + " is " + buttonState.ToString() + " " + TimeHeld);
                    SendMessage(new InputMessage(buttonComponent.Player, buttonComponent.ButtonName, new_ActionState, new_TimeHeld));
                }

                SetComponent(buttonComponentEntity, new ButtonComponent(buttonComponent.Player, buttonComponent.ButtonName, buttonComponent.Keys, buttonComponent.Buttons, new_TimeHeld, new_ActionState));
            }
        }

        public ActionState PressButton(ActionState button)
        {
            switch(button)
            {
                case ActionState.Pressed:
                case ActionState.Held:
                    return ActionState.Held;
                default:
                    return ActionState.Pressed;
            }
        }
        
        public ActionState ReleaseButton(ActionState button)
        {
            switch(button)
            {
                case ActionState.Released:
                case ActionState.None:
                    return ActionState.None;
                default:
                    return ActionState.Released;
            }
        }
        
    }
}