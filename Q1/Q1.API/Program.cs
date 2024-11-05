using Q1.API.DependencyInjection.Extensions;
using Q1.BO.DepencencyInjection.Extensions;
using Q1.BO.DepencencyInjection.Options;
using Q1.DAO;
using Q1.DAO.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);



// Services BO
builder.Services.AddServicesBo();

// Services DAO
builder.Services.AddRepositoryPersistence();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<SilverJewelry2023DbContext>();

// Services API
builder.Services.AddJwtServices(builder.Configuration);
builder.Services.AddSwaggerServices();

var app = builder.Build();

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