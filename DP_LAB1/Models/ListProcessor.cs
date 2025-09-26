using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_LAB1.Models
{
    // <summary>
    /// Головний процесор для обробки списків
    /// </summary>
    public static class ListProcessor
    {
        /// <summary>
        /// Функція для обробки вхідного рядка
        /// </summary>
        public static ProcessingResult Process(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return ProcessingResult.Empty();

            try
            {
                return ProcessValidInput(input);
            }
            catch (Exception ex)
            {
                return ProcessingResult.Error($"Помилка при обробці: {ex.Message}");
            }
        }

        private static ProcessingResult ProcessValidInput(string input)
        {
            var elements = ListOperations.ParseInput(input);

            if (elements.Length == 0)
                return ProcessingResult.Error("Не вдалося розпарсити вхідні дані");

            var inputList = ListOperations.FromArray(elements);
            var resultList = ListOperations.ProcessVariant9(inputList);
            var details = CreateProcessingInfo(inputList);

            return ProcessingResult.Success(inputList, resultList, details);
        }

        private static ProcessingInfo CreateProcessingInfo(ListNode inputList)
        {
            var negatives = ListOperations.FilterNegatives(inputList);

            return new ProcessingInfo(
                ListOperations.Length(inputList),
                ListOperations.CountNegatives(inputList),
                ListOperations.ProductOfNegatives(inputList),
                ListOperations.FormatElements(negatives)
            );
        }
    }
}
