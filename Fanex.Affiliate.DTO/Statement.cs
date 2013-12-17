// --------------------------------------------------------------------------
// <copyright file="Statement.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.03">Created</change>
// </history>
namespace Fanex.Affiliate.DTO
{
    using System;
    using Resources;

    /// <summary>
    /// Statement data object.
    /// </summary>
    public class Statement
    {
        private string _status = string.Empty;

        public long TransId { get; set; }

        public DateTime MonthDate { get; set; }

        public string MonthDateString
        {
            get
            {
                return Utility.ConvertDateTime(
                    MonthDate,
                    "-",
                    TransType == 202 || TransType == 203,
                    false);
            }
        }

        public int TransType { get; set; }

        public string TransTypeName
        {
            get
            {
                switch (TransType)
                {
                    case 203:
                        return Lang.Adjustment;
                    case 201:
                        return Lang.MonthlyWinlost;
                    case 202:
                        return Lang.PaymentRequest;
                    default:
                        return Lang.Transaction;
                }
            }
        }

        public decimal Amount { get; set; }

        public string Status
        {
            get
            {
                return Utility.Status(_status);
            }

            set
            {
                _status = value;
            }
        }

        public string Creator { get; set; }

        public string Description { get; set; }
    }
}