namespace BookLibrary.Common.Extensions;

public static class IEnumerableExtensions
{
    public static IEnumerable<T> GetUnincluded<T>(this IEnumerable<T> source, IEnumerable<T> inclusion) where T: class
    {
        return source.Where(x => !inclusion.Contains(x));
    }
}
