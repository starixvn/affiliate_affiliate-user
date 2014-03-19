// --------------------------------------------------------------------------
// <copyright file="PaymentDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.03">Created</change>
//   <change who="Kaden.Trinh" date="2013.06.13">Get list payment request</change>
// </history>
namespace Fanex.Affiliate.DataAccess.Impl
{
    using System;
    using System.Collections;
    using Criteria;
    using Data;
    using DTO;
    using Interface;
    using ViewModels;

    /// <summary>
    /// Interface payment data access.
    /// </summary>
    public class PaymentDataAccess : IPaymentDataAccess
    {
        /// <summary>
        /// Get payment request.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object.</returns>
        public PaymentViewModel GetPaymentRequest(PaymentCriteria criteria)
        {
            var payment = new PaymentViewModel();
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                payment.ListPayment = objectDb.Query<Payment>(param);

                // get output parameter
                payment.PreferredPayment = param["preferredpayment"].ToString();
                payment.BalanceBroughtForward = Convert.ToDecimal(param["balbroughtforward"]);
                payment.MinimumWithdrawalAmount = Convert.ToDecimal(param["minamount"]);
            }

            return payment;
        }

        /// <summary>
        /// Insert payment request.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object.</returns>
        public PaymentViewModel InsertPaymentRequest(PaymentRequestCriteria criteria)
        {
            var payment = new PaymentViewModel();
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(param);

                criteria.ErrCode = Convert.ToInt16(param["errcode"]);
                if (criteria.ErrCode == 0)
                {
                    // get output parameter
                    payment.PreferredLanguage = param["preferredlanguage"].ToString();
                    payment.TransDate = Convert.ToDateTime(param["transdate"]);
                    payment.TransID = Convert.ToInt64(param["transid"]);
                    payment.MailOption = Convert.ToBoolean(param["mailoption"]);
                }
            }

            return payment;
        }

        /// <summary>
        /// Cancel payment request.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object.</returns>
        public PaymentViewModel CancelPaymentRequest(PaymentCancelCriteria criteria)
        {
            var payment = new PaymentViewModel();
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(param);

                criteria.ErrCode = Convert.ToInt16(param["errcode"]);
                if (criteria.ErrCode == 0)
                {
                    // get output parameter
                    payment.PreferredLanguage = param["preferredlanguage"].ToString();
                    payment.MailOption = Convert.ToBoolean(param["mailoption"]);
                    payment.TransDate = Convert.ToDateTime(param["transdate"]);
                    payment.Amount = Convert.ToDecimal(param["amount"]);
                }
            }

            return payment;
        }

        /// <summary>
        /// Get payment request's player info.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object.</returns>
        public PaymentViewModel GetPaymentRequestPlayerInfo(PaymentPlayerInfoCriteria criteria)
        {
            var payment = new PaymentViewModel();
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(param);

                // get output parameter
                payment.PlayerUsername = param["playerusername"].ToString();
                payment.SystemName = param["systemname"].ToString();
                payment.DummyBalAcc = param["dummybalacc"].ToString();
                payment.AffCurrency = param["affcurrency"].ToString();
            }

            return payment;
        }
    }
}