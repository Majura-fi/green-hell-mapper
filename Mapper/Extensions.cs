namespace MapperSource;

public static class Extensions
{
    public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value)
    {
        if (dic.ContainsKey(key))
        {
            return false;
        }

        dic.Add(key, value);
        return true;
    }
}
