using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Calculator2
{
    public partial class MainWindow : Window
    {
        private readonly CalculatorCommand _calculatorCommand = new CalculatorCommand(); // Екземпляр класу команди калькулятора
        public string _pastaction; // Зберігає попередню дію користувача

        // Ініціалізація подій кнопок
        private void InitializeButtons()
        {
            // Проходимося по всіх дочірніх елементах головного контейнера
            foreach (UIElement el in MainRoo.Children)
            {
                // Якщо елемент є кнопкою, додаємо обробник подій клацання
                if (el is Button)
                {
                    ((Button)el).Click += Button_Click;
                }
            }
        }

        // Обробник подій для клацання на кнопки
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string comandstr = button.Content.ToString();
            ICommand command = null;

            // Визначаємо, яку саме кнопку натиснули та створюємо відповідну команду
            switch (comandstr)
            {
                // Очищення тексту
                case "C":
                    command = new Clear(textLabel);
                    _pastaction = null;
                    break;
                // Виконання обчислень
                case "=":
                    command = new Equals(textLabel);
                    break;
                // Очищення останнього введеного значення
                case "CE":
                    command = new ClearEntryCommand(textLabel, _pastaction);
                    break;
                // Видалення одного символу назад
                case "⬅︎":
                    command = new Backspace(textLabel);
                    break;
                // Обчислення квадратного кореня
                case "√":
                    command = new SquareRoot(textLabel);
                    break;
                // Виведення числа Пі
                case "Pi":
                    command = new PI(textLabel);
                    break;
                // Обчислення відсотка
                case "%":
                    command = new Prochent(textLabel);
                    break;
                // Піднесення до квадрату
                case "^2":
                    command = new Square(textLabel);
                    break;
                // Обчислення логарифма
                case "log":
                    command = new Logarithm(textLabel);
                    break;
                // Додаткові кнопки
                case "(=":
                    // Створюємо екземпляр класу DopCnopki з необхідними аргументами
                    command = new DopCnopki(textLabel, piButton, percentageButton, factorialButton, squareButton, logButton);
                    break;
            }
            // Виконуємо команду, якщо вона є, або додаємо символ до тексту
            if (command != null)
            {
                _calculatorCommand.SetCommand(command);
                _calculatorCommand.ExecuteCommand();
            }
            else
            {
                textLabel.Text += comandstr;
                _pastaction += comandstr;
            }
        }

        // Обробник події зміни тексту в мітці
        private void text_TextChanged(object sender, TextChangedEventArgs e)
        {
            FormattedText formattedText = new FormattedText(textLabel.Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(textLabel.FontFamily, textLabel.FontStyle, textLabel.FontWeight, textLabel.FontStretch), textLabel.FontSize, Brushes.Black, new NumberSubstitution(), TextFormattingMode.Display);

            double textWidth = formattedText.WidthIncludingTrailingWhitespace;

            // Якщо текст виходить за межі поля, зменшуємо розмір шрифту
            if (textWidth > textLabel.ActualWidth)
            {
                textLabel.FontSize -= 3; // Можна налаштувати інший крок зменшення
            }
        }
    }
}
