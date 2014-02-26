// ----------------------------------------------------------------------
// <copyright file="TestAffServices.aspx.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/02/25">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Site
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Test Affiliate Services.
    /// </summary>
    public partial class TestAffServices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void GetAffiliateEmailInfo(object sender, EventArgs e)
        {
            AffiliateServicesConsumer.AffiliateAPIClient affServices = new AffiliateServicesConsumer.AffiliateAPIClient();
            
            AffiliateServicesConsumer.AffiliateEmailInfo affInfo = affServices.GetAffiliateEmail(siteName.Value, bTag.Value);
            returnedAffID.InnerText = bTag.Value.Split('_')[1];
            returnedFullName.InnerText = affInfo.Fullname;
            returnedUsername.InnerText = affInfo.Username;
            returnedEmail.InnerText = affInfo.Email;
        }
    }
}