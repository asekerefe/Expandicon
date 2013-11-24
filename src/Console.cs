using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

/*
 * Console
 * 
 * A singleton class which controls the console system;
 * the brain. Main responsibilities:
 * 
 * - Registration of console commmands, variables and game objects.  
 * - Console input completion
 * - Running the commands by passing the relevant parameters
 * 
 * written by Alican Şekerefe 
*/

namespace Expandicon
{
    public class Console
    {
        //the singleton
        private static Console singleton = null;

        //lists and dictionaries for the commands and variables
        private List<string> commandList = null;
        private Dictionary<string, Command> commandNameToCommandMap = null;
        private List<string> variableAliasList = null;
        private Dictionary<string, Variable> aliasToVariableMap = null;
        private List<string> gameObjectAliasList = null;
        private Dictionary<string, GameObject> aliasToGameObjectMap = null;

        //a subscriber list for console events
        private List<ConsoleCallbackInterface> eventSubscribers = null;

        //singleton getter
        public static Console getSingleton()
        {
            if (singleton == null)
                singleton = new Console();
            return singleton;
        }

        private Console()
        {
            initialize();
        }

        //initializes the data structures
        private void initialize()
        {
            aliasToVariableMap = new Dictionary<string, Variable>();
            variableAliasList = new List<string>();

            commandNameToCommandMap = new Dictionary<string, Command>();
            commandList = new List<string>();

            aliasToGameObjectMap = new Dictionary<string, GameObject>();
            gameObjectAliasList = new List<string>();


            eventSubscribers = new List<ConsoleCallbackInterface>();

            registerBuiltInCommands();
        }

        //registers the built-in commands to the system
        private void registerBuiltInCommands()
        {
            //variable commands
            registerCommand(new GetCommand());
            registerCommand(new SetCommand());
            registerCommand(new ResetCommand());
            //gameobject commands
            registerCommand(new GetPositionCommand());
            registerCommand(new SetPositionCommand());
            registerCommand(new ActivateCommand());
            registerCommand(new DeactivateCommand());
            registerCommand(new DestroyCommand());
            //generic commands
            registerCommand(new SendRateCommand());
            registerCommand(new TimeCommand());
        }

        //registers the given object as an event subscriber to send notifications
        public void registerAsEventSubscriber(ConsoleCallbackInterface target)
        {
            //prevent having duplications and null references
            if (target != null && !eventSubscribers.Contains(target))
                eventSubscribers.Add(target);
        }

        //registers the gameobject with its alias
        public void registerGameObject(string alias, GameObject gameObject)
        {
            if (gameObject != null)
            {
                //make sure there won't be duplications
                if (!aliasToGameObjectMap.ContainsKey(alias))
                {
                    gameObjectAliasList.Add(alias);
                    aliasToGameObjectMap[alias] = gameObject;
                }
            }
            else
                Debug.LogError("gameObject is NULL!");
        }

        //registers the command with the given commandName alias
        //to the console system
        public void registerCommand(Command command)
        {
            if (command != null)
            {
                //add the command to the structures
                string commandName = command.getCommandName();
                commandList.Add(commandName);
                commandNameToCommandMap[commandName] = command;
            }
        }

        //register the variable with the given alias to the console.
        //alias: the name that will be used from the console
        //targetObject: owner of the variable
        //targetVariableName: variable's name as string
        public void registerVariable(string alias, object targetObject, string targetVariableName)
        {
            if (targetObject != null)
            {
                //store data given with in a VariableInfo object to keep it 
                Variable varInfo = new Variable(alias, targetObject, targetVariableName);
                aliasToVariableMap[alias] = varInfo;
                variableAliasList.Add(alias);
            }
            else
                Debug.LogError("targetObject is NULL!");
        }

        //multi-alias version of registerVariable which a string[]
        //to create multiple aliases to the same variable
        public void registerVariable(string[] aliases, object targetObject, string targetVariableName)
        {
            if (aliases != null)
                foreach (string alias in aliases)
                    registerVariable(alias, targetObject, targetVariableName);
        }

        //returns the closest command name compared to the given input
        public string getClosestCommandName(string incompleteCommandName)
        {
            return findClosestWord(incompleteCommandName, commandList);
        }

        //returns the closest alias name compared to the given input
        public string getClosestVariableAlias(string incompleteAliasName)
        {
            return findClosestWord(incompleteAliasName, variableAliasList);
        }

        //returns the closest alias name compared to the given input
        private string getClosestGameObjectAlias(string incompleteGameObjectName)
        {
            return findClosestWord(incompleteGameObjectName, gameObjectAliasList);
        }

