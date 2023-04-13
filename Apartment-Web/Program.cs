using Autofac.Extensions.DependencyInjection;
using Autofac;
using Apartment_Web.DependencyResolvers;
using Business.Mappings;
using System.Text.Json.Serialization;
using Business.SeedDatas;
using DataAccess.Concrete.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
}); //frontend icin derleme yapmamiza gerek kalmaz, degisiklikleri goruruz.
builder.Services.AddAutoMapper(typeof(MapProfile));
var app = builder.Build();

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
var seed = FakeDataGenerator.GenerateSubscriptionItems(50);
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var unitOfWork = services.GetRequiredService<IUnitOfWork>();
    foreach (var items in seed)
    {
        unitOfWork.subscriptionItemDal.Add(items);
    }
    unitOfWork.Commit();
}
app.Run();
