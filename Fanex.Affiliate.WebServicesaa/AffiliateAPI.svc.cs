// ----------------------------------------------------------------------
// <copyright file="AffiliateAPI.svc.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/02/25">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using System.Text;
    using System.Xml.Serialization;
    using Fanex.Data.Configuration;
    using Services;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AffiliateAPI" in code, svc and config file together.
    public class AffiliateAPI : IAffiliateAPI
    {
        protected readonly IAffiliateService AffiliateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AffiliateAPI" /> class.
        /// </summary>
        public AffiliateAPI()
        {
            DbSettingProviderManager.Current.Start();
            AffiliateService = new AffiliateService();
        }

        /// <summary>
        /// Get Affiliate email.
        /// </summary>
        /// <param name="siteName">Site name.</param>
        /// <param name="btag">Special tag that contains information.</param>
        /// <returns>Affiliate email information.</returns>
        public AffiliateEmailInfo GetAffiliateEmail(string siteName, string btag)
        {
            var affID = int.Parse(btag.Trim().Split('_')[1]);
            var criteria = new AffiliateInfoCriteria
            {
                AffID = affID
            };

            return AffiliateService.GetAffiliateInfo(criteria, siteName.Trim());
        }
    }
}
