using ONEbook.UM.Models;

namespace Fanex.Affiliate.Services
{
    public interface IUnderMaintenanceService
    {
        UnderMaintenanceTime GetUnderMaintenanceTime();

        bool IsUnderMaintenance();
    }
}
