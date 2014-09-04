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
/// RNG Casino -History Image - HOLDEM.
/// </summary>
public partial class HistoryImage_Holdem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GenImage_Holdem objImg = new GenImage_Holdem();
        objImg.GenResultPic(this.Context);
    }
}
