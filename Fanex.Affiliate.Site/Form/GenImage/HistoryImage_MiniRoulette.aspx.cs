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
/// RNG Casino -History Image - Mini Roulette.
/// </summary>
public partial class HistoryImage_MiniRoulette : System.Web.UI.Page
{
    /// <summary>
    /// Page load.
    /// </summary>
    /// <param name="sender">Object sender.</param>
    /// <param name="e">Event arguments.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        GenImage_MiniRoulette objImg = new GenImage_MiniRoulette();
        objImg.GenResultPic(this.Context);
    }
}
