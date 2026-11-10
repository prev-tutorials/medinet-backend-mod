using Api.Modules.Appointments;
using Api.Modules.Doctors;
using Api.Modules.Patients;
using Api.Modules.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.SharedServices(builder.Configuration);
builder.Services.DoctorServices();
builder.Services.PatientServices();
builder.Services.AppointmentServices();


builder.Services.AddCors(options =>
{
    var allowedOrigins = builder.Configuration
        .GetSection("Cors:AllowedOrigins")
        .Get<string[]>() ?? [];

    options.AddPolicy("Frontend", policy =>
    {
        if (allowedOrigins.Length == 0)
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        else
            policy
                .WithOrigins(allowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseSwaggerUI( 
    o => o.SwaggerEndpoint("/openapi/v1.json", "Api")
);

app.UseCors("Frontend");
app.SharedEndpoints();
app.DoctorEndpoints();
app.PatientEndpoints();
app.AppointmentEndpoints();

app.UseExceptionHandler();
app.Run();
