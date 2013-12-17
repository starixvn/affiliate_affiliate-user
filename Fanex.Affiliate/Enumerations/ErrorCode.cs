// --------------------------------------------------------------------------
// <copyright file="ErrorCode.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.28">Created</change>
// </history>
namespace Fanex.Affiliate.Enumerations
{
    public enum ErrorCode
    {
        /// <summary>
        /// Wrong username or password.
        /// </summary>
        WrongUserOrPassword = 101,

        /// <summary>
        /// Your account is closed.
        /// </summary>
        AccountClosed = 102,

        /// <summary>
        /// Your account is locked temporarily due to 5 consecutive failed login attempts.
        /// </summary>
        LoginFailedFiveTimes = 103,

        /// <summary>
        /// You have not entered an email address that we recognize. Please try again or <a href="ContactUs.aspx">contact us</a>.
        /// </summary>
        EmailIsNotRecognized = 104,

        /// <summary>
        /// Account is not approved. Please contact your administrator!.
        /// </summary>
        AccountIsNotApproved = 105,

        /// <summary>
        /// Email Address has been used. Please choose another one!.
        /// </summary>
        EmailUnavailable = 121,

        /// <summary>
        /// Invalid affiliate information.
        /// </summary>
        InvalidAffiliateInfo = 122,

        /// <summary>
        /// Create customer failed. Please try again.
        /// </summary>
        CreateCustomerFail = 123
    }
}