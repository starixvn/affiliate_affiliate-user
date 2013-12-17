// ----------------------------------------------------------------------
// <copyright file="IUserDataAccess.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/20">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.DataAccess.Interface
{
    using Criterias;
    using DTO;
    using NAP.Framework.Data;
    using ViewModels;

    /// <summary>
    /// Interface User DataAccess.
    /// </summary>
    public interface IUserDataAccess : IRepository<User>
    {
        /// <summary>
        /// Authenticate User.
        /// </summary>
        /// <param name="criteria">User Authenticate criteria.</param>
        /// <param name="errCode">Reference error code.</param>
        /// <returns>User model.</returns>
        User AuthenticateUser(AuthenticateUserCriteria criteria, ref int errCode);

        /// <summary>
        /// Check Is kicked out.
        /// </summary>
        /// <param name="criteria">Re-check in criteria.</param>
        /// <returns>Kick out or not.</returns>
        bool ReCheckIn(ReCheckInCriteria criteria);

        /// <summary>
        /// Update affiliate information.
        /// </summary>
        /// <param name="criteria">User criteria.</param>
        /// <returns>Return view.</returns>
        ResultViewModel AffiliateUpdate(UserCriteria criteria);

        /// <summary>
        /// Select Affiliate by affiliate ID.
        /// </summary>
        /// <param name="criteria">User criteria.</param>
        /// <returns>User View Model.</returns>
        UserViewModel AffiliateSelectById(UserCriteria criteria);

        /// <summary>
        /// User Logout.
        /// </summary>
        /// <param name="criteria">Log out user criteria.</param>
        void UserLogOut(LogOutUserCriteria criteria);

        /// <summary>
        /// Forget Password.
        /// </summary>
        /// <param name="criteria">Forget password criteria.</param>
        /// <returns>User model.</returns>
        User ForgetPassword(ForgetPasswordCriteria criteria);

        /// <summary>
        /// Change Password.
        /// </summary>
        /// <param name="criteria">Reset password criteria.</param>
        /// <returns>User model.</returns>
        User ChangePassword(ResetPasswordCriteria criteria);

        /// <summary>
        /// Get session of admin.
        /// </summary>
        /// <param name="criteria">Admin log in criteria.</param>
        /// <returns>Admin Session ID.</returns>
        string GetAdminNetSessionId(AdminLoginCriteria criteria);

        /// <summary>
        /// Login from admin site.
        /// </summary>
        /// <param name="criteria">Admin log in criteria.</param>
        /// <param name="errCode">Reference error code.</param>
        /// <returns>User model.</returns>
        User InternalAuthenticateUser(InternalLoginCriteria criteria, ref int errCode);
    }
}