public interface IObjectFactory<T>
{
    public T GetNewObject();
    public T InitializeObject(T Tobject);
}