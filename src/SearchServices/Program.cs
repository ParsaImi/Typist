using MongoDB.Entities;
using SearchService;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Polly;
using Polly.Extensions.Http;
using System.Net;
using Contracts;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransit(x => {
    x.AddConsumersFromNamespaceContaining<UserCreatedConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search" , false));
    x.UsingRabbitMq((context , cfg) => 
    {
        cfg.ReceiveEndpoint("search-user-created" , e => {
            e.UseMessageRetry(r => r.Interval(5 , 5));
            e.ConfigureConsumer<UserCreatedConsumer>(context);
        });
        cfg.ConfigureEndpoints(context);
    });
});
builder.Services.AddHttpClient<TypistSrvHttpClient>().AddPolicyHandler(GetPolicy());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



app.UseAuthorization();



app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Lifetime.ApplicationStarted.Register(async () => 
{
        try
    {
        await Dbinit.InitDb(app);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
});


app.Run();
static IAsyncPolicy<HttpResponseMessage> GetPolicy()
    => HttpPolicyExtensions.HandleTransientHttpError()
    .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
    .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));
