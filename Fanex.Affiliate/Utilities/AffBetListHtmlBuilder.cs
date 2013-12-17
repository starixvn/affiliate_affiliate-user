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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Affiliate.Criterias;
    using Fanex.Affiliate.DTO.BetList;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.Logging;

    /// <summary>
    /// TODO: Update summary.
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
        /// <param name="metaModel">Meta model.</param>
        /// <param name="currencyID">Currency ID.</param>
        /// <param name="currencyName">Currency Name.</param>
        /// <returns>Ticket status text.</returns>
        public static string BuildAffBetListResultStatus(
            this BetListHTMLBuilder builder,
            AffTicket ticket,
            ITicketHelper ticketHelper,
            List<ITicketData> ticketData,
            BetListCriteria metaModel,
            int currencyID,
            string currencyName)
        {
            const string StatusTemp = "<div class='status'>{0}</div>";
            var statusText = GetStatusText(ticket);
            var statusNormalText = string.Format(StatusTemp, statusText);
                        
            switch (ticket.BetTypeId)
            {
                case 71:
                    string casinoHandTemplate = "<div id='casino-hand-history-{0}'";
                    casinoHandTemplate += "'result casino-hand-history-link' url-param='{1}' onclick='BetList.ViewCasinoHandHistory({0});'>{2}</div>";
                    return statusNormalText + string.Format(
                        casinoHandTemplate,
                        ticket.RefNo,
                        GetCasinoHandHistoryParam(ticket, metaModel, currencyID, currencyName),
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
        /// <param name="metaModel">Meta model object.</param>
        /// <param name="currencyID">Currency ID.</param>
        /// <param name="currencyName">Currency Name.</param>
        /// <returns>Parameter for Casino Hand History.</returns>
        private static string GetCasinoHandHistoryParam(AffTicket ticket, BetListCriteria metaModel, int currencyID, string currencyName)
        {
            var param = string.Empty;
            param += "?RefNo=" + ticket.RefNo;
            param += "&FromDate=" + metaModel.FromDate;
            param += "&ToDate=" + metaModel.ToDate;
            param += "&SystemId=" + (string.IsNullOrEmpty(ticket.UserName) ? string.Empty : ticket.UserName);
            param += "&ExternalId=" + (string.IsNullOrEmpty(ticket.ExternalId) ? string.Empty : ticket.ExternalId);
            param += "&CustId=" + (ticket.CustId == 0 ? metaModel.CustId : ticket.CustId);
            param += "&CurrencyId=" + currencyID.ToString();
            param += "&CurrencyName=" + currencyName;
            param += "&BaseCurrency=" + metaModel.BaseCurrency;

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
