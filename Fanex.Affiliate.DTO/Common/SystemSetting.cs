namespace Fanex.Affiliate.DTO
{
    /// <summary>
    /// System Setting.
    /// </summary>
    public class SystemSetting
    {
        public int SId { get; set; }

        public string Type { get; set; }

        public string SName { get; set; }

        public string SValue { get; set; }

        public string DisplayText { get; set; }

        public string Description { get; set; }

        public string Environment { get; set; }
    }
}