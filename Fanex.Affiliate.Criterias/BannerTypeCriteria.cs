// -----------------------------------------------------------------------
// <copyright file="BannerTypeCriteria.cs" company="Nexcel Solutions Vietnam">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Muller.Nguyen" date="2013.06.28">Create</change>
// </history>

namespace Fanex.Affiliate.Criterias
{
    using NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BannerTypeCriteria : CriteriaBase
    {
        public BannerTypeCriteria()
        {
            Language = "en-US";
        }

        public string TypeName { get; set; }

        public string Language { get; set; }

        public int? ParentType { get; set; }

        public bool GenAll { get; set; }

        public string Category { get; set; }

        public int ActiveValue { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_BannerSelType";
        }
    }
}
