using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_LAB1.Models
{
    /// <summary>
    /// Представляє єдиний елемент у зв'язному списку.
    /// Може зберігати значення будь-якого типу та визначати, чи є воно числовим.
    /// </summary>
    public class ListElement
    {
        /// <summary>
        /// Збережене значення елемента.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Вказує, чи є збережене значення числовим типом або може бути перетворене на число.
        /// </summary>
        public bool IsNumeric { get; set; }

        /// <summary>
        /// Ініціалізує новий екземпляр елемента списку із заданим значенням.
        /// </summary>
        public ListElement(object value)
        {
            Value = value;
            IsNumeric = IsNumber(value);
        }

        /// <summary>
        /// Рекурсивно перевіряє, чи є передане значення числовим.
        /// </summary>
        private bool IsNumber(object value)
        {
            if (value == null) return false;

            if (value is int || value is double || value is float || value is decimal)
                return true;

            if (value is string str)
            {
                return IsStringNumberRecursive(str, 0, false, false);
            }

            return false;
        }

        /// <summary>
        /// Рекурсивно перевіряє, чи є рядок валідним числовим представленням.
        /// </summary>
        private bool IsStringNumberRecursive(string str, int index, bool hasDigit, bool hasDot)
        {
            if (index >= str.Length)
                return hasDigit;

            char currentChar = str[index];

            if (index == 0 && (currentChar == '+' || currentChar == '-'))
            {
                return IsStringNumberRecursive(str, index + 1, hasDigit, hasDot);
            }

            if (currentChar >= '0' && currentChar <= '9')
            {
                return IsStringNumberRecursive(str, index + 1, true, hasDot);
            }

            if (currentChar == '.' && !hasDot)
            {
                return IsStringNumberRecursive(str, index + 1, hasDigit, true);
            }

            return false;
        }

        /// <summary>
        /// Повертає числове значення елемента у форматі double.
        /// Якщо значення не є числовим, повертає 0.
        /// </summary>
        public double GetNumericValue()
        {
            if (!IsNumeric) return 0;

            if (Value is int intVal) return intVal;
            if (Value is double doubleVal) return doubleVal;
            if (Value is float floatVal) return floatVal;
            if (Value is decimal decimalVal) return (double)decimalVal;

            if (Value is string str)
            {
                return ParseStringToDoubleRecursive(str);
            }

            return 0;
        }

        /// <summary>
        /// Рекурсивно перетворює рядок на число типу double.
        /// </summary>
        private double ParseStringToDoubleRecursive(string str)
        {
            if (string.IsNullOrEmpty(str)) return 0;

            bool isNegative = str[0] == '-';
            int startIndex = (str[0] == '-' || str[0] == '+') ? 1 : 0;

            int dotPosition = FindDotPositionRecursive(str, startIndex);

            double result;

            if (dotPosition == -1)
            {
                result = ParseIntegerPartRecursive(str, startIndex, str.Length - 1);
            }
            else
            {
                double integerPart = ParseIntegerPartRecursive(str, startIndex, dotPosition - 1);
                double fractionalPart = ParseFractionalPartRecursive(str, dotPosition + 1, str.Length - 1);
                result = integerPart + fractionalPart;
            }

            return isNegative ? -result : result;
        }

        /// <summary>
        /// Рекурсивно знаходить позицію першої десяткової крапки в рядку.
        /// </summary>
        private int FindDotPositionRecursive(string str, int index)
        {
            if (index >= str.Length) return -1;
            if (str[index] == '.') return index;
            return FindDotPositionRecursive(str, index + 1);
        }

        /// <summary>
        /// Рекурсивно парсить цілу частину числового рядка.
        /// </summary>
        private double ParseIntegerPartRecursive(string str, int startIndex, int endIndex)
        {
            if (startIndex > endIndex) return 0;

            char lastChar = str[endIndex];
            if (lastChar < '0' || lastChar > '9') return 0;

            double lastDigit = lastChar - '0';
            double restValue = ParseIntegerPartRecursive(str, startIndex, endIndex - 1);

            return restValue * 10 + lastDigit;
        }

        /// <summary>
        /// Рекурсивно парсить дробову частину числового рядка.
        /// </summary>
        private double ParseFractionalPartRecursive(string str, int startIndex, int endIndex)
        {
            if (startIndex > endIndex) return 0;

            char firstChar = str[startIndex];
            if (firstChar < '0' || firstChar > '9') return 0;

            double firstDigit = firstChar - '0';
            double power = PowerOfTenRecursive(startIndex - (FindDotPositionRecursive(str, 0) + 1) + 1);
            double restValue = ParseFractionalPartRecursive(str, startIndex + 1, endIndex);

            return firstDigit / power + restValue;
        }

        /// <summary>
        /// Рекурсивно обчислює степінь числа 10.
        /// </summary>
        private double PowerOfTenRecursive(int power)
        {
            if (power <= 0) return 1;
            return 10 * PowerOfTenRecursive(power - 1);
        }

        /// <summary>
        /// Повертає рядкове представлення значення елемента.
        /// </summary>
        public override string ToString()
        {
            return Value?.ToString() ?? "null";
        }
    }
}
