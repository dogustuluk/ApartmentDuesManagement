using Autofac;
using System.Reflection;
using System;
using Web.Services;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using Business.Concrete;
using Business.Abstract;
using DataAccess.Concrete.UnitOfWork;
using DataAccess.Concrete.EntityFramework.Context;

namespace Web.DependencyResolvers
{
    public class AutofacBusinessModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<ApartmentDuesManagementContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterType<EfApartmentDal>().As<IApartmentDal>().SingleInstance();
            builder.RegisterType<ApartmentManager>().As<IApartmentService>().SingleInstance();

            builder.RegisterType<EfApartmentFlatDal>().As<IApartmentFlatDal>().InstancePerLifetimeScope();
            builder.RegisterType<ApartmentFlatManager>().As<IApartmentFlatService>().InstancePerLifetimeScope();

            //--
          //  var apiAssembly = Assembly.GetExecutingAssembly();
          //  var repoAssembly = Assembly.GetAssembly(typeof(ApartmentDuesManagementContext));

           // builder.RegisterAssemblyTypes(apiAssembly, repoAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
           // builder.RegisterAssemblyTypes(apiAssembly, repoAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();



            //-----
            //builder.Register(c => new HttpClient()).As<HttpClient>().InstancePerLifetimeScope();
            //builder.Register(c =>
            //{
            //    var httpClientFactory = c.Resolve<IHttpClientFactory>();
            //    return httpClientFactory.CreateClient();
            //}).As<HttpClient>();
        }
    }
}
