using System;
using System.Linq;

namespace ArrayUtils
{
    public class Program
    {
        // Оголошуємо делегат для фільтрації чисел
        public delegate bool FilterDelegate(int number, int k);

        public static void Main()
        {
            // Зчитуємо масив чисел з консолі
            Console.Write("Введіть масив чисел через пробіл: ");
            int[] masiven = Console.ReadLine().Split().Select(int.Parse).ToArray();

            // Зчитуємо значення k з консолі
            Console.Write("Введіть значення k: ");
            int k = int.Parse(Console.ReadLine());

            // Призначаємо метод IsMultiple делегату FilterDelegate
            FilterDelegate filtermasiv = IsMultiple;

            // Використання методу Where для фільтрації масиву
            int[] filteredArrayWhere = FilterArrayUsingWhere(masiven, k, filtermasiv);
            Console.WriteLine("Відфільтрований масив за допомогою методу Where:");
            foreach (var num in filteredArrayWhere)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();

            // Використання кастомної реалізації для фільтрації масиву
            int[] filteredArrayCustom = FilterArrayWithoutLibrary(masiven, k, filtermasiv);
            Console.WriteLine("Відфільтрований масив із використанням спеціальної реалізації:");
            foreach (var num in filteredArrayCustom)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();

            // Очікуємо вводу від користувача перед закриттям програми
            Console.WriteLine("Натисніть Enter, щоб закрити програму.");
            Console.ReadLine();
        }

        // Метод для фільтрації масиву за допомогою LINQ Where
        public static int[] FilterArrayUsingWhere(int[] inputArray, int k, FilterDelegate filter)
        {
            var result = inputArray.Where(x => filter(x, k)).ToArray();
            return result;
        }

        // Метод для фільтрації масиву без використання бібліотек
        public static int[] FilterArrayWithoutLibrary(int[] inputArray, int k, FilterDelegate filter)
        {
            // Рахуємо кількість елементів, що відповідають умові
            int count = 0;
            for (int i = 0; i < inputArray.Length; i++)
            {
                if (filter(inputArray[i], k))
                {
                    count++;
                }
            }

            // Створюємо результуючий масив потрібного розміру
            int[] resultArray = new int[count];
            int index = 0;

            // Заповнюємо результуючий масив елементами, що відповідають умові
            for (int i = 0; i < inputArray.Length; i++)
            {
                if (filter(inputArray[i], k))
                {
                    resultArray[index] = inputArray[i];
                    index++;
                }
            }

            return resultArray;
        }

        // Метод для перевірки чи є число кратним до k
        static bool IsMultiple(int number, int k)
        {
            if (number == 0)
            {
                return false; // Нуль не є кратним до жодного числа
            }
            return number % k == 0; // Перевірка на кратність
        }
    }
}
