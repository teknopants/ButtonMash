using Microsoft.Xna.Framework.Input;

namespace ButtonMash
{
    public struct Button
    {
        public Player Player { get; }
        public ButtonName ButtonName { get; }
        public ActionState ActionState { get; }
        public Keys[] Keys { get; }
        public Buttons[] Buttons { get; }
        public float TimeHeld {get; }

        public Button(Player player, ButtonName buttonName, Keys[] keys, Buttons[] buttons, float timeHeld = 0f, ActionState actionState = ActionState.None)
        {
            Player = player;
            ButtonName = buttonName;
            ActionState = actionState;
            Keys = keys;
            Buttons = buttons;
            TimeHeld = timeHeld;
        }
    }
}