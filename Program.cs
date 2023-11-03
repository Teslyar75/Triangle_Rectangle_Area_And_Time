/*Задание 2
Создайте набор методов:
■ Метод отображения текущего времени;
■ Метод отображения текущей даты;
■ Метод отображения текущего дня недели;
■ Метод для подсчета площади треугольника;
■ Метод для подсчета площади прямоугольника.
Для реализации проекта используйте делегаты: Action, Predicate, Func.*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Triangle_Rectangle_Area_And_Time
{
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }

    class Triangle : IEnumerable<Point>
    {
        private Point[] points = new Point[3];

        public Point this[int index]
        {
            get { return points[index]; }
            set { points[index] = value; }
        }

        public Triangle(Point p1, Point p2, Point p3)
        {
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;
        }

        public IEnumerator<Point> GetEnumerator()
        {
            foreach (Point point in points)
            {
                yield return point;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static bool IsTriangle(Point p1, Point p2, Point p3)
        {
            double a = CalculateDistance(p1, p2);
            double b = CalculateDistance(p2, p3);
            double c = CalculateDistance(p3, p1);

            return a + b > c && a + c > b && b + c > a;
        }

        private static double CalculateDistance(Point p1, Point p2)
        {
            int dx = p1.X - p2.X;
            int dy = p1.Y - p2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static void DisplayCurrentTime()
        {
            Action action = () => Console.WriteLine($"Текущее время: {DateTime.Now.ToLongTimeString()}");
            action();
        }

        public static void DisplayCurrentDate()
        {
            Action action = () => Console.WriteLine($"Текущая дата: {DateTime.Now.ToShortDateString()}");
            action();
        }

        public static void DisplayCurrentDayOfWeek()
        {
            Action action = () => Console.WriteLine($"Текущий день недели: {DateTime.Now.DayOfWeek}");
            action();
        }

        public static double CalculateTriangleArea(Point p1, Point p2, Point p3)
        {
            Func<Point, Point, double> calculateDistance = (point1, point2) =>
            {
                int dx = point1.X - point2.X;
                int dy = point1.Y - point2.Y;
                return Math.Sqrt(dx * dx + dy * dy);
            };

            double a = calculateDistance(p1, p2);
            double b = calculateDistance(p2, p3);
            double c = calculateDistance(p3, p1);
            double s = (a + b + c) / 2;

            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }

        public static double CalculateRectangleArea(Point p1, Point p2, Point p3)
        {
            double a = CalculateDistance(p1, p2);
            double b = CalculateDistance(p2, p3);

            return a * b;
        }


        public static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Создать треугольник");
                Console.WriteLine("2. Создать прямоугольник");
                Console.WriteLine("3. Выйти из программы");
                Console.Write("Выберите опцию: ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            CreateTriangle();
                            break;
                        case 2:
                            CreateRectangle();
                            break;
                        case 3:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Неправильный выбор.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Неправильный выбор.");
                }
            }
        }

        static void CreateTriangle()
        {
            Console.WriteLine("Введите координаты вершин треугольника через пробел:");
            Console.Write("Вершина 1 (X Y): ");
            Point p1 = ReadPoint();
            Console.Write("Вершина 2 (X Y): ");
            Point p2 = ReadPoint();
            Console.Write("Вершина 3 (X Y): ");
            Point p3 = ReadPoint();

            if (IsTriangle(p1, p2, p3))
            {
                Console.WriteLine("Координаты вершин треугольника:");
                foreach (Point point in new Triangle(p1, p2, p3))
                {
                    Console.WriteLine(point);
                }

                DisplayCurrentTime();
                DisplayCurrentDate();
                DisplayCurrentDayOfWeek();

                double triangleArea = CalculateTriangleArea(p1, p2, p3);
                Console.WriteLine($"Площадь треугольника: {triangleArea:F2}");
            }
            else
            {
                Console.WriteLine("Треугольник с такими координатами невозможен.");
            }
        }

        static void CreateRectangle()
        {
            Console.WriteLine("Введите координаты вершин прямоугольника через пробел:");
            Console.Write("Вершина 1 (X Y): ");
            Point p1 = ReadPoint();
            Console.Write("Вершина 2 (X Y): ");
            Point p2 = ReadPoint();
            Console.Write("Вершина 3 (X Y): ");
            Point p3 = ReadPoint();
            Console.Write("Вершина 4 (X Y): ");
            Point p4 = ReadPoint();

            DisplayCurrentTime();
            DisplayCurrentDate();
            DisplayCurrentDayOfWeek();

            double rectangleArea = CalculateRectangleArea(p1, p2, p3);
            Console.WriteLine($"Площадь прямоугольника: {rectangleArea:F2}");
        }

        static Point ReadPoint()
        {
            string[] input = Console.ReadLine().Split();
            if (input.Length == 2 && int.TryParse(input[0], out int x) && int.TryParse(input[1], out int y))
            {
                return new Point(x, y);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Повторите ввод (X Y):");
                return ReadPoint();
            }
        }
    }
}
