using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace Buses
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Панель, на которой выводится приветствие пользователя
        /// </summary>
        Panel greeting;

        /// <summary>
        /// Высота формы
        /// </summary>
        int height;
        /// <summary>
        /// Ширина формы
        /// </summary>
        int width;
        /// <summary>
        /// Расстояние между двумя кнопками по ширине
        /// </summary>
        int deltaWidth;
        /// <summary>
        /// Расстояние между двумя кнопками по высоте
        /// </summary>
        readonly int deltaHeight = 10;

        /// <summary>
        /// Список основных кнопок
        /// </summary>
        List<Button> mainButtons;
        /// <summary>
        /// Словарь текстов внутри основных кнопок
        /// </summary>
        Dictionary<int, string> textOfMainButtons;

        /// <summary>
        /// Список кнопок для рисования
        /// </summary>
        List<Button> buttonsForPaint;
        /// <summary>
        /// Словарь тесктов внутри кнопок для рисования
        /// </summary>
        Dictionary<int, string> textOfButtonsForPaint;

        /// <summary>
        /// Объект, с помощью которого рисуем на форме
        /// </summary>
        Graphics graphics;
        /// <summary>
        /// Объект, хранящий наш рисунок
        /// </summary>
        Bitmap picture;
        /// <summary>
        /// Ручка, которой рисуем нашу графику
        /// </summary>
        Pen pen;
        /// <summary>
        /// Флаг, показывающий, рисуем ли мы сейчас вершины графа
        /// </summary>
        bool paintCircles = false;
        /// <summary>
        /// Флаг, показывающий, рисуем ли мы сейчас ребра графа
        /// </summary>
        bool paintLines = false;
        /// <summary>
        /// Флаг, показывающий, удаляем ли мы сейчас вершины графа
        /// </summary>
        bool deleteCircles = false;
        /// <summary>
        /// Флаг, показывающий, удаляем ли мы сейчас вершины графа
        /// </summary>
        bool deleteLines = false;
        /// <summary>
        /// Флаг, показывающий, прокладываем ли мы сейчас путь в графе
        /// </summary>
        bool getDirections = false;
        /// <summary>
        /// Вершина, из которой идет ребро
        /// </summary>
        Graph.GraphTop topComeFrom = null;
        /// <summary>
        /// Радиус точки графа, который рисует пользователь
        /// </summary>
        readonly int radius = 15;
        /// <summary>
        /// Текстовое поле, показывающее какие вершины сейчас выбраны
        /// </summary>
        Label infoAboutTops;
        /// <summary>
        /// Текстовое поле, показывающее вершины в маршруте
        /// </summary>
        Label infoAboutRoute;

        /// <summary>
        /// Количество автобусов на маршруте
        /// </summary>
        uint numOfBuses = 1;
        /// <summary>
        /// Количество сидений в автобусе
        /// </summary>
        uint numOfSeats = 20;
        /// <summary>
        /// Список автобусов, идущих по маршруту
        /// </summary>
        List<Bus> buses;
        /// <summary>
        /// Индекс автобуса, который нужно отправить на маршрут
        /// </summary>
        int index;

        /// <summary>
        /// Флаг, показывающий может ли пользователь менять маршрут
        /// </summary>
        bool canChange = true;
        /// <summary>
        /// Граф
        /// </summary>
        readonly Graph.Graph graph;

        /// <summary>
        /// Имя файла, в который сохраняются данные
        /// </summary>
        string nameOfFile;

        /// <summary>
        /// Набор точек, показывающий зависимость кол-ва людей на остановке от времени
        /// </summary>
        Series seriesForAllPeople;
        /// <summary>
        /// Набор точек, показывающий зависимость кол-ва перевезенных пассажиров от времени
        /// </summary>
        Series seriesForPassangers;

        /// <summary>
        /// Конструктор класса Form1
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            
            Name = "Моделирование расписания автобусов";
            height = workPlace.Height;
            width = workPlace.Width;
            graph = new Graph.Graph();
            resetRoute.Visible = false;
            nameOfFile = "";

            timerForModeling.Interval = 1000;
            timerForGreeting.Interval = 3000;
            timerForGreeting.Start();
            Greeting();
        }

        /// <summary>
        /// Метод, инициализирующий списки кнопок и словари текстов
        /// </summary>
        private void InitDict()
        {
            mainButtons = new List<Button>(5);
            textOfMainButtons = new Dictionary<int, string>()
            {
                [0] = "Редактировать граф",
                [1] = "Редактировать маршрут",
                [2] = "Настроить систему",
                [3] = "Моделирование",
                [4] = "Построить графики",
            };

            buttonsForPaint = new List<Button>(2);
            textOfButtonsForPaint = new Dictionary<int, string>()
            {
                [0] = "Нарисовать вершину",
                [1] = "Нарисовать ребро",
                [2] = "Удалить вершину",
                [3] = "Удалить ребро",
                [4] = "Сбросить"
            };
        }

        /// <summary>
        /// Метод, добавляющий на форму кнопки
        /// </summary>
        /// <param name="buttons"> Коллекция, в которую нужно добавить кнопки </param>
        /// <param name="textOfButtons"> Словарь, в котором содержатся тексты кнопок </param>
        /// <param name="action"> Делегат, который передаем событию Click </param>
        /// <param name="numberOfElements"> Количество кнопок в будующей коллекции </param>
        /// <param name="color"> Цвет кнопок </param>
        /// <param name="coefWidth"> Коэффициент смещения по ширине </param>
        /// <param name="coefHeight"> Коэффициент смещения по высоте  </param>
        private void AddButtons(
                                    List<Button> buttons, Dictionary<int, string> textOfButtons, 
                                    Action<object, EventArgs> action, int numberOfElements, Color color,
                                    int coefWidth, int coefHeight, int biasWidth, int biasHeight
                                )
        {
            for (int i = 0; i < numberOfElements; ++i)
            {
                // Создаем элемент и добавляем его в Controls панели
                buttons.Add(new Button());
                workPlace.Controls.Add(buttons[i]);

                // Устанавливаем значения кнопки
                buttons[i].AutoSize = false;
                buttons[i].BackColor = color;
                buttons[i].Size = new Size(110, 55);
                buttons[i].Text = textOfButtons[i];
                buttons[i].Location = new Point(deltaWidth + biasWidth + coefWidth*i, 
                                                    biasHeight + coefHeight*i);

                // Подписываемся на событие Click
                buttons[i].Click += new EventHandler(action);
            }
        }

        /// <summary>
        /// Метод, создающий и располагающий на форме кнопки и вспомогательные элементы
        /// </summary>
        private void SetButtons()
        {
            deltaWidth = (width-550) / 6;

            // Создаем главные кнопки
            AddButtons(mainButtons, textOfMainButtons, MainButtonClick, 5, 
                            Color.AntiqueWhite, 110+deltaWidth, 0, 0, 0);
            UnblockButtons(mainButtons, false);
            mainButtons[0].Enabled = true;

            int eighth = (height-mainButtons[0].Height-2*deltaHeight) / 8;
            // Создаем кнопки для рисования
            AddButtons(buttonsForPaint, textOfButtonsForPaint, ButtonForPaintClick, 5, Color.AntiqueWhite,
                           0, eighth, 0, mainButtons[0].Height + 2*deltaHeight + 2* eighth);
            ShowButtons(buttonsForPaint, false);

            // Размещаем кнопку сброса маршрута
            resetRoute.Location = new Point(deltaWidth, (height-mainButtons[0].Bottom-resetRoute.Height)/2 
                                                + mainButtons[0].Bottom);

            // Размещаем кнопку старта и приостановки моделирования
            modelStart.Location = new Point(width - modelStart.Width - 10, height - modelStart.Height - 10);
            pauseModeling.Location = new Point(10, height - pauseModeling.Height - 10);
            // Размещаем ProgressBar
            progressOfModeling.Location = new Point((width-progressOfModeling.Width) / 2, 
                                                        mainButtons[0].Bottom + 10);

            // Размещаем списки вершин
            listBox1.Location = new Point(10, mainButtons[0].Bottom + deltaHeight);
            listBox1.Size = new Size((width-10) / 2, height - 2*deltaHeight - mainButtons[0].Bottom);
            tops.Location = new Point(10, progressOfModeling.Bottom + deltaHeight);
            tops.Size = new Size((width-10) / 4, height - 2*deltaHeight - progressOfModeling.Bottom - pauseModeling.Height);
            busList.Location = new Point(width / 2, progressOfModeling.Bottom + deltaHeight);
                busList.Size = new Size(tops.Width, tops.Height);
            // Размещаем надписи
            infoAboutTop.Location = new Point(tops.Right + 10, (height-mainButtons[0].Bottom)/4 +
                                                mainButtons[0].Bottom);
            infoAboutTop.AutoSize = true;
            infoAboutBus.Location = new Point(busList.Right + 10, (height-mainButtons[0].Bottom)/4 +
                                                mainButtons[0].Bottom);
            infoAboutBus.AutoSize = true;
            infoAboutBuses.Location = new Point(busList.Right + 10, 2*(height-mainButtons[0].Bottom)/4 +
                                                mainButtons[0].Bottom);
            infoAboutBuses.AutoSize = true;
            infoAboutSeats.Location = new Point(busList.Right + 10, 3*(height-mainButtons[0].Bottom)/4 +
                                                mainButtons[0].Bottom);
            infoAboutSeats.AutoSize = true;

            // Размещаем поля для ввода
            coeffB.Location = new Point((width-listBox1.Right)/3 + listBox1.Right - coeffB.Width,
                                                (height-mainButtons[0].Bottom)/6 + mainButtons[0].Bottom - coeffB.Height);
            dispersion.Location = new Point((width-listBox1.Right)/3 + listBox1.Right - dispersion.Width,
                                                2*(height-mainButtons[0].Bottom)/6 + mainButtons[0].Bottom - dispersion.Height);
            numberOfBuses.Location = new Point((width-listBox1.Right)/3 + listBox1.Right - numberOfBuses.Width,
                                                3*(height-mainButtons[0].Bottom)/6 + mainButtons[0].Bottom - numberOfBuses.Height);
            numberOfSeats.Location = new Point((width - listBox1.Right) / 3 + listBox1.Right - numberOfBuses.Width,
                                                4*(height-mainButtons[0].Bottom)/6 + mainButtons[0].Bottom - numberOfSeats.Height);
            speed.Location = new Point((width - listBox1.Right) / 3 + listBox1.Right - numberOfBuses.Width,
                                                5*(height-mainButtons[0].Bottom)/6 + mainButtons[0].Bottom - speed.Height);
            // Размещаем подсказки для полей для ввода
            infoCoeffB.Location = new Point(coeffB.Left, coeffB.Top - infoCoeffB.Height - 5);
            infoDisp.Location = new Point(dispersion.Left, dispersion.Top - infoDisp.Height - 5);
            infoNumBuses.Location = new Point(numberOfBuses.Left, numberOfBuses.Top - infoNumBuses.Height - 5);
            infoNumOfSeats.Location = new Point(numberOfSeats.Left, numberOfSeats.Top - infoNumOfSeats.Height - 5);
            infoSpeed.Location = new Point(speed.Left, speed.Top - infoSpeed.Height - 5);
            // Размещаем кнопку изменения параметров вершины и автобусов
            setCoeffAndDisp.Location = new Point(2*(width-listBox1.Right)/3 + listBox1.Right - setCoeffAndDisp.Width,
                                                    (height-mainButtons[0].Bottom)/2 + mainButtons[0].Bottom - setCoeffAndDisp.Height);

            // Создаем вспомагательные надписи
            infoAboutTops = new Label();
            workPlace.Controls.Add(infoAboutTops);
            infoAboutTops.Location = new Point(deltaWidth, mainButtons[0].Height + 2*deltaHeight + eighth);
            infoAboutTops.AutoSize = true;
            infoAboutTops.Visible = false;
            infoAboutTops.Text = "Выберете действие";

            infoAboutRoute = new Label();
            workPlace.Controls.Add(infoAboutRoute);
            infoAboutRoute.Location = infoAboutTops.Location;
            infoAboutRoute.AutoSize = true;
            infoAboutRoute.Visible = false;

            // Перемещаем полотно для рисования
            forPaint.Location = new Point(2*deltaWidth + 110, 2*deltaWidth + 55);

            // Размещаем графики и настраиваем их
            allPeople.Location = new Point(10, mainButtons[0].Bottom + 10);
            allPeople.Size = new Size(width/2 - 15, height - mainButtons[0].Bottom - 20);
            passengers.Location = new Point(width/2 + 5, mainButtons[0].Bottom + 10);
            passengers.Size = new Size(allPeople.Width, allPeople.Height);

            // Добавляем области для рисования графиков и настраиваим их
            allPeople.ChartAreas.Add(CreateArea("All people", 6, 24));
            passengers.ChartAreas.Add(CreateArea("Passengers", 6, 24));
            // Создаем и настраиваем набор точек для рисования графика
            seriesForAllPeople = new Series("Кол-во людей\nна остановке");
            seriesForAllPeople.ChartType = SeriesChartType.Line;
            seriesForAllPeople.ChartArea = "All people";

            seriesForPassangers = new Series("Кол-во перевезен-\nных пассажиров");
            seriesForPassangers.ChartType = SeriesChartType.Line;
            seriesForPassangers.ChartArea = "Passengers";
            
            // Добавляем созданные наборы точек
            allPeople.Series.Add(seriesForAllPeople);
            passengers.Series.Add(seriesForPassangers);
        }

        /// <summary>
        /// Метод, создающий нужный ChartArea
        /// </summary>
        /// <param name="name"> Имя ChartArea </param>
        /// <param name="minX"> Минимальное значение по оси </param>
        /// <param name="maxX"> Максимальное значение по оси </param>
        /// <returns> Возвращает созданную по параметрам ChartArea </returns>
        private ChartArea CreateArea(string name, double minX, double maxX)
        {
            ChartArea chartArea = new ChartArea(name);
            chartArea.Position = new ElementPosition(0, 0, 100, 100);
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisX.Minimum = minX;
            chartArea.AxisX.Maximum = maxX;

            return chartArea;
        }

        /// <summary>
        /// Метод, обрабатывающий нажатие на одну из основных кнопок
        /// </summary>
        /// <param name="sender"> Объект, который подписался на событие </param>
        /// <param name="eventArgs"> Дополнительная информация </param>
        private void MainButtonClick(object sender, EventArgs eventArgs)
        {
            Button button = sender as Button;

            // Определяем какая кнопка вызвала событие
            int index = 0;
            for (int i = 0; i < mainButtons.Count; ++i)
                if (button == mainButtons[i])
                {
                    index = i;
                    break;
                }

            switch (index)
            {
                case (0):                   // Редактировать граф
                    forPaint.Visible = true;
                    // Инициализируем инструменты для графики 
                    if (graphics == null)
                    {
                        // Растягиваем место для рисования на весь экран и задаем ему белый цвет
                        forPaint.Size = Screen.PrimaryScreen.Bounds.Size;
                        forPaint.BackColor = Color.White;

                        // Инициализируем инструменты для рисования
                        picture = new Bitmap(forPaint.Width, forPaint.Height);
                        graphics = Graphics.FromImage(picture);
                        pen = new Pen(Color.Black, 2);
                    }
                    ShowButtons(buttonsForPaint, true);
                    infoAboutTops.Visible = true;
                    infoAboutTops.Text = "Выберете действие";
                    infoAboutRoute.Visible = false;
                    resetRoute.Visible = false;
                    listBox1.Visible = false;

                    coeffB.Visible = false;
                    dispersion.Visible = false;
                    numberOfBuses.Visible = false;
                    numberOfSeats.Visible = false;
                    speed.Visible = false;
                    setCoeffAndDisp.Visible = false;
                    infoCoeffB.Visible = false;
                    infoDisp.Visible = false;
                    infoNumBuses.Visible = false;
                    infoNumOfSeats.Visible = false;

                    modelStart.Visible = false;
                    pauseModeling.Visible = false;
                    progressOfModeling.Visible = false;

                    allPeople.Visible = false;
                    passengers.Visible = false;
                    break;
                case (1):                   // Редактировать маршрут
                    // Скрываем и показываем нужные элементы
                    Canceldrawing();
                    infoAboutTops.Visible = false;
                    forPaint.Visible = true;
                    getDirections = true;
                    infoAboutRoute.Visible = true;
                    resetRoute.Visible = true;
                    listBox1.Visible = false;

                    coeffB.Visible = false;
                    dispersion.Visible = false;
                    numberOfBuses.Visible = false;
                    numberOfSeats.Visible = false;
                    speed.Visible = false;
                    setCoeffAndDisp.Visible = false;
                    infoCoeffB.Visible = false;
                    infoDisp.Visible = false;
                    infoNumBuses.Visible = false;
                    infoNumOfSeats.Visible = false;
                    infoSpeed.Visible = false;

                    modelStart.Visible = false;
                    pauseModeling.Visible = false;
                    progressOfModeling.Visible = false;
                    tops.Visible = false;
                    busList.Visible = false;
                    infoAboutTop.Visible = false;
                    infoAboutBus.Visible = false;
                    infoAboutBuses.Visible = false;
                    infoAboutSeats.Visible = false;

                    allPeople.Visible = false;
                    passengers.Visible = false;
                    // Если у нас еще нет элементов в маршруте, задаем соответствующую надпись
                    if (Bus.Route.Count == 0)
                        infoAboutRoute.Text = "Проложите кольцевой\nмаршрут\n";
                    break;
                case (2):                   // Настроить систему
                    Canceldrawing();
                    getDirections = false;
                    infoAboutRoute.Visible = false;
                    resetRoute.Visible = false;
                    modelStart.Visible = false;
                    pauseModeling.Visible = false;
                    progressOfModeling.Visible = false;
                    tops.Visible = false;
                    busList.Visible = false;
                    infoAboutTop.Visible = false;
                    infoAboutBus.Visible = false;
                    infoAboutBuses.Visible = false;
                    infoAboutSeats.Visible = false;

                    allPeople.Visible = false;
                    passengers.Visible = false;

                    DisplayTops();
                    break;
                case (3):                   // Моделирование
                    Canceldrawing();
                    getDirections = false;
                    infoAboutRoute.Visible = false;
                    resetRoute.Visible = false;
                    listBox1.Visible = false;

                    coeffB.Visible = false;
                    dispersion.Visible = false;
                    numberOfBuses.Visible = false;
                    numberOfSeats.Visible = false;
                    speed.Visible = false;
                    setCoeffAndDisp.Visible = false;
                    infoCoeffB.Visible = false;
                    infoDisp.Visible = false;
                    infoNumBuses.Visible = false;
                    infoNumOfSeats.Visible = false;
                    infoSpeed.Visible = false;

                    modelStart.Visible = true;
                    pauseModeling.Visible = true;
                    progressOfModeling.Visible = true;

                    allPeople.Visible = false;
                    passengers.Visible = false;

                    StartModeling();
                    break;
                case (4):                   // Построить графики
                    Canceldrawing();
                    getDirections = false;
                    infoAboutRoute.Visible = false;
                    resetRoute.Visible = false;
                    listBox1.Visible = false;

                    coeffB.Visible = false;
                    dispersion.Visible = false;
                    numberOfBuses.Visible = false;
                    numberOfSeats.Visible = false;
                    speed.Visible = false;
                    setCoeffAndDisp.Visible = false;
                    infoCoeffB.Visible = false;
                    infoDisp.Visible = false;
                    infoNumBuses.Visible = false;
                    infoNumOfSeats.Visible = false;
                    infoSpeed.Visible = false;

                    modelStart.Visible = false;
                    pauseModeling.Visible = false;
                    progressOfModeling.Visible = false;
                    tops.Visible = false;
                    busList.Visible = false;
                    infoAboutTop.Visible = false;
                    infoAboutBus.Visible = false;
                    infoAboutBuses.Visible = false;
                    infoAboutSeats.Visible = false;

                    allPeople.Visible = true;
                    passengers.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// Метод, изменяющий значение окна с информацией
        /// </summary>
        /// <param name="sender"> Объект, вызвавший метод </param>
        /// <param name="e"> Дополнительная информация </param>
        private void tops_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Получаем имя вершины
            int name = (int)tops.SelectedItem;
            // Получаем вершину с заданным именем
            Graph.GraphTop top = graph.GetTop(name);
            if (top != null)
                infoAboutTop.Text = $"Количество людей -- {top.NumOfPeople}\n" +
                                    $"Изначальное число людей -- {top.StartNum}\n" +
                                    $"Дисперсия значений -- {top.Dispersion}";
        }

        /// <summary>
        /// Метод, вызываемый при выборе другого элемента в busList
        /// </summary>
        /// <param name="sender"> Объект, вызывающий метод </param>
        /// <param name="e"> Дополнительная информация </param>
        private void busList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Получаем имя вершины
            uint name = (uint)busList.SelectedItem;
            // Получаем автобус с заданным именем
            Bus bus = buses.FirstOrDefault(elem => elem.Name == name);
            if (bus != null)
                infoAboutBus.Text = $"Кол-во перевезенных людей -- {bus.PassengersCarried}\n" +
                                    $"Кол-во кругов по маршруту -- {bus.NumberOfCircles}\n" +
                                    $"Кол-во людей в автобусе -- {bus.NumOfPeople}\n" +
                                    $"Следующая остановка -- {Bus.Route[bus.Index].Name}";
        }

        /// <summary>
        /// Кнопка, запускающая моделирование
        /// </summary>
        /// <param name="sender"> Объект, вызвавший метод </param>
        /// <param name="e"> Дополнительная информация </param>
        private void modelStart_Click(object sender, EventArgs e)
        {
            if (modelStart.Text == "Продолжить моделирование")
            {
                for (int i = 0; i < buses.Count; ++i)
                    buses[i].ResumeTheBusMove();

                timerForModeling.Start();
                pauseModeling.Enabled = true;
            }
            else
            {
                // Очищаем графики
                seriesForAllPeople.Points.Clear();
                seriesForPassangers.Points.Clear();

                for (int i = 0; i < buses.Count; ++i)
                    buses[i].StartModeling();

                Bus.SetInterval(numOfBuses);
                busTick.Interval = 1;
                index = 0;
                for (int i = 0; i < Bus.Route.Count; ++i)
                    Bus.Route[i].StartModeling();
                // Первый автобус выезжает без задержок, остальные через интервалы
                busTick.Start();
                timerForModeling.Start();
                mainButtons[mainButtons.Count - 1].Enabled = true;
                mainButtons[2].Enabled = false;
                modelStart.Text = "Продолжить моделирование";
            }

            modelStart.Enabled = false;
        }

        /// <summary>
        /// Метод, запускающий автобус на машрут
        /// </summary>
        /// <param name="sender"> Объект, вызывающий метод </param>
        /// <param name="e"> Дополнительная информация </param>
        private void busTick_Tick(object sender, EventArgs e)
        {
            try
            {
                buses[index++].TakeABus();

                if (index >= buses.Count)
                {
                    busTick.Stop();
                    pauseModeling.Enabled = true;
                }
                else
                    busTick.Interval = (int)Bus.Interval;
            }
            catch (ArgumentException error)
            {
                MessageBox.Show($"Возникла ошибка при отправке автобусов:\n{error.Message}");
            }
        }

        /// <summary>
        /// Метод, запускающий моделирование
        /// </summary>
        private void StartModeling()
        {
            // Проверяем коллекцию автобусов на пустоту, если пуста, заполняем
            if (buses == null)
            {
                buses = new List<Bus>((int)numOfBuses);
                for (uint i = 0; i < numOfBuses; ++i)
                    buses.Add(new Bus(i + 1, numOfSeats));
            }

            // Получаем все вершины
            List<int> namesOfTops = new List<int>(Bus.Route.Count - 1);
            for (int i = 0; i < namesOfTops.Capacity; ++i)
                namesOfTops.Add(Bus.Route[i].Name);

            // Получаем все автобусы
            List<uint> namesOfBuses = new List<uint>(buses.Count);
            foreach (Bus bus in buses)
                namesOfBuses.Add(bus.Name);

            // Устанавливаем источники данных и флаги
            tops.DataSource = namesOfTops;
            tops.Visible = true;
            busList.DataSource = namesOfBuses;
            busList.Visible = true;
            infoAboutTop.Visible = true;
            infoAboutBus.Visible = true;
            infoAboutBuses.Visible = true;
            infoAboutBuses.Text = $"Количество автобусов -- {numOfBuses}\nСкорость автобуса -- {Bus.Speed}";
            infoAboutSeats.Visible = true;
            infoAboutSeats.Text = $"Количество мест -- {numOfSeats}";

            // Устанавливаем границы моделирования (один день)
            progressOfModeling.Minimum = 6 * 2;     // Начало на увеличивающий множитель
            progressOfModeling.Maximum = 24 * 2;    // Конец на увеличивающий множитель
        }

        /// <summary>
        /// Метод, вызываемый при тике таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerForModeling_Tick(object sender, EventArgs e)
        {
            // Если время моделирования не вышло, продолжаем моделирование
            if (progressOfModeling.Value < 48)
            {
                // Обновляем значение полей
                progressOfModeling.Value++;
                UpdateTopsValue();
                busList_SelectedIndexChanged(this, new EventArgs());
            }
            // Иначе записываем результат в файл 
            else
            {
                // Останавливаем работу автобусов
                for (int i = 0; i < buses.Count; ++i)
                    buses[i].StopTheBusMove();

                // Останавливаем таймер и инициализируем кнопки и progressBar
                timerForModeling.Stop();
                mainButtons[2].Enabled = true;
                modelStart.Text = "Начать моделирование";
                modelStart.Enabled = true;
                pauseModeling.Enabled = false;
                progressOfModeling.Value = progressOfModeling.Minimum;

                // Если путь до файла еще не указан, пытаемся указать
                if (nameOfFile == "")
                {
                    // Спрашиваем пользователя о его желании записать результаты моделирования в файл
                    DialogResult result = MessageBox.Show("Хотите записать результаты моделирования в файл?",
                                                            "Запись в файл", MessageBoxButtons.YesNo);

                    // Если пользователь хочет записывать данные моделирования, просим указать файл
                    if (result == DialogResult.Yes)
                    {
                        using(SaveFileDialog saveFile = new SaveFileDialog())
                        {
                            saveFile.InitialDirectory = "c:\\";
                            saveFile.Filter = "txt files (*.txt)|*.txt";
                            saveFile.FilterIndex = 2;

                            if (saveFile.ShowDialog() == DialogResult.OK)
                                // Получаем путь нужного файла
                                nameOfFile = saveFile.FileName;
                            else
                                nameOfFile = "None";
                        }
                    }
                    else
                        nameOfFile = "None";
                }

                // Если пользователь указал файл, записываем в него данные
                if (nameOfFile != "None")
                {
                    try
                    {
                        // Создаем поток
                        using (StreamWriter writer = new StreamWriter(nameOfFile, true))
                        {
                            // Записываем в него информацию
                            writer.WriteLine($"Дата моделирования -- {DateTime.Now}");
                            writer.WriteLine($"Количество остановок в пути -- {Bus.Route.Count - 1}");
                            writer.WriteLine($"Количество автобусов на маршруте -- {numOfBuses}");
                            writer.WriteLine($"Количество мест в автобусе -- {numOfSeats}");
                            writer.WriteLine($"Скорость автобусов -- {Bus.Speed}");
                            writer.WriteLine($"Интервал движения -- {Bus.Interval}\n");
                            uint numOfPeople = 0;
                            for (int i = 0; i < Bus.Route.Count - 1; ++i)
                            {
                                // Вычисляем общее количество людей на всех остановках
                                numOfPeople += Bus.Route[i].TotalNumOfPeople;
                                // Выводм информацию об остановке
                                writer.WriteLine($"Остановка {Bus.Route[i].Name}:\n\tКоличество людей, побывавших на " +
                                                    $"ней -- {Bus.Route[i].TotalNumOfPeople}" +
                                                    $"\n\tОставшееся количество людей на остановке -- {Bus.Route[i].NumOfPeople}" +
                                                    $"\n\tНачальное количество людей на остановке -- {Bus.Route[i].StartNum}" +
                                                    $"\n\tДисперсия мат ожидания числа пассажиров от времени -- {Bus.Route[i].Dispersion}\n");
                            }

                            uint numOfPassenger = 0;
                            for (int i = 0; i < buses.Count; ++i)
                            {
                                // Вычисляем общее количество перевезенных пассажиров
                                numOfPassenger += buses[i].PassengersCarried;
                                // Выводим информацию об автобусе
                                writer.WriteLine($"Автобус №{i + 1}:\n\tПроехал {buses[i].NumberOfCircles} кругов;" +
                                                    $"\n\tПеревез {buses[i].PassengersCarried} пассажиров;" +
                                                    $"\n\tПоследняя остановка, на которую автобус ехал -- {Bus.Route[buses[i].Index].Name};\n");
                            }

                            writer.WriteLine($"Общее количество людей на остановке за период моделирования -- {numOfPeople}");
                            writer.WriteLine($"Общее количество перевезенных пассажиров -- {numOfPassenger}\n");
                            writer.Write("\n");
                        }
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show($"Не удалось записать данные:\n{error.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// Метод, обновляющий количество людей в вершинах
        /// </summary>
        private void UpdateTopsValue()
        {
            uint number = 0;

            for (int i = 0; i < Bus.Route.Count - 1; ++i)
            {
                Bus.Route[i].UpdateNumOfPeople(progressOfModeling.Value);
                number += Bus.Route[i].NumOfPeople;
            }
            // Обновляем отображаемую на Label информацию
            tops_SelectedIndexChanged(this, new EventArgs());

            // Добавляем точку на график количества людей
            seriesForAllPeople.Points.AddXY(progressOfModeling.Value/2, number);
            // Вычисляем количество перевезенных пассажиров
            number = 0;
            foreach (Bus bus in buses)
                number += bus.PassengersCarried;
            // Добавляем новую точку на график перевезенных пассажиров
            seriesForPassangers.Points.AddXY(progressOfModeling.Value/2, number);
        }

        /// <summary>
        /// Метод, приостанавливающий моделирование
        /// </summary>
        /// <param name="sender"> Объект, вызвавший метод </param>
        /// <param name="e"> Дополнительная информация </param>
        private void pauseModeling_Click(object sender, EventArgs e)
        {
            // Останавливаем работу таймера и автобусов на маршрутах
            timerForModeling.Stop();
            for (int i = 0; i < buses.Count; ++i)
                buses[i].StopTheBusMove();

            pauseModeling.Enabled = false;
            modelStart.Enabled = true;
        }

        /// <summary>
        /// Метод, возобновляющий моделирование
        /// </summary>
        /// <param name="sender"> Объект, вызвавший метод </param>
        /// <param name="e"> Дополнительная информация </param>
        private void resumeModeling_Click(object sender, EventArgs e)
        {
            timerForModeling.Start();
        }

        /// <summary>
        /// Метод, показывающий на экране список всех вершин
        /// </summary>
        private void DisplayTops()
        {
            listBox1.Visible = true;
            coeffB.Visible = true;
            dispersion.Visible = true;
            numberOfBuses.Visible = true;
            numberOfSeats.Visible = true;
            speed.Visible = true;
            setCoeffAndDisp.Visible = true;
            infoCoeffB.Visible = true;
            infoDisp.Visible = true;
            infoNumBuses.Visible = true;
            infoNumOfSeats.Visible = true;
            infoSpeed.Visible = true;

            // Отображаем все вершины, количество автобусов и сидений в одном автобусе
            List<Graph.GraphTop> list = new List<Graph.GraphTop>(Bus.Route);
            if ((list.Count != 1) && (list[0] == list[list.Count - 1]))
                list.RemoveAt(list.Count - 1);
            List<int> names = new List<int>(list.Count);
            foreach (Graph.GraphTop elem in list)
                names.Add(elem.Name);

            listBox1.DataSource = names;
            numberOfBuses.Text = numOfBuses.ToString();
            numberOfSeats.Text = numOfSeats.ToString();
            speed.Text = Bus.Speed.ToString();
        }

        /// <summary>
        /// Метод, вызываемый при выборе другого элемента в listBox1
        /// </summary>
        /// <param name="sender"> Объект, вызывающий метод </param>
        /// <param name="e"> Дополнительная информация </param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Получаем имя вершины
            int name = ((int)(sender as ListBox).SelectedItem);
            // Получаем вершину с заданным именем
            Graph.GraphTop top = graph.GetTop(name);
            if (top != null)
            {
                coeffB.Text = top.StartNum.ToString();
                dispersion.Text = top.Dispersion.ToString();
            }
        }

        /// <summary>
        /// Метод, с помощью которого записываем новые значения коэффициента и дисперсии
        /// </summary>
        /// <param name="sender"> Объект, вызывающий метод </param>
        /// <param name="e"> Дополнительная информация </param>
        private void setCoeffAndDisp_Click(object sender, EventArgs e)
        {
            // Получаем имя вершины
            int name = (int)listBox1.SelectedItem;
            try
            {
                // Записываем новые данные
                Graph.GraphTop sought = Bus.Route.First(elem => elem.Name == name);
                sought.StartNum = uint.Parse(coeffB.Text);
                sought.StartModeling();
                sought.Dispersion = float.Parse(dispersion.Text);
                buses = null;
                numOfBuses = uint.Parse(numberOfBuses.Text);
                if (numOfBuses == 0)
                {
                    numOfBuses = 1;
                    numberOfBuses.Text = numOfBuses.ToString();
                    MessageBox.Show("На маршруте не может быть 0 автобусов!");
                }

                numOfSeats = uint.Parse(numberOfSeats.Text);
                if (numOfSeats == 0)
                {
                    numOfSeats = 20;
                    numberOfSeats.Text = numOfSeats.ToString();
                    MessageBox.Show("В автобусе не может быть 0 сидений!");
                }
                else if (numOfSeats > 201)
                {
                    numOfSeats = 20;
                    numberOfSeats.Text = numOfSeats.ToString();
                    MessageBox.Show("В автобусе не может быть больше 201 сидения!");
                }

                Bus.Speed = uint.Parse(speed.Text);
            }
            catch (Exception error)
            {
                MessageBox.Show($"Не удалось записать новые значения:\n{error.Message}");
            }
        }

        /// <summary>
        /// Метод, убирающий флаги, которые позволяют рисовать граф
        /// </summary>
        private void Canceldrawing()
        {
            forPaint.Visible = false;
            paintCircles = false;
            paintLines = false;
            deleteCircles = false;
            deleteLines = false;
            infoAboutTops.Visible = false;

            ShowButtons(buttonsForPaint, false);
        }

        /// <summary>
        /// Метод, устанавливающий флаги в состояние state
        /// </summary>
        /// <param name="state"> Состояние флагов </param>
        private void SetFlags(bool state)
        {
            paintCircles = state;
            paintLines = state;
            deleteCircles = state;
            deleteLines = state;
        }

        /// <summary>
        /// Метод, обрабатывающий нажатие на одну из кнопок для рисования
        /// </summary>
        /// <param name="sender"> Объект, который подписался на событие </param>
        /// <param name="eventArgs"> Дополнительная информация </param>
        private void ButtonForPaintClick(object sender, EventArgs eventArgs)
        {
            // Определяем какая кнопка вызвала событие
            Button button = sender as Button;
            int index = 0;
            for (int i = 0; i < buttonsForPaint.Count; ++i)
                if (button == buttonsForPaint[i])
                {
                    index = i;
                    break;
                }

            switch (index)
            {
                case (0):                   // Нарисовать вершину
                    infoAboutTops.Text = "Нарисуйте вершину";
                    SetFlags(false);
                    paintCircles = true;
                    break;
                case (1):                   // Нарисовать ребро
                    infoAboutTops.Text = "Выберете вершину";
                    SetFlags(false);
                    paintLines = true;
                    break;
                case (2):                   // Удалить вершину
                    infoAboutTops.Text = "Выберете вершину";
                    SetFlags(false);
                    deleteCircles = true;
                    break;
                case (3):                   // Удалить ребро
                    infoAboutTops.Text = "Выберете вершину";
                    SetFlags(false);
                    deleteLines = true;
                    break;
                case (4):                   // Сбросить
                    infoAboutTops.Text = "Граф сброшен";
                    SetFlags(false);
                    graphics.Clear(forPaint.BackColor);
                    graph.Clear();
                    UnblockButtons(mainButtons, false);
                    mainButtons[0].Enabled = true;
                    break;
            }

            topComeFrom = null;
        }

        /// <summary>
        /// Метод, рисующий стрелки на холсте
        /// </summary>
        /// <param name="firstTop"> Вершина, из которой строим ребро </param>
        /// <param name="secondTop"> Вершина, в которую строим ребро </param>
        private void PaintLines(Graph.GraphTop firstTop, Graph.GraphTop secondTop)
        {
            pen.EndCap = LineCap.ArrowAnchor;   // Задаем завершение линии в виде стрелки
            pen.Width = 4.0F;                   // Увеличивае толщину пера    
            graphics.DrawLine(pen, firstTop.Point_, secondTop.Point_);
        }

        /// <summary>
        /// Метод, затирающий линию
        /// </summary>
        /// <param name="firstTop"> Вершина, из которой выходит ребро </param>
        /// <param name="secondTop"> Вершина, в которую приходит ребро </param>
        private void EraseLine(Graph.GraphTop firstTop, Graph.GraphTop secondTop)
        {
            try
            {
                graph.DeleteEdge(firstTop, secondTop);
                pen.Color = forPaint.BackColor;
                pen.Width = 4.0f;
                PaintLines(firstTop, secondTop);
                pen.Color = Color.Black;
                pen.Width = 2.0f;

                // Обновляем контуры вершин
                graphics.DrawEllipse(pen, firstTop.Point_.X - radius,
                                    firstTop.Point_.Y - radius, 2 * radius, 2 * radius);
                graphics.DrawEllipse(pen, secondTop.Point_.X - radius,
                                    secondTop.Point_.Y - radius, 2 * radius, 2 * radius);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Указанного ребра не существует!");
            }
        }

        /// <summary>
        /// Метод, с помощью которого осуществляется рисование на форме
        /// </summary>
        /// <param name="e"> Дополнительная информация </param>
        private void Drawing(MouseEventArgs e)
        {
            pen.StartCap = LineCap.Flat;    // Задаем обычное завершение линии
            pen.Width = 2.0f;               // Задаем обычную ширину линии
            if (paintCircles)
            {
                // Если мы еще не сняли блокировку -- снимаем ее
                if (!mainButtons[1].Enabled)
                    mainButtons[1].Enabled = true;

                // Добавляем новую вершину в список смежности
                graph.AddTop(graph.LenghtAdjacencyList + 1, e.Location);
                // Рисуем круг и подписываем его
                graphics.DrawEllipse(pen, e.X - radius, e.Y - radius, 2 * radius, 2 * radius);
                graphics.DrawString(graph.LenghtAdjacencyList.ToString(), Font, Brushes.Black,
                                        e.X - radius, e.Y + radius);
                topComeFrom = null;
            }
            else if (paintLines)
            {
                // Рисуем линии
                if (topComeFrom == null)
                    topComeFrom = graph.FindTheTop(e.Location, radius);
                else
                {
                    Graph.GraphTop secondTop = graph.FindTheTop(e.Location, radius);
                    if (secondTop != null)
                    {
                        if (secondTop != topComeFrom)
                        {
                            PaintLines(topComeFrom, secondTop);
                            graph.AddEdge(topComeFrom, secondTop);
                        }
                        else
                            MessageBox.Show("В графе не может быть петель!");

                        topComeFrom = null;
                    }
                }

                infoAboutTops.Text = topComeFrom == null ? "Выберете вершину" : $"Выбрана вершина {topComeFrom.Name}";
            }
            else if (deleteCircles)
            {
                topComeFrom = graph.FindTheTop(e.Location, radius);
                if (topComeFrom != null)
                {
                    if (graph.LenghtAdjacencyList == 1)
                    {
                        UnblockButtons(mainButtons, false);
                        mainButtons[0].Enabled = true;
                    }
                    else
                    {
                        List<Graph.GraphTop> list = graph.FindTheTopInOther(topComeFrom.Name);
                        foreach (var top in list)
                            EraseLine(top, topComeFrom);

                        list = graph.GetAllEdges(topComeFrom);
                        foreach (var top in list)
                            EraseLine(topComeFrom, top);
                    }

                    pen.Color = forPaint.BackColor;
                    graphics.DrawEllipse(pen, topComeFrom.Point_.X - radius,
                                            topComeFrom.Point_.Y - radius, 2 * radius, 2 * radius);
                    graphics.DrawString(topComeFrom.Name.ToString(), Font, Brushes.White,
                                            topComeFrom.Point_.X - radius, topComeFrom.Point_.Y + radius);
                    graph.DeleteTop(topComeFrom);
                    pen.Color = Color.Black;
                    topComeFrom = null;
                }
            }
            else if (deleteLines)
            {
                // Рисуем линии
                if (topComeFrom == null)
                    topComeFrom = graph.FindTheTop(e.Location, radius);
                else
                {
                    Graph.GraphTop secondTop = graph.FindTheTop(e.Location, radius);
                    if (secondTop != null)
                    {
                        EraseLine(topComeFrom, secondTop);
                        topComeFrom = null;
                    }
                }

                infoAboutTops.Text = topComeFrom == null ? "Выберете вершину" : $"Выбрана вершина {topComeFrom.Name}";
            }

            // Отображаем изображение
            forPaint.Image = picture;
        }

        /// <summary>
        /// Метод, рисующий на форме граф
        /// </summary>
        /// <param name="sender"> Объект, вызывающий метод </param>
        /// <param name="e"> Дополнительная информация </param>
        private void forPaint_MouseClick(object sender, MouseEventArgs e)
        {
            if (paintCircles || deleteCircles || paintLines || deleteLines)
                Drawing(e);
            else if (getDirections && canChange)
            {
                // Выбираем вершины
                topComeFrom = graph.FindTheTop(e.Location, radius);
                if (topComeFrom != null)
                {
                    if (Bus.Route.Count == 0)
                    {
                        Bus.AddTopOfRoute(topComeFrom);
                        
                        infoAboutRoute.Text += $"Выбранный путь:\n{Bus.Route[0].Name}";
                        mainButtons[mainButtons.Count - 3].Enabled = true;
                    }
                    else
                    {
                        if (graph.EdgeIsExist(Bus.Route[Bus.Route.Count - 1], topComeFrom.Name))
                        {
                            string add = "";
                            // Если есть петля, добавляем возможность моделировать и убираем возможность изменять
                            if (Bus.Route[0] == topComeFrom)
                            {
                                mainButtons[mainButtons.Count - 2].Enabled = true;
                                canChange = false;
                                add = "\nМаршрут проложен!";
                            }

                            Bus.AddTopOfRoute(topComeFrom);
                            infoAboutRoute.Text += $"->{topComeFrom.Name}" + add;
                        }
                        else
                            MessageBox.Show("Между выбранными вершинами нет ребра!\nВыбранная вершина не добавлена в путь");
                    }
                }
            }
        }

        /// <summary>
        /// Метод, сбрасывающий текущий маршрут
        /// </summary>
        /// <param name="sender"> Объект, вызывающий метод </param>
        /// <param name="e"> Дополнительная информация </param>
        private void resetRoute_Click(object sender, EventArgs e)
        {
            Bus.Route.Clear();
            Bus.LengthOfRoute.Clear();
            infoAboutRoute.Text = "Проложите кольцевой\nмаршрут\n";
            UnblockButtons(mainButtons, false);
            mainButtons[0].Enabled = true;
            mainButtons[1].Enabled = true;
            canChange = true;
        }

        /// <summary>
        /// Метод, скрывающий или показывающий на форме кнопки
        /// </summary>
        /// <param name="collection"> Коллекция, в которой нужно перебрать кнопки </param>
        /// <param name="state"> Состояние видимости кнопки (true -- видна, false -- нет) </param>
        private void ShowButtons(List<Button> collection, bool state)
        {
            for (int i = 0; i < collection.Count; ++i)
                collection[i].Visible = state;
        }

        /// <summary>
        /// Метод, разблокирующий или блокирующий на форме кнопки
        /// </summary>
        /// <param name="collection"> Коллекция, в которой нужно перебрать кнопки </param>
        /// <param name="state"> Состояние блокировки кнопки (true -- разблокирована, false -- нет) </param>
        private void UnblockButtons(List<Button> collection, bool state)
        {
            for (int i = 0; i < collection.Count; ++i)
                collection[i].Enabled = state;
        }

        /// <summary>
        /// Метод, пересчитывающий расположение и размеры элементов формы
        /// </summary>
        /// <param name="sender"> Объект, который подписался на событие </param>
        /// <param name="e"> Дополнительная информация </param>
        private void Form1_SizeChanged(object sender, System.EventArgs e)
        {
            height = workPlace.Height;
            width = workPlace.Width;

            if (mainButtons != null)
            {
                deltaWidth = (width - 550) / 6;
                for (int i = 0; i < mainButtons.Count; ++i)
                    mainButtons[i].Location = new Point(deltaWidth + (110 + deltaWidth) * i, deltaHeight);

                int eighth = (height - mainButtons[0].Height - 2 * deltaHeight) / 8;
                for (int i = 0; i < buttonsForPaint.Count; ++i)
                    buttonsForPaint[i].Location = new Point(deltaWidth, mainButtons[0].Bottom + deltaHeight + eighth * (i+2));

                infoAboutTops.Location = new Point(deltaWidth, mainButtons[0].Bottom + deltaHeight + eighth);
                infoAboutRoute.Location = infoAboutTops.Location;
                forPaint.Location = new Point(2*deltaWidth + buttonsForPaint[0].Width, 2*deltaHeight + mainButtons[0].Height);
                resetRoute.Location = new Point(deltaWidth, (height-mainButtons[0].Bottom-resetRoute.Height) / 2);
                // Размещаем кнопку старта и остановки моделирования
                modelStart.Location = new Point(width - modelStart.Width - 10, height - modelStart.Height - 10);
                pauseModeling.Location = new Point(10, height - pauseModeling.Height - 10);
                // Размещаем ProgressBar
                progressOfModeling.Location = new Point((width-progressOfModeling.Width) / 2, mainButtons[0].Bottom + 10);
                // Размещаем списки вершин
                listBox1.Location = new Point(10, mainButtons[0].Bottom + deltaHeight);
                listBox1.Size = new Size((width-10) / 2, height - 2*deltaHeight - mainButtons[0].Bottom);
                tops.Location = new Point(10, progressOfModeling.Bottom + deltaHeight);
                tops.Size = new Size((width-10) / 4, height - 2*deltaHeight - progressOfModeling.Bottom - pauseModeling.Height);
                busList.Location = new Point(width / 2, progressOfModeling.Bottom + deltaHeight);
                busList.Size = new Size(tops.Width, tops.Height);
                // Размещаем надписи
                infoAboutTop.Location = new Point(tops.Right + 10, (height-mainButtons[0].Bottom)/4 +
                                                    mainButtons[0].Bottom);
                infoAboutBus.Location = new Point(busList.Right + 10, (height-mainButtons[0].Bottom)/4 +
                                                    mainButtons[0].Bottom);
                infoAboutBuses.Location = new Point(busList.Right + 10, 2*(height-mainButtons[0].Bottom)/4 +
                                                        mainButtons[0].Bottom);
                infoAboutSeats.Location = new Point(busList.Right + 10, 3*(height-mainButtons[0].Bottom)/4 +
                                                        mainButtons[0].Bottom);
                // Размещаем поля для ввода
                coeffB.Location = new Point((width-listBox1.Right)/3 + listBox1.Right - coeffB.Width,
                                                (height-mainButtons[0].Bottom)/6 + mainButtons[0].Bottom - coeffB.Height);
                dispersion.Location = new Point((width-listBox1.Right)/3 + listBox1.Right - dispersion.Width,
                                                    2*(height-mainButtons[0].Bottom)/6 + mainButtons[0].Bottom - dispersion.Height);
                numberOfBuses.Location = new Point((width-listBox1.Right)/3 + listBox1.Right - numberOfBuses.Width,
                                                    3*(height-mainButtons[0].Bottom)/6 + mainButtons[0].Bottom - numberOfBuses.Height);
                numberOfSeats.Location = new Point((width-listBox1.Right)/3 + listBox1.Right - numberOfBuses.Width,
                                                    4*(height-mainButtons[0].Bottom)/6 + mainButtons[0].Bottom - numberOfSeats.Height);
                speed.Location = new Point((width-listBox1.Right)/3 + listBox1.Right - numberOfBuses.Width,
                                                5*(height-mainButtons[0].Bottom)/6 + mainButtons[0].Bottom - speed.Height);
                // Размещаем подсказки для полей для ввода
                infoCoeffB.Location = new Point(coeffB.Left, coeffB.Top - infoCoeffB.Height - 5);
                infoDisp.Location = new Point(dispersion.Left, dispersion.Top - infoDisp.Height - 5);
                infoNumBuses.Location = new Point(numberOfBuses.Left, numberOfBuses.Top - infoNumBuses.Height - 5);
                infoNumOfSeats.Location = new Point(numberOfSeats.Left, numberOfSeats.Top - infoNumOfSeats.Height - 5);
                infoSpeed.Location = new Point(speed.Left, speed.Top - infoSpeed.Height - 5);
                // Размещаем графики
                allPeople.Location = new Point(10, mainButtons[0].Bottom + 10);
                allPeople.Size = new Size(width / 2 - 15, height - mainButtons[0].Bottom - 20);
                passengers.Location = new Point(width / 2 + 5, mainButtons[0].Bottom + 10);
                passengers.Size = new Size(allPeople.Width, allPeople.Height);
            }
        }

        /// <summary>
        /// Метод, приветствующий пользователя
        /// </summary>
        void Greeting()
        {
            workPlace.Visible = false;
            forPaint.Visible = false;

            greeting = new Panel();
            Controls.Add(greeting);
            greeting.Dock = DockStyle.Fill;
            greeting.Size = new Size(workPlace.Width, workPlace.Height);
            greeting.Location = new Point(workPlace.Top, workPlace.Left);

            Label text = new Label();
            text.Font = new Font(text.Font.Name, 24, text.Font.Style);
            text.Text = "Добро пожаловать!";
            greeting.Controls.Add(text);
            text.AutoSize = true;
            text.Location = new Point((greeting.Width-text.Size.Width) / 2, (greeting.Height-text.Size.Height) / 2);
        }

        /// <summary>
        /// Метод, убирающий надпись с формы
        /// </summary>
        /// <param name="sender"> Объект, который подписался на событие </param>
        /// <param name="e"> Дополнительная информация </param>
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            greeting.Controls.Clear();
            Controls.Remove(greeting);
            timerForGreeting.Stop();
            workPlace.Visible = true;

            InitDict();
            SetButtons();
        }
    }
}
