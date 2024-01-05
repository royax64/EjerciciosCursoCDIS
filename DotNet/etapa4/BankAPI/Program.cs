using BankAPI.Data;
using BankAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<BancoContext>(builder.Configuration.GetConnectionString("BankConnection"));
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<CuentaService>();
builder.Services.AddScoped<TipoCuentaService>();
builder.Services.AddScoped<TipoTransaccionService>();
builder.Services.AddScoped<AdminLoginService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();
app.UseHttpsRedirection();


app.Run();


