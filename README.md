# Лабораторная работа №3 - Механизмы наследования в классах

## 📋 Описание проекта

Данный проект представляет собой приложение для управления клиентами банка, сотрудниками, студентами и преподавателями, разработанное в рамках лабораторной работы №3 по дисциплине "Объектно-ориентированное программирование". Приложение демонстрирует механизмы наследования, перегрузку методов, работу с файлами и различные способы передачи параметров.

## 🎯 Цели лабораторной работы

1. Изучение и применение **механизмов наследования** в C#
2. Построение **иерархии классов** с использованием наследования
3. Реализация **перегруженных методов** чтения/записи файлов
4. Демонстрация различных способов **передачи параметров**: по значению, по ссылке (ref), выходные параметры (out)
5. Реализация методов с **необязательными параметрами**
6. Демонстрация **преобразований типов** (UpCasting и DownCasting)
7. Разработка **вычислительных методов** (возраст, стаж, дни до сессии)

## 🏗️ Иерархия классов

```
Object (базовый класс .NET)
  └── Client (базовый класс проекта)
      ├── Employee (Сотрудник)
      │   └── Prepod (Преподаватель)
      └── Student (Студент)
          └── StudentDistant (Студент-заочник)
```

### Класс Client
**Основные свойства:**
- `Id` - идентификатор клиента
- `LastName`, `FirstName`, `MiddleName` - ФИО
- `Balance` - баланс счёта
- `BirthDate` - дата рождения
- `IsVip` - признак VIP-клиента

**Ключевые методы:**
- `GetAge()` - вычисление возраста клиента
- `GetClientSummary()` - метод с 3 необязательными параметрами
- `Transfer()` - перевод средств (3 перегруженные версии)
- `WriteToFile()`, `ReadFromFile()` - работа с файлами (2 перегруженные версии)

### Класс Employee : Client
**Дополнительные свойства:**
- `EmployeeId` - табельный номер
- `HireDate` - дата приёма на работу
- `Position` - должность

**Новые методы:**
- `GetExperience()` - вычисление стажа работы в годах
- `AverageSalary()` - демонстрация передачи параметров (по значению, ref, out)

### Класс Prepod : Employee
**Дополнительные свойства:**
- `Department` - кафедра
- `Disciplines` - список преподаваемых дисциплин

**Новые методы:**
- `AddDiscipline()`, `RemoveDiscipline()` - управление дисциплинами
- `GetDisciplinesList()` - получение списка дисциплин

### Класс Student : Client
**Дополнительные свойства:**
- `Group` - учебная группа
- `Course` - курс обучения
- `AverageGrade` - средний балл
- `Specialty` - специальность

**Новые методы:**
- `IsExcellentStudent()` - проверка на отличника (средний балл ≥ 4.5)
- `HasScholarship()` - проверка наличия стипендии (средний балл ≥ 4.0)

### Класс StudentDistant : Student
**Дополнительные свойства:**
- `SessionCity` - город проживания
- `NextSessionDate` - дата следующей сессии
- `WorkPlace` - место работы

**Новые методы:**
- `IsSessionSoon()` - проверка, скоро ли сессия (менее 30 дней)
- `DaysUntilSession()` - количество дней до сессии

## 📚 Реализованные механизмы

### 1. Перегрузка методов

#### Перегрузка конструкторов
```csharp
// Конструктор по умолчанию
public Client()

// Конструктор с двумя параметрами
public Client(string lastName, string firstName)

// Конструктор с тремя параметрами
public Client(string lastName, string firstName, decimal initialBalance)

// Полный конструктор
public Client(int id, string lastName, string firstName, string middleName,
              string passportNumber, string phoneNumber, decimal balance, bool isVip)
```

