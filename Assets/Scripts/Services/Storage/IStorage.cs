namespace Services.Storage
{
    /// <summary>
    /// Interface defining methods for persisting and retrieving calculation history data.
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Saves the provided calculation expressions and their corresponding results to persistent storage.
        /// </summary>
        /// <param name="expressions">Array of calculation expressions as strings.</param>
        /// <param name="results">Array of calculation results as strings.</param>
        void SaveHistory(string[] expressions, string[] results);

        /// <summary>
        /// Loads the calculation history from persistent storage.
        /// </summary>
        /// <returns>A tuple containing arrays of expressions and their corresponding results.</returns>
        (string[] expressions, string[] results) LoadHistory();
    }
}