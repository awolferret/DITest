using System.Threading.Tasks;
using GameInfrastructure.Services;

namespace GameInfrastructure.UI.Services.UIFactory
{
    public interface IUIFactory : IService
    {
        void CreateShop();
        Task CreateUIRoot();
    }
}