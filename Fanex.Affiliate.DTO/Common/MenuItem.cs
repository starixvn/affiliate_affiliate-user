namespace Fanex.Affiliate.DTO
{
    /// <summary>
    /// Menu Item data object.
    /// </summary>
    public class MenuItem
    {
        public string MenuName { get; set; }

        public string ParentName { get; set; }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public string Roles { get; set; }
    }
}
