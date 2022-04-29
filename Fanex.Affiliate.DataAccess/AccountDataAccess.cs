namespace Fanex.Affiliate.DataAccess
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Common.Extensions;
    using Criteria;
    using Data;
    using DTO;
    using Fanex.Data.SqlServer;
    using ViewModels;

    /// <summary>
    /// Account data access.
    /// </summary>
    public class AccountDataAccess : IAccountDataAccess
    {
        #region Account Overview

        /// <summary>
        /// Get statistics for Affiliate.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <param name="listProduct">Out product list.</param>
        /// <returns>Return object.</returns>
        public StatisticsOverview GetStatisticsOverview(AccountStatisticInfoCriteria criteria, ref List<Product> listProduct)
        {
            var statistics = new StatisticsOverview();
            using (var objectDb = new SqlObjectDb(criteria.GetSettingKey()))
            {
                listProduct = objectDb.Query<Product>(criteria) as List<Product>;
                var param = objectDb.GetOutputParams();

                // Get output parameters
                statistics.TotalActivePlayer = Convert.ToInt32(param["totalactiveplayer"]);
                statistics.TotalPlayerDepUp = Convert.ToInt32(param["totalplayerdepup"]);
                statistics.NewSignUpDep = Convert.ToInt32(param["newsignupdep"]);
                statistics.NewSignUp = Convert.ToInt32(param["newsignup"]);
                statistics.TotalPlayer = Convert.ToInt32(param["totalplayer"]);
                statistics.TotalUniqueClicks = Convert.ToInt32(param["totaluniqueclicks"]);
                statistics.TotalClicks = Convert.ToInt32(param["totalclicks"]);
                statistics.BalBroughtforward = Convert.ToDecimal(param["balbroughtforward"]);
                statistics.TotalDeposit = Convert.ToDecimal(param["totaldeposit"]);
                statistics.TotalDepositUp = Convert.ToDecimal(param["totaldepositup"]);
                statistics.TotalTurnover = Convert.ToDecimal(param["totalturnover"]);
                statistics.TotalTurnoverUp = Convert.ToDecimal(param["totalturnoverup"]);
                statistics.TotalWinlostUp = Convert.ToDecimal(param["totalwinlostup"]);
                statistics.TotalWinlost = Convert.ToDecimal(param["totalwinlost"]);
            }

            return statistics;
        }

        /// <summary>
        /// Get Current Commission Setting.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object.</returns>
        public AccountOverviewViewModel GetCommissionSetting(AccountCommGroupCriteria criteria)
        {
            var commResult = new AccountOverviewViewModel();
            using (var objectDb = new SqlObjectDb(criteria.GetSettingKey()))
            {
                var data = objectDb.ExecuteReader(criteria);

                // Get data to GroupSetting
                while (data.Read())
                {
                    commResult.ListGroupSetting.Add(new GroupSetting
                    {
                        GroupId = data.ToInt("groupid"),
                        ProductId = data.ToByte("productid"),
                        TierId = data.ToByte("tierid"),
                        MinAmount = data.ToDecimal("minamount"),
                        MaxAmount = data.ToDecimal("maxamount"),
                        MinQM = data.ToInt("minqm"),
                        MaxQM = data.ToInt("maxqm"),
                        Commission = data.ToDecimal("commission")
                    });
                }

                // Get data to Product
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        commResult.ListProductOnCommissionSetting.Add(new Product
                        {
                            ProductId = data.ToByte("productid"),
                            ProductName = data["productname"].ToString()
                        });
                    }
                }
            }

            return commResult;
        }

        #endregion

        #region Daily Earnings

        /// <summary>
        /// Get Daily Earnings.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>List of daily earning.</returns>
        public IEnumerable<DailyEarning> GetDailyEarnings(DailyEarningCriteria criteria)
        {
            List<DailyEarning> data;
            using (var objectDb = new SqlObjectDb(criteria.GetSettingKey()))
            {
                data = objectDb.Query<DailyEarning>(criteria) as List<DailyEarning>;
                var param = objectDb.GetOutputParams();
                criteria.AffCurrency = Convert.ToString(param["affcurrency"]);
            }

            return data;
        }

        /// <summary>
        /// Get Daily Earnings Detail.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>List of daily earning detail.</returns>
        public IEnumerable<DailyEarningsDetail> GetDailyEarningsDetail(DailyEarningsDetailCriteria criteria)
        {
            List<DailyEarningsDetail> data;
            using (var objectDb = new SqlObjectDb(criteria.GetSettingKey()))
            {
                data = objectDb.Query<DailyEarningsDetail>(criteria) as List<DailyEarningsDetail>;
                var param = objectDb.GetOutputParams();
                criteria.AffCurrency = Convert.ToString(param["affcurrency"]);
            }

            return data;
        }

        #endregion

        #region Change password

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Old-Password string.</returns>
        public string GetChangePasswordInfo(ChangePasswordInfoCriteria criteria)
        {
            var oldPassword = string.Empty;

            using (var objectDb = new SqlObjectDb(criteria.GetSettingKey()))
            {
                var reader = objectDb.ExecuteReader(criteria);

                while (reader.Read())
                {
                    oldPassword = reader["password"].ToString();
                }

                criteria.ErrCode = Converter.ToInt(objectDb.GetOutputParams()["errcode"]);
            }

            return oldPassword;
        }

        /// <summary>
        /// Update change password result into DB.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        public void UpdateChangePasswordResult(ChangePasswordCriteria criteria)
        {
            using (var objectDb = new SqlObjectDb(criteria.GetSettingKey()))
            {
                objectDb.ExecuteNonQuery(criteria);
                var param = objectDb.GetOutputParams();
                criteria.ErrCode = Converter.ToInt(param["errcode"]);
            }
        }

        #endregion

        #region Get Default Product Comm

        /// <summary>
        /// Get Default Product Commission.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Default Product Commission Model.</returns>
        public DefaultProductCommissionViewModel GetDefaultProductCommission(DefaultCommissionCriteria criteria)
        {
            var result = new DefaultProductCommissionViewModel();

            using (var objectDb = new SqlObjectDb(criteria.GetSettingKey()))
            {
                var reader = objectDb.ExecuteReader(criteria);

                // Get data to GroupSetting
                while (reader.Read())
                {
                    result.GroupSetting.Add(new GroupSetting
                    {
                        ProductId = reader.ToByte("productid"),
                        TierId = reader.ToByte("tierid"),
                        MinAmount = reader.ToDecimal("minamount"),
                        MaxAmount = reader.ToDecimal("maxamount"),
                        MinQM = reader.ToInt("minqm"),
                        MaxQM = reader.ToInt("maxqm"),
                        Commission = reader.ToDecimal("commission")
                    });
                }

                // Get data to Product
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        result.ProductList.Add(new Product
                        {
                            ProductId = reader.ToByte("productid"),
                            ProductName = reader["productname"].ToString()
                        });
                    }
                }
            }

            return result;
        }

        #endregion

        #region Get Customer Info

        /// <summary>
        /// Get Customer Info.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Customer Info Model.</returns>
        public IEnumerable<CustomerInfo> GetCustomerInfo(CustomerInfoCriteria criteria)
        {
            List<CustomerInfo> data;
            using (var objectDb = new SqlObjectDb(criteria.GetSettingKey()))
            {
                data = objectDb.Query<CustomerInfo>(criteria) as List<CustomerInfo>;
                var param = objectDb.GetOutputParams();
                criteria.TotalPages = Converter.ToInt(param["totalpages"]);
            }

            return data;
        }

        #endregion
    }
}