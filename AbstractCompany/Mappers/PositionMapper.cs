using Domain;
using Entities;

namespace Mappers
{
    public static class PositionMapper
    {
        public static Position ToDomain(this PositionEntity position) =>
            new Position
            {
                Id = position.Id,
                Name = position.Name
            };

        public static PositionEntity ToEntity(this Position position) =>
            new PositionEntity
            {
                Id = position.Id,
                Name = position.Name
            };
    }
}