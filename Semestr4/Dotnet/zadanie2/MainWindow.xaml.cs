using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Zadanie2
{
    public partial class MainWindow : Window
    {
        private string _operation = string.Empty;
        private double _operand1;
        private double _operand2;
        private bool _isSecondOperand;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnDigitClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (Display.Text == "0" || _isSecondOperand)
            {
                Display.Text = button.Content.ToString();
                _isSecondOperand = false;
            }
            else
            {
                Display.Text += button.Content.ToString();
            }
        }

        private void OnClearClick(object sender, RoutedEventArgs e)
        {
            Display.Text = "0";
            PreviousOperation.Text = string.Empty;
            _operation = string.Empty;
            _isSecondOperand = false;
        }

        private void OnOperationClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            _operand1 = double.Parse(Display.Text);
            _operation = button.Content.ToString();
            PreviousOperation.Text = $"{_operand1} {_operation}";
            _isSecondOperand = true;
        }

        private void OnEqualsClick(object sender, RoutedEventArgs e)
        {
            _operand2 = double.Parse(Display.Text);

            double result = _operation switch
            {
                "+" => _operand1 + _operand2,
                "-" => _operand1 - _operand2,
                "*" => _operand1 * _operand2,
                "/" => _operand1 / _operand2,
                "^" => Math.Pow(_operand1, _operand2),
                "mod" => _operand1 % _operand2,
                "%" => (_operand1 / 100) * _operand2,
                _ => 0
            };

            Display.Text = result.ToString();
            PreviousOperation.Text = $"{_operand1} {_operation} {_operand2} = {result}";
            _isSecondOperand = true;
        }

        private void OnFunctionClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            double operand = double.Parse(Display.Text);
            double result = button.Content.ToString() switch
            {
                "√" => Math.Sqrt(operand),
                "1/x" => 1 / operand,
                "!" => Factorial(operand),
                "log" => Math.Log10(operand),
                "ln" => Math.Log(operand),
                "log2" => Math.Log2(operand),
                "⌊x⌋" => Math.Floor(operand),
                "⌈x⌉" => Math.Ceiling(operand),
                _ => 0
            };

            Display.Text = result.ToString();
            PreviousOperation.Text = $"{button.Content}({operand}) = {result}";
        }

        private double Factorial(double number)
        {
            if (number < 0)
                throw new ArgumentException("Liczba nie może być ujemna.");
            return Enumerable.Range(1, (int)number).Aggregate(1, (p, item) => p * item);
        }
    }
}
