using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_LAB1.Models
{
    /// <summary>
    /// Надає набір статичних методів для виконання декларативних операцій над зв'язними списками.
    /// </summary>
    public static class ListOperations
    {
        /// <summary>
        /// Селектор для отримання головного елемента (голови) списку.
        /// </summary>
        public static ListElement SelectHead(ListNode list)
        {
            return list?.Head;
        }

        /// <summary>
        /// Селектор для отримання залишку списку (хвоста).
        /// </summary>
        public static ListNode SelectTail(ListNode list)
        {
            return list?.Tail;
        }

        /// <summary>
        /// Конструктор для створення нового вузла списку з голови та хвоста.
        /// </summary>
        public static ListNode Cons(ListElement head, ListNode tail)
        {
            if (head == null) return tail;
            return new ListNode(head, tail);
        }

        /// <summary>
        /// Рекурсивно обчислює довжину списку.
        /// </summary>
        public static int GetLength(ListNode list)
        {
            if (list == null)
                return 0;
            return 1 + GetLength(SelectTail(list));
        }

        /// <summary>
        /// Рекурсивно обчислює добуток від'ємних числових елементів у списку.
        /// </summary>
        public static double ProductOfNegativeNumbers(ListNode list)
        {
            if (list == null)
                return 1;

            ListElement head = SelectHead(list);
            ListNode tail = SelectTail(list);
            double tailProduct = ProductOfNegativeNumbers(tail);

            if (head != null && head.IsNumeric && head.GetNumericValue() < 0)
            {
                return head.GetNumericValue() * tailProduct;
            }

            return tailProduct;
        }

        /// <summary>
        /// Формує новий список з двох елементів: добутку від'ємних чисел та довжини вхідного списку.
        /// </summary>
        public static ListNode ProcessListVariant9(ListNode inputList)
        {
            double product = ProductOfNegativeNumbers(inputList);
            int length = GetLength(inputList);

            ListElement productElement = new ListElement(product);
            ListElement lengthElement = new ListElement(length);

            return Cons(productElement, Cons(lengthElement, null));
        }

        /// <summary>
        /// Рекурсивно перетворює зв'язний список у незмінний список рядків.
        /// </summary>
        public static ImmutableStringList ListToStringArray(ListNode list)
        {
            int length = GetLength(list);
            ImmutableStringList emptyList = new ImmutableStringList(length);
            return FillStringListRecursive(list, emptyList);
        }

        /// <summary>
        /// Допоміжний метод для рекурсивного заповнення незмінного списку рядків.
        /// </summary>
        private static ImmutableStringList FillStringListRecursive(ListNode list, ImmutableStringList currentList)
        {
            if (list == null) return currentList;

            ListElement head = SelectHead(list);
            ListNode tail = SelectTail(list);
            ImmutableStringList newList = currentList.Add(head.ToString());

            return FillStringListRecursive(tail, newList);
        }

        /// <summary>
        /// Рекурсивно створює зв'язний список з масиву рядків.
        /// </summary>
        public static ListNode StringArrayToList(string[] array)
        {
            if (array == null || array.Length == 0)
                return null;
            return BuildListFromArrayRecursive(array, 0);
        }

        /// <summary>
        /// Допоміжний метод для рекурсивної побудови списку з масиву.
        /// </summary>
        private static ListNode BuildListFromArrayRecursive(string[] array, int index)
        {
            if (index >= array.Length)
                return null;

            ListElement element = new ListElement(array[index]);
            ListNode tail = BuildListFromArrayRecursive(array, index + 1);
            return Cons(element, tail);
        }

        /// <summary>
        /// Рекурсивно підраховує кількість від'ємних чисел у списку.
        /// </summary>
        public static int CountNegativeNumbers(ListNode list)
        {
            if (list == null)
                return 0;

            ListElement head = SelectHead(list);
            ListNode tail = SelectTail(list);
            int headCount = (head != null && head.IsNumeric && head.GetNumericValue() < 0) ? 1 : 0;

            return headCount + CountNegativeNumbers(tail);
        }

        /// <summary>
        /// Рекурсивно створює новий список, що містить тільки від'ємні числа з вхідного списку.
        /// </summary>
        public static ListNode GetNegativeNumbersList(ListNode list)
        {
            if (list == null)
                return null;

            ListElement head = SelectHead(list);
            ListNode tail = SelectTail(list);
            ListNode tailNegatives = GetNegativeNumbersList(tail);

            if (head != null && head.IsNumeric && head.GetNumericValue() < 0)
            {
                return Cons(head, tailNegatives);
            }

            return tailNegatives;
        }

        /// <summary>
        /// Рекурсивно парсить вхідний рядок, розділяючи його на масив елементів.
        /// </summary>
        public static string[] ParseInputString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return new string[0];

            int count = CountElementsRecursive(input, 0, false);
            if (count == 0) return new string[0];

            string[] result = new string[count];
            ParseElementsRecursive(input, 0, result, 0, 0);
            return result;
        }

        /// <summary>
        /// Рекурсивно підраховує кількість елементів у рядку на основі роздільників.
        /// </summary>
        private static int CountElementsRecursive(string input, int index, bool inElement)
        {
            if (index >= input.Length)
                return inElement ? 1 : 0;

            char currentChar = input[index];
            bool isDelimiter = (currentChar == ',' || currentChar == ';' ||
                               currentChar == ' ' || currentChar == '\t');

            if (isDelimiter)
            {
                return (inElement ? 1 : 0) + CountElementsRecursive(input, index + 1, false);
            }
            else
            {
                return CountElementsRecursive(input, index + 1, true);
            }
        }

        /// <summary>
        /// Рекурсивно витягає елементи з рядка та заповнює ними масив.
        /// </summary>
        private static void ParseElementsRecursive(string input, int inputIndex,
            string[] result, int resultIndex, int elementStart)
        {
            if (inputIndex >= input.Length)
            {
                if (elementStart < inputIndex && resultIndex < result.Length)
                {
                    result[resultIndex] = ExtractAndTrimSubstring(input, elementStart, inputIndex - 1);
                }
                return;
            }

            char currentChar = input[inputIndex];
            bool isDelimiter = (currentChar == ',' || currentChar == ';' ||
                               currentChar == ' ' || currentChar == '\t');

            if (isDelimiter)
            {
                if (elementStart < inputIndex && resultIndex < result.Length)
                {
                    result[resultIndex] = ExtractAndTrimSubstring(input, elementStart, inputIndex - 1);
                    int nextElementStart = FindNextNonDelimiterRecursive(input, inputIndex);
                    ParseElementsRecursive(input, nextElementStart, result, resultIndex + 1, nextElementStart);
                }
                else
                {
                    int nextStart = FindNextNonDelimiterRecursive(input, inputIndex);
                    ParseElementsRecursive(input, nextStart, result, resultIndex, nextStart);
                }
            }
            else
            {
                ParseElementsRecursive(input, inputIndex + 1, result, resultIndex, elementStart);
            }
        }

        /// <summary>
        /// Рекурсивно знаходить індекс наступного символу, що не є роздільником.
        /// </summary>
        private static int FindNextNonDelimiterRecursive(string input, int index)
        {
            if (index >= input.Length) return index;

            char currentChar = input[index];
            bool isDelimiter = (currentChar == ',' || currentChar == ';' ||
                               currentChar == ' ' || currentChar == '\t');

            return isDelimiter ? FindNextNonDelimiterRecursive(input, index + 1) : index;
        }

        /// <summary>
        /// Витягує підрядок та рекурсивно очищає його від пробілів на початку та в кінці.
        /// </summary>
        private static string ExtractAndTrimSubstring(string source, int start, int end)
        {
            if (start > end || start < 0 || end >= source.Length) return "";

            int trimmedStart = FindFirstNonSpaceRecursive(source, start, end);
            if (trimmedStart > end) return "";

            int trimmedEnd = FindLastNonSpaceRecursive(source, trimmedStart, end);
            return ExtractSubstringRecursive(source, trimmedStart, trimmedEnd);
        }

        /// <summary>
        /// Рекурсивно знаходить індекс першого символу, що не є пробілом.
        /// </summary>
        private static int FindFirstNonSpaceRecursive(string source, int current, int end)
        {
            if (current > end) return current;
            return (source[current] == ' ' || source[current] == '\t')
                ? FindFirstNonSpaceRecursive(source, current + 1, end)
                : current;
        }

        /// <summary>
        /// Рекурсивно знаходить індекс останнього символу, що не є пробілом.
        /// </summary>
        private static int FindLastNonSpaceRecursive(string source, int start, int current)
        {
            if (current < start) return start - 1;
            return (source[current] == ' ' || source[current] == '\t')
                ? FindLastNonSpaceRecursive(source, start, current - 1)
                : current;
        }

        /// <summary>
        /// Рекурсивно витягує підрядок за заданими індексами.
        /// </summary>
        private static string ExtractSubstringRecursive(string source, int start, int end)
        {
            return (start > end) ? "" : BuildStringRecursive(source, start, end, start);
        }

        /// <summary>
        /// Рекурсивно будує рядок із символів вихідного рядка.
        /// </summary>
        private static string BuildStringRecursive(string source, int start, int end, int current)
        {
            if (current > end) return "";
            return source[current] + BuildStringRecursive(source, start, end, current + 1);
        }

        /// <summary>
        /// Рекурсивно форматує масив в рядок з комами
        /// </summary>
        public static string FormatStringArrayRecursive(string[] array, int index)
        {
            if (array == null || index >= array.Length)
                return "";

            if (index == array.Length - 1)
                return array[index];

            return array[index] + ", " + FormatStringArrayRecursive(array, index + 1);
        }
    }
}
