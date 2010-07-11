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

        private void OnClickDigit(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int digit = Convert.ToInt32(btn.Content.ToString());
            HandleDigit(digit);
        }

        private void OnClickOperator(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Tag != null)
            {
                Operator op = (Operator)btn.Tag;
                ExecuteLastOperator(op);
            }
        }

        private void OnClickDecimal(object sender, RoutedEventArgs e)
        {
            HandleDecimal();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Resources["myBrush"] = new SolidColorBrush(Colors.Green);
            Application.Current.Resources["myBrush"] = new SolidColorBrush(Colors.Green);
            button9.BorderBrush = (Brush)TryFindResource("myBrush");
        }
    }
}
