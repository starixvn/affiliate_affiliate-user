// --------------------------------------------------------------------------
// <copyright file="MessageViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Brian.Dinh" date="2013.07.03">Created</change>
// </history>

namespace Fanex.Affiliate.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using DTO;
    using Resources;
    using Web.Pager;

    /// <summary>
    /// Message view model.
    /// </summary>
    public class MessageViewModel
    {
        public byte? SearchOption { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public IEnumerable<SelectListItem> SearchOptionList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem 
                    {
                        Value = string.Empty,
                        Text = Lang.All,
                        Selected = SearchOption == null
                    },
                    new SelectListItem 
                    {
                        Value = "0",
                        Text = Lang.Normal,
                        Selected = SearchOption.HasValue && SearchOption.Value == 0
                    },
                    new SelectListItem 
                    {
                        Value = "1",
                        Text = Lang.Important,
                        Selected = SearchOption.HasValue && SearchOption.Value == 1
                    }
                };
            }
        }

        public IPagedList<Message> MessageList { get; set; }
    }
}