        //returns the first similar word to the 'input' in the words list
        private string findClosestWord(string input, List<string> words)
        {
            string result = input;
            foreach (string possibleWord in words)
            {
                //perform case insensitive comparison
                if (possibleWord.ToLower().StartsWith(input))
                {
                    result = possibleWord;
                    break;
                }
            }

            return result;
        }

        //runs the given console command as input
        public void run(string input)
        {
            string feedback = "";
            //get parameters
            List<string> parameters = new List<string>(input.Trim().Split(' '));
            if (parameters.Count > 0)
            {
                //get the command name
                string commandName = parameters[0].Trim();
                parameters.RemoveAt(0);
                //check whether it exists
                Command command = null;
                commandNameToCommandMap.TryGetValue(commandName, out command);

                //TODO: Refactor!
                if (command != null)
                {
                    //check whether the command requires a variable to run
                    if (command.getParameterType() == ParameterType.VARIABLE)
                    {
                        //make sure the next parameter is a registered variable
                        if (parameters.Count > 0 && variableAliasList.Contains(parameters[0]))
                        {
                            //get VariableInfo and run the command by passing it to the Command object
                            string alias = parameters[0];
                            parameters.RemoveAt(0);
                            Variable info = aliasToVariableMap[alias];
                            object targetObject = info.getTargetObject();
                            if(targetObject!=null)
                                feedback = commandName + ": " + command.runCommand(info, parameters.ToArray());
                            else
                                feedback = "Console error: object is NULL!";
                        }
                        else
                            feedback = "Console error: command expects a valid variable alias";
                    }
                    else if (command.getParameterType() == ParameterType.GAMEOBJECT)
                    {
                        //make sure the next parameter is a registered variable
                        if (parameters.Count > 0 && gameObjectAliasList.Contains(parameters[0]))
                        {
                            string alias = parameters[0];
                            parameters.RemoveAt(0);
                            GameObject gameObject = aliasToGameObjectMap[alias];
                            if (gameObject != null)
                                feedback = commandName + ": " + command.runCommand(gameObject, parameters.ToArray());
                            else
                                feedback = "Console error: given GameObject has been destroyed!";
                        }
                        else
                            feedback = "Console error: command expects a valid variable alias";
                    }
                    else
                        feedback = commandName + ": " + command.runCommand(parameters.ToArray());

                }
                else
                    feedback = "Console error: command not found";

            }

            //prints the feedback message either provided by the console or the command
            printMessage(feedback);
        }

        //performs a completion operation on the given console input
        public string getCompletedInput(string input)
        {
            string result = input.Trim();
            List<string> tokens = new List<string>(input.Split(' '));

            //if there is only one token, then it is a command
            if (tokens.Count == 1)
            {
                //get the closest one
                string closestCommand = getClosestCommandName(tokens[0]);
                //check if it is found
                if (closestCommand != tokens[0])
                    //put an empty space at the end of the result
                    result = closestCommand + " ";
            }
            //if there are two tokens, the second one should be the variable
            else if (tokens.Count == 2)
            {
                //make sure the first token is a valid commmand
                Command targetCommand = getCommandByAlias(tokens[0]);
                if (targetCommand != null)
                {
                    //command is valid. find out which type of parameter it requires
                    ParameterType paramType = targetCommand.getParameterType();

                    if (paramType != ParameterType.GENERIC)
                    {
                        string closestPossibleAlias = "";

                        if (paramType == ParameterType.VARIABLE)
                            closestPossibleAlias = getClosestVariableAlias(tokens[1]);
                        else if (paramType == ParameterType.GAMEOBJECT)
                            closestPossibleAlias = getClosestGameObjectAlias(tokens[1]);

                        //check if it is found
                        if (closestPossibleAlias != tokens[1])
                            //put an empty space at the end of the result
                            result = tokens[0] + " " + closestPossibleAlias + " ";
                    }
                    else
                        //no parameters required. so return the command's itself
                        result = tokens[0];
                }
            }

            return result;
        }

        //used for printing messages to the console, a file etc. depending on the role of the subscriber.
        public void printMessage(string message)
        {
            notifyNewMessage(message);
        }

        //notifies the subscribers about the message to be printed
        private void notifyNewMessage(string message)
        {
            foreach (ConsoleCallbackInterface target in eventSubscribers)
            {
                try
                {
                    target.handleMessageReceivedEvent(message);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        //returns the command object by its alias.
        private Command getCommandByAlias(string alias)
        {
            Command command = null;
            commandNameToCommandMap.TryGetValue(alias, out command);
            return command;
        }

        //returns the variable object by its alias.
        private Variable getVariableByAlias(string alias)
        {
            Variable variable = null;
            aliasToVariableMap.TryGetValue(alias, out variable);
            return variable;
        }
    }
}