using System;
using VendorMachine.Interfaces;
using VendorMachine.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using VendorMachine.Repositories;

namespace VendingMachine // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        
        static void Main(string[] args)
        {            
             var serviceProvider = new ServiceCollection()              
                .AddSingleton<ICoinService, CoinService>()
                .AddSingleton<IProductService, ProductService>()  
                .AddSingleton<IVendingMachineService, VendingMachineService>()
                .AddSingleton<IProductRepository,ProductRepository>()
                .BuildServiceProvider();

            var service = serviceProvider.GetService<IVendingMachineService>();
            service.Execute();
        }       
    }
}