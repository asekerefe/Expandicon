using System;
using System.Collections.Generic;
using System.Text;

/*
 * ConsoleCallbackInterface 
 * 
 * a callback inteface for capturing and 
 * handling events from Console
 * to be used by the console GUI or -alternatively-
 * a file logger
 * 
 * written by Alican Şekerefe
*/

namespace Expandicon
{
    public interface ConsoleCallbackInterface
    {
        //called when there is a message to print to the console screen
        void handleMessageReceivedEvent(string messageFromConsole);
    }
}

