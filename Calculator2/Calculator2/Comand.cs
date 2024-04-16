using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Calculator2
{
    // Інтерфейс для визначення методу виконання команди
    public interface ICommand
    {
        void Execute();
    }

    // Клас, що представляє об'єкт команди калькулятора
    public class CalculatorCommand
    {
        private ICommand _command;

        // Метод для встановлення команди
        public void SetCommand(ICommand command) { _command = command; }

        // Метод для виконання команди
        public void ExecuteCommand() { _command.Execute(); }

    }

    // Команда для очищення текстового поля
    public class Clear : ICommand
    {
        private TextBox _textBox;

        public Clear(TextBox textBox)
        {
            _textBox = textBox;
        }

        public void Execute()
        {
            _textBox.Clear();

            _textBox.FontSize = 48;

        }
    }

    // Команда для обчислення виразу
    public class Equals : ICommand
    {
        private TextBox _textBox;

        public Equals(TextBox textBox) { _textBox = textBox; }

        public void Execute()
        {
            string expressi = _textBox.Text.Replace(',', '.');
            try
            {
                _textBox.Text = new DataTable().Compute(expressi, null).ToString();
            }
            catch (SyntaxErrorException)
            {
                // Обробка помилки у разі неправильного введення виразу
                MessageBox.Show("Помилка: неправильний вираз!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    // Команда для очищення останнього введеного значення
    public class ClearEntryCommand : ICommand
    {
        private TextBox _textBox;
        private string _pastAction;

        public ClearEntryCommand(TextBox textBox, string pastAction)
        {
            _textBox = textBox;
            _pastAction = pastAction;
        }

        public void Execute()
        {
            if (!string.IsNullOrEmpty(_pastAction))
            {
                _textBox.Text = _pastAction;
            }
        }
    }

    // Команда для додавання числа Пі до текстового поля
    public class PI : ICommand
    {
        private TextBox _textBox;

        public PI(TextBox textBox)
        {
            _textBox = textBox;
        }

        public void Execute()
        {
            _textBox.Text += "3,14";
        }
    }

    // Команда для видалення останнього символу з текстового поля
    public class Backspace : ICommand
    {
        private TextBox _textBox;

        public Backspace(TextBox textBox)
        {
            _textBox = textBox;
        }

        public void Execute()
        {
            if (_textBox.Text.Length > 0)
            {
                _textBox.Text = _textBox.Text.Remove(_textBox.Text.Length - 1);
            }
        }
    }

    // Команда для обчислення логарифму останнього числа
    public class Logarithm : ICommand
    {
        private TextBox _textBox;

        public Logarithm(TextBox textBox)
        {
            _textBox = textBox;
        }

        public void Execute()
        {
            // Розділяємо текст на числа та оператори
            string[] parts = _textBox.Text.Split(new char[] { '+', '*', '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0)
            {
                double logarithmValue;
                if (Double.TryParse(parts.Last(), out logarithmValue))
                {
                    // Обчислюємо логарифм з останнього числа
                    _textBox.Text = _textBox.Text.Remove(_textBox.Text.LastIndexOf(parts.Last())) + Math.Log(logarithmValue, 2).ToString();
                }
            }
        }
    }

    // Команда для переведення числа у відсоток
    public class Prochent : ICommand
    {
        private TextBox _textBox;

        public Prochent(TextBox textBox)
        {
            _textBox = textBox;
        }

        public void Execute()
        {
            AddPercentage();
        }

        private void AddPercentage()
        {
            if (decimal.TryParse(_textBox.Text, out decimal value))
            {
                value /= 100;
                _textBox.Text = value.ToString();
            }
        }
    }

    // Команда для обчислення квадратного кореня останнього числа
    public class SquareRoot : ICommand
    {
        private TextBox _textBox;

        public SquareRoot(TextBox textBox)
        {
            _textBox = textBox;
        }

        public void Execute()
        {
            // Розділяємо текст на числа та оператори
            string[] parts = _textBox.Text.Split(new char[] { '+', '*', '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0)
            {
                double lastNumber;
                if (Double.TryParse(parts.Last(), out lastNumber))
                {
                    if (lastNumber >= 0)
                    {
                        // Обчислюємо квадратний корінь з останнього числа
                        _textBox.Text = _textBox.Text.Remove(_textBox.Text.LastIndexOf(parts.Last())) + Math.Sqrt(lastNumber).ToString();
                    }
                }
            }
        }
    }

    // Команда для піднесення останнього числа до квадрату
    public class Square : ICommand
    {
        private TextBox _textBox;

        public Square(TextBox textBox)
        {
            _textBox = textBox;
        }

        public void Execute()
        {
            // Розділяємо текст на числа та оператори
            string[] parts = _textBox.Text.Split(new char[] { '-', '+', '*', '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0)
            {
                double lastNumber;
                if (Double.TryParse(parts.Last(), out lastNumber))
                {
                    // Підносимо останнє число до квадрату
                    _textBox.Text = _textBox.Text.Remove(_textBox.Text.LastIndexOf(parts.Last())) + Math.Pow(lastNumber, 2).ToString();
                }
            }
        }
    }

    // Команда для показу або приховування додаткових кнопок калькулятора
    public class DopCnopki : ICommand
    {
        private TextBox _textBox;
        private Button piButton;
        private Button percentageButton;
        private Button factorialButton;
        private Button squareButton;
        private Button logButton;

        public DopCnopki(TextBox textBox, Button piButton, Button percentageButton, Button factorialButton, Button squareButton, Button logButton)
        {
            _textBox = textBox;
            this.piButton = piButton;
            this.percentageButton = percentageButton;
            this.factorialButton = factorialButton;
            this.squareButton = squareButton;
            this.logButton = logButton;
        }

        public void Execute()
        {
            ShowHiddenButtons();
        }

        private void ShowHiddenButtons()
        {
            if (piButton.Visibility == Visibility.Visible)
            {
                // Приховати кнопки
                piButton.Visibility = Visibility.Collapsed;
                percentageButton.Visibility = Visibility.Collapsed;
                factorialButton.Visibility = Visibility.Collapsed;
                squareButton.Visibility = Visibility.Collapsed;
                logButton.Visibility = Visibility.Collapsed;

                // Змінити стовпчик прихованих кнопок на Auto
                Grid.SetColumn(piButton, 1);
                Grid.SetColumn(percentageButton, 1);
                Grid.SetColumn(factorialButton, 1);
                Grid.SetColumn(squareButton, 1);
                Grid.SetColumn(logButton, 1);
            }
            else
            {
                // Показати приховані кнопки
                piButton.Visibility = Visibility.Visible;
                percentageButton.Visibility = Visibility.Visible;
                factorialButton.Visibility = Visibility.Visible;
                squareButton.Visibility = Visibility.Visible;
                logButton.Visibility = Visibility.Visible;

                // Змінити стовпчик прихованих кнопок на автоматичний розмір
                Grid.SetColumn(piButton, 4);
                Grid.SetColumn(percentageButton, 4);
                Grid.SetColumn(factorialButton, 4);
                Grid.SetColumn(squareButton, 4);
                Grid.SetColumn(logButton, 4);
            }
        }
    }
}
