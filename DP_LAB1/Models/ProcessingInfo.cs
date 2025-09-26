using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_LAB1.Models
{
    /// <summary>
    /// Незмінний результат обробки
    /// </summary>
    public class ProcessingResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public ListNode InputList { get; set; }
        public ListNode ResultList { get; set; }
        public ProcessingInfo Details { get; set; }

        private ProcessingResult(bool success, string error,
            ListNode input, ListNode result, ProcessingInfo details)
        {
            IsSuccess = success;
            ErrorMessage = error ?? "";
            InputList = input;
            ResultList = result;
            Details = details ?? new ProcessingInfo();
        }

        public static ProcessingResult Success(ListNode input, ListNode result, ProcessingInfo details) =>
            new ProcessingResult(true, null, input, result, details);

        public static ProcessingResult Error(string message) =>
            new ProcessingResult(false, message, null, null, null);

        public static ProcessingResult Empty() =>
            new ProcessingResult(true, null, null, null, new ProcessingInfo());
    }

    public class ProcessingInfo
    {
        public int InputLength { get; set; }
        public int NegativeCount { get; set; }
        public double ProductOfNegatives { get; set; }
        public string NegativeNumbersDisplay { get; set; }

        public ProcessingInfo() : this(0, 0, 1.0, "") { }

        public ProcessingInfo(int length, int negCount, double product, string display)
        {
            InputLength = length;
            NegativeCount = negCount;
            ProductOfNegatives = product;
            NegativeNumbersDisplay = display;
        }
    }
}
