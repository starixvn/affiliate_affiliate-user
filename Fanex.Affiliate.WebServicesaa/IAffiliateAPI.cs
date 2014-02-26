// ----------------------------------------------------------------------
// <copyright file="IAffiliateAPI.cs" company="Nexcel Solutions Vietnam">
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
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using System.Text;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAffiliateAPI
    {
        /// <summary>
        /// Get Affiliate email.
        /// </summary>
        /// <param name="siteName">Site name.</param>
        /// <param name="btag">Special tag that contains information.</param>
        /// <returns>Affiliate email information.</returns>
        [OperationContract]
        AffiliateEmailInfo GetAffiliateEmail(string siteName, string btag);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class AffiliateEmailInfo
    {
        public AffiliateEmailInfo()
        {
            Fullname = "Invalid affiliate ID.";
            Username = "Invalid affiliate ID.";
            Email = "Invalid affiliate ID.";
        }

        [DataMember]
        public string Fullname { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}
