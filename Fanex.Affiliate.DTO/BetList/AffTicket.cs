// -----------------------------------------------------------------------
// <copyright file="AffTicket.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DTO.BetList
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AffTicket : Fanex.BetList.Core.Entities.Ticket
    {
        public string ExternalId { get; set; }

        public decimal LicenseeWl { get; set; }

        public int CurrencyId { get; set; }
    }
}