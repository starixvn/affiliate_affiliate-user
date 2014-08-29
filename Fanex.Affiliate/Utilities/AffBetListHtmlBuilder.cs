// ----------------------------------------------------------------------
// <copyright file="AffBetListHtmlBuilder.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/11/22">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Utilities
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Affiliate.Criteria;
    using Fanex.Affiliate.DTO.BetList;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Affiliate BetList HTML Builder.
    /// </summary>
    public static class AffBetListHtmlBuilder
    {
        /// <summary>
        /// Get ticket status.
        /// </summary>
        /// <param name="builder">Bet list builder.</param>
        /// <param name="ticket">Affiliate ticket object.</param>
        /// <param name="ticketHelper">Ticket helper.</param>
        /// <param name="ticketData">List of ticket data.</param>
        /// <param name="criteria">Meta model.</param>
        /// <param name="currencyID">Currency ID.</param>
        /// <param name="currencyName">Currency Name.</param>
        /// <param name="username">User Name.</param>
        /// <returns>Ticket status text.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static string BuildAffBetListResultStatus(
            this BetListHTMLBuilder builder,
            AffTicket ticket,
            ITicketHelper ticketHelper,
            List<ITicketData> ticketData,
            BetListCriteria criteria,
            int currencyID,
            string currencyName,
            string username)
        {
            const string StatusTemp = "<div class='status'>{0}</div>";
            var statusText = GetStatusText(ticket);
            var statusNormalText = string.Format(StatusTemp, statusText);
                        
            switch (ticket.BetTypeId)
            {
                case 71:
                    string casinoHandTemplate = "<div id='casino-hand-history-{0}'";
                    casinoHandTemplate += "class='result casino-hand-history-link' url-param='{1}' onclick='BetList.ViewCasinoHandHistory({0});'>{2}</div>";
                    return statusNormalText + string.Format(
                        casinoHandTemplate,
                        ticket.RefNo,
                        GetCasinoHandHistoryParam(ticket, criteria, currencyID, currencyName, username),
                        "Results");
                case 72:
                case 73:
                    return statusNormalText;

                default:
                    return builder.BuildStatus(ticket, ticketHelper, ticketData, false);
            }
        }

        /// <summary>
        /// Get Casino Hand History Parameter.
        /// </summary>
        /// <param name="ticket">LRF Ticket object.</param>
        /// <param name="criteria">Meta model object.</param>
        /// <param name="currencyID">Currency ID.</param>
        /// <param name="currencyName">Currency Name.</param>
        /// <param name="username">User's Name.</param>
        /// <returns>Parameter for Casino Hand History.</returns>
        private static string GetCasinoHandHistoryParam(
            AffTicket ticket,
            BetListCriteria criteria,
            int currencyID,
            string currencyName,
            string username)
        {
            var param = string.Empty;
            param += "?RefNo=" + ticket.RefNo;
            param += "&FromDate=" + criteria.FromDate;
            param += "&ToDate=" + criteria.ToDate;
            param += "&SystemId=" + (string.IsNullOrEmpty(ticket.UserName) ? username : ticket.UserName);
            param += "&ExternalId=" + (string.IsNullOrEmpty(ticket.ExternalId) ? string.Empty : ticket.ExternalId);
            param += "&CustId=" + (ticket.CustId == 0 ? criteria.CustId : ticket.CustId);
            param += "&CurrencyId=" + currencyID.ToString();
            param += "&CurrencyName=" + currencyName;
            param += "&BaseCurrency=" + criteria.BaseCurrency;

            return param;
        }

        /// <summary>
        /// Get status text by status id.
        /// </summary>
        /// <param name="ticket">LRF ticket object.</param>
        /// <returns>Status text.</returns>
        private static string GetStatusText(AffTicket ticket)
        {
            var statusText = string.Empty;

            switch (ticket.Status.ToLower())
            {
                case BetStatus.Running:
                    statusText = CoreBetList.running;
                    break;

                case BetStatus.Reject:
                    statusText = CoreBetList.reject;
                    break;

                case BetStatus.Won:
                    statusText = CoreBetList.won;
                    break;

                case BetStatus.Lose:
                    statusText = CoreBetList.lose;
                    break;

                case BetStatus.Draw:
                    statusText = CoreBetList.draw;
                    break;

                case BetStatus.Waiting:
                    statusText = CoreBetList.waiting;
                    break;

                case BetStatus.Void:
                    statusText = CoreBetList.voided;
                    break;

                case BetStatus.Refund:
                    statusText = CoreBetList.refund;
                    break;
            }

            return statusText;
        }
    }
}
