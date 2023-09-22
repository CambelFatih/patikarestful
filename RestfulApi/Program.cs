//program.cs
using MyWebApi.Middlewares;
using MyWebApi.Repositories;
using MyWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();
// ...
builder.Services.AddScoped<IProductRepository, JsonProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// ...
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Middlewares
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseRouting();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
