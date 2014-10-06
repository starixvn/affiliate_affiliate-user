// ----------------------------------------------------------------------
// <copyright file="AffiliateAPI.asmx.cs" company="Nexcel Solutions Vietnam">
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
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Services;
    using System.Xml;
    using System.Xml.Serialization;
    using Criteria;
    using Fanex.Data.Configuration;
    using Services;

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    /// <summary>
    /// Affiliate's services.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class AffiliateAPI : System.Web.Services.WebService
    {
        private readonly IAffiliateService _affiliateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AffiliateAPI" /> class.
        /// </summary>
        public AffiliateAPI()
        {
            DbSettingProviderManager.Current.Start();
            _affiliateService = new AffiliateService();
        }

        /// <summary>
        /// Convert an object into XML format.
        /// </summary>
        /// <param name="inputObject">Input object.</param>
        /// <returns>XML String.</returns>
        public static string ToXml(object inputObject)
        {
            XmlSerializer serializer = new XmlSerializer(inputObject.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, inputObject);
                return textWriter.ToString();
            }
        }

        /// <summary>
        /// Get Affiliate Email.
        /// </summary>
        /// <param name="siteName">Site Name.</param>
        /// <param name="btag">Identify B tag.</param>
        /// <returns>Affiliate email.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Reviewed. Suppression is OK here.")]
        [WebMethod]
        public string GetAffiliateEmail(string siteName, string btag)
        {
            try
            {
                var affId = int.Parse(btag.Trim().Split('_')[1]);
                var criteria = new AffiliateInfoCriteria { AffId = affId };
                return ToXml(_affiliateService.GetAffiliateInfo(criteria, siteName.Trim().ToLower()));
            }
            catch
            {
                return ToXml(new AffiliateInfo(100, "Invalid Site Name."));
            }
        }
    }
}