// --------------------------------------------------------------------------
// <copyright file="PaymentService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.03">Created</change>
//   <change who="Kaden.Trinh" date="2013.06.13">Get list payment request</change>
//   <change who="Long.Luu" date="2013.07.01">handle payment request/cancel payment</change>
// </history>

namespace Fanex.Affiliate.Services
{
    using Common.Helper;
    using Criteria;
    using DataAccess;
    using ViewModels;

    /// <summary>
    /// Payment Service.
    /// </summary>
    public class PaymentService : IPaymentService
    {
        #region Fields

        /// <summary>
        /// Define Payment DataAccess.
        /// </summary>
        private readonly IPaymentDataAccess _paymentDataAccess;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentService" /> class.
        /// </summary>
        public PaymentService()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentService" /> class.
        /// </summary>
        /// <param name="paymentDataAccess">Payment Data Access.</param>
        private PaymentService(IPaymentDataAccess paymentDataAccess)
        {
            _paymentDataAccess = paymentDataAccess ?? new PaymentDataAccess();
        }

        #endregion Constructor

        /// <summary>
        /// Get payment request.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object.</returns>
        public PaymentViewModel GetPaymentRequest(PaymentCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var result = _paymentDataAccess.GetPaymentRequest(criteria);

            return result;
        }

        /// <summary>
        /// Insert payment request.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object.</returns>
        public PaymentViewModel InsertPaymentRequest(PaymentRequestCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _paymentDataAccess.InsertPaymentRequest(criteria);
        }

        /// <summary>
        /// Cancel payment request.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object.</returns>
        public PaymentViewModel CancelPaymentRequest(PaymentCancelCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _paymentDataAccess.CancelPaymentRequest(criteria);
        }

        /// <summary>
        /// Get payment request's player info.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object.</returns>
        public PaymentViewModel GetPaymentRequestPlayerInfo(PaymentPlayerInfoCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _paymentDataAccess.GetPaymentRequestPlayerInfo(criteria);
        }
    }
}