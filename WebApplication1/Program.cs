using Microsoft.EntityFrameworkCore;
using WebApplication1.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<HRMContext>(opt => opt.UseInMemoryDatabase("HRMDB"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // 3. Get the instance of HRMContext in our service layer
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<HRMContext>();

    // 4. Call the SeedDataGenerator to generate seed data
    SeedDataGenerator.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
