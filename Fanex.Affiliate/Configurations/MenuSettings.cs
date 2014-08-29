// ----------------------------------------------------------------------
// <copyright file="MenuSettings.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/10/29">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Configurations
{
    using System.Collections.Generic;
    using Fanex.Affiliate.DTO;
    
    /// <summary>
    /// Menu Settings.
    /// </summary>
    public static class MenuSettings
    {
        private static List<MenuItem> menusList = new List<MenuItem>
        {
            new MenuItem() 
            { 
                MenuName = "AccountOverview", ParentName = string.Empty, ActionName = "Overview", ControllerName = "Account", Roles = "Normal" 
            },
            new MenuItem() 
            { 
                MenuName = "Statistics", ParentName = string.Empty,  ActionName = "Overview", ControllerName = "Account", Roles = "Kiosk" 
            },

            new MenuItem() 
            {
                MenuName = "CustomerInfo", ParentName = string.Empty,  ActionName = "CustomerInfoKiosk", ControllerName = "Account", Roles = "Kiosk" 
            },
            new MenuItem() 
            { 
                MenuName = "StatementHistory", ParentName = string.Empty,  ActionName = "StatementHistory", ControllerName = "Account", Roles = "Normal" 
            },

            new MenuItem() 
            { 
                MenuName = "DailyEarnings", ParentName = string.Empty,  ActionName = "DailyEarnings", ControllerName = "Account", Roles = "All" 
            },
            new MenuItem() 
            { 
                MenuName = "AccountInformation", ParentName = string.Empty,  ActionName = "MyAccount", ControllerName = "Account", Roles = "All" 
            },

            new MenuItem() 
            { 
                MenuName = "PaymentRequest", ParentName = string.Empty,  ActionName = "PaymentRequest", ControllerName = "Account", Roles = "Normal" 
            },

            new MenuItem() 
            { 
                MenuName = "MarketingTools", ParentName = string.Empty,  ActionName = string.Empty, ControllerName = string.Empty, Roles = "All" 
            },
            new MenuItem() 
            { 
                MenuName = "MediaGallery", ParentName = "MarketingTools",  ActionName = "MediaGallery", ControllerName = "Account", Roles = "All" 
            },
            new MenuItem() 
            { 
                MenuName = "TextBox", ParentName = "MarketingTools",  ActionName = "TextBox", ControllerName = "Account", Roles = "All" 
            },

            new MenuItem() 
            { 
                MenuName = "TrafficStatistics", ParentName = string.Empty, ActionName = string.Empty, ControllerName = string.Empty, Roles = "Normal" 
            },
            new MenuItem() 
            { 
                MenuName = "Referrals", ParentName = "TrafficStatistics",  ActionName = "ReferrersStatistics", ControllerName = "Account", Roles = "Normal" 
            },
            new MenuItem() 
            { 
                MenuName = "LandingPages", ParentName = "TrafficStatistics",  ActionName = "LandingPages", ControllerName = "Account", Roles = "Normal"
            }
        };

        public static ICollection<MenuItem> Menus()
        {
            return menusList;
        }
    }
}
