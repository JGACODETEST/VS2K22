using Microsoft.Extensions.Hosting;
using NET7.Service.Model.Dto;

namespace NET7.Service
{
    public interface IService<T> where T : class
    {
        void Listar(HostApplicationBuilder builder);

        T Grabar(HostApplicationBuilder builder, T dto);

        bool Eliminar(HostApplicationBuilder builder, T dto);
    }
}