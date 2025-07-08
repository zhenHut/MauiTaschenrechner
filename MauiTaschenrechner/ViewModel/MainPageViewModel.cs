using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiTaschenrechner.Enums;

namespace MauiTaschenrechner.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        #region Fields
        private string _firstNumber = "";
        private bool IsEqualOk => this.Op != Operators.None;
        private const string _zeroNumber = "0";
        #endregion

        #region Property
        #pragma warning disable IDE0044 // Modifizierer "readonly" hinzufügen
        [ObservableProperty]
        private string _currentNumber = "0";
        
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CalculateResultCommand))]
        private Operators _op = Operators.None;

        #endregion

        #region Methods

        [RelayCommand]
        private void AddNumber(string number)
        {
            if (CurrentNumber == _zeroNumber)
            {
                CurrentNumber = number;
            }
            else
                CurrentNumber += number;
        }

        [RelayCommand(CanExecute = nameof(IsEqualOk))]
        private void CalculateResult()
        {
            switch (this.Op)
            {
                case Operators.Plus:
                    this.CurrentNumber =
                        (double.Parse(_firstNumber) + double.Parse(CurrentNumber)).ToString();
                    break;

                case Operators.Minus:
                    this.CurrentNumber =
                        (double.Parse(_firstNumber) - double.Parse(CurrentNumber)).ToString();
                    break;

                case Operators.Multiply:
                    this.CurrentNumber =
                        (double.Parse(_firstNumber) * double.Parse(CurrentNumber)).ToString();
                    break;

                case Operators.Devide:
                    this.CurrentNumber =
                        (double.Parse(_firstNumber) / double.Parse(CurrentNumber)).ToString();
                    break;

            }
            this.Op = Operators.None;
            //CalculateResultCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand]
        public void Clear()
        {
            this.Op = Operators.None;
            this._firstNumber = _zeroNumber;
            this.CurrentNumber = _zeroNumber;
            //CalculateResultCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand]
        private void SetOperator(string op) 
        {
            if (this.Op != Operators.None)
                CalculateResult();

            this.Op = Enum.Parse<Operators>(op);
            _firstNumber = CurrentNumber;
            CurrentNumber = _zeroNumber;
            //CalculateResultCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand]
        private void SignChange(string op)
        {
            if(CurrentNumber.StartsWith("-"))
                CurrentNumber = CurrentNumber.Substring(1);
            else if(CurrentNumber != _zeroNumber)
                CurrentNumber = "-" + CurrentNumber;
        }

        #endregion
    }
}

