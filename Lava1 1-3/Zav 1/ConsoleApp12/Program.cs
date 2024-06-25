using System;
using System.Threading;

namespace timer
{
    public delegate void TimerEvent();
    public class Timer
    {
        private TimerEvent methodToExecute;
        private int interval;
        private bool isRunning;
        public Timer(int intervalS, TimerEvent methodExecut)
        {
            interval = intervalS * 1000; 
            methodToExecute = methodExecut;
            isRunning = false; 
        }
        public void Start()
        {
            if (!isRunning)
            {
                isRunning = true; 
                Thread thread = new Thread(RunTimer);
                thread.Start(); 
            }
        }
        private void RunTimer()
        {
            while (isRunning)
            {
                Thread.Sleep(interval); 
                methodToExecute(); 
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer(2, SomeMethod); // Виконання SomeMethod кожні 2 секунди
            timer.Start();
            Timer timer1 = new Timer(4, SomeMethod1); // Виконання SomeMethod1 кожні 4 секунди
            timer1.Start();
            Timer timer2 = new Timer(6, SomeMethod2); // Виконання SomeMethod2 кожні 6 секунд
            timer2.Start();
            Console.ReadLine();
        }
        static void SomeMethod()
        {
            Console.WriteLine("Dvorechenets");// Приклад методу, який виконується таймером
        }
        static void SomeMethod1()
        {
            Console.WriteLine("Ivan");//Ще один приклад методу, який виконується таймером
        }
        static void SomeMethod2()
        {
            Console.WriteLine("Valentinovich");// Ще один приклад методу, який виконується таймером
        }
    }
}