#### Перегрузка методов чтения/записи файлов
```csharp
// Версия 1: принимает путь к файлу
public void WriteToFile(string path)

// Версия 2: принимает SaveFileDialog
public void WriteToFile(SaveFileDialog dialog)

// Аналогично для ReadFromFile
public void ReadFromFile(string path)
public void ReadFromFile(OpenFileDialog dialog)
```

### 2. Передача параметров

#### По значению (по умолчанию)
```csharp
public decimal AverageSalary(int colMonth, decimal[] salaries)
{
    colMonth = 999;  // изменение НЕ сохранится
    // ...
}
```

#### По ссылке (ref)
```csharp
public decimal AverageSalary(ref int colMonth, decimal[] salaries)
{
    colMonth = 999;  // изменение СОХРАНИТСЯ
    // ...
}
```

#### Выходные параметры (out)
```csharp
public decimal AverageSalary(decimal[] salaries, out decimal minSalary, out decimal maxSalary)
{
    // Метод возвращает 3 значения: return + 2 out параметра
}
```

#### Необязательные параметры (3 параметра)
```csharp
public string GetClientSummary(bool includeBalance = true, 
                              bool includeAge = true, 
                              bool includeVipStatus = false)
{
    // Можно вызывать с разным количеством аргументов
}
```

### 3. Работа с файлами (StreamReader/StreamWriter)

**Запись данных:**
```csharp
using (StreamWriter writer = new StreamWriter(path))
{
    writer.WriteLine($"ID:{Id}");
    writer.WriteLine($"Фамилия:{LastName}");
    writer.WriteLine($"Имя:{FirstName}");
    writer.WriteLine($"Возраст:{GetAge()}");
    // ... остальные поля
}
```

**Чтение данных:**
```csharp
using (StreamReader reader = new StreamReader(path))
{
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        string[] parts = line.Split(':');
        // Обработка данных
    }
}
```

### 4. Преобразования типов

#### UpCasting (восходящее преобразование)
```csharp
Student student = new Student("Петров", "Петр", "ПИ-101", 2);
Client clientRef = student;  // НЕЯВНОЕ преобразование
```

#### DownCasting (нисходящее преобразование)
```csharp
Client clientRef = student;
Student studentRef = (Student)clientRef;  // ЯВНОЕ преобразование
```

#### Безопасная проверка типов
```csharp
// Оператор is
if (clientRef is Student)
{
    Student s = (Student)clientRef;
}

// Оператор as
Student s = clientRef as Student;
if (s != null)
{
    // Безопасное использование
}
```

### 5. Вычислительные методы

```csharp
// Вычисление возраста
public int GetAge()
{
    return CalculateAge(this.BirthDate);
}

// Вычисление стажа работы
public int GetExperience()
{
    DateTime today = DateTime.Today;
    int years = today.Year - HireDate.Year;
    if (HireDate.Date > today.AddYears(-years))
        years--;
    return years;
}

// Вычисление дней до сессии
public int DaysUntilSession()
{
    TimeSpan diff = NextSessionDate - DateTime.Now;
    return diff.Days;
}
```

## 🎓 Демонстрируемые возможности

### Механизмы наследования:
- ✅ Иерархия классов с 4 уровнями наследования
- ✅ Цепочки вызовов конструкторов базового класса
- ✅ Наследование свойств и методов
- ✅ Переопределение методов (ToString)
- ✅ Демонстрация наследования от Object

### Перегрузка методов:
- ✅ Перегруженные конструкторы (4 версии)
- ✅ Перегруженные методы Transfer (3 версии)
- ✅ Перегруженные методы чтения/записи (2 версии в каждом классе)

### Передача параметров:
- ✅ По значению (int)
- ✅ По ссылке (ref)
- ✅ Выходные параметры (out)
- ✅ Необязательные параметры (3 параметра)

### Работа с файлами:
- ✅ StreamWriter для записи
- ✅ StreamReader для чтения
- ✅ Реализовано в каждом классе иерархии

### Преобразования типов:
- ✅ UpCasting (неявное)
- ✅ DownCasting (явное)
- ✅ Операторы is и as

