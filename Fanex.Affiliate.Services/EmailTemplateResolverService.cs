namespace Fanex.Affiliate.Services
{
    using System.IO;
    using RazorEngine;
    using RazorEngine.Templating;

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
        public string GetEmailBody(string templatePath, object model)
        {
            var template = File.ReadAllText(templatePath);
            return Engine.Razor.RunCompile(template, templatePath, null, model);
        }
    }
}