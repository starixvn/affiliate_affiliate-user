namespace Fanex.Affiliate.Services
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
