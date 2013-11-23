Expandicon
==========

Expandicon is a console implementation to be used in Unity3D projects in order to change values and perform specific operations on GameObjects without interacting the Unity3D editor. 

Expandicon stands for '<b>Expandable Console</b>' which identifes its main feature: expandability.

Project Expandicon is currently under development.

Current features:

- Variable and GameObject registration to manage them inside the console
- Custom command registration
- get/set/reset the value of a variable
- get/set position of a GameObject
- time command to get UnityEngine.Time.time
- sendrate command to set Network.sendRate
- activate/deactive a GameObject
- Command completion
- Console panel with command history


Getting Started
===============

Core of the console does not require a GUI to work. Therefore, in order to perform operations such as running a commmand or autocompleting an input, you can directly use its related methods by passing strings.

Console is a singleton class which utilizes from publish/subscribe design pattern to notify its subscribers about the output messages generated by the console or the command. By using the related interface, you can use the messages to print on your own console GUI or a console logger.

At latest version, Expandicon comes with a console panel which handles IO operations. Tutorial will be added soon.



In order to use Expandicon to manage your variables or GameObjects runtime, you should register them to the console. 

Variables
=========

To register a variable, you should pass its object and the target variable's name as string with an alias:
<code>Console.getSingleton().registerVariable("pID", obj, "playerID");</code>

After registering the variable, you will able to run related commands on your variables.

For instance, to change the value of PlayerID variable to 10, you should type '<b>set pID 10</b>'.


GameObjects
===========

Currently, target GameObjects are registered the same with Variables. However, you can define your auto registration script to add them on desired GameObjects.

To register a GameObject to the console, you should send an alias and object's itself.
 
<code>Console.getSingleton().registerGameObject("soldier", gameObject);</code>

Now, defined commands can be run on 'soldier' game object. For instance, you can type '<b>setposition soldier 0 10 0</b>' to set its position to (0,10,0)



