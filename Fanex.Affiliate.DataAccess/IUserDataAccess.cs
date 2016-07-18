namespace Fanex.Affiliate.DataAccess
{
    using Criteria;
    using DTO;
    using Fanex.Data.Repository;
    using ViewModels;

    /// <summary>
    /// Interface User DataAccess.
    /// </summary>
    public interface IUserDataAccess : IReadonlyRepository<User>
    {
        /// <summary>
        /// Authenticate User.
        /// </summary>
        /// <param name="criteria">User Authenticate criteria.</param>
        /// <returns>User model.</returns>
        User AuthenticateUser(AffiliateLoginCriteria criteria);

        /// <summary>
        /// Update login result into DB.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        void UpdateLoginResult(AffiliateLoginResultCriteria criteria);

        /// <summary>
        /// Check Is kicked out.
        /// </summary>
        /// <param name="criteria">Re-check in criteria.</param>
        /// <returns>Kick out or not.</returns>
        bool ReCheckIn(ReCheckInCriteria criteria);

        /// <summary>
        /// Insert affiliate information.
        /// </summary>
        /// <param name="criteria">User criteria.</param>
        /// <returns>Return view.</returns>
        ResultViewModel AffiliateInsert(UserInsertCriteria criteria);

        /// <summary>
        /// Update affiliate information.
        /// </summary>
        /// <param name="criteria">User criteria.</param>
        /// <returns>Return view.</returns>
        ResultViewModel AffiliateUpdate(UserUpdateInfoCriteria criteria);

        /// <summary>
        /// Select Affiliate by affiliate ID.
        /// </summary>
        /// <param name="criteria">User criteria.</param>
        /// <returns>User View Model.</returns>
        UserViewModel AffiliateSelectById(UserSelectInfoCriteria criteria);

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
        /// <returns>User model.</returns>
        User InternalAuthenticateUser(InternalLoginCriteria criteria);
    }
}