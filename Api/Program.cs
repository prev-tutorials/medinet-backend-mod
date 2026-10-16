

using Api.Modules.Doctors;
using Api.Modules.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.SharedServices(builder.Configuration);
builder.Services.DoctorsService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseSwaggerUI( 
    o => o.SwaggerEndpoint("/openapi/v1.json", "Api")
);

app.DoctorsEndpoints();
app.Run();
