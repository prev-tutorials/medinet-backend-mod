using MediatR;

namespace Api.Modules.Shared.Application.Features.HealthCheck;

public record HealthCheckResponse(
    string Status
);

public record HealthCheckQuery()
    : IRequest<HealthCheckResponse>;
