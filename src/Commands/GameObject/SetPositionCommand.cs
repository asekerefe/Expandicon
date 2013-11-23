using System;
using UnityEngine;

/*
 * SetPositionCommand 
 * 
 * Sets the position of the given game object
 * with its parameters
 * 
 * usage: 'setposition <gameobject> <x y z>'
 * 
 * written by Alican Şekerefe
*/

namespace Expandicon
{
    public class SetPositionCommand : Command
    {
        //constructor
        public SetPositionCommand() : base("setposition") { }

        //sets the position of the given game object considering the parameters
        //returns the feedback message 
        public override string runCommand(GameObject target, string[] parameters)
        {
            string feedbackMessage = "";

            //check the count of the parameters; it should be 3
            if (parameters.Length == 3)
            {
                try
                {
                    //convert to float
                    float x = float.Parse(parameters[0]);
                    float y = float.Parse(parameters[1]);
                    float z = float.Parse(parameters[2]);

                    //conversion successful. set the position
                    setPosition(target, x, y, z);

                    feedbackMessage = "position is set to " + new Vector3(x, y, z);
                }
                catch (Exception e)
                {
                    feedbackMessage = "type mismatch";
                }
            }
            else
                feedbackMessage = "incorrect number of parameters! Try passing x, y and z";


            return feedbackMessage;
        }

        protected virtual void setPosition(GameObject target, float x, float y, float z)
        {
            target.transform.position = new Vector3(x, y, z);
        }


        //it requires a game object. so, return GAMEOBJECT
        public override ParameterType getParameterType()
        {
            return ParameterType.GAMEOBJECT;
        }
    }
}
