using MauiTaschenrechner.Core.ViewModel;
using MauiTaschenrechner.Enums;

namespace MauiTaschenrechner.Test
{
    
    public class MainPageViewModelTests
    {
        [Fact]
        public void Initial_CurrentNumber_IsZero()
        {
           // Arrange & Act
           var viewModel = new MainPageViewModel();

            // Assert
            Assert.Equal("0", viewModel.CurrentNumber);
        }

        [Fact]
        public void AddNumber_AppendsDigit()
        {
            // Arrange
            var vm = new MainPageViewModel();
            
            // Act
            vm.AddNumberCommand.Execute("3");

            // Assert
            Assert.Equal("3", vm.CurrentNumber);
        }

        [Fact]
        public void AddNumber_Zero_ReplacedWithDigit()
        {
            // Arrange
            var vm = new MainPageViewModel();
            vm.CurrentNumber = "0";

            // Act
            vm.AddNumberCommand.Execute("3");

            // Assert
            Assert.Equal("3", vm.CurrentNumber);
        }

        [Fact]
        public void AddNumber_MultipleDigits_CorrectResult()
        {
            // Arrange
            var vm = new MainPageViewModel();
            vm.CurrentNumber = "0";

            // Act
            vm.AddNumberCommand.Execute("1");
            vm.AddNumberCommand.Execute("2");
            vm.AddNumberCommand.Execute("3");

            // Assert
            Assert.Equal("123", vm.CurrentNumber);
        }



        [Fact]
        public void SetOperator_SetsEnumCorrectly() 
        {
            // Arrange
            var vm = new MainPageViewModel();

            // Act
            vm.SetOperatorCommand.Execute("Plus");

            // Assert
            Assert.Equal(Operators.Plus, vm.Op);
        }

        [Fact]
        public void SetOperator_SavesFirstNumber()
        {
            // Arrange
            var vm = new MainPageViewModel();
            vm.CurrentNumber = "42";

            // Act
            vm.SetOperatorCommand.Execute("Minus");

            // Assert
            Assert.Equal("42", GetPrivateField(vm, "_firstNumber"));
        }

        // hierbei wird mit Reflection das Private Feld _firstNumber herangezogen
        private static string GetPrivateField(MainPageViewModel vm, string fieldName)
        {
            var field = typeof(MainPageViewModel).GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (string)field?.GetValue(vm)!;
        }

        [Fact]
        public void SetOperator_ClearsCurrentNumber()
        {
            // Arrange
            var vm = new MainPageViewModel();
            vm.CurrentNumber = "123";

            // Act
            vm.SetOperatorCommand.Execute("Multiply");

            // Assert
            Assert.Equal("0", vm.CurrentNumber);
        }



        [Fact]
        public void Calculate_Addition_WorksCorrectly() 
        {
            // Arrange
            var vm = new MainPageViewModel();

            // Act
            vm.AddNumberCommand.Execute("2");
            vm.AddNumberCommand.Execute("0");
            vm.SetOperatorCommand.Execute("Plus");
            vm.AddNumberCommand.Execute("5");
            vm.CalculateResultCommand.Execute(null);

            // Assert
            Assert.Equal("25",vm.CurrentNumber);
        }

        [Fact]
        public void Calculate_Subtraction_WorksCorrectly()
        {
            // Arrange
            var vm = new MainPageViewModel();

            // Act
            vm.AddNumberCommand.Execute("4");
            vm.AddNumberCommand.Execute("6");
            vm.SetOperatorCommand.Execute("Minus");
            vm.AddNumberCommand.Execute("5");
            vm.CalculateResultCommand.Execute(null);

            // Assert
            Assert.Equal("41", vm.CurrentNumber);
        }

        [Fact]
        public void Calculate_Multiplication_WorksCorrectly()
        {
            // Arrange
            var vm = new MainPageViewModel();

            // Act
            vm.AddNumberCommand.Execute("4");
            
            vm.SetOperatorCommand.Execute("Multiply");
            vm.AddNumberCommand.Execute("5");
            vm.CalculateResultCommand.Execute(null);

            // Assert
            Assert.Equal("20", vm.CurrentNumber);
        }

        [Fact]
        public void Calculate_Devision_WorksCorrectly()
        {
            // Arrange
            var vm = new MainPageViewModel();

            // Act
            vm.AddNumberCommand.Execute("60");
            
            vm.SetOperatorCommand.Execute("Divide");
            vm.AddNumberCommand.Execute("5");
            vm.CalculateResultCommand.Execute(null);

            // Assert
            Assert.Equal("12", vm.CurrentNumber);
        }

        [Fact]
        public void Calculate_DevisionByZero_ShowsError()
        {
            // Arrange
            var vm = new MainPageViewModel();

            // Act
            vm.AddNumberCommand.Execute("60");

            vm.SetOperatorCommand.Execute("Divide");
            vm.AddNumberCommand.Execute("0");
            vm.CalculateResultCommand.Execute(null);

            // Assert
            Assert.Equal("ERROR", vm.CurrentNumber);
        }

        [Fact]
        public void Calculate_DivisionByZero_ThrowsException()
        {
            // Arrange
            var vm = new MainPageViewModel();
            
            // Act
            vm.AddNumberCommand.Execute("5");
            vm.SetOperatorCommand.Execute("Divide");
            vm.AddNumberCommand.Execute("0");

            //Assert
            Assert.Throws<DivideByZeroException>(() =>
            {
                vm.CalculateResultCommand.Execute(null);
            });
        }


        [Fact]
        public void SignChange_PositiveToNegative()
        {
            // Arrange
            var vm = new MainPageViewModel();

            // Act
            vm.CurrentNumber = "20";
            vm.SignChangeCommand.Execute(vm.CurrentNumber);

            Assert.Equal("-20",vm.CurrentNumber);
        
        }

        [Fact]
        public void SignChange_NegativeToPositive()
        {
            // Arrange
            var vm = new MainPageViewModel();

            // Act
            vm.CurrentNumber = "-20";
            vm.SignChangeCommand.Execute(vm.CurrentNumber);

            // Assert
            Assert.Equal("20", vm.CurrentNumber);

        }

        [Fact]
        public void Clear_ResetsAllFields()
        {
            // Arrange
            var vm = new MainPageViewModel();

            // Act
            vm.CurrentNumber = "6000";
            vm.ClearCommand.Execute(vm.CurrentNumber);

            // Assert
            Assert.Equal("0", vm.CurrentNumber);
        }

    }
}