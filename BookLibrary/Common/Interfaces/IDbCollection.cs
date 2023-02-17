using BookLibrary.Storage.Models;

namespace BookLibrary.Common.Interfaces;

public interface IDbCollection<T> : IDbCollection where T : BaseModel
{
    void Create(T entity);
    T Read(string id);
    void Update(T entity);
    void Delete(string id);
    IEnumerable<T> GetAll();
    IEnumerable<T> GetByCondition(Func<T, bool> condition);
    bool Exists(string id);
    bool Exists(Func<T, bool> condition);
    T? Find(string id);
    T? Find(Func<T, bool> condition);
}

public interface IDbCollection { }