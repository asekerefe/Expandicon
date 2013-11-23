using UnityEngine;

/*
 * TimeCommand
 * 
 * Prints the UnityEngine.Time.time
 * 
 * usage: 'time'
 * 
 * written by Alican Şekerefe
*/

namespace Expandicon
{
    public class TimeCommand : Command
    {
        //constructor
        public TimeCommand() : base("time") { }

        //prints the time from UnityEngine.Time.time
        public override string runCommand(string[] parameters)
        {
            return "Time.time: " + Time.time;
        }

        //time command does not require any type of parameters to run. so, return GENERIC
        public override ParameterType getParameterType()
        {
            return ParameterType.GENERIC;
        }
    }
}

