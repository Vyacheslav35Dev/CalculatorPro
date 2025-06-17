namespace Services.Storage
{
    /// <summary>
    /// Interface for saving and loading calculation history.
    /// </summary>
    public interface IStorage
    {
        void SaveHistory(string[] expressions, float[] results);
        (string[] expressions, float[] results) LoadHistory();
    }
}