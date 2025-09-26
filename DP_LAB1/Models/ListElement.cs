using System;
using System.Globalization; // Потрібен для надійного парсингу

namespace DP_LAB1.Models
{
    /// <summary>
    /// Елемент списку з неявною типізацією
    /// </summary>
    public class ListElement
    {
        public object Value { get; }
        public bool IsNumeric { get; }

        public ListElement(object value)
        {
            Value = value;
            IsNumeric = TryParseAsNumber(value, out _);
        }

        public double GetNumericValue() =>
            TryParseAsNumber(Value, out double result) ? result : 0;

        /// <summary>
        /// Чиста функція для перевірки та парсингу числа
        /// </summary>
        private static bool TryParseAsNumber(object value, out double result)
        {
            result = 0;

            return value switch
            {
                int i => SetResult(i, out result),
                double d => SetResult(d, out result),
                float f => SetResult(f, out result),
                decimal dec => SetResult((double)dec, out result),
                string s => ParseString(s, out result),
                _ => false
            };
        }

        private static bool SetResult(double value, out double result)
        {
            result = value;
            return true;
        }

        private static bool ParseString(string str, out double result)
        {
            result = 0;
            if (string.IsNullOrEmpty(str)) return false;

            return double.TryParse(str, out result);
        }

        public override string ToString() => Value?.ToString() ?? "null";
    }
}