using System;

namespace DP_LAB1.Models
{
    /// <summary>
    /// Функції для роботи зі списками
    /// </summary>
    public static class ListOperations
    {
        /// <summary>
        /// Конструктор списку (аналог cons)
        /// </summary>
        public static ListNode Cons(ListElement element, ListNode tail) =>
            new ListNode(element, tail);

        /// <summary>
        /// Отримання голови списку
        /// </summary>
        public static ListElement Head(ListNode list) => list?.Element;

        /// <summary>
        /// Отримання хвоста списку
        /// </summary>
        public static ListNode Tail(ListNode list) => list?.Next;

        /// <summary>
        /// Перевірка чи список порожній
        /// </summary>
        public static bool IsEmpty(ListNode list) => list == null;

        /// <summary>
        /// Чиста функція: довжина списку
        /// </summary>
        public static int Length(ListNode list) =>
            IsEmpty(list) ? 0 : 1 + Length(Tail(list));

        /// <summary>
        /// Перетворення масиву в список
        /// </summary>
        public static ListNode FromArray(string[] array) =>
            FromArrayRec(array, 0);

        private static ListNode FromArrayRec(string[] array, int index) =>
            index >= array.Length
                ? null
                : Cons(new ListElement(array[index]), FromArrayRec(array, index + 1));

        /// <summary>
        /// Перетворення списку в масив
        /// </summary>
        public static string[] ToArray(ListNode list)
        {
            var length = Length(list);
            if (length == 0) return new string[0];

            var result = new string[length];
            FillArray(list, result, 0);
            return result;
        }

        private static void FillArray(ListNode list, string[] array, int index)
        {
            if (IsEmpty(list)) return;

            array[index] = Head(list).ToString();
            FillArray(Tail(list), array, index + 1);
        }

        /// <summary>
        /// ВАРІАНТ 9: Обробка списку для створення результату з двох елементів
        /// 1. Добуток від'ємних чисел
        /// 2. Довжина списку
        /// </summary>
        public static ListNode ProcessVariant9(ListNode inputList)
        {
            var productOfNegatives = ProductOfNegatives(inputList);
            var listLength = Length(inputList);

            return Cons(
                new ListElement(productOfNegatives),
                Cons(new ListElement(listLength), null));
        }

        /// <summary>
        /// Чиста функція: добуток від'ємних чисел у списку
        /// </summary>
        public static double ProductOfNegatives(ListNode list) =>
            IsEmpty(list)
                ? 1.0  // Нейтральний елемент для добутку
                : MultiplyIfNegative(Head(list)) * ProductOfNegatives(Tail(list));

        private static double MultiplyIfNegative(ListElement element)
        {
            if (element == null || !element.IsNumeric) return 1.0;

            var value = element.GetNumericValue();
            return value < 0 ? value : 1.0;
        }

        /// <summary>
        /// Чиста функція: кількість від'ємних чисел
        /// </summary>
        public static int CountNegatives(ListNode list) =>
            IsEmpty(list)
                ? 0
                : (IsNegative(Head(list)) ? 1 : 0) + CountNegatives(Tail(list));

        /// <summary>
        /// Чиста функція: перевірка чи елемент від'ємний
        /// </summary>
        private static bool IsNegative(ListElement element) =>
            element != null && element.IsNumeric && element.GetNumericValue() < 0;

        /// <summary>
        /// Чиста функція: фільтрація від'ємних чисел
        /// </summary>
        public static ListNode FilterNegatives(ListNode list) =>
            IsEmpty(list)
                ? null
                : IsNegative(Head(list))
                    ? Cons(Head(list), FilterNegatives(Tail(list)))
                    : FilterNegatives(Tail(list));

        /// <summary>
        /// Чиста функція: перетворення списку в рядок для відображення
        /// </summary>
        public static string FormatList(ListNode list) =>
            IsEmpty(list) ? "[]" : "[" + FormatElements(list) + "]";

        public static string FormatElements(ListNode list) =>
            IsEmpty(list)
                ? ""
                : IsEmpty(Tail(list))
                    ? Head(list).ToString()
                    : Head(list).ToString() + ", " + FormatElements(Tail(list));

        /// <summary>
        /// Парсинг вхідного рядка (використовуємо вбудовані функції тільки для парсингу)
        /// </summary>
        public static string[] ParseInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new string[0];

            // Використовуємо вбудовані функції для парсингу
            return input
                .Split(new char[] { ',', ';', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .ToArray();
        }
    }
}