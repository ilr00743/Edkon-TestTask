namespace Services.SaveLoad
{
    public interface ILoader
    {
        T Load<T>(string key);
    }
}