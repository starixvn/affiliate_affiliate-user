// ----------------------------------------------------------------------
// <copyright file="AbsolutePathExtension.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/21">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Site.App_Start
{
    using System.Web;

    /// <summary>
    /// My Extensions.
    /// </summary>
    internal static class AbsolutePathExtension
    {
        /// <summary>
        /// Get absolute path.
        /// </summary>
        /// <param name="context">Http context.</param>
        /// <param name="path">Path value.</param>
        /// <returns>Return path.</returns>
        public static string RelativeFromAbsolutePath(this HttpContextBase context, string path)
        {
            var request = context.Request;
            var applicationPath = request.PhysicalApplicationPath;
            var virtualDir = request.ApplicationPath;
            virtualDir = virtualDir == "/" ? virtualDir : (virtualDir + "/");
            return path.Replace(applicationPath, virtualDir).Replace(@"\", "/");
        }
    }
}