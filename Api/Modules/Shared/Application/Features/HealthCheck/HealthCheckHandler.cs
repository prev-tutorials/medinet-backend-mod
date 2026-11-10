using MediatR;

namespace Api.Modules.Shared.Application.Features.HealthCheck;

public class HealthCheckHandler
    : IRequestHandler<HealthCheckQuery, HealthCheckResponse>
{
    public Task<HealthCheckResponse> Handle(
        HealthCheckQuery query,
        CancellationToken ct
    )
        => Task.FromResult(new HealthCheckResponse("Healthy"));
}
