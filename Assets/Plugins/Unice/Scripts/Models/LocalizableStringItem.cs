using System;

namespace Unice.Models
{
    /// <summary>
    /// String localized for a language.
    /// </summary>
    [Serializable]
    public class LocalizableStringItem
    {
        /// <summary>
        /// The language that the string is in.
        /// </summary>
        public LanguageSO Language;

        /// <summary>
        /// The style that text will use to display.
        /// </summary>
        public TextStyleSO TextStyle;
        
        /// <summary>
        /// The language text.
        /// </summary>
        public string Value;
    }
}