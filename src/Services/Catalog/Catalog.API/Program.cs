var builder = WebApplication.CreateBuilder(args);

// Add service to container

var app = builder.Build();

// Config the http request pipeline
app.MapGet("/", () => "Hello World!");

app.Run();
