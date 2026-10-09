# Лабораторная работа №2 - Объектно-ориентированное программирование

## 📋 Описание проекта

Данный проект представляет собой приложение для управления клиентами банка, разработанное в рамках лабораторной работы №2 по дисциплине "Объектно-ориентированное программирование". Приложение демонстрирует основные концепции ООП: свойства, коллекции, методы, привязку данных и работу с графикой в Windows Forms.

## 🎯 Цели лабораторной работы

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
