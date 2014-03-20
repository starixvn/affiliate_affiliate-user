// --------------------------------------------------------------------------
// <copyright file="UserService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.05">Created</change>
//   <change who="Leo.Vo" date="2013.07.04">Internal login from admin site</change>
// </history>

namespace Fanex.Affiliate.Services.Impl
{
    using Common;
    using Common.Helper;
    using Criteria;
    using DataAccess.Impl;
    using DataAccess.Interface;
    using DTO;
    using Interface;
    using ViewModels;

    /// <summary>
    /// User Service.
    /// </summary>
    public class UserService : IUserService
    {
        #region Fields

        /// <summary>
        /// Define user DataAccess.
        /// </summary>
        private readonly IUserDataAccess _userDataAccess;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService" /> class.
        /// </summary>
        public UserService()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService" /> class.
        /// </summary>
        /// <param name="userDataAccess">User DataAccess.</param>
        private UserService(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess ?? new UserDataAccess();
        }

        #endregion Constructor

        /// <summary>
        /// Validates a username and password.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <param name="errCode">Error code.</param>
        /// <returns>Return object user.</returns>
        public User AuthenticateUser(AffiliateLoginCriteria criteria, ref int errCode)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            criteria.Password = Cryptography.CfsEnCode(criteria.Password);
            return _userDataAccess.AuthenticateUser(criteria, ref errCode);
        }

        /// <summary>
        /// Validates a username and password.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return boolean Is kicked out.</returns>
        public bool ReCheckIn(ReCheckInCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _userDataAccess.ReCheckIn(criteria);
        }

        /// <summary>
        /// User Logout.
        /// </summary>
        /// <param name="criteria">Log out user criteria.</param>
        public void UserLogOut(LogOutUserCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            _userDataAccess.UserLogOut(criteria);
        }

        /// <summary>
        /// Update affiliate.
        /// </summary>
        /// <param name="criteria">User criteria.</param>
        /// <returns>Return view.</returns>
        public ResultViewModel AffiliateUpdate(UserCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _userDataAccess.AffiliateUpdate(criteria);
        }

        /// <summary>
        /// Select Affiliate by Affiliate ID.
        /// </summary>
        /// <param name="criteria">User criteria.</param>
        /// <returns>User view model.</returns>
        public UserViewModel AffiliateSelectByID(UserCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _userDataAccess.AffiliateSelectById(criteria);
        }

        /// <summary>
        /// Forget password.
        /// </summary>
        /// <param name="criteria">Forget password criteria.</param>
        /// <returns>User model.</returns>
        public User ForgetPassword(ForgetPasswordCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _userDataAccess.ForgetPassword(criteria);
        }

        /// <summary>
        /// Change password when selecting Forget Password function.
        /// </summary>
        /// <param name="criteria">Reset password password.</param>
        /// <returns>User model.</returns>
        public User ChangePassword(ResetPasswordCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            criteria.NewPwd = Cryptography.CfsEnCode(criteria.NewPwd);
            return _userDataAccess.ChangePassword(criteria);
        }

        #region Admin Process Login

        /// <summary>
        /// Gets the admin net session ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Admin session ID.</returns>
        public string GetAdminNetSessionID(AdminLoginCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _userDataAccess.GetAdminNetSessionId(criteria);
        }

        /// <summary>
        /// Get user info when login from admin site.
        /// </summary>
        /// <param name="criteria">Internal login criteria.</param>
        /// <param name="errCode">Error code.</param>
        /// <returns>User model.</returns>
        public User InternalAuthenticateUser(InternalLoginCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _userDataAccess.InternalAuthenticateUser(criteria);
        }

        #endregion Admin Process Login
    }
}