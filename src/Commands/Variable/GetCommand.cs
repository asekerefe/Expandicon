using UnityEngine;
using System.Collections;

/*
 * GetCommand
 * 
 * This command returns the value of the
 * given variable as the feedback message
 * 
 * usage: 'get <variable name>'
 * 
 * written by Alican Şekerefe
*/

namespace Expandicon
{
    public class GetCommand : Command
    {
        //constructor
        public GetCommand() : base("get") { }

        //returns the value of the variable as feedback message. parameters are not processed
        public override string runCommand(Variable varInfo, string[] parameters)
        {
            string feedbackMessage = "";
            //get the value and generate the message
            feedbackMessage = varInfo.getAlias() + " -> '" + varInfo.getCurrentValue() + "'";

            return feedbackMessage;
        }

        //this command requires a variable to run. so it should return VARIABLE
        public override ParameterType getParameterType()
        {
            return ParameterType.VARIABLE;
        }
    }
}