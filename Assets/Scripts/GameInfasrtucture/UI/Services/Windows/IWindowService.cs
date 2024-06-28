using GameInfrastructure.Services;

namespace GameInfrastructure.UI.Services.Windows
{
    public interface IWindowService : IService
    {
        void Open(WindowId windowId);
    }
}