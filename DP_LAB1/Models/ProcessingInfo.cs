using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_LAB1.Models
{
    /// <summary>
    /// Містить детальну інформацію про результати обробки списку.
    /// </summary>
    public class ProcessingInfo
    {
        /// <summary>
        /// Довжина вхідного списку.
        /// </summary>
        public int InputLength { get; set; }

        /// <summary>
        /// Кількість від'ємних чисел у списку.
        /// </summary>
        public int NegativeCount { get; set; }

        /// <summary>
        /// Добуток від'ємних чисел у списку.
        /// </summary>
        public double ProductOfNegatives { get; set; }

        /// <summary>
        /// Рядкове представлення списку від'ємних чисел.
        /// </summary>
        public string NegativeNumbersDisplay { get; set; } = "";
    }
}
