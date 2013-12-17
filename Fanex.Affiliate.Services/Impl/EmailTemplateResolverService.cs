// ----------------------------------------------------------------------
// <copyright file="EmailTemplateResolverService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (coffee) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/06/25">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Services.Impl
{
    using System.IO;
    using Interface;
    using RazorEngine;

    /// <summary>
    /// Loading email template body service.
    /// </summary>
    public class EmailTemplateResolverService : IEmailTemplateResolverService
    {
        /// <summary>
        /// Get the whole content of a email template file.
        /// </summary>
        /// <param name="templatePath">Path to the template file.</param>
        /// <param name="model">Data for mapping into email template.</param>
        /// <returns>Email body.</returns>
        public string GetEmailBody(string templatePath, dynamic model)
        {
            try
            {
                var template = File.ReadAllText(templatePath);
                return Razor.Parse(template, model);
            }
            catch
            {
                throw;
            }
        }
    }
}
