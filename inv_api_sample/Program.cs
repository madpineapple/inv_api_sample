using InvDataAccess.Data;
using InvDataAccess.DBAccess;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options=>{ options.JsonSerializerOptions.PropertyNamingPolicy = null;});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IProductData, ProductData>();
builder.Services.AddSingleton<ICustomerData, CustomerData>();
builder.Services.AddSingleton<IOrderData, OrderData>();
builder.Services.AddScoped<IMaterialProductData, MaterialProductData>();


builder.Services.AddSingleton<IMySQLDataAccess, MySQLDataAccess>();
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowLocalhost", policy =>
  policy.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
  app.UseCors("AllowLocalhost");
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
