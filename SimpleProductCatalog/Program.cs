using AutoMapper;
using SimpleProductCatalog.Abstraction.Interface;
using SimpleProductCatalog.Application.ProfileMap;
using SimpleProductCatalog.Application.Services;
using SimpleProductCatalog.Infra.Data.DBContext;
using SimpleProductCatalog.Infra.Data.Repository;
using SimpleProductCatalog.Infra.Data.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddDbContext<SimpleProductCatalogDBContext>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
