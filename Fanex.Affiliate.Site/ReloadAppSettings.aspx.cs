// ----------------------------------------------------------------------
// <copyright file="ReloadAppSettings.aspx.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/29">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Site
{
    using System;
    using System.Configuration;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Reload App Settings.
    /// </summary>
    public partial class ReloadAppSettings : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}