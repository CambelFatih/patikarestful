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
builder.Services.AddScoped<JsonProductRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IProductRepository, JsonProductRepository>();
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
