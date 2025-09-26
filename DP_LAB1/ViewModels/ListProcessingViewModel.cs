using DP_LAB1.Commands;
using DP_LAB1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DP_LAB1.ViewModels
{
    public class ListProcessingViewModel : INotifyPropertyChanged
    {
        private string _inputList = "";
        private string _inputListFormatted = "";
        private string _resultList = "";
        private bool _isProcessed = false;
        private ProcessingInfo _processingDetails = new();
        private string _errorMessage = "";
        private bool _hasError = false;

        public string InputList
        {
            get => _inputList;
            set
            {
                if (_inputList != value)
                {
                    _inputList = value;
                    OnPropertyChanged();
                    ClearResults();
                }
            }
        }

        public string InputListFormatted
        {
            get => _inputListFormatted;
            private set
            {
                if (_inputListFormatted != value)
                {
                    _inputListFormatted = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ResultList
        {
            get => _resultList;
            private set
            {
                if (_resultList != value)
                {
                    _resultList = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsProcessed
        {
            get => _isProcessed;
            private set
            {
                if (_isProcessed != value)
                {
                    _isProcessed = value;
                    OnPropertyChanged();
                }
            }
        }

        public ProcessingInfo ProcessingDetails
        {
            get => _processingDetails;
            private set
            {
                if (_processingDetails != value)
                {
                    _processingDetails = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            private set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool HasError
        {
            get => _hasError;
            private set
            {
                if (_hasError != value)
                {
                    _hasError = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ProcessListCommand { get; }

        public ListProcessingViewModel()
        {
            ProcessListCommand = new RelayCommand(ProcessList, CanProcessList);
        }

        private bool CanProcessList(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(InputList);
        }

        private void ProcessList(object? parameter)
        {
            try
            {
                ClearResults();

                string[] elements = ListOperations.ParseInputString(InputList);
                if (elements.Length == 0)
                {
                    SetError("Не вдалося розпарсити вхідні дані. Перевірте формат.");
                    return;
                }

                ListNode inputListNode = ListOperations.StringArrayToList(elements);

                ImmutableStringList inputForDisplay = ListOperations.ListToStringArray(inputListNode);
                InputListFormatted = FormatListForDisplay(inputForDisplay);

                ListNode resultListNode = ListOperations.ProcessListVariant9(inputListNode);

                ImmutableStringList resultForDisplay = ListOperations.ListToStringArray(resultListNode);
                ResultList = FormatListForDisplay(resultForDisplay);

                ProcessingDetails = new ProcessingInfo
                {
                    InputLength = ListOperations.GetLength(inputListNode),
                    NegativeCount = ListOperations.CountNegativeNumbers(inputListNode),
                    ProductOfNegatives = ListOperations.ProductOfNegativeNumbers(inputListNode),
                    NegativeNumbersDisplay = GetNegativeNumbersDisplay(inputListNode)
                };

                IsProcessed = true;
            }
            catch (Exception ex)
            {
                SetError($"Помилка при обробці: {ex.Message}");
            }
        }

        private void ClearResults()
        {
            InputListFormatted = "";
            ResultList = "";
            IsProcessed = false;
            ProcessingDetails = new ProcessingInfo();
            ErrorMessage = "";
            HasError = false;
        }

        private void SetError(string message)
        {
            ErrorMessage = message;
            HasError = true;
            IsProcessed = false;
        }

        private string FormatListForDisplay(ImmutableStringList list)
        {
            if (list.Count == 0) return "[]";

            string[] items = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                items[i] = list[i] ?? "null";
            }
            return "[" + string.Join(", ", items) + "]";
        }

        private string GetNegativeNumbersDisplay(ListNode list)
        {
            ListNode negativesList = ListOperations.GetNegativeNumbersList(list);
            if (negativesList == null) return "";

            ImmutableStringList negativesForDisplay = ListOperations.ListToStringArray(negativesList);
            if (negativesForDisplay.Count == 0) return "";

            string[] items = new string[negativesForDisplay.Count];
            for (int i = 0; i < negativesForDisplay.Count; i++)
            {
                items[i] = negativesForDisplay[i] ?? "null";
            }
            return string.Join(", ", items);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
