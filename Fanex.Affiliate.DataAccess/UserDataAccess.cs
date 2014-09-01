// --------------------------------------------------------------------------
// <copyright file="UserDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.05">Created</change>
//   <change who="Kaden.Trinh" date="2013.07.01">Register aff, get affiliate info, update aff</change>
//   <change who="Leo.Vo" date="2013.07.03">Login from admin site</change>
// </history>

namespace Fanex.Affiliate.DataAccess
{
    using System;
    using System.Linq;
    using Common;
    using Criteria;
    using Data;
    using DTO;
    using Fanex.NAP.Framework.Command;
    using NAP.Framework;
    using NAP.Framework.Data;
    using ViewModels;

    /// <summary>
    /// User DataAccess.
    /// </summary>
    public class UserDataAccess : ReadonlyRepository<User>, IUserDataAccess
    {
        /// <summary>
        /// Authenticate User.
        /// </summary>
        /// <param name="criteria">User Authenticate criteria.</param>
        /// <returns>User model.</returns>
        public User AuthenticateUser(AffiliateLoginCriteria criteria)
        {
            var user = new User();
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var currentParams = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(currentParams);
                criteria.ErrCode = Converter.ToInt(currentParams["errcode"]);
                var affId = Converter.ToInt(currentParams["affid"]);
                if (affId > 0)
                {
                    user.AffId = Converter.ToInt(currentParams["affid"]);
                    user.Username = Converter.ToString2(currentParams["username"]);
                    user.FirstName = Converter.ToString2(currentParams["firstname"]);
                    user.LastName = Converter.ToString2(currentParams["lastname"]);
                    user.Email = Converter.ToString2(currentParams["email"]);
                    user.PreferLang = Converter.ToString2(currentParams["preferlang"]);
                    user.Currency = Converter.ToString2(currentParams["currency"]);
                    user.CurrencyID = Converter.ToInt(currentParams["currencyid"]);
                    user.MailOption = Converter.ToBool(currentParams["mailoption"]);
                    user.HasSecurityQuestion = Converter.ToBool(currentParams["hassecurityquestion"]);
                    user.IsAllowedPersonalizedPage = Converter.ToBool(currentParams["isallowedpersonalizedpage"]);
                    user.AffLevel = Converter.ToInt(currentParams["afflevel"]);
                    user.AffType = Converter.ToInt(currentParams["afftype"]);
                    user.OnUserID = Converter.ToInt(currentParams["onuserid"]);
                }
            }

            return user;
        }

