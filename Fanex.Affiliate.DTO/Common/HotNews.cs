namespace Fanex.Affiliate.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Hot News class.
    /// </summary>
    [Serializable]
    public class HotNews
    {
        public HotNews()
        {
            Message = new Dictionary<string, string>();
        }

        public bool Important { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed. Suppression is OK here")]
        public Dictionary<string, string> Message { get; set; }

        public string GetMessageByLanguage(string lang)
        {
            var language = string.IsNullOrEmpty(lang) ? "en-US" : lang;
            return Message.ContainsKey(lang) ? Message[language] : string.Empty;
        }
    }
}
