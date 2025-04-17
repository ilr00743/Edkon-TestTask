namespace Services.SaveLoad
{
    public interface ISaver
    {
        void Save<T>(string key, T data);
    }
}