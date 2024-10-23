using System;

namespace StudentApp
{
    // Абстрактный класс Person
    public abstract class Person
    {
        // Абстрактное поле (свойство) для имени
        public abstract string Name { get; }

        // Абстрактный метод для получения информации о человеке
        public abstract string GetInfo();
    }

    // Класс Student, производный от Person
    public class Student : Person
    {
        // Закрытое поле для имени студента
        private string _name;

        // Публичное свойство для имени (переопределение абстрактного свойства)
        public override string Name => _name;

        // Публичное свойство для возраста студента
        public int Age { get; set; }

        // Конструктор, принимающий только имя
        public Student(string name)
        {
            _name = name;
            Age = 0; // Возраст по умолчанию
        }

        // Конструктор, принимающий имя и возраст
        public Student(string name, int age)
        {
            _name = name;
            Age = age;
        }

        // Переопределение метода GetInfo()
        public override string GetInfo()
        {
            return $"Имя: {Name}, Возраст: {Age}";
        }

        // Метод для увеличения возраста студента на единицу
        public void BecomeOlder()
        {
            Age++;
        }

        // Переопределение метода ToString() класса Object
        public override string ToString()
        {
            return GetInfo();
        }
    }

    // Класс ITStudent, производный от Student
    public class ITStudent : Student
    {
        public string Specialization { get; set; }

        // Конструктор ITStudent, принимающий имя, возраст и специализацию
        public ITStudent(string name, int age, string specialization)
            : base(name, age)
        {
            Specialization = specialization;
        }

        // Переопределение метода GetInfo() с добавлением специализации
        public override string GetInfo()
        {
            return base.GetInfo() + $", Специализация: {Specialization}";
        }

        // Скрытие метода ToString() класса Student (не переопределение)
        new public string ToString()
        {
            return $"IT Студент: {Name}, Возраст: {Age}, Специализация: {Specialization}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создание объекта класса Student
            Student student1 = new Student("Иван");
            Console.WriteLine(student1); // Использует переопределенный ToString()

            student1.BecomeOlder();
            Console.WriteLine(student1); // Имя: Иван, Возраст: 1

            // Создание объекта класса ITStudent
            ITStudent itStudent1 = new ITStudent("Анна", 20, "Программирование");
            Console.WriteLine(itStudent1); // Использует скрытый ToString()

            itStudent1.BecomeOlder();
            Console.WriteLine(itStudent1.GetInfo()); // Имя: Анна, Возраст: 21, Специализация: Программирование

            // Демонстрация различия между переопределением и скрытием методов
            Console.WriteLine(itStudent1.GetInfo());  // Переопределенный метод из Student
            Console.WriteLine(((Student)itStudent1).GetInfo());  // Переопределенный метод из Student

            // Скрытие метода ToString()
            Console.WriteLine(itStudent1.ToString());  // Скрытый метод из ITStudent
            Console.WriteLine(((Student)itStudent1).ToString());  // Переопределенный метод из Student
        }
    }
}