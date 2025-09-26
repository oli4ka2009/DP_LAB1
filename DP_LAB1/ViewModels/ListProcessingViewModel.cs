using CommunityToolkit.Mvvm.ComponentModel;
using DP_LAB1.Models;
using System;

namespace DP_LAB1.ViewModels
{
    public partial class ListProcessingViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(InputListFormatted))]
        [NotifyPropertyChangedFor(nameof(ResultList))]
        [NotifyPropertyChangedFor(nameof(IsProcessed))]
        [NotifyPropertyChangedFor(nameof(ProcessingDetails))]
        [NotifyPropertyChangedFor(nameof(ErrorMessage))]
        [NotifyPropertyChangedFor(nameof(HasError))]
        private string _inputList = "";

        private ProcessingResult _processingResult = ProcessingResult.Empty();

        partial void OnInputListChanged(string value)
        {
            _processingResult = ListProcessor.Process(value);
        }

        public string InputListFormatted => ListOperations.FormatList(_processingResult.InputList);
        public string ResultList => ListOperations.FormatList(_processingResult.ResultList);
        public bool IsProcessed => _processingResult.IsSuccess && ListOperations.Length(_processingResult.InputList) > 0;
        public ProcessingInfo ProcessingDetails => _processingResult.Details;
        public string ErrorMessage => _processingResult.ErrorMessage;
        public bool HasError => !_processingResult.IsSuccess;
    }
}