using System.Collections.Concurrent;

var instanceId = Guid.NewGuid();
var counter = new ConcurrentDictionary<string, long>();
const string counterKey = "counter";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGet("/counter", () =>
{
    var resultValue = counter.AddOrUpdate(counterKey, 1, (_, l) => l + 1);
    return Results.Ok(new CounterResult(instanceId, resultValue));
});

app.MapGet("/echo", (IHttpContextAccessor contextAccessor) =>
    {
        var ctx = contextAccessor.HttpContext ??
                  throw new ArgumentNullException(nameof(contextAccessor), "Context not found!");
        return Results.Ok(
            new Echo(
                ctx.Request.Host.ToString(),
                ctx.Request.Path,
                ctx.Request.Protocol,
                ctx.Request.ContentType,
                ctx.Request.ContentLength,
                ctx.Request.Headers.ToDictionary(s => s.Key, s => s.Value.ToString())
            )
        );
    })
    .WithName("Get request echo")
    .WithOpenApi();

app.Run();

record Echo(
    string? Host,
    string? Path,
    string? Protocol,
    string? ContentType,
    long? ContentLength,
    IDictionary<string, string> Headers
);

record CounterResult(Guid InstanceId, long Counter);