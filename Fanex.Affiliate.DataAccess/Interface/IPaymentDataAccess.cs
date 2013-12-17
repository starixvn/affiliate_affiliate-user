// --------------------------------------------------------------------------
// <copyright file="IPaymentDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.03">Created</change>
//   <change who="Kaden.Trinh" date="2013.06.13">Get list payment request</change>
// </history>
namespace Fanex.Affiliate.DataAccess.Interface
{
    using Criterias;
    using ViewModels;

    /// <summary>
    /// Interface payment data access.
    /// </summary>
    public interface IPaymentDataAccess
    {
        /// <summary>
        /// Get payment request.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object.</returns>
        PaymentViewModel GetPaymentRequest(PaymentCriteria criteria);

        /// <summary>
        /// Insert payment request.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object.</returns>
        PaymentViewModel InsertPaymentRequest(PaymentRequestCriteria criteria);

        /// <summary>
        /// Cancel payment request.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object.</returns>
        PaymentViewModel CancelPaymentRequest(PaymentCancelCriteria criteria);

        /// <summary>
        /// Get payment request's player info.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object.</returns>
        PaymentViewModel GetPaymentRequestPlayerInfo(PaymentPlayerInfoCriteria criteria);
    }
}