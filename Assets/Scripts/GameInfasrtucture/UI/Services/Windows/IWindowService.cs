using GameInfasrtucture.Services;

namespace GameInfasrtucture.UI.Services.Windows
{
    public interface IWindowService : IService
    {
        void Open(WindowId windowId);
    }
}