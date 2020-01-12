using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public enum ButtonStatus
    {
        Off,
        Down,
        Pressed,
        Up
    }
    public class EasyButton
    {
        public StateMachine<ButtonStatus> State { get; protected set; }
        public string ButtonID;

        public delegate void ButtonDownMethodDelegate();
        public delegate void ButtonPressedMethodDelegate();
        public delegate void ButtonUpMethodDelegate();

        public ButtonDownMethodDelegate ButtonDownMethod;
        public ButtonPressedMethodDelegate ButtonPressedMethod;
        public ButtonUpMethodDelegate ButtonUpMethod;

        public EasyButton(string playerId, string buttonID, ButtonDownMethodDelegate btnDown, ButtonPressedMethodDelegate btnPressed, ButtonUpMethodDelegate btnUp)
        {
            ButtonID = playerId + "_" + buttonID;
            ButtonDownMethod = btnDown;
            ButtonPressedMethod = btnPressed;
            ButtonUpMethod = btnUp;
            State = new StateMachine<ButtonStatus>(null, false);
            State.ChangeState(ButtonStatus.Off);
        }

        public virtual void TriggerButtonDown()
        {
            ButtonDownMethod();
        }

        public virtual void TriggerButtonPressed()
        {
            ButtonPressedMethod();
        }

        public virtual void TriggerButtonUp()
        {
            ButtonUpMethod();
        }

    }
}
