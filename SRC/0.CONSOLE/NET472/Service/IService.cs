using Unity;

namespace NET472.Service
{
    public interface IService<T> where T : class
    {
        void Listar(IUnityContainer container);

        T Grabar(IUnityContainer container, T dto);
    }
}