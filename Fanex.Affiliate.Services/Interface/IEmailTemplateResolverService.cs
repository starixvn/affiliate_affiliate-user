// ----------------------------------------------------------------------
// <copyright file="IEmailTemplateResolverService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (coffee) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/06/25">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Services.Interface
{
    /// <summary>
    /// Email Template Resolver Service.
    /// </summary>
    public interface IEmailTemplateResolverService
    {
        /// <summary>
        /// Get the whole content of a email template file.
        /// </summary>
        /// <param name="templatePath">Path to the template file.</param>
        /// <param name="model">Data for mapping into email template.</param>
        /// <returns>Email body.</returns>
        string GetEmailBody(string templatePath, dynamic model);
    }
}
