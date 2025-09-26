using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_LAB1.Models
{
    /// <summary>
    /// Незмінний список рядків у декларативному стилі
    /// Кожна операція створює новий список замість зміни існуючого
    /// </summary>
    public class ImmutableStringList
    {
        private readonly string[] items;
        private readonly int count;

        private ImmutableStringList(string[] items, int count)
        {
            this.items = items;
            this.count = count;
        }

        public ImmutableStringList(int capacity)
        {
            items = new string[capacity];
            count = 0;
        }

        public int Count => count;

        public string this[int index]
        {
            get { return (index >= 0 && index < count) ? items[index] : null; }
        }

        /// <summary>
        /// ДЕКЛАРАТИВНЕ додавання елементу - повертає НОВИЙ список
        /// Не змінює поточний список, а створює новий
        /// </summary>
        public ImmutableStringList Add(string item)
        {
            if (count >= items.Length)
            {
                return this;
            }

            string[] newItems = CreateNewArrayWithItem(items, count, item);

            return new ImmutableStringList(newItems, count + 1);
        }

        /// <summary>
        /// Рекурсивне створення нового масиву з додатковим елементом
        /// </summary>
        private string[] CreateNewArrayWithItem(string[] source, int sourceCount, string newItem)
        {
            string[] result = new string[source.Length];

            CopyArrayRecursive(source, result, 0, sourceCount);

            result[sourceCount] = newItem;

            return result;
        }

        /// <summary>
        /// Рекурсивне копіювання масиву
        /// </summary>
        private void CopyArrayRecursive(string[] source, string[] target, int index, int maxIndex)
        {
            if (index >= maxIndex) return;

            target[index] = source[index];
            CopyArrayRecursive(source, target, index + 1, maxIndex);
        }

        /// <summary>
        /// Перетворення в масив (створює новий масив)
        /// </summary>
        public string[] ToArray()
        {
            string[] result = new string[count];
            CopyToArrayRecursive(result, 0);
            return result;
        }

        /// <summary>
        /// Рекурсивне копіювання в результуючий масив
        /// </summary>
        private void CopyToArrayRecursive(string[] target, int index)
        {
            if (index >= count) return;

            target[index] = items[index];
            CopyToArrayRecursive(target, index + 1);
        }
    }
}
