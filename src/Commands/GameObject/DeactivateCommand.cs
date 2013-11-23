using UnityEngine;

/*
 * Deactivate 
 * 
 * Changes the state of the game object to Deactive
 * using GameObject.SetActive method
 * 
 * usage: 'activate <gameobject>'
 * 
 * written by Alican Şekerefe
*/

namespace Expandicon
{
    public class DeactivateCommand : StateCommand
    {
        //constructor
        public DeactivateCommand() : base("deactivate") { }

        //sets the given game object's state to deactive
        protected override void setState(GameObject target)
        {
            target.SetActive(false);
        }
    }
}

