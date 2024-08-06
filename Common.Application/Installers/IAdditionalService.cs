using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Common.Application.Installers
{
    public interface IAdditionalService
    {
        void Run(IServiceProvider serviceProvider, Assembly assembly, IConfiguration configuration);
    }
}