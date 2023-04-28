using Entities.Concrete.EntityFramework.Context;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations
{
    public class Validations
    {
        public static string NotEmptyMessage { get; set; } = "{PropertyName} alanı boş olamaz";
        public class ApartmentValidation : AbstractValidator<ApartmentAddDto>
        {
            public ApartmentValidation()
            {
                RuleFor(x => x.Code).NotEmpty().WithMessage(NotEmptyMessage);
                RuleFor(x=> x.ApartmentName).NotEmpty().WithMessage(NotEmptyMessage);

            }
        }
    }
}
