using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Unice.Models
{
    /// <summary>
    /// Text style used for displaying text.
    /// </summary>
    [CreateAssetMenu(fileName = "new TextStyle", menuName = "Unice/Models/TextStyle")]
    public class TextStyleSO : ScriptableObject
    {
        public TMP_FontAsset FontAsset;

        public FloatSO FontSize;

        public ColorSO Color;

        public Material Material;
        
        
        #if UNITY_EDITOR
        
        /// <summary>
        /// Search tag from within Project.
        /// </summary>
        public const string Tag = "t:TextStyleSO";
        
        /// <summary>
        /// Find all TextStyleSO assets in project.
        /// </summary>
        /// <returns>Dictionary of all assets with the filename as its key.</returns>
        public static Dictionary<string, TextStyleSO> FindAllInProject()
        {
            var textStyleDict = new Dictionary<string, TextStyleSO>();
            
            // find all assets in project.
            string[] textStyleGuids = AssetDatabase.FindAssets(Tag);
            
            foreach (string textStyleGuid in textStyleGuids)
            {
                // get filename without extension.
                string path = AssetDatabase.GUIDToAssetPath(textStyleGuid);
                string filename = Path.GetFileNameWithoutExtension(path);
                
                textStyleDict.Add(filename, AssetDatabase.LoadAssetAtPath<TextStyleSO>(path));
            }

            return textStyleDict;
        }
        
        #endif
    }
}
