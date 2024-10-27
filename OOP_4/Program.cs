using System;

namespace StudentApp
{
    // Интерфейс IPerson
    public interface IPerson
    {
        string Name { get; }
        string GetInfo();
    }
    // Интерфейс ISpecialist, производный от IPerson
    public interface ISpecialist : IPerson
    {
        string Specialization { get; }
        string GetSpecializationInfo();
    }
    // Абстрактный класс Person
    public abstract class Person : IPerson
    {
        // Абстрактное поле (свойство) для имени
        public abstract string Name { get; }

        // Абстрактный метод для получения информации о человеке
        public abstract string GetInfo();
    }

    // Класс Subject для любимого предмета студента
    public class Subject
    {
        public string SubjectName { get; set; }

        public Subject(string subjectName)
        {
            SubjectName = subjectName;
        }

        public override string ToString()
        {
            return SubjectName;
        }
    }

    // Класс Student, производный от Person
    public class Student : Person, ICloneable, IComparable<Student>
    {
        private string _name;
        public override string Name => _name;
        public int Age { get; set; }

        // Поле для любимого предмета
        public Subject FavoriteSubject { get; set; }

        // Конструктор, принимающий только имя
        public Student(string name)
        {
            _name = name;
            Age = 0; // Возраст по умолчанию
            FavoriteSubject = new Subject("Неизвестно"); // Предмет по умолчанию
        }

        // Конструктор, принимающий имя и возраст
        public Student(string name, int age)
        {
            _name = name;
            Age = age;
            FavoriteSubject = new Subject("Неизвестно"); // Предмет по умолчанию
        }

        // Переопределение метода GetInfo()
        public override string GetInfo()
        {
            return $"Имя: {Name}, Возраст: {Age}, Любимый предмет: {FavoriteSubject}";
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

        // Реализация метода Clone() из ICloneable
        public object Clone()
        {
            return new Student(_name, Age) { FavoriteSubject = new Subject(FavoriteSubject.SubjectName) };
        }

        // Реализация метода CompareTo() из IComparable
        public int CompareTo(Student other)
        {
            if (other == null) return 1;
            return Age.CompareTo(other.Age);
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
            FavoriteSubject = new Subject("Программирование"); // Установка любимого предмета по умолчанию
        }

        // Переопределение метода GetInfo() с добавлением специализации
        public override string GetInfo()
        {
            return base.GetInfo() + $", Специализация: {Specialization}";
        }
        // Реализация метода GetSpecializationInfo() из ISpecialist
        public string GetSpecializationInfo()
        {
            return $"Специализация: {Specialization}";
        }

        // Скрытие метода ToString() класса Student (не переопределение)
        new public string ToString()
        {
            return $"IT Студент: {Name}, Возраст: {Age}, Специализация: {Specialization}, Любимый предмет: {FavoriteSubject}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создание объекта класса Student
            Student student1 = new Student("Иван");
            student1.FavoriteSubject = new Subject("Математика");
            Console.WriteLine(student1); // Использует переопределенный ToString()

            student1.BecomeOlder();
            Console.WriteLine(student1); // Имя: Иван, Возраст: 1

            // Создание объекта класса ITStudent
            ITStudent itStudent1 = new ITStudent("Анна", 20, "Программирование");
            itStudent1.FavoriteSubject = new Subject("Информатика");
            Console.WriteLine(itStudent1); // Использует скрытый ToString()

            itStudent1.BecomeOlder();
            Console.WriteLine(itStudent1.GetInfo()); // Имя: Анна, Возраст: 21, Специализация: Программирование

            // Демонстрация клонирования студента
            Student clonedStudent = (Student)student1.Clone();
            Console.WriteLine($"Клонированный студент: {clonedStudent}");

            // Сравнение студентов по возрасту
            Console.WriteLine($"Сравнение студентов: {student1.CompareTo(itStudent1)}");

            Console.WriteLine(itStudent1.GetSpecializationInfo());  // Специализация: Программирование

            Console.WriteLine(((Student)itStudent1).GetInfo());  // Переопределенный метод из Student

            Console.WriteLine(itStudent1.ToString());  // Скрытый метод из ITStudent

            Console.WriteLine(((Student)itStudent1).ToString());  // Переопределенный метод из Student
        }
    }
}