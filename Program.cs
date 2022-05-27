using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddDbContext<SchoolContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext") ?? throw new InvalidOperationException("Connection string 'SchoolContext' not found.")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter(); //provides helpful error information in the development environment for EF
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

// to ponizej warto uzywac na pocztatku gdy aplikacja sie szybko zmienia bo za kadym razem tracimy cale dane
// potem zeby zachowac dane robi sie migrationa
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<SchoolContext>();
    //context.Database.EnsureCreated(); // usuwa database i jej zawartosc, towrzy nowy model i create DB od nowa
    DbInitializer.Initialize(context); // ciekawe, bo po zakomentowaniu tego wcalnie nie usunal DB
}
// Drop-Database -Confirm

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
