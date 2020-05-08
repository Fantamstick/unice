using System;
using UnityEngine;

namespace Unice.Attributes
{
    /// <summary>
    /// Expose methods to inspector.<para />
    /// Property/Field Attribute of a serialized class to allow it to expose its methods to inspector
    /// using the "MethodButton" attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ExposeDebugMethodsAttribute : PropertyAttribute
    {
        public ExposeDebugMethodsAttribute()
        {
        }
    }
}