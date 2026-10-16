# Лабораторная работа №3: Механизмы наследования в классах

Проект демонстрирует основные концепции объектно-ориентированного программирования на C#: наследование, полиморфизм, инкапсуляцию и работу с файлами.

## 📋 Содержание

- [Описание проекта](#описание-проекта)
- [Иерархия классов](#иерархия-классов)
- [Основные возможности](#основные-возможности)
- [Технологии](#технологии)
- [Установка и запуск](#установка-и-запуск)
- [Структура проекта](#структура-проекта)

## 📝 Описание проекта

Проект состоит из двух частей:
1. **BankLibrary** - библиотека классов с иерархией наследования
2. **BankTestApp** - Windows Forms приложение для тестирования функциональности

## 🏗️ Иерархия классов

```
Object (базовый класс .NET)
  └── Client (базовый класс проекта - клиент банка)
      ├── Employee (сотрудник)
      │   └── Prepod (преподаватель)
      └── Student (студент)
          └── StudentDistant (студент-заочник)
```

### Client (Клиент банка)
**Основные свойства:**
- `Id` - идентификатор
- `LastName`, `FirstName`, `MiddleName` - ФИО
- `Balance` - баланс счёта
- `BirthDate` - дата рождения
- `IsVip` - VIP-статус

**Ключевые методы:**
- `GetAge()` - вычисление возраста
- `Deposit()`, `Withdraw()` - операции со счётом
- `Transfer()` - перевод средств (3 перегруженные версии)
- `WriteToFile()` / `ReadFromFile()` - работа с файлами

### Employee : Client (Сотрудник)
**Дополнительные свойства:**
- `EmployeeId` - табельный номер
- `HireDate` - дата приёма на работу
- `Position` - должность

**Новые методы:**
- `GetExperience()` - вычисление стажа работы
- `AverageSalary()` - расчёт средней зарплаты (демонстрация передачи параметров)

### Prepod : Employee (Преподаватель)
**Дополнительные свойства:**
- `Department` - кафедра
- `Disciplines` - список дисциплин

**Новые методы:**
- `AddDiscipline()` / `RemoveDiscipline()` - управление дисциплинами
- `GetDisciplinesList()` - получение списка дисциплин

### Student : Client (Студент)
**Дополнительные свойства:**
- `Group` - учебная группа
- `Course` - курс обучения (1-5)
- `AverageGrade` - средний балл
- `Specialty` - специальность

**Новые методы:**
- `IsExcellentStudent()` - проверка на отличника (≥ 4.5)
- `HasScholarship()` - проверка наличия стипендии (≥ 4.0)
- `GetCourseName()` - название курса

### StudentDistant : Student (Студент-заочник)
**Дополнительные свойства:**
- `SessionCity` - город проживания
- `NextSessionDate` - дата следующей сессии
- `WorkPlace` - место работы

**Новые методы:**
- `IsSessionSoon()` - сессия скоро (< 30 дней)
- `DaysUntilSession()` - дней до сессии

## ✨ Основные возможности

### 1. Наследование
- 4 уровня наследования
- Каждый класс расширяет функциональность базового
- Цепочки вызовов конструкторов: `base()`

### 2. Перегрузка методов
**Перегруженные конструкторы:**
```csharp
public Client()
public Client(string lastName, string firstName)
public Client(string lastName, string firstName, decimal balance)
public Client(int id, string lastName, string firstName, ...)
```

**Перегруженные методы Transfer:**
```csharp
public bool Transfer(Client recipient, decimal amount)
public bool Transfer(Client recipient, decimal amount, string comment)
public bool Transfer(Client recipient, decimal amount, bool withFee)
```

**Перегруженные методы работы с файлами:**
```csharp
public void WriteToFile(string path)
public void WriteToFile(SaveFileDialog dialog)
```

### 3. Работа с файлами (StreamReader/StreamWriter)
- Запись данных объектов в текстовые файлы
- Чтение данных из файлов
- Каждый класс сохраняет свои специфические поля

**Пример формата файла:**
```
ID:1
Фамилия:Иванов
Имя:Иван
Возраст:25
ТабельныйНомер:101
Должность:Менеджер
Стаж:3
```

### 4. Передача параметров в методы

**По значению (по умолчанию):**
```csharp
public decimal AverageSalary(int colMonth, decimal[] salaries)
// colMonth - передаётся по значению (изменения НЕ сохраняются)
// salaries - ссылочный тип (изменения СОХРАНЯЮТСЯ)
```

**По ссылке (ref):**
```csharp
public decimal AverageSalary(ref int colMonth, decimal[] salaries)
// colMonth - передаётся по ссылке (изменения СОХРАНЯЮТСЯ)
```

**Выходные параметры (out):**
```csharp
public decimal AverageSalary(decimal[] salaries, 
                            out decimal minSalary, 
                            out decimal maxSalary)
// Метод возвращает 1 значение через return + 2 через out
```

**Необязательные параметры (3 параметра):**
```csharp
public string GetClientSummary(bool includeBalance = true, 
                              bool includeAge = true, 
                              bool includeVipStatus = false)
```

### 5. Преобразования типов

**UpCasting (восходящее преобразование):**
```csharp
Student student = new Student("Петров", "Петр", "ПИ-101", 2);
Client clientRef = student;  // Неявное преобразование (безопасно)
```

**DownCasting (нисходящее преобразование):**
```csharp
Student studentRef = (Student)clientRef;  // Явное приведение типа
```

**Оператор is (проверка типа):**
```csharp
if (clientRef is Student)
{
    Console.WriteLine("Объект является Student");
}
```

**Оператор as (безопасное приведение):**
```csharp
Student student = clientRef as Student;
if (student != null)
{
    // Безопасно используем методы Student
}
```

### 6. Вычислительные методы
- `GetAge()` - вычисление возраста клиента
- `GetExperience()` - вычисление стажа сотрудника
- `DaysUntilSession()` - вычисление дней до сессии
- `IsExcellentStudent()` - проверка на отличника

### 7. Наследование от Object
Все классы неявно наследуются от `System.Object`:
- `ToString()` - переопределён в каждом классе
- `GetType()` - получение типа объекта
- `GetHashCode()` - получение хеш-кода

## 🛠️ Технологии

- **Язык:** C# (.NET Framework 4.7.2)
- **IDE:** Visual Studio 2022
- **UI:** Windows Forms
- **Namespace:** System.IO (StreamReader, StreamWriter)

## 📦 Установка и запуск

### Требования
- Windows 10/11
- Visual Studio 2019 или новее (с поддержкой .NET Framework 4.7.2)
- .NET Framework 4.7.2 или выше

### Шаги установки

1. **Клонировать репозиторий:**
```bash
git clone https://github.com/ваш-username/BankLibrary.git
cd BankLibrary
```

2. **Открыть решение:**
- Запустить Visual Studio
- Открыть файл `BankLibrary.sln`

3. **Скомпилировать проект:**
- Build → Build Solution (Ctrl+Shift+B)

4. **Запустить приложение:**
- Установить `BankTestApp` как стартовый проект (правый клик → Set as Startup Project)
- Debug → Start Debugging (F5)

### Быстрый запуск через командную строку

```bash
# Сборка проекта
dotnet build

# Запуск (из директории BankTestApp/bin/Debug)
BankTestApp.exe
```

## 📂 Структура проекта

```
BankLibrary/
├── BankLibrary/              # Библиотека классов
│   ├── Client.cs            # Базовый класс (клиент банка)
│   ├── Employee.cs          # Сотрудник (наследник Client)
│   ├── Prepod.cs            # Преподаватель (наследник Employee)
│   ├── Student.cs           # Студент (наследник Client)
│   └── StudentDistant.cs    # Студент-заочник (наследник Student)
│
├── BankTestApp/              # Windows Forms приложение
│   ├── Form1.cs             # Главная форма с тестами
│   ├── Form1.Designer.cs    # Дизайн формы
│   └── Program.cs           # Точка входа
│
├── BankLibrary.sln          # Файл решения
├── README.md                # Этот файл
└── Пояснения_к_лабораторной_работе_3.md  # Подробная документация
```

## 🎨 Функциональность приложения

Приложение содержит вкладки для тестирования:

### 1. Вкладка "Client"
- Создание клиентов
- Работа с коллекциями
- Статические методы
- Загрузка фотографии
- Сохранение/загрузка из файла

### 2. Вкладка "Параметры"
- Передача по значению
- Передача по ссылке (ref)
- Выходные параметры (out)
- Необязательные параметры

### 3. Вкладка "Employee"
- Создание сотрудников
- Вычисление стажа работы
- Наследование свойств и методов

### 4. Вкладка "Prepod"
- Создание преподавателей
- Управление дисциплинами
- Демонстрация 3-х уровней наследования

### 5. Вкладка "Student"
- Создание студентов
- Проверка на отличника
- Расчёт стипендии

### 6. Вкладка "StudentDistant"
- Создание студентов-заочников
- Расчёт дней до сессии
- Демонстрация 4-х уровней наследования

### 7. Вкладка "Преобразования"
- UpCasting (Student → Client)
- DownCasting (Client → Student)
- Оператор is
- Оператор as

## 📚 Дополнительная документация

Подробное описание всех концепций и примеры кода находятся в файле:
[Пояснения_к_лабораторной_работе_3.md](./Пояснения_к_лабораторной_работе_3.md)

Документ включает:
- Детальное описание каждого класса
- Примеры использования всех методов
- Объяснение механизмов наследования
- Примеры передачи параметров
- Схемы преобразований типов
- Выводы по работе

## 🎯 Требования лабораторной работы

Все требования к лабораторной работе №3 выполнены:

- ✅ Наследование (4 уровня)
- ✅ Перегрузка методов (конструкторы, Transfer, работа с файлами)
- ✅ Работа с файлами (StreamReader/StreamWriter)
- ✅ Передача параметров (по значению, ref, out, необязательные)
- ✅ Вычислительные методы (возраст, стаж, дни до сессии)
- ✅ Преобразования типов (UpCasting, DownCasting, is, as)
- ✅ Наследование от Object (ToString, GetType, GetHashCode)

## 🤝 Автор

Проект выполнен в рамках учебной лабораторной работы №3 по дисциплине "Программирование".

**Дата выполнения:** Октябрь 2026

## 📄 Лицензия

Этот проект создан в образовательных целях.

---

### 💡 Полезные ссылки

- [Документация C# - Наследование](https://learn.microsoft.com/ru-ru/dotnet/csharp/fundamentals/tutorials/inheritance)
- [StreamWriter Class](https://docs.microsoft.com/ru-ru/dotnet/api/system.io.streamwriter)
- [StreamReader Class](https://docs.microsoft.com/ru-ru/dotnet/api/system.io.streamreader)
- [Методы C#](https://learn.microsoft.com/ru-ru/dotnet/csharp/programming-guide/classes-and-structs/methods)
- [Приведение и преобразование типов](https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/types/casting-and-type-conversions)

1. Изучение и применение **свойств (Properties)** в C#
2. Работа с **коллекциями** (List<T>)
3. Реализация **привязки данных** (DataBindings) к элементам формы
4. Работа с **графическими элементами** (PictureBox, OpenFileDialog)
5. Демонстрация **перегрузки методов** (method overloading)
6. Использование **статических методов** класса

## 🏗️ Структура проекта

Проект состоит из двух частей:

### 1. **BankLibrary** - Библиотека классов
Содержит основной класс `Client` с бизнес-логикой:
- Свойства клиента (ID, ФИО, баланс, дата рождения и т.д.)
- Методы для работы со счетом
- Статические методы для вспомогательных операций

### 2. **BankTestApp** - Windows Forms приложение
Пользовательский интерфейс для демонстрации функционала:
- Форма с элементами управления
- Коллекция клиентов
- Привязка данных
- Работа с изображениями

## 📚 Подробное объяснение кода

### Класс Client (BankLibrary/Client.cs)

#### 1. Свойства (Properties)

Свойства - это члены класса, которые предоставляют гибкий механизм для чтения, записи или вычисления значений полей.

```csharp
public string LastName
{
    get { return lastName; }
    set
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            lastName = "";
        }
        else if (char.IsUpper(value[0]))
        {
            lastName = value;
        }
        else
        {
            throw new ArgumentException("Фамилия должна начинаться с заглавной буквы!");
        }
    }
}
```

**Объяснение:**
- `get` - аксессор для чтения значения
- `set` - аксессор для записи значения с валидацией
- В данном примере проверяется, что фамилия начинается с заглавной буквы
- Это демонстрирует **инкапсуляцию** - скрытие внутренней реализации и контроль доступа к данным

**Автоматические свойства:**
```csharp
public int Id { get; set; }
public string FirstName { get; set; }
public decimal Balance { get; set; }
```
Компилятор автоматически создает скрытое поле для хранения значения.

#### 2. Статические члены класса

**Статическое поле:**
```csharp
public static string BankName;
```
- Принадлежит классу, а не объекту
- Одно значение для всех экземпляров класса
- Доступ через имя класса: `Client.BankName`

**Константа:**
```csharp
public const decimal TransferFeePercent = 1.5m;
```
- Значение устанавливается при компиляции
- Не может быть изменено во время выполнения

**Readonly поле:**
```csharp
public readonly DateTime RegistrationDate;
```
- Может быть установлено только в конструкторе
- Каждый объект имеет своё значение
- Не может быть изменено после создания объекта

**Статический конструктор:**
```csharp
static Client()
{
    BankName = "ООО Тмыв Денег";
}
```
- Вызывается автоматически перед первым использованием класса
- Используется для инициализации статических полей

#### 3. Перегрузка конструкторов

```csharp
// Конструктор без параметров
public Client() { ... }

// Конструктор с 2 параметрами
public Client(string lastName, string firstName) { ... }

// Конструктор с 3 параметрами
public Client(string lastName, string firstName, decimal initialBalance) { ... }

// Полный конструктор
public Client(int id, string lastName, string firstName, string middleName,
              string passportNumber, string phoneNumber, decimal balance, bool isVip) { ... }
```

**Объяснение:**
- **Перегрузка конструкторов** позволяет создавать объекты разными способами
- Каждый конструктор принимает разное количество параметров
- Это пример **полиморфизма** - одно имя, разные реализации

#### 4. Методы класса

**Обычные методы:**
```csharp
public string GetFullName()
{
    return $"{LastName} {FirstName} {MiddleName}".Trim();
}

public void Deposit(decimal amount)
{
    if (amount > 0)
    {
        Balance += amount;
    }
}

public bool Withdraw(decimal amount)
{
    if (amount > 0 && amount <= Balance)
    {
        Balance -= amount;
        return true;
    }
    return false;
}
```

#### 5. Перегрузка методов (Method Overloading)

Три варианта метода `Transfer()` с разными параметрами:

```csharp
// Вариант 1: базовый перевод
public bool Transfer(Client recipient, decimal amount)
{
    if (amount <= 0 || amount > Balance)
        return false;
    
    Balance -= amount;
    recipient.Balance += amount;
    return true;
}

// Вариант 2: перевод с комментарием
public bool Transfer(Client recipient, decimal amount, string comment)
{
    if (amount <= 0 || amount > Balance)
        return false;
    
    Balance -= amount;
    recipient.Balance += amount;
    return true;
}

// Вариант 3: перевод с учетом комиссии
public bool Transfer(Client recipient, decimal amount, bool withFee)
{
    if (amount <= 0)
        return false;
    
    decimal totalAmount = amount;
    if (withFee)
    {
        totalAmount = amount + (amount * TransferFeePercent / 100);
    }
    
    if (totalAmount > Balance)
        return false;
    
    Balance -= totalAmount;
    recipient.Balance += amount;
    return true;
}
```

**Объяснение:**
- Все три метода имеют одинаковое имя `Transfer`
- Отличаются **сигнатурой** (типом и количеством параметров)
- Компилятор автоматически выбирает нужный метод по переданным аргументам
- Это **полиморфизм времени компиляции**

#### 6. Статические методы

```csharp
public static int CalculateAge(DateTime birthDate)
{
    DateTime today = DateTime.Today;
    int age = today.Year - birthDate.Year;
    
    if (birthDate.Date > today.AddYears(-age))
    {
        age--;
    }
    
    return age;
}

public static Client GetRicherClient(Client client1, Client client2)
{
    if (client1 == null || client2 == null)
        return null;
    
    return client1.Balance >= client2.Balance ? client1 : client2;
}
```

**Объяснение:**
- Статические методы принадлежат классу, а не объекту
- Вызываются через имя класса: `Client.CalculateAge(date)`
- Не имеют доступа к полям экземпляра (this)
- Используются для вспомогательных операций

#### 7. Переопределение метода ToString()

```csharp
public override string ToString()
{
    return $"{GetFullName()}, Баланс: {Balance:F2} руб.";
}
```

**Объяснение:**
- Переопределяем метод из базового класса `Object`
- Вызывается автоматически при выводе объекта в строку
- Используется для удобного отображения в ListBox

### Форма BankTestApp (Form1.cs)

#### 1. Коллекции

```csharp
private List<Client> clients = new List<Client>();
```

**Объяснение:**
- `List<T>` - обобщенная коллекция для хранения объектов типа T
- Динамический размер (автоматически увеличивается)
- Методы: `Add()`, `Remove()`, `Clear()`, индексатор `[]`

#### 2. Привязка данных (DataBindings)

```csharp
private void InitializeBindings()
{
    currentClient = new Client("Иванов", "Иван", 10000m);
    
    txtLastName.DataBindings.Add("Text", currentClient, "LastName", 
        false, DataSourceUpdateMode.OnPropertyChanged);
    txtFirstName.DataBindings.Add("Text", currentClient, "FirstName", 
        false, DataSourceUpdateMode.OnPropertyChanged);
    txtBalance.DataBindings.Add("Text", currentClient, "Balance", 
        true, DataSourceUpdateMode.OnPropertyChanged, "0", "F2");
    dtpBirthDate.DataBindings.Add("Value", currentClient, "BirthDate", 
        false, DataSourceUpdateMode.OnPropertyChanged);
    chkIsVip.DataBindings.Add("Checked", currentClient, "IsVip", 
        false, DataSourceUpdateMode.OnPropertyChanged);
}
```

**Объяснение:**
- **DataBindings** связывают свойство элемента управления со свойством объекта
- Изменения автоматически синхронизируются в обе стороны
- Параметры:
  - `"Text"` - свойство элемента управления
  - `currentClient` - объект-источник данных
  - `"LastName"` - свойство объекта
  - `DataSourceUpdateMode.OnPropertyChanged` - режим обновления

#### 3. Работа с коллекцией и ListBox

```csharp
private void btnAddClient_Click(object sender, EventArgs e)
{
    try
    {
        Client newClient = new Client
        {
            Id = clients.Count + 1,
            LastName = txtLastName.Text,
            FirstName = txtFirstName.Text,
            Balance = decimal.Parse(txtBalance.Text),
            BirthDate = dtpBirthDate.Value,
            IsVip = chkIsVip.Checked
        };
        
        clients.Add(newClient);
        UpdateClientList();
        
        Output($"✅ Клиент добавлен в коллекцию!\n\nВсего клиентов: {clients.Count}");
    }
    catch (ArgumentException ex)
    {
        Output($"❌ ОШИБКА: {ex.Message}");
    }
}

private void UpdateClientList()
{
    listBoxClients.Items.Clear();
    foreach (Client c in clients)
    {
        listBoxClients.Items.Add(c.ToString());
    }
}
```

**Объяснение:**
- Создаём новый объект с помощью **инициализатора объекта**
- Добавляем в коллекцию методом `Add()`
- Обновляем ListBox, перебирая коллекцию через `foreach`
- `ToString()` вызывается автоматически при добавлении в ListBox

#### 4. Работа с графикой

```csharp
private void btnLoadFoto_Click(object sender, EventArgs e)
{
    if (openFileDialogFoto.ShowDialog() == DialogResult.OK)
    {
        try
        {
            Image img = Image.FromFile(openFileDialogFoto.FileName);
            pictureBoxFoto.Image = img;
            pictureBoxFoto.SizeMode = PictureBoxSizeMode.Zoom;
            
            if (currentClient != null)
            {
                currentClient.PhotoPath = openFileDialogFoto.FileName;
            }
            
            Output($"✅ Фото загружено успешно!");
        }
        catch (Exception ex)
        {
            Output($"❌ ОШИБКА при загрузке изображения:\n{ex.Message}");
        }
    }
}
```

**Объяснение:**
- `OpenFileDialog` - диалоговое окно для выбора файла
- `Image.FromFile()` - загрузка изображения из файла
- `PictureBox.Image` - отображение изображения в элементе управления
- `SizeMode.Zoom` - масштабирование с сохранением пропорций

#### 5. Обработка событий

```csharp
private void listBoxClients_SelectedIndexChanged(object sender, EventArgs e)
{
    if (listBoxClients.SelectedIndex >= 0)
    {
        Client selectedClient = clients[listBoxClients.SelectedIndex];
        Output($"📋 ВЫБРАН КЛИЕНТ:\n\n{selectedClient.GetClientInfo()}");
        
        if (!string.IsNullOrEmpty(selectedClient.PhotoPath))
        {
            try
            {
                pictureBoxFoto.Image = Image.FromFile(selectedClient.PhotoPath);
            }
            catch { }
        }
    }
}
```

**Объяснение:**
- Событие срабатывает при выборе элемента в ListBox
- `SelectedIndex` - индекс выбранного элемента
- Получаем объект клиента из коллекции по индексу
- Отображаем информацию и фото выбранного клиента

## 🔑 Ключевые концепции ООП в проекте

### 1. **Инкапсуляция**
- Скрытие данных через private поля
- Доступ к данным через свойства с валидацией
- Пример: `LastName` с проверкой заглавной буквы

### 2. **Полиморфизм**
- Перегрузка конструкторов (4 варианта)
- Перегрузка методов (`Transfer` - 3 варианта)
- Переопределение `ToString()`

### 3. **Абстракция**
- Класс `Client` представляет абстрактную модель клиента банка
- Скрывает сложность реализации за простым интерфейсом

## 🎓 Демонстрируемые возможности

### Windows Forms элементы:
- ✅ **TextBox** - ввод текста
- ✅ **ListBox** - список клиентов
- ✅ **Button** - кнопки действий
- ✅ **DateTimePicker** - выбор даты
- ✅ **CheckBox** - флаг VIP-клиента
- ✅ **PictureBox** - отображение фото
- ✅ **OpenFileDialog** - выбор файла

### C# возможности:
- ✅ Свойства с get/set и валидацией
- ✅ Автоматические свойства
- ✅ Коллекции (List<T>)
- ✅ Привязка данных (DataBindings)
- ✅ Перегрузка методов и конструкторов
- ✅ Статические члены класса
- ✅ Обработка исключений (try-catch)
- ✅ Инициализаторы объектов

## 🚀 Как запустить проект

1. Откройте решение `BankLibrary.slnx` в Visual Studio
2. Постройте решение (Build → Build Solution)
3. Запустите проект BankTestApp (F5)

## 📖 Как использовать приложение

1. **Добавление клиента:**
   - Заполните поля: Фамилия, Имя, Баланс
   - Выберите дату рождения
   - Отметьте VIP при необходимости
   - Нажмите "Добавить клиента"

2. **Загрузка фото:**
   - Нажмите "Загрузить фото"
   - Выберите изображение
   - Фото отобразится в PictureBox

3. **Просмотр клиентов:**
   - Выберите клиента из списка
   - Информация отобразится внизу

4. **Статические методы:**
   - Нажмите "Вызвать статический метод"
   - Увидите расчет возраста и сравнение клиентов

## 📝 Защита лабораторной работы

### Вопросы, которые может задать преподаватель:

**1. Что такое свойства в C#?**
- Свойства - это члены класса, которые предоставляют гибкий механизм для чтения и записи полей
- Состоят из аксессоров get и set
- Позволяют добавить валидацию и логику при обращении к данным

**2. В чем разница между полем и свойством?**
- Поле - это переменная класса
- Свойство - это метод, который выглядит как поле
- Свойства обеспечивают инкапсуляцию и контроль доступа

**3. Что такое перегрузка методов?**
- Несколько методов с одинаковым именем, но разными параметрами
- Компилятор выбирает нужный метод по сигнатуре
- Пример полиморфизма времени компиляции

**4. Зачем нужны статические члены класса?**
- Принадлежат классу, а не объекту
- Общие данные для всех экземпляров
- Вспомогательные методы, не зависящие от состояния объекта

**5. Что такое привязка данных?**
- Механизм автоматической синхронизации между UI и объектом
- Изменения в TextBox автоматически отражаются в свойстве объекта
- Упрощает код и уменьшает количество обработчиков событий

**6. В чем разница между const и readonly?**
- `const` - константа времени компиляции, общая для всех
- `readonly` - константа времени выполнения, может иметь разные значения для каждого объекта

**7. Зачем переопределять ToString()?**
- Для удобного отображения объекта в строковом виде
- Автоматически вызывается при выводе объекта
- Используется в ListBox, ComboBox и других элементах

## 📊 Структура решения

```
BankLibrary/
├── BankLibrary/              # Библиотека классов
│   ├── Client.cs            # Основной класс клиента
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

## 👨‍💻 Автор

Выполнено в рамках лабораторной работы №2 по дисциплине "Объектно-ориентированное программирование"

## 📅 Дата создания

Октябрь 2026
#   l a b o r a t o r n a y a - r a b o t a - 3 - o p  
 #   l a b o r a t o r n a y a - r a b o t a - 3 - o p  
 