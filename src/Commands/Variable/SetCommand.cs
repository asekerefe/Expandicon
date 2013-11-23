using UnityEngine;
using System.Collections;
using System;

/* 
 * SetCommand
 * 
 * The responsibility of this Command is
 * to set the value of the relevant 
 * varible to the provided one.
 * 
 * usage 'set <variablename> <value>'
 * 
 * written by Alican Şekerefe
*/
namespace Expandicon
{
    public class SetCommand : Command
    {
        //constructor
        public SetCommand() : base("set") { }

        //sets the value of the related variable by parsing the parameters
        public override string runCommand(Variable varInfo, string[] parameters)
        {
            string feedbackMessage = "";

            //check whether there are parameters
            if (parameters != null && parameters.Length > 0)
            {
                //store the current one for feedback
                object previousValue = varInfo.getCurrentValue();
                try
                {
                    //try to set the value
                    varInfo.setValue(parameters[0] as object);
                    feedbackMessage += "value of " + varInfo.getAlias() + " has been changed from '" + previousValue + "' to '" + parameters[0] + "'";
                }
                catch (FormatException e)
                {
                    //handle format exception
                    feedbackMessage += "type mismatch for -> '" + parameters[0] + "'";
                }
                catch (OverflowException e)
                {
                    //handle overflow exception
                    feedbackMessage += "input value causes overflow";
                }
            }
            else
                //there are no values provided
                feedbackMessage += "value is missing";


            return feedbackMessage;
        }

        //this command requires a variable to run. so it should return VARIABLE
        public override ParameterType getParameterType()
        {
            return ParameterType.VARIABLE;
        }
    }
}