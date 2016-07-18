namespace Fanex.Affiliate.DataAccess
{
    using System;
    using System.Linq;
    using Common;
    using Common.Extensions;
    using Criteria;
    using Data;
    using DTO;
    using Fanex.Data.Repository;
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
                var param = criteria.GetSpParams();
                var reader = objectDb.ExecuteReader(param);

                while (reader.Read())
                {
                    user.AffId = reader.ToInt("affid");
                    user.Password = reader["password"].ToString();
                    user.FirstName = reader["firstname"].ToString();
                    user.LastName = reader["lastname"].ToString();
                    user.Email = reader["email"].ToString();
                    user.MailOption = reader.ToBool("mailoption");
                    user.PreferLang = reader["preferlang"].ToString();
                    user.HasSecurityQuestion = reader.ToBool("hassecurityquestion");
                    user.IsAllowedPersonalizedPage = reader.ToBool("isallowedpersonalizedpage");
                    user.AffLevel = reader.ToInt("afflevel");
                    user.AffType = reader.ToInt("afftype");
                    user.Currency = reader["currency"].ToString();
                    user.CurrencyID = reader.ToInt("currencyid");
                }

                criteria.ErrCode = Converter.ToInt(objectDb.GetOuputParams()["errcode"]);
            }

            return user;
        }

        /// <summary>
        /// Update login result into DB.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        public void UpdateLoginResult(AffiliateLoginResultCriteria criteria)
        {
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(param);
                criteria.OnUserID = Converter.ToInt(param["onuserid"]);
                criteria.ErrCode = Converter.ToInt(param["errcode"]);
            }
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
                criteria.ErrCode = Converter.ToInt(param["errcode"]);

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