### Windows Forms элементы:
- ✅ TextBox, ListBox, Button
- ✅ DateTimePicker
- ✅ CheckBox
- ✅ ComboBox для выбора из справочников
- ✅ OpenFileDialog, SaveFileDialog

## 🚀 Как запустить проект

1. Откройте решение `BankLibrary.slnx` в Visual Studio
2. Постройте решение (Build → Build Solution)
3. Запустите проект BankTestApp (F5)

## 📖 Как использовать приложение

### Вкладка "Клиент (Client)":
- Заполните поля ФИО, баланс, дата рождения
- Протестируйте статические методы
- Добавьте клиента в коллекцию

### Вкладка "Сотрудник (Employee)":
- Создайте объект Employee
- Протестируйте передачу параметров:
  - По значению
  - По ссылке (ref)
  - Выходные параметры (out)
- Сохраните/загрузите данные в файл

### Вкладка "Преподаватель (Prepod)":
- Создайте объект Prepod
- Выберите кафедру из справочника
- Назначьте дисциплины
- Продемонстрируйте наследование методов

### Вкладка "Студент (Student)":
- Создайте объект Student или StudentDistant
- Продемонстрируйте цепочку наследования
- Протестируйте преобразования типов (UpCasting/DownCasting)

### Вкладка "Необязательные параметры":
- Протестируйте все варианты вызова метода с необязательными параметрами
- Используйте именованные параметры

## 📊 Структура решения

```
BankLibrary/
├── BankLibrary/              # Библиотека классов
│   ├── Client.cs            # Базовый класс
│   ├── Employee.cs          # Наследник Client
│   ├── Prepod.cs            # Наследник Employee
│   ├── Student.cs           # Наследник Client
│   ├── StudentDistant.cs    # Наследник Student
│   └── Properties/
│       └── AssemblyInfo.cs
├── BankTestApp/             # Windows Forms приложение
│   ├── Form1.cs            # Код формы
│   ├── Form1.Designer.cs   # Дизайнер формы
│   ├── Form1.resx          # Ресурсы формы
│   ├── Program.cs          # Точка входа
│   └── Properties/
└── BankLibrary.slnx        # Файл решения
```

## 🔑 Ключевые концепции ООП в проекте

### 1. **Наследование**
- Повторное использование кода базового класса
- Расширение функциональности в классах-наследниках
- Цепочка: Object → Client → Employee → Prepod

### 2. **Инкапсуляция**
- Свойства с get/set
- Валидация данных (например, LastName должна начинаться с заглавной буквы)
- Скрытие реализации

### 3. **Полиморфизм**
- Перегрузка конструкторов и методов
- Переопределение ToString()
- Различное поведение методов в зависимости от типа

### 4. **Абстракция**
- Иерархия от общего (Client) к специфичному (StudentDistant)
- Выделение общих характеристик в базовый класс

## 📝 Выполненные требования лабораторной работы №3

✅ Создана иерархия классов с наследованием  
✅ Реализованы перегруженные конструкторы  
✅ Разработаны перегруженные методы чтения/записи файлов (StreamReader/StreamWriter)  
✅ Созданы вычислительные методы (возраст, стаж, дни до сессии)  
✅ Продемонстрированы все способы передачи параметров:
- По значению
- По ссылке (ref)
- Выходные параметры (out)
- Необязательные параметры (3 параметра)

✅ Реализованы преобразования типов (UpCasting и DownCasting)  
✅ Продемонстрировано наследование от Object  
✅ Разработан интерфейс пользователя для тестирования

## 📄 Подробная документация

Подробное описание реализации всех механизмов находится в файле:  
`Пояснения_к_лабораторной_работе_3.md`

## 👨‍💻 Автор

Выполнено в рамках лабораторной работы №3 по дисциплине "Объектно-ориентированное программирование"

## 📅 Дата создания

Октябрь 2026
