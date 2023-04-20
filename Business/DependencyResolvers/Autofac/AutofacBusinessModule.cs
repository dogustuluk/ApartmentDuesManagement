using Autofac;
using Business.Abstract;
using Business.Concrete;
using Business.SeedDatas;
using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Concrete.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        //baris beyden yasam dongusune karar vermek icin tavsiye al.
        //transient icin --> InstancePerDependency(). istek basina yeni bir ornek olusturur. ornek senaryo: bir bilesen coklu kullanicili bir ortamda birden fazla bagimsiz ornege ihtiyac duyarsa kullanilabilir. daha hafif ve performanslidir fakat bellek kullanimini ciddi oranda arttirir. bilesenler kucuk ve hafif ise kullanilabilir. ek olarak bu yasam dongusu test ortaminda daha cok kullanislidir. her bir test icin yeni bilesen olusturup testlerin birbirinden farkli olmasini saglamakla beraber test verilerinin de bulasmasini onler.
        
           
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<ApartmentDuesManagementContext>().AsSelf().InstancePerLifetimeScope();//Bu kod Autofac container'ına, ApartmentDuesManagementContext sınıfının kendisini kaydeder ve yaşam döngüsünü de belirtir. manager sınıfının içerisine unit of work enjekte edilememe hatasını önler.


            //builder.RegisterType<ApartmentDuesManagementContext>().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<ApartmentDuesManagementContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerLifetimeScope();
            builder.RegisterType<UrlHelper>().AsSelf().InstancePerDependency();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();


            builder.RegisterType<EfApartmentDal>().As<IApartmentDal>().InstancePerLifetimeScope();
            builder.RegisterType<ApartmentManager>().As<IApartmentService>().InstancePerLifetimeScope();
            
            builder.RegisterType<EfApartmentFlatDal>().As<IApartmentFlat>().InstancePerLifetimeScope();
            builder.RegisterType<ApartmentFlatManager>().As<IApartmentFlatService>().InstancePerLifetimeScope();

            builder.RegisterType<EfVwApartmentFlatDal>().As<IVwApartmentFlat>().InstancePerLifetimeScope();
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
