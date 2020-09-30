using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repository.Concrete;

namespace server.Repository.Abstract {
	public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class {
		protected TodoContext TodoContext { get; set; }
		public RepositoryBase (TodoContext todoContext) {
			
			this.TodoContext = todoContext; 
		}

		public TEntity FindById(int id) {

			return this.TodoContext.Set<TEntity>().Find(id);
		}
		
		public IEnumerable<TEntity> FindAll() { 

			return this.TodoContext.Set<TEntity>().ToList();
		}
		
		public IEnumerable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression) {			

			return this.TodoContext.Set<TEntity>().Where(expression).ToList();
		}

		
		public void Create(TEntity entity) {

			this.TodoContext.Set<TEntity> ().Add(entity);
		}
		
		public void Update(TEntity entity) {
			
			 this.TodoContext.Entry(entity).State = EntityState.Modified;
		
		}
		
		public void Remove(TEntity entity) {
			
			this.TodoContext.Set<TEntity>().Remove(entity);
		
		}

	
		
  }
}