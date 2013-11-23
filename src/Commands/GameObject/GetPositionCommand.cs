using UnityEngine;

/*
 * GetPositionCommand
 * 
 * Returns the global position of the given
 * GameObject.
 * 
 * usage: 'getposition <gameobject>'
 * 
 * written by Alican Şekerefe
*/

namespace Expandicon
{
    public class GetPositionCommand : Command
    {
        //constructor
        public GetPositionCommand() : base("getposition") { }


        //get position requires a gameobject to print its position info. return GAMEOBJECT
        public override ParameterType getParameterType()
        {
            return ParameterType.GAMEOBJECT;
        }

        //returns the position info of the target game object as the feedback message
        public override string runCommand(GameObject gameObject, string[] parameters)
        {
            return gameObject.transform.position.ToString();
        }
    }
}
