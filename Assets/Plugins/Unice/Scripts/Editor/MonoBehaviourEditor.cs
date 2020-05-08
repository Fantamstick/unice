using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unice.Attributes;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unice.Editor
{
    /// <summary>
    /// Base custom inspector for all MonoBehaviour components.<para />
    /// Checks if component implements an "ExposeDebugMethods" attribute, and if so,
    /// displays testable buttons within the inspector.
    /// </summary>
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class MonoBehaviourEditor : UnityEditor.Editor
    {
        List<ExposedMethod> methodList = new List<ExposedMethod>();
        object methodCaller;
        
        public override void OnInspectorGUI()
        {
            // Draw the default inspector as usual.
            DrawDefaultInspector();
            
            if (!Application.isPlaying)
                return;

            // find exposed methods if none have been searched yet.
            if (methodList.Count == 0)
                methodList = FindCallableMethods(target);
            
            // draw exposed methods as buttons in inspector.
            DrawCustomInspector(target);
        }

       public void DrawCustomInspector(Object obj)
        {
            // draw from the last line all exposed methods as buttons.
            var rect = GUILayoutUtility.GetLastRect();

            // add extra space below last position to fit buttons.
            var startPosition = new Rect(rect.x, rect.y + 26, rect.width, rect.height);
            
            float btnHeight = rect.height;
            
            GUILayout.Space( 26 + methodList.Count * 16);
            
            for (int i = 0; i < methodList.Count; i++)
            {
                var btnCaller = methodList[i];
                Rect buttonRect = new Rect(startPosition.x, startPosition.y + (btnHeight + 6) * i, startPosition.width, btnHeight);
                if (GUI.Button(buttonRect, btnCaller.Attrib.ButtonName))
                {
                    btnCaller.MethodInfo.Invoke(methodCaller, null);
                }
            }
        }

        List<ExposedMethod> FindCallableMethods(Object obj)
        {
            var mehods = new List<ExposedMethod>();
            mehods = FindExposedFieldMethods(obj);
            mehods.AddRange(FindExposedPropertyMethods(obj));

            return mehods;
        }

        List<ExposedMethod> FindExposedFieldMethods(Object obj)
        {
            var methods = new List<ExposedMethod>();
            
            FieldInfo[] fields = obj.GetType().GetFields();
            foreach (var field in fields)
            {
                // find field with ExposeDebugsMethod attribute
                if (GetAttributeOfType(typeof(ExposeDebugMethodsAttribute), field.CustomAttributes) == null)
                    continue;
                
                // Save caller method so we can call method through its instance
                methodCaller = field.GetValue(obj);
                if (methodCaller == null) 
                    continue;
                
                methods.AddRange(FindExposedMethods(field));
            }

            return methods;
        }
        
        List<ExposedMethod> FindExposedPropertyMethods(Object obj)
        {
            var methods = new List<ExposedMethod>();
            
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                // find field with ExposeDebugsMethod attribute
                if (GetAttributeOfType(typeof(ExposeDebugMethodsAttribute), property.CustomAttributes) == null)
                    continue;
                
                // Save caller method so we can call method through its instance
                methodCaller = property.GetValue(obj);
                if (methodCaller == null) 
                    continue;
                
                methods.AddRange(FindExposedMethods(property));
            }

            return methods;
        }
        
        List<ExposedMethod> FindExposedMethods(MemberInfo obj)
        {
            var methods = new List<ExposedMethod>();

            // get all methods in field instance
            var callerMethods = methodCaller.GetType().GetMethods();
            foreach (var callerMethod in callerMethods)
            {
                // find method with attribute with button attribute
                var attribType = callerMethod.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(MethodButtonAttribute));
                if (attribType == null) continue;

                // create button info if attribute exists
                var callerAttrib = (MethodButtonAttribute) callerMethod.GetCustomAttribute(typeof(MethodButtonAttribute));
                methods.Add(new ExposedMethod {Attrib = callerAttrib, MethodInfo = callerMethod});
            }

            return methods;
        }
        
        CustomAttributeData GetAttributeOfType(Type attribType, IEnumerable<CustomAttributeData> attribs)
        {
            return attribs.FirstOrDefault(a => a.AttributeType == attribType);
        }
        
        /// <summary>
        /// Information of exposed method.
        /// </summary>
        class ExposedMethod
        {
            public MethodButtonAttribute Attrib;
            public MethodInfo MethodInfo;
        }
    }
}
