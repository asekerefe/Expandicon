using UnityEngine;
using System.Collections;
using System;

/*
 * StateCommand
 * 
 * An abstract class to be used by
 * Enable and Disable commands mutually
 *
 * Since it's abstract, this command cannot
 * be used directly
 * 
 * written by Alican Þekerefe
*/

namespace Expandicon
{
    public abstract class StateCommand : Command
    {
        //constructor
        protected StateCommand(string commandName) : base(commandName) { }

        //requires a gameobject to change its state. return GAMEOBJECT
        public override ParameterType getParameterType()
        {
            return ParameterType.GAMEOBJECT;
        }

        //changes the state of the given game object by calling the abstract method
        public override string runCommand(GameObject target, string[] parameters)
        {
            string feedbackMessage = "";
            setState(target);
            bool isActive = target.activeSelf;
            feedbackMessage = "gameobject's state has been set to " + (isActive ? "Active" : "Deactive");

            return feedbackMessage;
        }

        //abstract method to be implemented by Enable or Disable
        protected abstract void setState(GameObject target);
    }
}