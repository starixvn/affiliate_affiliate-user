// --------------------------------------------------------------------------
// <copyright file="AdminLoginCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.07.03">Created</change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using System.ComponentModel.DataAnnotations;
    using NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AdminLoginCriteria : CriteriaBase
    {
        public AdminLoginCriteria()
        {
            NetSessionId = string.Empty;
        }

        [Required]
        [SpParam(Ignored = true)]
        public string ChalengeStr { get; set; }

        [Required]
        [SpParam(Ignored = true)]
        public string AccKey { get; set; }

        [Required]
        [SpParam(Ignored = true)]
        public string CustName { get; set; }

        [Required]
        [SpParam(Ignored = true)]
        public int AccId { get; set; }

        [Required]
        [SpParam(SqlParamName = "on_userid")]
        public int AccOnId { get; set; }

        [SpParam(SqlParamName = "netsessionid")]
        public string NetSessionId { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_OnUserSelNetSessionID";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}