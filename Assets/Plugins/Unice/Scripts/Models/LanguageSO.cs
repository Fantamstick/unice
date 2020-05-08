using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Unice.Models
{
    /// <summary>
    /// Language data object.
    /// </summary>
    [CreateAssetMenu(fileName = "new Language", menuName = "Unice/Models/Language")]
    public class LanguageSO : ScriptableObject
    {
        /// <summary>
        /// 2-letter ISO 639-1 Language Code.
        /// </summary>
        [Tooltip("2-letter ISO 639-1 Language Code.")]
        public string Code;
        
        /// <summary>
        /// Displayable name of language.
        /// </summary>
        [Tooltip("Displayable name of language.")]
        public string Name;
        
        
        #if UNITY_EDITOR
        
        /// <summary>
        /// Search tag from within Project.
        /// </summary>
        public const string Tag = "t:LanguageSO";
        
        /// <summary>
        /// Find all LanguageSO assets in project.
        /// </summary>
        /// <returns>Dictionary of all assets with the 2-character language code as its key.</returns>
        public static Dictionary<string, LanguageSO> FindAllInProject()
        {
            var langDict = new Dictionary<string, LanguageSO>();
            
            // find all assets in project.
            string[] langAssets = AssetDatabase.FindAssets(Tag);
            
            foreach (string langAsset in langAssets)
            {
                // get language asset.
                string path = AssetDatabase.GUIDToAssetPath(langAsset);
                var lang = AssetDatabase.LoadAssetAtPath<LanguageSO>(path);
                
                langDict.Add(lang.Code, lang);
            }

            return langDict;
        }
        
        #endif
    }
}
