using System.Collections.Generic;
using Unice.Models;
using UnityEngine;

namespace Unice.Services.Localization
{
    /// <summary>
    /// Localization service.
    /// </summary>
    [CreateAssetMenu(fileName = "new localization service", menuName = "Unice/Services/Localization")]
    public class LocalizationService : ScriptableObject
    {
        /// <summary>
        /// Current language being used.
        /// </summary>
        public LanguageSO CurrentLanguage;
        /// <summary>
        /// Available languages that are supported.
        /// </summary>
        public List<LanguageSO> AvailableLanguages;
    }
}
