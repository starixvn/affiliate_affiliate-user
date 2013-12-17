// ----------------------------------------------------------------------
// <copyright file="MenuItem.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/10/29">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.DTO
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MenuItem
    {
        public string MenuName { get; set; }

        public string ParentName { get; set; }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public string Roles { get; set; }
    }
}
