using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Buses.Graph
{
    /// <summary>
    /// Класс, описывающий вершину, из которой идет ребро
    /// </summary>
    class GraphTop
    {
        /// <summary>
        /// Статическое поле, с помощью которого генерируем случайные значения
        /// </summary>
        static Random random = new Random();

        /// <summary>
        /// Имя вершины, из которой идет ребро
        /// </summary>
        private readonly int name;
        /// <summary>
        /// Свойство, возвращающее имя вершины, из которой идет ребро
        /// </summary>
        public int Name => name;

        /// <summary>
        /// Координаты расположения вершины
        /// </summary>
        private readonly Point point;
        /// <summary>
        /// Свойство, возвращающее координаты расположения вершины
        /// </summary>
        public Point Point_ => point;
        /// <summary>
        /// Начальное количество людей
        /// </summary>
        private uint startNum;
        /// <summary>
        /// Свойство, возвращающее коэффициент b (также можно это значение установить)
        /// </summary>
        public uint StartNum
        {
            get => startNum;
            set 
            {
                startNum = value;
                numOfPeople = startNum;
            }
        }
        /// <summary>
        /// Дисперсия значений 
        /// </summary>
        private float dispersion;
        /// <summary>
        /// Свойсвтво, возврщающее дисперсию значения (также можно это значение установить)
        /// </summary>
        public float Dispersion
        {
            get { return dispersion; }
            set { dispersion = value; }
        }
        /// <summary>
        /// Количество людей на остановке
        /// </summary>
        private uint numOfPeople;
        /// <summary>
        /// Свойство, возвращающее количество людей на остановке
        /// </summary>
        public uint NumOfPeople
        {
            get => numOfPeople;
            set => numOfPeople = value;
        }
        /// <summary>
        /// Общее количество людей, бывших на остановке за промежуток моделирования
        /// </summary>
        private uint totalNumOfPeople;
        /// <summary>
        /// Свойство, возвращающее общее количество людей, бывших на остановке за промежуток моделирования
        /// </summary>
        public uint TotalNumOfPeople => totalNumOfPeople;

        /// <summary>
        /// Конструктор класса TopComeFrom
        /// </summary>
        /// <param name="nameOfTop"> Имя вершины, из которой идет ребро </param>
        /// <param name="point"> Координаты расположения вершины </param>
        public GraphTop(int nameOfTop, Point point)
        {
            name = nameOfTop;
            this.point = point;
            startNum = 0;
            dispersion = 0.0f;
            StartModeling();
        }

        /// <summary>
        /// Метод, инициализирующий нужные поля для следующей итерации моделирования
        /// </summary>
        public void StartModeling()
        {
            numOfPeople = startNum;
            totalNumOfPeople = startNum;
        }

        /// <summary>
        /// Метод, обновляющий количество людей на остановке
        /// </summary>
        /// <param name="time"> Текущее значение времени </param>
        public void UpdateNumOfPeople(int time)
        {
            // Функция математического ожидания -- y = (x-15*2)^2 + coeffB +- rand,
            // где rand -- случайное значение в пределах дисперсии
            uint value = (uint)(Math.Pow((time - 15*2), 2) + random.NextDouble()*dispersion);
            numOfPeople += value;
            totalNumOfPeople += value;
        }
    }

    /// <summary>
    /// Класс, описывающий граф, задающийся списком смежности
    /// </summary>
    class Graph
    {
        /// <summary>
        /// Класс, описывающий ребро графа
        /// </summary>
        class Edge
        {
            /// <summary>
            /// Имя вершины, в которую направлено ребро
            /// </summary>
            private readonly int name;
            /// <summary>
            /// Свойство, возвращающее имя вершины, в которую направлено ребро
            /// </summary>
            public int Name => name;

            /// <summary>
            /// Конструктор класс Edge
            /// </summary>
            /// <param name="top"> Вершина, в котрую идет ребро </param>
            public Edge(GraphTop top)
            {
                name = top.Name;
            }
        }

        /// <summary>
        /// Список смежности
        /// </summary>
        private readonly Dictionary<GraphTop, List<Edge>> adjacencyList;
        /// <summary>
        /// Свойство, возвращающее длину списка смежности
        /// </summary>
        public int LenghtAdjacencyList => adjacencyList.Count;

        /// <summary>
        /// Конструктор класса Graph
        /// </summary>
        public Graph()
        {
            adjacencyList = new Dictionary<GraphTop, List<Edge>>();
        }

        /// <summary>
        /// Метод, возвразающий вершину с заданным именем
        /// </summary>
        /// <param name="name"> Имя вершины </param>
        /// <returns> Заданная вершина или null </returns>
        public GraphTop GetTop(int name)
        {
            try
            {
                return adjacencyList.First(elem => elem.Key.Name == name).Key;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        /// <summary>
        /// Метод, возвращающий все вершины графа
        /// </summary>
        /// <returns> Список всех вершин </returns>
        public List<GraphTop> GetAllTops()
        {
            List<GraphTop> result = new List<GraphTop>(adjacencyList.Count);

            foreach (var elem in adjacencyList)
                result.Add(elem.Key);

            return result;
        }

        /// <summary>
        /// Метод, находящий вершину по ее координатам
        /// </summary>
        /// <param name="point"> Точка, в которой ищем вершину </param>
        /// <param name="raduis"> Значение радиуса точки </param>
        /// <returns> Возвращает подходящую вершину или null </returns>
        public GraphTop FindTheTop(Point point, int raduis)
        {
            // Проверяем вершину на попадание в определенную облать покруг точки
            foreach (GraphTop topComeFrom in adjacencyList.Keys)
            {
                Point top = topComeFrom.Point_;
                if ((point.X >= top.X - raduis) && (point.X <= top.X + raduis) &&
                        (point.Y >= top.Y - raduis) && (point.Y <= top.Y + raduis))
                    return topComeFrom;
            }

            return null;
        }

        /// <summary>
        /// Метод ищет заданную вершину в списке смежности
        /// </summary>
        /// <param name="nameOfTop"> Имя вершины, которую ищем </param>
        /// <returns> Возвращает список всех вершин, у которых есть ребро с nameOfTop </returns>
        public List<GraphTop> FindTheTopInOther(int nameOfTop)
        {
            List<GraphTop> tops = new List<GraphTop>();

            // Ищем вершины, из которых есть ребро в nameOfTop
            foreach (var list in adjacencyList)
                foreach (Edge edge in list.Value)
                    if (edge.Name == nameOfTop)
                    {
                        tops.Add(list.Key);
                        break;
                    }

            return tops;
        }

        /// <summary>
        /// Метод, возвращает все вершины, в которые идет ребро из source
        /// </summary>
        /// <param name="source"> Вершина, из которой ищем ребра </param>
        /// <returns></returns>
        public List<GraphTop> GetAllEdges(GraphTop source)
        {
            List<GraphTop> tops = new List<GraphTop>();

            // Берем все вершины, в которое есть ребро
            foreach (Edge edge in adjacencyList[source])
            {
                GraphTop graphTop = adjacencyList.First(elem => elem.Key.Name == edge.Name).Key;
                tops.Add(graphTop);
            }

            return tops;
        }

        /// <summary>
        /// Метод, проверяющий, существует ли ребро между двумя вершинами
        /// </summary>
        /// <param name="top"> Вершина, из которой ребро выходит </param>
        /// <param name="name"> Имя вершины, в которое ребро должно зайти </param>
        /// <returns> Существует ли ребро (true -- да, false -- нет) </returns>
        public bool EdgeIsExist(GraphTop top, int name)
        {
            try
            {
                adjacencyList[top].First(elem => elem.Name == name);
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        /// <summary>
        /// Метод, добавляющий вершину в список смежности
        /// </summary>
        /// <param name="nameOfTop"> Имя добавляемой вершины </param>
        public void AddTop(int nameOfTop, Point point)
        {
            // Добавляем вершину, если она отсутствует в нашем списке
            try
            {
                adjacencyList.Keys.First(top => top.Name == nameOfTop);
            }
            catch (InvalidOperationException)
            {
                adjacencyList.Add(new GraphTop(nameOfTop, point), new List<Edge>());
            }
        }

        /// <summary>
        /// Метод, удаляющий вершину
        /// </summary>
        /// <param name="top"></param>
        public void DeleteTop(GraphTop top)
        {
            // Удаляем вершину
            if (adjacencyList.ContainsKey(top))
            {
                adjacencyList[top].Clear();
                adjacencyList.Remove(top);
            }
            else
                MessageBox.Show($"Вершины {top.Name} не существует!");
        }

        /// <summary>
        /// Метод, с помощью которого добавляется ребро в список смежности
        /// </summary>
        /// <param name="comeFrom"> Вершина, из которой идет ребро </param>
        /// <param name="comeTo"> Вершина, в которую идет ребро </param>
        public void AddEdge(GraphTop comeFrom, GraphTop comeTo)
        {
            // Добавляем ребра
            if (adjacencyList.ContainsKey(comeFrom))
            {
                try
                {
                    // Пытаемся найти элемент в списке с таким названием
                    adjacencyList[comeFrom].First(elem => elem.Name == comeTo.Name);
                    // Если нашли, выводим сообщение
                    MessageBox.Show("Добавляемое ребро уже существует!");
                }
                catch (InvalidOperationException)
                {
                    // Если не нашли, добавляем
                    adjacencyList[comeFrom].Add(new Edge(comeTo));
                }
            }
            else
                MessageBox.Show($"Вершины {comeFrom.Name} не существует!");
        }

        /// <summary>
        /// Метод, с помощью которого удаляется ребро из списка смежности
        /// </summary>
        /// <param name="comeFrom"> Вершина, из которой идет ребро </param>
        /// <param name="comeTo"> Вершина, в которую идет ребро </param>
        public void DeleteEdge(GraphTop comeFrom, GraphTop comeTo)
        {
            // Удаляем ребра
            if (adjacencyList.ContainsKey(comeFrom))
                adjacencyList[comeFrom].Remove(adjacencyList[comeFrom].First(top => top.Name == comeTo.Name));
            else
                MessageBox.Show($"Вершины {comeFrom.Name} не существует!");
        }

        /// <summary>
        /// Метод, с помощью которого очищается список смежности
        /// </summary>
        public void Clear()
        {
            foreach (List<Edge> edges in adjacencyList.Values)
                edges.Clear();
            adjacencyList.Clear();
        }
    }
}
