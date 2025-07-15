using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;
using Sistema_de_Controle_de_Frequência.Data;
using Sistema_de_Controle_de_Frequência.Repositories;
using Sistema_de_Controle_de_Frequência.Services;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFrequenciaRepository, FrequenciaRepository>();
builder.Services.AddScoped<FrequenciaService>();
builder.Services.AddScoped<ISetorRepository, SetorRepository>();
builder.Services.AddScoped<IStatusFrequenciaRepository, StatusFrequenciaRepository>();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();


QuestPDF.Settings.License = LicenseType.Community;

app.Run();