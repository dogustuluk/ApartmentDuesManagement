using Entities.Concrete.EntityFramework.Context;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Validations
{
    public class Validations
    {
        public static string NotEmptyMessage { get; set; } = "{PropertyName} alanı boş olamaz";
        public static string MaxMessageLength { get; set; } = "{PropertyName} alanı maksimum {0} karakter olabilir";
        public class ApartmentAddValidation : AbstractValidator<ApartmentAddDto>
        {
            public ApartmentAddValidation()
            {
                RuleFor(x => x.Code).NotEmpty().WithMessage(NotEmptyMessage);
                RuleFor(x => x.ApartmentName).NotEmpty().WithMessage(NotEmptyMessage);

            }
        }
        public class ApartmentUpdateValidation : AbstractValidator<ApartmentUpdateDto>
        {
            public ApartmentUpdateValidation()
            {
                RuleFor(x => x.Code).NotEmpty().WithMessage(NotEmptyMessage);

                RuleFor(x => x.ApartmentName)
                    .NotEmpty().WithMessage(NotEmptyMessage).WithName("Apartman Adı")
                    .MaximumLength(100).WithMessage("{PropertyName} alanı maksimum 100 karakter olabilir.").WithName("Apartman Adı");

                RuleFor(x => x.BlockNo)
                    .NotEmpty().WithMessage(NotEmptyMessage).WithName("Blok Numarası")
                    .MaximumLength(50).WithMessage("{PropertyName} alanı maksimum 50 karakter olabilir.").WithName("Blok Numarası");

                RuleFor(x => x.DoorNumber)
                    .NotEmpty().WithMessage(NotEmptyMessage).WithName("Kapı Numarası")
                    .MaximumLength(50).WithMessage("{PropertyName} alanı maksimum 50 karakter olabilir.").WithName("Kapı Numarası");

                RuleFor(x => x.OpenAdress)
                    .MaximumLength(500).WithMessage("{PropertyName} alani maksimum 500 karakter olabilir.").WithName("Açık Adres");

                RuleFor(x => x.ResponsibleMemberInfo.NameSurname)
                    .NotEmpty().WithMessage(NotEmptyMessage).WithName("Yönetici Ad-Soyad")
                    .MaximumLength(150).WithMessage("{PropertyName} alani en fazla 150 karakter olabilir.").WithName("Yönetici Ad-Soyad")
                     .Must(nameSurname =>
                     {
                         string handleNameSurname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nameSurname.ToLower());
                         return nameSurname == handleNameSurname;
                     }).WithMessage("Yönetici Ad-Soyad alanı için sadece ilk harfler büyük olacak şekilde girin.");



                RuleFor(x => x.ResponsibleMemberInfo.PhoneNumber)
                    .NotEmpty().WithMessage(NotEmptyMessage).WithName("Yönetici Telefonu")
                    .MaximumLength(13).WithMessage("{PropertyName} alani en fazla 11 karakter olabilir.").WithName("Yönetici Telefonu")
                    .Matches(new Regex(@"^5\d{2} \d{3} \d{4}$")).WithMessage("{PropertyName} alani icin dogru bir telefon formati girin;5xx xxx xxxx");

                RuleFor(x => x.ResponsibleMemberInfo.Email)
                    .NotEmpty().WithMessage(NotEmptyMessage).WithName("Yönetici E-Posta")
                    .MaximumLength(150).WithMessage("{PropertyName} alani en fazla 150 karakter olabilir.").WithName("Yönetici E-Posta")
                    .EmailAddress().WithMessage("{PropertyName} alanina uygun bir e-posta adresi girin.").WithName("Yönetici E-Posta");
            }
        }
        public class ApartmentFlatAddValidation : AbstractValidator<ApartmentFlatAddDto>
        {
            public ApartmentFlatAddValidation() 
            { 
                RuleFor(x => x.Code).NotEmpty().WithMessage(NotEmptyMessage)
                    .MaximumLength(50).WithMessage("{PropertyName} alanı en fazla 50 karakterden oluşmalı.");

                RuleFor(x => x.FlatNumber).NotEmpty().WithMessage(NotEmptyMessage)
                    .MaximumLength(10).WithMessage("{PropertyName} alanı en fazla 10 karakterden oluşmalı.");
               
                RuleFor(x => x.FlatOwnerId).NotEmpty().WithMessage(NotEmptyMessage);
                
                RuleFor(x => x.Floor).NotEmpty().WithMessage(NotEmptyMessage)
                    .MaximumLength(3).WithMessage("{PropertyName} alanı en fazla 3 karakterden oluşmalı.");
               
                RuleFor(x => x.CarPlate).NotEmpty().WithMessage(NotEmptyMessage)
                    .MaximumLength(8).WithMessage("{PropertyName} alanı en fazla 3 karakterden oluşmalı.");

            }
        }
        

    }
}
