// --------------------------------------------------------------------------
// <copyright file="MessageCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.29">Created</change>
// </history>

namespace Fanex.Affiliate.Criterias
{
    using System;
    using NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MessageCriteria : CriteriaBase
    {
        public MessageCriteria()
        {
            SearchOption = null;
            FromDate = null;
            ToDate = null;
        }

        [SpParam(SqlParamName = "Important")]
        public byte? SearchOption { get; set; } 

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Language { get; set; }
        
        [SpParam(SqlParamName = "PageIndex")]
        public int Page 
        {
            get
            {
                return _page;
            }

            set 
            {
                _page = value <= 0 ? 1 : value;
            }
        }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int NRows { get; set; }
        
        [SpParam(Ignored = true)]
        private int _page { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_HotNewsSel_New";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}