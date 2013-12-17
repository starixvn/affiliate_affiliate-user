// -----------------------------------------------------------------------
// <copyright file="HotNews.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DTO
{
    using System;
    using System.Collections.Generic;

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

        public Dictionary<string, string> Message { get; set; }

        public string GetMessageByLanguage(string lang = "en-US")
        {
            return Message.ContainsKey(lang) ? Message[lang] : string.Empty;
        }
    }
}
