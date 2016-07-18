namespace Fanex.Affiliate.DTO
{
    using System;

    /// <summary>
    /// Message data object.
    /// </summary>
    public class Message
    {
        public int MessageId { get; set; }

        public DateTime PostedDate { get; set; }

        public int Creator { get; set; }

        public string CreatorName { get; set; }

        public string UpdateName { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool Important { get; set; }

        public string Content { get; set; }
    }
}