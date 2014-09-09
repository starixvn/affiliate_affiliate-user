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
/// RNG Casino -History Image - Scratch.
/// </summary>
public partial class HistoryImage_Scratch : System.Web.UI.Page
{
    /// <summary>
    /// Page load.
    /// </summary>
    /// <param name="sender">Object sender.</param>
    /// <param name="e">Event arguments.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        GenImage_Scratch objImg = new GenImage_Scratch();
        objImg.GenResultPic(this.Context);
    }
}
