using System.Collections.Generic;
using UnityEngine;

namespace Unice.Models
{
    /// <summary>
    /// Localizable string data object.
    /// </summary>
    [CreateAssetMenu(fileName = "new TranslatableStringSO", menuName = "Unice/Models/TranslatableStringSO")]
    public class LocalizableStringSO : ScriptableObject
    {
        /// <summary>
        /// Available translations for string.
        /// </summary>
        public List<LocalizableStringItem> Localizations = new List<LocalizableStringItem>();
        
        /// <summary>
        /// Get localizable string for specific language. 
        /// </summary>
        /// <param name="key">Language of string.</param>
        /// <exception cref="NoLocalizationException">No localization exists.</exception>
        public LocalizableStringItem this[LanguageSO key]
        {
            get
            {
                if (Localizations == null || Localizations.Count == 0) throw new NoLocalizationException();
                
                for (int i = 0; i < Localizations.Count; i++)
                {
                    if (key == Localizations[i].Language) return Localizations[i];
                }

                var defaultItem = Localizations[0];
                Debug.LogWarning($"Localized item for {key.Code} not found. Using default {defaultItem.Language.Code}");
                
                return defaultItem;
            }
        }
    }
}
