using BookLibrary.Common.Interfaces;
using BookLibrary.Storage.Structures;

namespace BookLibrary.Storage;

public static class CollectionStorage
{
    private static Dictionary<Type, IDbCollection>? _collections = null;

    public static void Init()
    {
        _collections = new Dictionary<Type, IDbCollection>();
    }

    public static void Register<T>() where T : Models.BaseModel
    {
        AssertInitialized();

        var type = typeof(T);
        _collections![type] = new HashSetStorage<T>();
    }

    public static IDbCollection<T> GetCollection<T>() where T : Models.BaseModel
    {
        AssertInitialized();

        var type = typeof(T);
        IDbCollection? result;
        _collections!.TryGetValue(type, out result);
        if (result is null)
        {
            throw new Exception($"Collection with type '{type.Name}' not registered");
        }

        return (IDbCollection<T>)result;
    }

    private static void AssertInitialized()
    {
        if (_collections is null)
        {
            throw new Exception("Firstly you must call init method");
        }
    }
}
