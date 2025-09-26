using System;
using System.Collections.Generic;
using System.Linq;

namespace DP_LAB1.Models
{
    /// <summary>
    /// Незмінний список рядків у декларативному стилі
    /// Кожна операція створює новий список замість зміни існуючого
    /// </summary>
    public sealed class ImmutableStringList
    {
        public static readonly ImmutableStringList Empty = new ImmutableStringList(Array.Empty<string>());

        private readonly string[] _items;

        private ImmutableStringList(string[] items)
        {
            _items = items;
        }

        public ImmutableStringList(IEnumerable<string> items)
        {
            _items = items?.ToArray() ?? Array.Empty<string>();
        }

        public int Count => _items.Length;

        public string this[int index]
        {
            get
            {
                if (index < 0 || index >= _items.Length)
                    throw new IndexOutOfRangeException();
                return _items[index];
            }
        }

        /// <summary>
        /// Додавання елементу. Створює новий список, на 1 елемент довший.
        /// </summary>
        public ImmutableStringList Add(string item)
        {
            string[] newItems = AddItemRecursive(_items, item, 0);
            return new ImmutableStringList(newItems);
        }

        /// <summary>
        /// Повертає копію внутрішнього масиву для збереження незмінності.
        /// </summary>
        public string[] ToArray()
        {
            return CopyRecursive(_items, 0);
        }

        /// <summary>
        /// Функція, що створює новий масив з доданим елементом.
        /// Вона будує масив "знизу вгору" (від кінця до початку).
        /// </summary>
        private static string[] AddItemRecursive(string[] source, string newItem, int index)
        {
            if (index >= source.Length)
            {
                var finalArray = new string[source.Length + 1];
                finalArray[index] = newItem;
                return finalArray;
            }

            string[] tailArray = AddItemRecursive(source, newItem, index + 1);

            tailArray[index] = source[index];

            return tailArray;
        }

        /// <summary>
        /// Функція, що створює точну копію масиву.
        /// </summary>
        private static string[] CopyRecursive(string[] source, int index)
        {
            if (index >= source.Length)
            {
                return new string[source.Length];
            }

            string[] copiedTail = CopyRecursive(source, index + 1);

            copiedTail[index] = source[index];

            return copiedTail;
        }
    }
}