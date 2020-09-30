using System;
using server.Repository.Concrete;

namespace server.Repository
{
    public interface IRepositoryWrap : IDisposable
    {
         IUserRepository User { get; } 
         IActivityRepository Activity { get; }
         void Save();

  }
}