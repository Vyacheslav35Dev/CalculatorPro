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
        // Regular expression to validate that the expression contains only digits, plus signs, and whitespace.
        private static readonly Regex ValidExpressionRegex = new Regex(@"^[\d+\s]+$", RegexOptions.Compiled);

        public float? Calculate(string expression)
        {
            // Check for null or whitespace input
            if (string.IsNullOrWhiteSpace(expression))
                return null;

            // Validate that the expression contains only allowed characters
            if (!ValidExpressionRegex.IsMatch(expression))
                return null;

            // Split the expression by '+' and remove empty entries
            var parts = expression.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);

            float sum = 0f;
            foreach (var part in parts)
            {
                string trimmedPart = part.Trim();
                // Try to parse each part to a float
                if (float.TryParse(trimmedPart, out float number))
                {
                    sum += number; // Add to total sum
                }
                else
                {
                    // Parsing failed, invalid input
                    return null;
                }
            }

            // Return the calculated sum
            return sum;
        }
    }
}