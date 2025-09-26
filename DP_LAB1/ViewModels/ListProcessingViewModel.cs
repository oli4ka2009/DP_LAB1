using CommunityToolkit.Mvvm.ComponentModel;
using DP_LAB1.Models;
using System;

namespace DP_LAB1.ViewModels
{
    public partial class ListProcessingViewModel : ObservableObject
    {
        /// <summary>
        /// Внутрішній клас для інкапсуляції результатів обробки.
        /// </summary>
        private class ProcessingResult
        {
            public bool IsSuccess { get; }
            public string ErrorMessage { get; }
            public ImmutableStringList InputList { get; }
            public ImmutableStringList ResultList { get; }
            public ProcessingInfo Details { get; }

            private ProcessingResult(ImmutableStringList input, ImmutableStringList result, ProcessingInfo details)
            {
                IsSuccess = true;
                ErrorMessage = string.Empty;
                InputList = input;
                ResultList = result;
                Details = details;
            }

            private ProcessingResult(string errorMessage)
            {
                IsSuccess = false;
                ErrorMessage = errorMessage;
                InputList = new ImmutableStringList(0);
                ResultList = new ImmutableStringList(0);
                Details = new ProcessingInfo();
            }

            public static ProcessingResult Create(string rawInput)
            {
                if (string.IsNullOrWhiteSpace(rawInput))
                {
                    return new ProcessingResult(new ImmutableStringList(0), new ImmutableStringList(0), new ProcessingInfo());
                }

                try
                {
                    string[] elements = ListOperations.ParseInputString(rawInput);
                    if (elements.Length == 0)
                    {
                        return new ProcessingResult("Не вдалося розпарсити вхідні дані. Перевірте формат.");
                    }

                    ListNode inputListNode = ListOperations.StringArrayToList(elements);

                    var details = new ProcessingInfo
                    {
                        InputLength = ListOperations.GetLength(inputListNode),
                        NegativeCount = ListOperations.CountNegativeNumbers(inputListNode),
                        ProductOfNegatives = ListOperations.ProductOfNegativeNumbers(inputListNode),
                        NegativeNumbersDisplay = GetNegativeNumbersDisplay(inputListNode)
                    };

                    ListNode resultListNode = ListOperations.ProcessListVariant9(inputListNode);

                    return new ProcessingResult(
                        ListOperations.ListToStringArray(inputListNode),
                        ListOperations.ListToStringArray(resultListNode),
                        details);
                }
                catch (Exception ex)
                {
                    return new ProcessingResult($"Помилка при обробці: {ex.Message}");
                }
            }
            private static string GetNegativeNumbersDisplay(ListNode list)
            {
                ListNode negativesList = ListOperations.GetNegativeNumbersList(list);
                if (negativesList == null) return "";

                ImmutableStringList negativesForDisplay = ListOperations.ListToStringArray(negativesList);
                if (negativesForDisplay.Count == 0) return "";

                return ListOperations.FormatStringArrayRecursive(negativesForDisplay.ToArray(), 0);
            }
        }

        [ObservableProperty]
        private string _inputList = "";

        private ProcessingResult _processingResult;

        public ListProcessingViewModel()
        {
            _processingResult = ProcessingResult.Create(_inputList);
        }

        partial void OnInputListChanged(string value)
        {
            ProcessList();
        }

        public string InputListFormatted => FormatListForDisplay(_processingResult.InputList);
        public string ResultList => FormatListForDisplay(_processingResult.ResultList);
        public bool IsProcessed => _processingResult.IsSuccess && _processingResult.InputList.Count > 0;
        public ProcessingInfo ProcessingDetails => _processingResult.Details;
        public string ErrorMessage => _processingResult.ErrorMessage;
        public bool HasError => !_processingResult.IsSuccess;

        private void ProcessList()
        {
            _processingResult = ProcessingResult.Create(InputList);

            OnPropertyChanged(nameof(InputListFormatted));
            OnPropertyChanged(nameof(ResultList));
            OnPropertyChanged(nameof(IsProcessed));
            OnPropertyChanged(nameof(ProcessingDetails));
            OnPropertyChanged(nameof(ErrorMessage));
            OnPropertyChanged(nameof(HasError));
        }

        private string FormatListForDisplay(ImmutableStringList list)
        {
            if (list.Count == 0) return "[]";
            return "[" + ListOperations.FormatStringArrayRecursive(list.ToArray(), 0) + "]";
        }
    }
}