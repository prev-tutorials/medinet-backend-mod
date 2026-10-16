using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Api.Modules.Shared.Domain;

namespace Api.Modules.Shared.Infrastructure.Persistence;

public static class ModelBuilderExtensions
{
    public static void ApplySoftDeleteFilter(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(Entity).IsAssignableFrom(entityType.ClrType))
                continue;

            if (entityType.BaseType is not null)
                continue;

            var parameter = Expression.Parameter(entityType.ClrType, "e");
            var isDeleted = Expression.Property(parameter, nameof(Entity.IsDeleted));
            var filter    = Expression.Lambda(Expression.Not(isDeleted), parameter);

            entityType.SetQueryFilter(filter);
        }
    }
}