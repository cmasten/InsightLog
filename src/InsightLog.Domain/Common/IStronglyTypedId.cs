public interface IStronglyTypedId<T> where T : notnull
{
    T Value { get; }
}
