using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public class Messages
    {
        public class GeneralMessages
        {
            public static string GeneralError = "Bir hata ile karşılaşıldı.";
            public static string NotFound = "Bir kayıt bulunamadı.";
            public static string AccessDenied = "Erişim reddedildi.";
        }

        public class CityMessages
        {
            public static string AddedCity = "Şehir başarılı bir şekilde eklendi.";
            public static string UpdatedCity = "Şehir başarılı bir şekilde güncellendi.";
            public static string CityAlreadyExists = "Bu şehir daha önceden eklenmiş.";
        }
        public class ApartmentMessages
        {
            public static string AddedApartment = "Apartman başarılı bir şekilde eklendi.";
            public static string UpdatedApartment = "Apartman başarılı bir şekilde güncellendi.";
            public static string ApartmentAlreadyExists = "Bu apartman daha önceden eklenmiş.";
        }
        public class ApartmentFlatMessages
        {
            public static string AddedApartmentFlat = "Daire başarılı bir şekilde eklendi.";
            public static string UpdatedApartmentFlat = "Daire başarılı bir şekilde güncellendi.";
            public static string ApartmentFlatAlreadyExists = "Bu daire daha önceden eklenmiş.";
        }
        public class MemberMessages
        {
            public static string AddedUser = "Kullanıcı kaydı başarılı.";
            public static string UpdatedUser = "Kullanıcı başarılı bir şekilde güncellendi.";
            public static string UserNotFound = "Kullanıcı bulunamadı";
            public static string PasswordError = "Şifre ya da email hatalı";
            public static string SuccessfulLogin = "Giriş başarılı";
            public static string UseAlreadyExists = "Bu mail adresi daha onceden alinmis";
        }
        public class CountyMessages
        {
            public static string AddedCounty = "İlçe başarılı bir şekilde eklendi.";
            public static string UpdatedCounty = "İlçe başarılı bir şekilde güncellendi.";
            public static string CountyAlreadyExists = "Bu İlçe daha önceden eklenmiş.";
        }
        public class ExpenseMessages
        { }
        public class ParameterMessages
        { }
        public class RoleMessages
        { }
        public class SubscriptionMessages
        { }
        public class TransactionMessages
        { }
        


        


        


    }
}
