using Apartment_Web.Helpers;
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
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerLifetimeScope();
            builder.RegisterType<UrlHelper>().AsSelf().InstancePerDependency();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            
            
            builder.RegisterType<EfApartmentDal>().As<IApartmentDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfVwApartmentDal>().As<IVwApartmentDal>().InstancePerLifetimeScope();
            builder.RegisterType<ApartmentManager>().As<IApartmentService>().InstancePerLifetimeScope();
            builder.RegisterType<ApartmentViewManager>().As<IApartmentViewService>().InstancePerLifetimeScope();


            builder.RegisterType<EfApartmentFlatDal>().As<IApartmentFlat>().InstancePerLifetimeScope();
            builder.RegisterType<EfVwApartmentFlatDal>().As<IVwApartmentFlat>().InstancePerLifetimeScope();
            builder.RegisterType<ApartmentFlatManager>().As<IApartmentFlatService>().InstancePerLifetimeScope();
            builder.RegisterType<ApartmentFlatViewManager>().As<IApartmentFlatViewService>().InstancePerLifetimeScope();


            builder.RegisterType<EfMemberDal>().As<IMemberDal>().InstancePerLifetimeScope();
            builder.RegisterType<MemberManager>().As<IMemberService>().InstancePerLifetimeScope();

            builder.RegisterType<EfSubscriptionDal>().As<ISubscriptionDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfSubscriptionItemDal>().As<ISubscriptionItemDal>().InstancePerLifetimeScope();

            builder.RegisterType<EfCityDal>().As<ICityDal>().InstancePerLifetimeScope();
            builder.RegisterType<CityManager>().As<ICityService>().InstancePerLifetimeScope();
        }
    }
}
