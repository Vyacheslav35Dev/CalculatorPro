using System;
using System.Text.RegularExpressions;

namespace Models
{
    /// <summary>
    /// Implementation of the calculator logic.
    /// Supports only addition of numbers separated by '+'.
    /// </summary>
    public class CalculatorModel : ICalculatorModel
    {
        public float? Calculate(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                return null;

            // Validate that expression contains only digits, plus signs, and whitespace.
            if (!Regex.IsMatch(expression, @"^[\d+\s]+$"))
                return null;

            try
            {
                // Split the expression by '+' to get individual numbers.
                string[] parts = expression.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                float sum = 0;
                foreach (var part in parts)
                {
                    // Parse each part to float after trimming whitespace.
                    if (float.TryParse(part.Trim(), out float number))
                        sum += number;
                    else
                        return null; // Parsing failed, invalid input.
                }
                return sum;
            }
            catch
            {
                // Catch any unexpected exceptions and return null.
                return null;
            }
        }
    }
}