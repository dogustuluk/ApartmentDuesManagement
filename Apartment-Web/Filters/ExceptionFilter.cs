using Azure;
using Entities.Concrete;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Mail;

namespace Apartment_Web.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _environment;
        private readonly IModelMetadataProvider _metadataProvider;
       // private readonly ILogger _logger;

        public ExceptionFilter(IHostEnvironment environment, IModelMetadataProvider metadataProvider)
        {
            _environment = environment;
            _metadataProvider = metadataProvider;
           // _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (_environment.IsDevelopment())
            {
                context.ExceptionHandled = true;
                var errorModel = new ErrorModel();
                ViewResult result;

                if (context.HttpContext.Response.StatusCode == 404)
                {
                    errorModel.Message = $"Boyle bir sayfa bulunmamaktadir.";
                    errorModel.Detail = context.Exception.Message;
                    result = new ViewResult { ViewName = "Error", StatusCode = 404 };
                    result.StatusCode = 404;

                   
                }


                switch (context.Exception)
                {
                    case NullReferenceException:
                        errorModel.Message = $"Üzgünüz, işleminiz sırasında beklenmedik bir null veriye rastlandı. Sorunu en kısa sürede çözeceğiz.";
                        errorModel.Detail = context.Exception.Message;
                        result = new ViewResult { ViewName = "Error" };
                        result.StatusCode = 403;
                       // _logger.LogError(context.Exception, context.Exception.Message);
                        break;
                    
                    case SqlNullValueException:
                        errorModel.Message = $"Üzgünüz, işleminiz sırasında beklenmedik bir veritabanı hatası oluştu. Sorunu en kısa sürede çözeceğiz.";
                        errorModel.Detail = context.Exception.Message;
                        result = new ViewResult { ViewName = "Error" };
                        result.StatusCode = 500;//500 kodu -> internal server error
                        //_logger.LogError(context.Exception, context.Exception.Message);
                        break;

                   

                    default:
                        errorModel.Message = $"Üzgünüz, işleminiz sırasında beklenmedik bir hata oluştu. Sorunu en kısa sürede çözeceğiz.";
                        result = new ViewResult { ViewName = "Error" };
                        result.StatusCode = 500;//500 kodu -> internal server error
                       // _logger.LogError(context.Exception, "Beklenmedik bir hata olustu.");
                        break;
                }
                result.ViewData = new ViewDataDictionary(_metadataProvider, context.ModelState);
                result.ViewData.Add("ErrorModel", errorModel);
                context.Result = result;
            }
        }
    }
}
