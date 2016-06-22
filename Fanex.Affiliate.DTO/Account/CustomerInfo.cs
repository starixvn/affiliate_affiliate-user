namespace Fanex.Affiliate.DTO
{
    using System;

    /// <summary>
    /// Customer Information.
    /// </summary>
    public class CustomerInfo
    {
        public string Username { get; set; }
        
        public DateTime RegisteredDate { get; set; }

        public string RegisteredDateString
        {
            get
            {
                return Utility.ConvertDateTime(RegisteredDate, "-", true, false);
            }
        }

        public DateTime? CreatedDate { get; set; }

        public string FirstDepositDateString
        {
            get
            {
                return CreatedDate == null ?
                    string.Empty :
                    Utility.ConvertDateTime(CreatedDate, "-", true, false);
            }
        }

        public string Currency { get; set; }
    }
}