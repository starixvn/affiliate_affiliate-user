// --------------------------------------------------------------------------
// <copyright file="MessageCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.29">Created</change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Message Criteria.
    /// </summary>
    public class MessageCriteria : CriteriaBase
    {
        public MessageCriteria()
        {
            SearchOption = null;
            FromDate = null;
            ToDate = null;
        }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        [SpParam(SqlParamName = "Important")]
        public byte? SearchOption { get; set; }

        public string Language { get; set; }

        public int PageSize { get; set; }
        
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

        [SpParam(Ignored = true)]
        public int NRows { get; set; }
        
        [SpParam(Ignored = true)]
        public int _page { get; set; }

        // Output parameters
        public int TotalPages { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_HotNewsSel_New";
        }
    }
}