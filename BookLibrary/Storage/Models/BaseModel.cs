namespace BookLibrary.Storage.Models;

public class BaseModel
{
    public string Id { get; }

    public BaseModel(string id)
    {
        Id = string.IsNullOrEmpty(id) ? Guid.NewGuid().ToString() : id;
    }

    public BaseModel()
    {
        Id = Guid.NewGuid().ToString();
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
