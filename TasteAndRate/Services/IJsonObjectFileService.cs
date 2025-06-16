namespace TasteAndRate.Services
{
    public interface IJsonObjectFileService<T> where T : class
    {
        void Save(string filePath, T data);
        T Load(string filePath);
    }
}