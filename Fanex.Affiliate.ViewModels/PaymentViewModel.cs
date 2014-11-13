// --------------------------------------------------------------------------
// <copyright file="PaymentViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.13">Created</change>
//   <change who="Long.Luu" date="2013.06.28">Add more properties</change>
// </history>

namespace Fanex.Affiliate.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DTO;
    using Resources;

    /// <summary>
    /// Payment view model.
    /// </summary>
    public class PaymentViewModel
    {
        public string PreferredPayment { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "Msg143")]
        [Display(Name = "Amount", ResourceType = typeof(Lang))]
        public decimal Amount { get; set; }

        public decimal BalanceBroughtForward { get; set; }

        public decimal MinimumWithdrawalAmount { get; set; }

        public string PreferredLanguage { get; set; }

        public bool MailOption { get; set; }

        public DateTime TransDate { get; set; }

        public long TransID { get; set; }

        public string PlayerUsername { get; set; }

        public string SystemName { get; set; }

        public string DummyBalAcc { get; set; }

        public string AffCurrency { get; set; }

        public IEnumerable<Payment> ListPayment { get; set; }
    }
}