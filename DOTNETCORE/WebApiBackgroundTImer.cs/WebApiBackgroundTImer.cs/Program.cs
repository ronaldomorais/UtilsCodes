using Serilog;
using WebApiBackgroundTImer.cs.Applications;
using WebApiBackgroundTImer.cs.Extensions;
using WebApiBackgroundTImer.cs.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddBackgroundTimerService();

var app = builder.Build();

app.BackgroundTimerAppInitialize();
app.BackgroundTimerServiceApi();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
