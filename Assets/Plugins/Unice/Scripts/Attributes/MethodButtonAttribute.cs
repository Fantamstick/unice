using UnityEngine;

namespace Unice.Attributes
{
    /// <summary>
    /// Attribute a method so it can be invoked via a button from within the inspector.<para />
    /// Requires the class to be Serializeable and for its instance to use the "ExposeDebugMethods" attribute
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class MethodButtonAttribute : PropertyAttribute
    {
        /// <summary>
        /// Name of the button to display in the inspector
        /// </summary>
        public readonly string ButtonName;

        public MethodButtonAttribute(string buttonName)
        {
            ButtonName = buttonName;
        }
    }
}