using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace server.Repository.Concrete {
	public interface IRepositoryBase<TEntity> {
		TEntity FindById(int id);
		IEnumerable<TEntity> FindAll();
		IEnumerable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);
		void Create(TEntity entity);
		void Update(TEntity entity);  
		void Remove(TEntity entity);
	
  }
}