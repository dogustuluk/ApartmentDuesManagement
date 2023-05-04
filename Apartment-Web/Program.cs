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
using Apartment_Web.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ExceptionFilter>();

}).AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblyContaining<Validations>();
}).AddRazorRuntimeCompilation().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddAutoMapper(typeof(MapProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseStatusCodePages();

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
