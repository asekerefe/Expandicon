using System;
using UnityEngine;

/*
 * SendRateCommand
 * 
 * This class is responsible for
 * changing the send rate of the network
 * 
 * usage: 'sendrate <value>'
 * 
 * written by Alican Şekerefe
*/

namespace Expandicon
{
    public class SendRateCommand : Command
    {
        //constructor
        public SendRateCommand() : base("sendrate") { }

        //sets the network send rate to the given value
        public override string runCommand(string[] parameters)
        {
            string feedbackMessage = "";

            if (parameters.Length == 1)
            {
                try
                {
                    //convert to float
                    float newRate = float.Parse(parameters[0]);
                    //successful. store the old value first
                    float oldRate = Network.sendRate;
                    //change the rate
                    Network.sendRate = newRate;
                    feedbackMessage = "Network.sendRate has been changed from " + oldRate + " to " + newRate;

                }
                catch (Exception e)
                {
                    feedbackMessage = "type mismatch";
                }
            }
            else
                feedbackMessage = "incorrect number of parameters! Try passing a single value";


            return feedbackMessage;
        }


        //this command does not require any special object. return GENERIC
        public override ParameterType getParameterType()
        {
            return ParameterType.GENERIC;
        }
    }
}