// ----------------------------------------------------------------------
// <copyright file="StyleRelativePathTransform.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/21">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Site.App_Start
{
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web.Optimization;

    /// <summary>
    /// Style Relative Path Transform.
    /// </summary>
    internal class StyleRelativePathTransform : IBundleTransform
    {
        /// <summary>
        /// Function Process.
        /// </summary>
        /// <param name="context">Bundle Context.</param>
        /// <param name="response">Bundle Response.</param>
        public void Process(BundleContext context, BundleResponse response)
        {
            response.Content = string.Empty;

            var pattern = new Regex(@"url\s*\(\s*([""']?)([^:)]+)\1\s*\)", RegexOptions.IgnoreCase);

            //// open each of the files
            foreach (FileInfo cssFileInfo in response.Files)
            {
                if (cssFileInfo.Exists)
                {
                    // apply the RegEx to the file (to change relative paths)
                    string contents = File.ReadAllText(cssFileInfo.FullName);
                    MatchCollection matches = pattern.Matches(contents);

                    // Ignore the file if no match
                    if (matches.Count > 0)
                    {
                        string cssFilePath = cssFileInfo.DirectoryName;
                        context.HttpContext.RelativeFromAbsolutePath(cssFilePath);
                        foreach (Match match in matches)
                        {
                            // this is a path that is relative to the CSS file
                            string relativeToCSS = match.Groups[2].Value;

                            // combine the relative path to the cssAbsolute
                            string absoluteToUrl = Path.GetFullPath(Path.Combine(cssFilePath, relativeToCSS));

                            // make this server relative
                            string serverRelativeUrl = context.HttpContext.RelativeFromAbsolutePath(absoluteToUrl);

                            string quote = match.Groups[1].Value;
                            string replace = string.Format("url({0}{1}{0})", quote, serverRelativeUrl);
                            contents = contents.Replace(match.Groups[0].Value, replace);
                        }
                    }

                    // copy the result into the response.
                    response.Content = string.Format("{0}\r\n{1}", response.Content, contents);
                }
            }
        }
    }
}