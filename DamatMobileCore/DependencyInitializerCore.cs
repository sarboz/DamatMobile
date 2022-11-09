using System;
using System.Net.Http;
using Autofac;
using Autofac.Core;
using AutoMapper;
using DamatMobile.Core.Abstractions;
using DamatMobile.Core.Abstractions.Api;
using DamatMobile.Core.Abstractions.Repositories;
using DamatMobile.Core.Abstractions.Services;
using DamatMobile.Core.Context;
using DamatMobile.Core.Mappings;
using DamatMobile.Core.Repositories;
using DamatMobile.Core.Services;
using DamatMobile.Core.ViewModels;
using Refit;

namespace DamatMobile.Core
{
    public abstract class DependencyInitializerCore
    {
        public static IContainer Container { get; private set; }

        public IContainer Build()
        {
            var builder = new ContainerBuilder();
            //viewModels
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<HomeViewModel>().AsSelf();
            builder.RegisterType<LoginViewModel>().AsSelf();
            builder.RegisterType<ConfirmCustomerViewModel>().AsSelf();
            builder.RegisterType<NotificationViewModel>().AsSelf();
            builder.RegisterType<BrandImagesViewModel>().AsSelf();

            //repositories
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<BrandRepository>().As<IBrandRepository>();
            builder.RegisterType<VirtualCardRepository>().As<IVirtualCardRepository>();
            builder.RegisterType<PurchaseHistoryRepository>().As<IPurchaseHistoryRepository>();
            builder.RegisterType<NewsRepository>().As<INewsRepository>();

            
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<DependencyResolver>().As<IDependencyResolver>();

            builder.RegisterType<DamatContext>().As<IArmugonContext>().SingleInstance().OnActivated(DatabaseCreating);

            //services
            builder.RegisterType<BrandService>().As<IBrandService>();
            builder.RegisterType<NewsService>().As<INewsService>();
            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<AuthorizationService>().As<IAuthorizationService>();

            var configuration = new MapperConfiguration(expression => expression.AddMaps(typeof(Profiles)));

            builder.Register(_ => configuration.CreateMapper()).As<IMapper>().SingleInstance();
            
            builder.Register(_ => GetApi<IApiEndpoints>()).As<IApiEndpoints>();
            RegisterServices(builder);
            return Container = builder.Build();
        }

        private void DatabaseCreating(IActivatedEventArgs<DamatContext> obj)
        {
            try
            {
                obj.Instance.Database.EnsureCreated();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private T GetApi<T>()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = ((message, certificate2, arg3, arg4) => true);
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.BaseAddress = new Uri(AppConstants.ApiUrl);
            return RestService.For<T>(httpClient);
        }

        public abstract void RegisterServices(ContainerBuilder builder);
    }
}