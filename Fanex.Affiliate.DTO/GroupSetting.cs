// --------------------------------------------------------------------------
// <copyright file="GroupSetting.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.11">Created</change>
// </history>
namespace Fanex.Affiliate.DTO
{
    /// <summary>
    /// Group settings.
    /// </summary>
    public class GroupSetting
    {
        public int GroupId { get; set; }

        public byte ProductId { get; set; }

        public byte TierId { get; set; }

        public decimal MinAmount { get; set; }

        public decimal MaxAmount { get; set; }

        public int MinQM { get; set; }

        public int MaxQM { get; set; }

        public decimal Commission { get; set; }
    }
}