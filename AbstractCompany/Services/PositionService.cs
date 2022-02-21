#region Usings

using Data.Repositories;
using Data.Repositories.Abstract;
using Domain;
using Mappers;
using Services.Abstract;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Services
{
    public class PositionService : IPositionService
    {
        #region Fields

        private readonly IPositionRepository _PositionRepository;

        #endregion

        #region Constructors

        public PositionService()
        {
            _PositionRepository = new PositionRepository();
        }

        public PositionService(IPositionRepository positionRepository)
        {
            _PositionRepository = positionRepository;
        }

        #endregion

        #region Methods

        public bool Add(Position entity) => _PositionRepository.Add(entity.ToEntity());

        public bool Update(Position entity) => _PositionRepository.Update(entity.ToEntity());

        public bool Delete(int id) => _PositionRepository.Delete(id);

        public Position Get(int id) => _PositionRepository.Get(id).ToDomain();

        public IEnumerable<Position> GetAll() =>
            _PositionRepository.GetAll()
                .Select(position => position.ToDomain());

        #endregion
    }
}