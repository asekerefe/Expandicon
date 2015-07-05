using UnityEngine;
using System.Collections.Generic;
using Expandicon;

/*
 * ConsolePanel
 * 
 * A class that creates and manages a 
 * console for using the Console
 * 
 * written by Alican Þekerefe
*/

public class ConsolePanel : MonoBehaviour, ConsoleCallbackInterface
{
    //number of lines that the console will 
    //show for history output messages
    public int lineCount = 8;
    //creates atest object and initializes 
    //the Console with its variables
    public bool initializeTestObjects = true;

    private Console console = null;

    private Camera camera = null;
    private List<GUIText> lines = null;
    private Stack<string> commandHistoryStack = null;
    private Stack<string> messageStack = null;
    private int commandHistoryStackIndex = 0;
    private bool isConsoleOpen = true;

    private string currentInput = "";

    private void Start()
    {
        initializeLines();
        if(initializeTestObjects)
            initializeConsoleTest();

        //get the camera for show/hide operation
        camera = transform.GetComponentInChildren<Camera>();
        console = new Console();
        console.registerAsEventSubscriber(this);
    }


    //an initializer for the console
    private void initializeConsoleTest()
    {
        //creates a gameobject that contains the test script
        GameObject testObject = new GameObject();
        testObject.name = "ConsoleTestObject";
        ConsoleTest testScript=testObject.AddComponent<ConsoleTest>();
        
        console.registerVariable("myBoolVar", testScript, "boolVar");
        console.registerVariable("myIntVar", testScript, "intVar");
        console.registerVariable("myFloatVar", testScript, "floatVar");
        console.registerVariable("myStringVar", testScript, "stringVar");

        GameObject testSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        testSphere.transform.parent = this.transform;
        testSphere.transform.position = Camera.main.transform.position + new Vector3(0, 0, 5);

        console.registerGameObject("mySphere", testSphere);
    }

    //initializes the lines to be shown as message output
    private void initializeLines()
    {
        //initialize the structures
        lines = new List<GUIText>();
        commandHistoryStack = new Stack<string>();
        messageStack = new Stack<string>();

        //find the Header object to get the reference starting point
        Transform header = transform.Find("Header");
        if (header != null)
        {
            //create a master container for all the line texts
            GameObject lineContainer = new GameObject();
            lineContainer.name = "Lines";
            lineContainer.layer = LayerMask.NameToLayer("GUI");
            lineContainer.transform.parent = this.transform;
            //create line texts
            for (int i = 0; i < lineCount; i++)
            {
                GameObject line = new GameObject();
                line.name = "Line" + (i + 1);
                line.layer = LayerMask.NameToLayer("GUI");
                GUIText lineText = line.AddComponent<GUIText>();
                line.transform.parent = lineContainer.transform;
                line.transform.position = header.position + new Vector3(0, 0, 100f); ;
                //set its position
                lineText.pixelOffset = new Vector2(14f, 14f * i);
                lines.Add(lineText);
            }
        }
        else
            Debug.LogError("Error! Header object for starting point reference is not found!");
    }

    //shows or hides the console by turning on/off the GUI camera
    public void showHideConsole(bool show)
    {
        camera.enabled = isConsoleOpen = show;
    }

    private void Update()
    {
        //process IO at every frame
        processIO();
    }

    //processes the IO events
    private void processIO()
    {
        if (Input.GetKeyDown(KeyCode.Home))
            showHideConsole(!isConsoleOpen);

        //if the console is not open, do not process anything.
        if (isConsoleOpen)
        {
            //remove the last char from the current input string
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if(currentInput.Length>0)
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);
            }
            else if (!Input.GetKey(KeyCode.Backspace))
            {
                //enter has been hit. run the command
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //save current command
                    commandHistoryStack.Push(currentInput);
                    //print it
                    printOutput(currentInput, false);
                    console.run(currentInput);
                    //reset the command stack index
                    commandHistoryStackIndex = -1;
                    currentInput = "";
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                    currentInput = getHistoricalCommandInput(true);
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    currentInput = getHistoricalCommandInput(false);
                else if (Input.GetKeyDown(KeyCode.Tab))
                    //try to complete the input through the Console
                    currentInput = console.getCompletedInput(currentInput);
                else
                    //add every other pressed buttons to the string
                    currentInput += Input.inputString;
            }

            lines[0].text = currentInput;
        }
    }

    //returns the next or the previous command input from the history
    //considering the input direction
    private string getHistoricalCommandInput(bool prev)
    {
        string[] stackArr=commandHistoryStack.ToArray();
        //increment/decrement the index get normalize it
        commandHistoryStackIndex = getNormalizedIndex(commandHistoryStackIndex + (prev ? 1 : -1),stackArr.Length);
        //if stack is empty, return an empty string
        return stackArr.Length>0?stackArr[commandHistoryStackIndex]:"";
    }

    //updates the gui texts with the latest values from the message stack
    private void updateLines()
    {
        string[] stackArr=messageStack.ToArray();
        for (int i = 0; i < stackArr.Length && i+1 < lineCount; i++)
            lines[i + 1].text = stackArr[i];
    }

    //returns a normalized cyclic index for the given index and length
    private int getNormalizedIndex(int index, int length)
    {
        return index < 0 ? length-1 : (index >= length ? 0 : index);
    }

    //prints a message to the console by adding it to the stack and updating
    //the screen
    private void printOutput(string message, bool updateConsole)
    {
        messageStack.Push(message);

        if (updateConsole)
            updateLines();
    }

    //message received event handler. 
    public void handleMessageReceivedEvent(string messageFromConsole)
    {
        printOutput(messageFromConsole, true);
    }
}