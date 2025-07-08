using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiTaschenrechner.Enums;

namespace MauiTaschenrechner.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        #region Fields
        private string _firstNumber = "";
        private bool IsEqualOk => Op != Operators.None;
        private const string _zeroNumber = "0";
        #endregion

        #region Property
        #pragma warning disable IDE0044
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
            switch (Op)
            {
                case Operators.Plus:
                    CurrentNumber =
                        (double.Parse(_firstNumber) + double.Parse(CurrentNumber)).ToString();
                    break;

                case Operators.Minus:
                    CurrentNumber =
                        (double.Parse(_firstNumber) - double.Parse(CurrentNumber)).ToString();
                    break;

                case Operators.Multiply:
                    CurrentNumber =
                        (double.Parse(_firstNumber) * double.Parse(CurrentNumber)).ToString();
                    break;

                case Operators.Devide:
                    CurrentNumber =
                        (double.Parse(_firstNumber) / double.Parse(CurrentNumber)).ToString();
                    break;
            }
            this.Op = Operators.None;

        }

        [RelayCommand]
        public void Clear()
        {
            Op = Operators.None;
            _firstNumber = _zeroNumber;
            CurrentNumber = _zeroNumber;
        }

        [RelayCommand]
        private void SetOperator(string op)
        {
            if (Op != Operators.None)
                CalculateResult();

            Op = Enum.Parse<Operators>(op);
            _firstNumber = CurrentNumber;
            CurrentNumber = _zeroNumber;
        }

        [RelayCommand]
        private void SignChange(string op)
        {
            if (CurrentNumber.StartsWith("-"))
                CurrentNumber = CurrentNumber.Substring(1);
            else if (CurrentNumber != _zeroNumber)
                CurrentNumber = "-" + CurrentNumber;
        }

        #endregion
    }
}

