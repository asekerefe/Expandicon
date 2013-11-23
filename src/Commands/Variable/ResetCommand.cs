using UnityEngine;
using System.Collections;

/*
 * ResetCommand
 * 
 * This command resets the value of
 * the given variable to its initial.
 * 
 * usage: 'reset <variable name>'
 * 
 * written by Alican Şekerefe
*/
namespace Expandicon
{
    public class ResetCommand : Command
    {
        //constructor
        public ResetCommand() : base("reset") { }

        //resets the value of the given variable. parameters are not processed
        public override string runCommand(Variable varInfo, string[] parameters)
        {
            string feedbackMessage = "";
            //reset the value
            varInfo.resetValue();
            feedbackMessage = "variable has been rolled back to its initial value '" + varInfo.getOriginalValue() + "'";

            return feedbackMessage;
        }

        //this command requires a variable to run. so it should return VARIABLE
        public override ParameterType getParameterType()
        {
            return ParameterType.VARIABLE;
        }
    }
}