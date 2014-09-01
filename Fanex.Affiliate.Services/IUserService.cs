// --------------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.05">Created</change>
//   <change who="Leo.Vo" date="2013.07.04">Internal login from admin site</change>
// </history>

namespace Fanex.Affiliate.Services
{
    using Criteria;
    using DTO;
    using ViewModels;

    /// <summary>
    /// Interface User Service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Validates a username and password.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object user.</returns>
        User AuthenticateUser(AffiliateLoginCriteria criteria);

        /// <summary>
        /// Validates a username and password.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return boolean Is kicked out.</returns>
        bool ReCheckIn(ReCheckInCriteria criteria);

        /// <summary>
        /// User Logout.
        /// </summary>
        /// <param name="criteria">Log out user criteria.</param>
        void UserLogOut(LogOutUserCriteria criteria);

        /// <summary>
        /// Insert affiliate.
        /// </summary>
        /// <param name="criteria">User criteria.</param>
        /// <returns>Return view.</returns>
        ResultViewModel AffiliateInsert(UserInsertCriteria criteria);

        /// <summary>
        /// Update affiliate.
        /// </summary>
        /// <param name="criteria">User criteria.</param>
        /// <returns>Return view.</returns>
        ResultViewModel AffiliateUpdate(UserUpdateInfoCriteria criteria);

        /// <summary>
        /// Select Affiliate by Affiliate ID.
        /// </summary>
        /// <param name="criteria">User criteria.</param>
        /// <returns>User view model.</returns>
        UserViewModel AffiliateSelectByID(UserSelectInfoCriteria criteria);

        /// <summary>
        /// Forget password.
        /// </summary>
        /// <param name="criteria">Forget password criteria.</param>
        /// <returns>User model.</returns>
        User ForgetPassword(ForgetPasswordCriteria criteria);

        /// <summary>
        /// Change password when selecting Forget Password function.
        /// </summary>
        /// <param name="criteria">Reset password password.</param>
        /// <returns>User model.</returns>
        User ChangePassword(ResetPasswordCriteria criteria);

        /// <summary>
        /// Gets the admin net session ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Admin session ID.</returns>
        string GetAdminNetSessionID(AdminLoginCriteria criteria);

        /// <summary>
        /// Get user info when login from admin site.
        /// </summary>
        /// <param name="criteria">Internal login criteria.</param>
        /// <returns>User model.</returns>
        User InternalAuthenticateUser(InternalLoginCriteria criteria);
    }
}