using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using HandHistoryLibrary.DataHandler.GenImage;
using JSoft.Utility;

/// <summary>
/// RNG Casino -History Image - Tattoo Art.
/// </summary>
public partial class HistoryImage_TattooArt : System.Web.UI.Page
{
    /// <summary>
    /// Page load.
    /// </summary>
    /// <param name="sender">Object sender.</param>
    /// <param name="e">Event arguments.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        GenImage_TattooArt objImg = new GenImage_TattooArt();
        objImg.GenResultPic(this.Context);
    }
}
