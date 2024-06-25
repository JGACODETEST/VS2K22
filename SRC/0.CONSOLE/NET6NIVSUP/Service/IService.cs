using Microsoft.Extensions.Hosting;
using NET6NIVSUP.Service.Model.Dto;

namespace NET6NIVSUP.Service
{
    public interface IService<T> where T : class
    {
        void Listar(HostApplicationBuilder builder);

        T Grabar(HostApplicationBuilder builder, T dto);

        bool Eliminar(HostApplicationBuilder builder, T dto);
    }
}