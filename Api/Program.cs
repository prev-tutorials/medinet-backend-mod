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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseSwaggerUI( 
    o => o.SwaggerEndpoint("/openapi/v1.json", "Api")
);

app.DoctorEndpoints();
app.PatientEndpoints();
app.AppointmentEndpoints();

app.UseExceptionHandler();
app.Run();
