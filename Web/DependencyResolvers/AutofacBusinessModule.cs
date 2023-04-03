using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.UnitOfWork;
using System.Reflection;
using System;
using Web.Services;
using Core.DataAccess;
using Core.DataAccess.EntityFramework;

namespace Web.DependencyResolvers
{
    public class AutofacBusinessModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApartmentDuesManagementContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork2>().InstancePerLifetimeScope();

            builder.RegisterType<EfApartmentDal>().As<IApartmentDal>().SingleInstance();
            builder.RegisterType<ApartmentManager>().As<IApartmentService>().SingleInstance();

            builder.RegisterType<EfApartmentFlatDal>().As<IApartmentFlat>().InstancePerLifetimeScope();
            builder.RegisterType<ApartmentFlatManager>().As<IApartmentFlatService>().InstancePerLifetimeScope();
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
