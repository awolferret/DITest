namespace GameInfrastructure.Services
{
    public class AllServices
    {
        private static AllServices _instrance;
        public static AllServices Container => _instrance ?? (_instrance = new AllServices());

        public void RegisterSingle<TService>(TService implinmetation) where TService : IService =>
            Implementation<TService>.ServiceInstance = implinmetation;

        public TService Single<TService>() where TService : IService => Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}