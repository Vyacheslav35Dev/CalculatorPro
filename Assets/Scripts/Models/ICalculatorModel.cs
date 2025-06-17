namespace Models
{
    /// <summary>
    /// Interface defining the calculator's business logic.
    /// </summary>
    public interface ICalculatorModel
    {
        /// <summary>
        /// Calculates the sum of numbers in the expression.
        /// Returns null if the expression is invalid.
        /// </summary>
        float? Calculate(string expression);
    }
}