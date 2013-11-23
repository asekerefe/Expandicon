using UnityEngine;

/*
 * Activate 
 * 
 * Changes the state of the game object to Active
 * using GameObject.SetActive method
 * 
 * usage: 'activate <gameobject>'
 * 
 * written by Alican Şekerefe
*/
namespace Expandicon
{
    public class ActivateCommand : StateCommand
    {
        //constructor
        public ActivateCommand() : base("activate") { }

        //sets the given game object's state to active
        protected override void setState(GameObject target)
        {
            target.SetActive(true);
        }
    }
}