using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/*
 * DestroyCommand 
 * 
 * Destroys the given GameObject using
 * GameObject.Destroy
 * 
 * usage 'destroy <gameobject>'
 * 
 * written by Alican Şekerefe
*/

namespace Expandicon
{
    public class DestroyCommand : Command
    {
        //constructor
        public DestroyCommand() : base("destroy") { }

        //called by the Console with a valid GameObject to be destroyed
        public override string runCommand(GameObject gameObject, string[] parameters)
        {
            //destroy the object
            GameObject.Destroy(gameObject);
            return "GameObject has been destroyed";
        }

        //because DestroyCommand needs a GameObject, this method returns GAMEOBJECT.
        public override ParameterType getParameterType()
        {
            return ParameterType.GAMEOBJECT;
        }
    }
}

