using GameInfasrtucture.Services;

namespace GameInfasrtucture.UI.Services.UIFactory
{
    public interface IUIFactory : IService
    {
        void CreateShop();
        void CreateUIRoot();
    }
}