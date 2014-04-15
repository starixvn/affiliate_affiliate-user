// ----------------------------------------------------------------------
// <copyright file="HotNews.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/03/26">Created</change>
//   <change who="" date=""></change>
// </history>

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