        /// <summary>
        /// Check Is kicked out.
        /// </summary>
        /// <param name="criteria">Re-check in criteria.</param>
        /// <returns>Kick out or not.</returns>
        public bool ReCheckIn(ReCheckInCriteria criteria)
        {
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var currentParams = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(currentParams);
                return Converter.ToBool(currentParams["iskickedout"]);
            }
        }

        /// <summary>
        /// User Logout.
        /// </summary>
        /// <param name="criteria">Log out user criteria.</param>
        public void UserLogOut(LogOutUserCriteria criteria)
        {
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var currentParams = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(currentParams);
            }
        }

        /// <summary>
        /// Forget Password.
        /// </summary>
        /// <param name="criteria">Forget password criteria.</param>
        /// <returns>User model.</returns>
        public User ForgetPassword(ForgetPasswordCriteria criteria)
        {
            var user = new User();
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var currentParams = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(currentParams);
                var affId = Converter.ToInt(currentParams["affid"]);
                if (affId > 0)
                {
                    user.AffId = Converter.ToInt(currentParams["affid"]);
                    user.Username = Converter.ToString2(currentParams["username"]);
                    user.FirstName = Converter.ToString2(currentParams["firstname"]);
                    user.LastName = Converter.ToString2(currentParams["lastname"]);
                    user.PreferLang = Converter.ToString2(currentParams["preferredlanguage"]);
                    user.MailOption = Converter.ToBool(currentParams["mailoption"]);
                }

                criteria.ErrCode = Converter.ToInt(currentParams["errcode"]);
            }

            return user;
        }

        /// <summary>
        /// Insert affiliate information.
        /// </summary>
        /// <param name="criteria">User criteria.</param>
        /// <returns>Return view.</returns>
        public ResultViewModel AffiliateInsert(UserInsertCriteria criteria)
        {
            ResultViewModel result = null;
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var currentParams = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(currentParams);
                result = new ResultViewModel
                {
                    ErrorCode = Converter.ToInt(currentParams["errcode"]),
                    Success = Converter.ToInt(currentParams["errcode"]) == 0
                };
            }

            return result;
        }

        /// <summary>
        /// Update affiliate information.
        /// </summary>
        /// <param name="criteria">User criteria.</param>
        /// <returns>Return view.</returns>
        public ResultViewModel AffiliateUpdate(UserUpdateInfoCriteria criteria)
        {
            ResultViewModel result = null;
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var currentParams = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(currentParams);
                result = new ResultViewModel
                {
                    ErrorCode = Converter.ToInt(currentParams["errcode"]),
                    Success = Converter.ToInt(currentParams["errcode"]) == 0
                };
            }

            return result;
        }

        /// <summary>
        /// Select Affiliate by affiliate ID.
        /// </summary>
        /// <param name="criteria">User criteria.</param>
        /// <returns>User View Model.</returns>
        public UserViewModel AffiliateSelectById(UserSelectInfoCriteria criteria)
        {
            UserViewModel result = null;
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                result = objectDb.QueryEntity<UserViewModel>(param);
            }

            return result;
        }

        /// <summary>
        /// Change Password.
        /// </summary>
        /// <param name="criteria">Reset password criteria.</param>
        /// <returns>User model.</returns>
        public User ChangePassword(ResetPasswordCriteria criteria)
        {
            var user = new User();
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var currentParams = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(currentParams);
                var email = Converter.ToString2(currentParams["email"]);
                if (!string.IsNullOrEmpty(email))
                {
                    user.Email = Converter.ToString2(currentParams["email"]);
                    user.Username = Converter.ToString2(currentParams["username"]);
                    user.FirstName = Converter.ToString2(currentParams["firstname"]);
                    user.LastName = Converter.ToString2(currentParams["lastname"]);
                    user.PreferLang = Converter.ToString2(currentParams["preferredlanguage"]);
                    user.MailOption = Converter.ToBool(currentParams["mailoption"]);
                }

                criteria.ErrCode = Converter.ToInt(currentParams["errcode"]);
            }

            return user;
        }

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="entity">User entity.</param>
        /// <returns>Command result.</returns>
        public ICommandResult Delete(User entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Insert entity.
        /// </summary>
        /// <param name="entity">User entity.</param>
        /// <returns>Command result.</returns>
        public ICommandResult Insert(User entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entity">User entity.</param>
        /// <returns>Command result.</returns>
        public ICommandResult Update(User entity)
        {
            throw new NotImplementedException();
        }

        #region Admin Process Login

        /// <summary>
        /// Get session of admin.
        /// </summary>
        /// <param name="criteria">Admin log in criteria.</param>
        /// <returns>Admin Session ID.</returns>
        public string GetAdminNetSessionId(AdminLoginCriteria criteria)
        {
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(param);
                var output = objectDb.GetOuputParams();

                return Convert.ToString(output["netsessionid"]);
            }
        }

        /// <summary>
        /// Login from admin site.
        /// </summary>
        /// <param name="criteria">Admin log in criteria.</param>
        /// <returns>User model.</returns>
        public User InternalAuthenticateUser(InternalLoginCriteria criteria)
        {
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                var internalLoginInfo = objectDb.Query<InternalLoginInfo>(param).FirstOrDefault();
                criteria.ErrorCode = Converter.ToInt(param["errcode"]);

                return internalLoginInfo == null
                    ? null
                    : new User
                            {
                                OnUserID = criteria.AccId,
                                AffId = internalLoginInfo.AffID,
                                Username = criteria.UserName.ToUpper() + " : [" + internalLoginInfo.AdminUserName + "]",
                                FirstName = internalLoginInfo.FirstName,
                                LastName = internalLoginInfo.LastName,
                                Email = internalLoginInfo.Email,
                                PreferLang = internalLoginInfo.PreferLang,
                                Currency = internalLoginInfo.Currency,
                                CurrencyID = internalLoginInfo.CurrencyID,
                                AffType = internalLoginInfo.AffType,
                                AffLevel = internalLoginInfo.AffLevel,
                                HasSecurityQuestion = true
                            };
            }
        }

        #endregion Admin Process Login
    }
}