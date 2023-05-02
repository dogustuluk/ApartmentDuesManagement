using Autofac.Extensions.DependencyInjection;
using Autofac;
using Apartment_Web.DependencyResolvers;
using Business.Mappings;
using System.Text.Json.Serialization;
using Business.SeedDatas;
using DataAccess.Concrete.UnitOfWork;
using FluentValidation.AspNetCore;
using System.Reflection;
using Business.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
builder.Services.AddControllersWithViews().AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblyContaining<Validations>();
}).AddRazorRuntimeCompilation().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
}); //frontend icin derleme yapmamiza gerek kalmaz, degisiklikleri goruruz.
builder.Services.AddAutoMapper(typeof(MapProfile));

var app = builder.Build();
//app.UseMiddleware<AutofacMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//var apartments = FakeDataGenerator.GenerateApartments(50);
//var seed = FakeDataGenerator.GenerateSubscriptionItems(50);
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var unitOfWork = services.GetRequiredService<IUnitOfWork>();
//    foreach (var items in apartments)
//    {
//        unitOfWork.apartmentDal.Add(items);
//    }
//    unitOfWork.Commit();
//}
app.Run();
