using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        //uyeler icin scope yasam dongusunu kullan. baris beyden yasam dongusune karar vermek icin tavsiye al.
        //transient icin --> InstancePerDependency(). istek basina yeni bir ornek olusturur. ornek senaryo: bir bilesen coklu kullanicili bir ortamda birden fazla bagimsiz ornege ihtiyac duyarsa kullanilabilir. daha hafif ve performanslidir fakat bellek kullanimini ciddi oranda arttirir. bilesenler kucuk ve hafif ise kullanilabilir. ek olarak bu yasam dongusu test ortaminda daha cok kullanislidir. her bir test icin yeni bilesen olusturup testlerin birbirinden farkli olmasini saglamakla beraber test verilerinin de bulasmasini onler.
        
           
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApartmentManager>().As<IApartmentService>().InstancePerLifetimeScope();
            builder.RegisterType<EfApartmentDal>().As<IApartmentDal>().InstancePerLifetimeScope();
        }
    }
}
