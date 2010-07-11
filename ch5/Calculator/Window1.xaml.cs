using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    public enum Operator
    {
        None = 0,
        Plus,
        Minus,
        Times,
        Divide,
        Equals
    }

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private Operator lastOperator = Operator.None;
        private decimal valueSoFar = 0;
        private bool numberHitSinceLastOperator = false;

        public Window1()
        {
            InitializeComponent();
        }

        private void ExecuteLastOperator(Operator newOperator)
        {
            decimal currentValue = Convert.ToDecimal(textBoxDisplay.Text);
            decimal newValue = currentValue;

            if (numberHitSinceLastOperator)
            {
                switch (lastOperator)
                {
                    case Operator.Plus:
                        newValue = valueSoFar + currentValue;
                        break;
                    case Operator.Minus:
                        newValue = valueSoFar - currentValue;
                        break;
                    case Operator.Times:
                        newValue = valueSoFar * currentValue;
                        break;
                    case Operator.Divide:
                        if (currentValue == 0)
                            newValue = 0;
                        else
                            newValue = valueSoFar / currentValue;
                        break;
                    case Operator.Equals:
                        newValue = currentValue;
                        break;
                }
            }

            valueSoFar = newValue;
            lastOperator = newOperator;
            numberHitSinceLastOperator = false;
            textBoxDisplay.Text = valueSoFar.ToString();
        }

        private void HandleDigit(int digit)
        {
            string valueSoFar = numberHitSinceLastOperator ? textBoxDisplay.Text : "";
            string newValue = valueSoFar + digit.ToString();

            textBoxDisplay.Text = newValue;
            numberHitSinceLastOperator = true;
        }

        private void HandleDecimal()
        {
            string valueSoFar = numberHitSinceLastOperator ? textBoxDisplay.Text : "";
            string newValue = "";

            if (valueSoFar.IndexOf(".") < 0)
            {
                if (valueSoFar.Length == 0)
                    newValue = "0.";
                else
                    newValue = valueSoFar + ".";
            }
            else
                newValue = valueSoFar;

            textBoxDisplay.Text = newValue;
            numberHitSinceLastOperator = true;
        }

        private void OnClick0(object sender, RoutedEventArgs e)
        {
            HandleDigit(0);
        }

        private void OnClick1(object sender, RoutedEventArgs e)
        {
            HandleDigit(1);
        }

        private void OnClick2(object sender, RoutedEventArgs e)
        {
            HandleDigit(2);
        }

        private void OnClick3(object sender, RoutedEventArgs e)
        {
            HandleDigit(3);
        }

        private void OnClick4(object sender, RoutedEventArgs e)
        {
            HandleDigit(4);
        }

        private void OnClick5(object sender, RoutedEventArgs e)
        {
            HandleDigit(5);
        }

        private void OnClick6(object sender, RoutedEventArgs e)
        {
            HandleDigit(6);
        }

        private void OnClick7(object sender, RoutedEventArgs e)
        {
            HandleDigit(7);
        }

        private void OnClick8(object sender, RoutedEventArgs e)
        {
            HandleDigit(8);
        }

        private void OnClick9(object sender, RoutedEventArgs e)
        {
            HandleDigit(9);
        }

        private void OnClickDivide(object sender, RoutedEventArgs e)
        {
            ExecuteLastOperator(Operator.Divide);
        }

        private void OnClickTimes(object sender, RoutedEventArgs e)
        {
            ExecuteLastOperator(Operator.Times);
        }

        private void OnClickMinus(object sender, RoutedEventArgs e)
        {
            ExecuteLastOperator(Operator.Minus);
        }

        private void OnClickDecimal(object sender, RoutedEventArgs e)
        {
            HandleDecimal();
        }

        private void OnClickEquals(object sender, RoutedEventArgs e)
        {
            ExecuteLastOperator(Operator.Equals);
        }

        private void OnClickPlus(object sender, RoutedEventArgs e)
        {
            ExecuteLastOperator(Operator.Plus);
        }
    }
}
