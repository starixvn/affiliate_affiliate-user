namespace Fanex.Affiliate.Services
{
    using Criteria;
    using ViewModels;

    /// <summary>
    /// Interface payment service.
    /// </summary>
    public interface IPaymentService
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