using UnityEngine;
using System.Collections;

/*
 * Command 
 * 
 * This is the abstract base class of all the
 * commands that can be run by the Console. 
 * 
 * written by Alican Şekerefe
*/
namespace Expandicon
{
    public abstract class Command
    {
        private string commandName = "";

        protected Command(string commandName)
        {
            this.commandName = commandName;
        }

        //this method is called by Console if the command
        //requires a variable to run. A feedback message should be returned
        //to be used in the console as command output
        public virtual string runCommand(Variable varInfo, string[] parameters)
        {
            return "Unimplemented method!";
        }

        //this method is called by Console if the command
        //do not require a specific parameter to perform its operation. 
        //A feedback message should be returned to be used in 
        //the console as command output
        public virtual string runCommand(string[] parameters)
        {
            return "Unimplemented method!";
        }

        //this method is called by Console if the command
        //is trying to be invoked with a GameObject instance to
        //perform a specific command. returns the feedback message
        //of the operation
        public virtual string runCommand(GameObject gameObject, string[] parameters)
        {
            return "Unimplemented method!";
        }

        //returns the parameter type of the command
        public abstract ParameterType getParameterType();

        //returns the name of the command
        public string getCommandName()
        {
            return commandName;
        }
    }
}