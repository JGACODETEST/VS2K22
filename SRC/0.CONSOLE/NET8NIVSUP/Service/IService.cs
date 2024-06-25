using Microsoft.Extensions.Hosting;
using NET8NIVSUP.Service.Model.Dto;

namespace NET8NIVSUP.Service
{
    public interface IService<T> where T : class
    {
        void Listar(HostApplicationBuilder builder);

        T Grabar(HostApplicationBuilder builder, T dto);

        bool Eliminar(HostApplicationBuilder builder, T dto);
    }
}