using System;
using System.Timers;
using System.Drawing;
using System.Collections.Generic;

namespace Buses
{
    /// <summary>
    /// Класс, описывающий автобус
    /// </summary>
    class Bus
    {
        /// <summary>
        /// Статическое поле, с помощью которого генерируем случайные значения
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Список точек, через которые проходит маршрут
        /// </summary>
        private static List<Graph.GraphTop> route = new List<Graph.GraphTop>();
        /// <summary>
        /// Свойство, возвращающее маршрут
        /// </summary>
        public static List<Graph.GraphTop> Route => route;
        /// <summary>
        /// Список длин путей
        /// </summary>
        private static List<double> lengthOfRoute = new List<double>();
        public static List<double> LengthOfRoute => lengthOfRoute;

        /// <summary>
        /// Скорость автобуса
        /// </summary>
        private static uint speed = 50;
        public static uint Speed
        {
            get => speed;
            set
            {
                if ((value <= 90) && (value != 0))
                    speed = value;
                else if (value == 0)
                    throw new ArgumentException($"Скорость не может быть равна 0!");
                else
                    throw new ArgumentException($"Слишком большая скорость для автобуса: " +
                                                $"максимальная = 90, ввели {value}");
            }
        }

        /// <summary>
        /// Интервал движения автобусов
        /// </summary>
        private static double interval;
        /// <summary>
        /// Свойство, возвращающее интервал движения автобусов
        /// </summary>
        public static double Interval => interval;

        /// <summary>
        /// Метод, с помощью которого добавляем новую вершину в путь
        /// </summary>
        /// <param name="newTop"></param>
        public static void AddTopOfRoute(Graph.GraphTop newTop)
        {
            // Добавляем новую точку и вычисляем длину до нее
            route.Add(newTop);
            if (lengthOfRoute.Count == 0)
                lengthOfRoute.Add(0.0);
            else
            {
                int lastIndex = route.Count - 1;
                int preLastIndex = lastIndex - 1;
                double distance = Math.Sqrt(Math.Pow((route[lastIndex].Point_.X - route[preLastIndex].Point_.X), 2) +
                                            Math.Pow((route[lastIndex].Point_.Y - route[preLastIndex].Point_.Y), 2));
                lengthOfRoute.Add(distance);
            }
        }

        /// <summary>
        /// Метод, с помощью которого задается интервал движения автобусов
        /// </summary>
        /// <param name="numberBuses"> Количество автобусов </param>
        public static void SetInterval(uint numberBuses)
        {
            if (route[0] == route[route.Count - 1])
            {
                // Общая длина пути
                double length = 0.0;
                foreach (double len in lengthOfRoute)
                    length += len;

                // Вычисляем интервал между автобусами
                interval = length / numberBuses;
            }
            else
                throw new Exception("Еще не составлен маршрут!");
        }

        /// <summary>
        /// Вместимость автобуса
        /// </summary>
        private readonly uint capacity;
        /// <summary>
        /// Количество людей в автобусе
        /// </summary>
        private uint numOfPeople;
        /// <summary>
        /// Свойство, возвращающее количество людей в автобусе
        /// </summary>
        public uint NumOfPeople => numOfPeople;
        /// <summary>
        /// Индекс остановки, к которой едет автобус
        /// </summary>
        private int index;
        /// <summary>
        /// Свойство, возврщающее индекс остановки, на которую едет автобус
        /// </summary>
        public int Index => index;  

        /// <summary>
        /// Количество перевезенных пассажиров
        /// </summary>
        private uint passengersCarried;
        /// <summary>
        /// Свойство, возвращающее количество перевезенных пассажиров
        /// </summary>
        public uint PassengersCarried => passengersCarried;
        /// <summary>
        /// Количество раз, пройденных автобусом от одной конечной, до другой
        /// </summary>
        private uint numberOfCircles;
        /// <summary>
        /// Свойство, возвращающее количество кругов, пройденных автобусом
        /// </summary>
        public uint NumberOfCircles => numberOfCircles;
        /// <summary>
        /// Имя автобуса
        /// </summary>
        private readonly uint name;
        /// <summary>
        /// Свойство, возвращающее имя автобуса
        /// </summary>
        public uint Name => name;

        /// <summary>
        /// Время движения автобуса до следующей остановки
        /// </summary>
        private Timer timer;

        /// <summary>
        /// Конструктор класса Bus
        /// </summary>
        /// <param name="name"> Имя автобуса </param>
        /// <param name="capacity"> Вместимость автобуса </param>
        public Bus(uint name, uint capacity)
        {   // Написать обработку update (обновление кол-ва людей в автобусе)
            this.capacity = capacity;
            this.name = name;
            StartModeling();
            timer = new Timer();
            timer.AutoReset = false;
            timer.Elapsed += Timer_Elapsed;
        }

        /// <summary>
        /// Метод, инициализирующий поля для моделирования
        /// </summary>
        public void StartModeling()
        {
            passengersCarried = 0;
            numberOfCircles = 0;
            index = 0;
            numOfPeople = 0;
        }

        /// <summary>
        /// Метод, вызываемый, когда автобус достигает следующей остановки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Вызываем соответсвующий метод остановки
            TakeABus();
        }

        /// <summary>
        /// Метод, с помощью которого люди выходят из автобуса
        /// </summary>
        private void GetOffTheBus()
        {
            // Если находимся на конечной остановке, начинаем новый круг
            if (index == route.Count - 1)
            {
                passengersCarried += numOfPeople;
                numOfPeople = 0;
                index = 0;
                ++numberOfCircles;
            }
            else
            {
                // Количество людей на выход
                int toExit = random.Next(0, (int)numOfPeople);
                passengersCarried += (uint)toExit;
                numOfPeople -= (uint)toExit;
            }
        }

        /// <summary>
        /// Метод, задающий время езды до следующей остановки
        /// </summary>
        private void SetTimeNextStop()
        {
            // Устанавливаем время до следующего тика
            timer.Interval = lengthOfRoute[++index] / speed * 60;
        }

        /// <summary>
        /// Метод, останавливающий работу автобуса
        /// </summary>
        public void StopTheBusMove()
        {
            timer.Stop();
        }

        /// <summary>
        /// Метод, возобновляющий работу автобуса
        /// </summary>
        public void ResumeTheBusMove()
        {
            timer.Start();
        }

        /// <summary>
        /// Метод, с помощью которого какое-то количество пассажиров садится в автобус
        /// </summary>
        public void TakeABus()
        {
            // Высадка пассажиров
            GetOffTheBus();

            // Посадка новых пассажиров в автобус
            if ((capacity - numOfPeople) > route[index].NumOfPeople)
            {
                numOfPeople = route[index].NumOfPeople;
                route[index].NumOfPeople = 0;
            }
            else
            {
                route[index].NumOfPeople -= capacity - numOfPeople;
                numOfPeople = capacity;
            }

            SetTimeNextStop();
            timer.Start();
        }
    }
}
