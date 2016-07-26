namespace Fanex.Affiliate.ViewModels
{
    using System;

    /// <summary>
    /// Result view model.
    /// </summary>
    [Serializable]
    public class ResultViewModel
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}