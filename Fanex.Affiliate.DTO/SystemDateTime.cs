// ----------------------------------------------------------------------
// <copyright file="SystemDateTime.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/20">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.DTO
{
    using System;

    /// <summary>
    /// System Date Time.
    /// </summary>
    public class SystemDateTime
    {
        public DateTime DatabaseDateTime { get; set; }

        public string GMTOffSet { get; set; }
    }
}
