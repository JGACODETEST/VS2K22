using Microsoft.Extensions.Hosting;
using NET8.Service.Model.Dto;

namespace NET8.Service
{
    public interface IService<T> where T : class
    {
        void Listar(HostApplicationBuilder builder);

        T Grabar(HostApplicationBuilder builder, T dto);

        bool Eliminar(HostApplicationBuilder builder, T dto);
    }
}