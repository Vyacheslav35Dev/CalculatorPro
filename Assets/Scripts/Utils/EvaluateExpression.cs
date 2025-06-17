using System.Text.RegularExpressions;

namespace Utils
{
    public class ExpressionEvaluator
    {
        // Метод для проверки и вычисления выражения
        public string EvaluateExpression(string expression)
        {
            expression = expression.Replace(" ", "");
            string pattern = @"^(\d+)\+(\d+)$";

            Match match = Regex.Match(expression, pattern);

            if (!match.Success)
            {
                return "Error";
            }

            // Попытка преобразовать части в числа
            if (int.TryParse(match.Groups[1].Value, out int num1) &&
                int.TryParse(match.Groups[2].Value, out int num2))
            {
                int result = num1 + num2;
                return result.ToString();
            }
            else
            {
                return "Error";
            }
        }
    }
}