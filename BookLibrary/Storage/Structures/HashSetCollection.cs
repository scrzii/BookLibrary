using BookLibrary.Common.Interfaces;
using BookLibrary.Storage.Models;

namespace BookLibrary.Storage.Structures;

public class HashSetStorage<T> : IDbCollection<T> where T : BaseModel
{
    private HashSet<T> _entities;

    public HashSetStorage()
    {
        _entities = new HashSet<T>();
    }

    public void Create(T entity)
    {
        lock (_entities)
        {
            var target = Find(entity.Id);
            AssertNull(target);

            _entities.Add(entity);
        }
    }

    public void Delete(string id)
    {
        lock (_entities)
        {
            var target = Find(id);
            AssertNotNull(target, id);

            _entities.Remove(target!);
        }
    }

    public IEnumerable<T> GetAll()
    {
        lock (_entities)
        {
            return _entities.ToArray();
        }
    }

    public IEnumerable<T> GetByCondition(Func<T, bool> condition)
    {
        lock (_entities)
        {
            return _entities.Where(condition);
        }
    }

    public void Update(T entity)
    {
        lock (_entities)
        {
            var target = Find(entity.Id);
            AssertNotNull(target, entity.Id);

            _entities.Remove(target!);
            _entities.Add(entity);
        }
    }

    public T Read(string id)
    {
        lock (_entities)
        {
            var target = Find(id);
            AssertNotNull(target, id);

            return target!;
        }
    }

    public bool Exists(string id)
    {
        lock (_entities)
        {
            return Find(id) is not null;
        }
    }

    public bool Exists(Func<T, bool> condition)
    {
        lock (_entities)
        {
            return Find(condition) is not null;
        }
    }

    public T? Find(string id)
    {
        return Find(x => x.Id == id);
    }

    public T? Find(Func<T, bool> condition)
    {
        return _entities.FirstOrDefault(condition);
    }

    private static void AssertNull(T? entity)
    {
        if (entity is not null)
        {
            throw new Exception($"Element exists. Id: {entity.Id}");
        }
    }

    private static void AssertNotNull(T? entity, string id)
    {
        if (entity is null)
        {
            throw new Exception($"Element does not exist. Id: {id}");
        }
    }
}
