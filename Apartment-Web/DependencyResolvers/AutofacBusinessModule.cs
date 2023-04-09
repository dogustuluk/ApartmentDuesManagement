using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Concrete.UnitOfWork;

namespace Apartment_Web.DependencyResolvers
{
    public class AutofacBusinessModule:Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApartmentDuesManagementContext>().AsSelf().InstancePerLifetimeScope();
            
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork2>().InstancePerLifetimeScope();

            
            
            builder.RegisterType<EfApartmentDal>().As<IApartmentDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfVwApartmentDal>().As<IVwApartmentDal>().InstancePerLifetimeScope();
            builder.RegisterType<ApartmentManager>().As<IApartmentService>().InstancePerLifetimeScope();
            builder.RegisterType<ApartmentViewManager>().As<IApartmentViewService>().InstancePerLifetimeScope();


            builder.RegisterType<EfApartmentFlatDal>().As<IApartmentFlat>().InstancePerLifetimeScope();
            builder.RegisterType<EfVwApartmentFlatDal>().As<IVwApartmentFlat>().InstancePerLifetimeScope();
            builder.RegisterType<ApartmentFlatManager>().As<IApartmentFlatService>().InstancePerLifetimeScope();
        }
    }
}
