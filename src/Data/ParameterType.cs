/*
 * ParameterType
 * 
 * Enumerator for specifying the
 * parameter type of a command.
 * 
 * GENERIC: can work with/without parameters.
 * Therefore, console will not look for an alias before running it.
 * 
 * VARIABLE: used for commands that require Variable parameters.
 * Console will expect a valid variable alias for the first parameter.
 * 
 * GAMEOBJECT: used for commands that require GameObject parameters.
 * Console will expect a valid GameObject alias for the first parameter.
 * 
 * written by Alican Şekerefe 
*/

namespace Expandicon
{
    public enum ParameterType
    {
        GENERIC,
        VARIABLE,
        GAMEOBJECT
    }
}
