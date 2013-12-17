// --------------------------------------------------------------------------
// <copyright file="UserViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Johnny.Trinh" date="2013.06.24">Created</change>
// </history>

namespace Fanex.Affiliate.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web.Mvc;
    using Configurations;
    using DTO;  
    using Fanex.Affiliate.Resources;

    /// <summary>
    /// Overview View Model.
    /// </summary>
    public class UserViewModel : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserViewModel" /> class.
        /// </summary>
        public UserViewModel()
        {
            CountryList = AppSettings.BaseCountries.ToList();
            LanguageList = AppSettings.BaseLanguages.ToList();
            ExchangeList = AppSettings.AffiliateCurrencies.ToList();
            SecurityQuestionList = new List<SecurityQuestion>
            {
                new SecurityQuestion() { QuestionID = Lang.MotherName, QuestionValue = Lang.MotherName },
                new SecurityQuestion() { QuestionID = Lang.NameOfBook, QuestionValue = Lang.NameOfBook },
                new SecurityQuestion() { QuestionID = Lang.PetName, QuestionValue = Lang.PetName },
                new SecurityQuestion() { QuestionID = Lang.Movie, QuestionValue = Lang.Movie },
                new SecurityQuestion() { QuestionID = Lang.Hobby, QuestionValue = Lang.Hobby },
                new SecurityQuestion() { QuestionID = Lang.SportTeam, QuestionValue = Lang.SportTeam },
                new SecurityQuestion() { QuestionID = Lang.SecretCode, QuestionValue = Lang.SecretCode }
            };
        }

        public int AffID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "FirstNameIsRequired")]
        [Display(ResourceType = typeof(Lang), Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "LastNameIsRequired")]
        [Display(Name = "LastName", ResourceType = typeof(Lang))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "ErrGender")]
        [Display(Name = "Gender", ResourceType = typeof(Lang))]
        public bool Gender { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "EmailIsRequire")]
        [Display(Name = "Email", ResourceType = typeof(Lang))]
        [RegularExpression(
            @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$",
            ErrorMessageResourceType = typeof(Lang),
            ErrorMessageResourceName = "Msg119")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "VerifyEmail", ResourceType = typeof(Lang))]
        [Compare("Email", ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "EmailMatch")]
        public string VerifyEmail { get; set; }

        [Display(Name = "ContactNumber", ResourceType = typeof(Lang))]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "ContactNumberIsRequired")]
        public string Phone { get; set; }

        [Display(Name = "Mobile", ResourceType = typeof(Lang))]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "MobileNumberIsRequired")]
        public string MobilePhone { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Lang))]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "AddressIsRequired")]
        public string Address { get; set; }

        [Display(Name = "City", ResourceType = typeof(Lang))]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "CityIsRequired")]
        public string City { get; set; }

        [Display(Name = "State", ResourceType = typeof(Lang))]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "StateIsRequired")]
        public string State { get; set; }

        [Display(Name = "Postal", ResourceType = typeof(Lang))]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "PostcodeZiIsRequired")]
        public string Postal { get; set; }

        [Display(Name = "SiteURL", ResourceType = typeof(Lang))]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "SiteURLIsRequired")]
        public string SiteURL { get; set; }

        [Display(Name = "MoreSiteURLs", ResourceType = typeof(Lang))]
        public string MoreSiteURLs { get; set; }

        [Display(Name = "EmailFormat", ResourceType = typeof(Lang))]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "SiteURLIsRequired")]
        public bool EmailFormat { get; set; }

        [Display(Name = "Answer", ResourceType = typeof(Lang))]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "AnswerIsRequired")]
        public string Answer { get; set; }

        [Display(Name = "PlayerUsername", ResourceType = typeof(Lang))]
        public string Player_Username { get; set; }

        public string PreferredPayment { get; set; }

        public int Player_IBCCustID { get; set; }

        [Display(Name = "Validation", ResourceType = typeof(Lang))]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "MsgEmptyCaptcha")]
        public string Captcha { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Lang))]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "CountryIsRequired")]
        public string Country { get; set; }

        public List<Country> CountryList { get; set; }

        public IEnumerable<SelectListItem> CountrySelectListItem
        {
            get
            {
                CountryList.Insert(0, new Country { CountryId = string.Empty, CountryName = Lang.SelectOne });
                var selectItemList = CountryList.Select(country => new SelectListItem
                {
                    Value = country.CountryId,
                    Text = country.CountryName
                }).ToList<SelectListItem>();

                return selectItemList;
            }
        }

        [Display(Name = "Language", ResourceType = typeof(Lang))]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "LanguageIsRequired")]
        public string Language { get; set; }

        public List<Language> LanguageList { get; set; }

        public IEnumerable<SelectListItem> LanguageSelectListItem
        {
            get
            {
                LanguageList.Insert(0, new Language { LanguageId = string.Empty, LanguageName = Lang.SelectOne });
                var selectItemList = LanguageList.Select(lang => new SelectListItem
                {
                    Value = lang.LanguageId,
                    Text = lang.LanguageName
                }).ToList<SelectListItem>();

                return selectItemList;
            }
        }

        [Display(Name = "Currency", ResourceType = typeof(Lang))]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "CurrencyIsRequired")]
        public short Currency { get; set; }

        public List<Exchange> ExchangeList { get; set; }

        public IEnumerable<SelectListItem> ExchangeSelectListItem
        {
            get
            {
                var selectItemList = ExchangeList.Select(exchange => new SelectListItem
                {
                    Value = exchange.ExchangeID.ToString(),
                    Text = exchange.Code
                }).ToList<SelectListItem>();

                return selectItemList;
            }
        }

        [Display(Name = "Question", ResourceType = typeof(Lang))]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "QuestionIsRequired")]
        public string Question { get; set; }

        public List<SecurityQuestion> SecurityQuestionList { get; set; }

        public IEnumerable<SelectListItem> SecurityQuestionSelectListItem
        {
            get
            {
                SecurityQuestionList.Insert(0, new SecurityQuestion { QuestionID = string.Empty, QuestionValue = Lang.SelectOne });
                var selectItemList = SecurityQuestionList.Select(securityQuestion => new SelectListItem
                {
                    Value = securityQuestion.QuestionID,
                    Text = securityQuestion.QuestionValue
                }).ToList<SelectListItem>();

                return selectItemList;
            }
        }

        /// <summary>
        /// Check whether More site url is valid.
        /// </summary>
        /// <param name="modelState">Model State Dictionary.</param>
        /// <returns>Valid or not.</returns>
        public bool IsMoreSiteUrlsValid(ModelStateDictionary modelState)
        {
            if (!string.IsNullOrEmpty(MoreSiteURLs))
            {
                var sites = MoreSiteURLs.Split(';');
                for (int index = 0; index < sites.Length - 1; index++)
                {
                    if (string.IsNullOrEmpty(sites[index]))
                    {
                        modelState.AddModelError("ErrWebsiteUrl", string.Format(Lang.MoreSiteUrl, index + 2));

                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Check whether view model is valid.
        /// </summary>
        /// <param name="modelState">Model State Dictionary.</param>
        /// <returns>Is valid or not.</returns>
        public bool IsValid(ModelStateDictionary modelState)
        {
            return IsMoreSiteUrlsValid(modelState) && modelState.IsValid;
        }

        /// <summary>
        /// Validate the model.
        /// </summary>
        /// <param name="context">Validation Context.</param>
        /// <returns>A list of validation results.</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            if (!AppSettings.IsKiosk && string.IsNullOrEmpty(Player_Username))
            {
                yield return new ValidationResult("Player Username is required.");
            }
        }
    }
}