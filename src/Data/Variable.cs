using UnityEngine;
using System;
using System.Reflection;
using System.Collections;

/*
 * Variable
 * 
 * This class handles set and get
 * operation of a given variable
 * with its owner object using .NET's
 * reflection.
 * 
 * written by Alican Şekerefe
*/

namespace Expandicon
{
    public class Variable
    {
        private object targetObject = null;
        private object originalValue = null;
        private string alias = "";
        private FieldInfo fieldInfo;

        //constructor of the class. 
        public Variable(string alias, object targetObject, string variableName)
        {
            this.targetObject = targetObject;
            this.alias = alias;

            //get the FieldInfo of the variable
            fieldInfo = targetObject.GetType().GetField(variableName);
            //store its original value for reset operation
            originalValue = fieldInfo.GetValue(targetObject);
        }

        //returns the owner object of the variable
        public object getTargetObject()
        {
            return targetObject;
        }

        //returns the initial value of the variable
        public object getOriginalValue()
        {
            return originalValue;
        }

        //sets the variable with the given value
        public void setValue(object value)
        {
            fieldInfo.SetValue(targetObject, Convert.ChangeType(value, fieldInfo.FieldType));
        }

        //resets the variable with its initial value
        public void resetValue()
        {
            setValue(originalValue);
        }

        //returns the alias name of the variable used by the console
        public string getAlias()
        {
            return alias;
        }

        //returns the current value of the variable.
        public object getCurrentValue()
        {
            return fieldInfo.GetValue(targetObject);
        }
    }
}