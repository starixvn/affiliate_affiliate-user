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
using JSoft.Utility;
using LiveCasinoLibrary.SBAgent;

/// <summary>
/// Live Generate History Images.
/// </summary>
public partial class LiveGenHistoryImg_GenImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strGameType = StringUtil.CStr(Request["GameType"]); // 遊戲分類

        var objHistoryImg = new GenHistoryImage();

        switch (strGameType)
        {
            case "B0001": // Bingo
                objHistoryImg.GenResultBingo(this.Context);
                break;
            default:
                break;
        }
    }
}
