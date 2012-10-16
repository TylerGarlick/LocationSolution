using System.Collections.Generic;

namespace LocationSolution.Services.Common
{
    public interface ISharedOperations<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
