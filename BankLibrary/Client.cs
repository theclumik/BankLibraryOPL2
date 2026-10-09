using System;

namespace BankLibrary
{
    /// <summary>
    /// Класс Client — представляет клиента банка.
    /// </summary>


    public class Client
    {
        /// СВОЙСТВА КЛАССА (Properties)

        private string lastName;
        private DateTime birthDate;
        private string photoPath;

        /// <summary>
        /// Уникальный идентификатор клиента в банковской системе.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Фамилия клиента. Должна начинаться с заглавной буквы.
        /// </summary>
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

        /// <summary>
        /// Имя клиента.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество клиента.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Номер паспорта клиента.
        /// </summary>
        public string PassportNumber { get; set; }

        /// <summary>
        /// Контактный телефон клиента.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Баланс счёта клиента в рублях.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Признак VIP-клиента.
        /// </summary>
        public bool IsVip { get; set; }

        /// <summary>
        /// Дата рождения клиента (для DateTimePicker).
        /// </summary>
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        /// <summary>
        /// Путь к фотографии клиента.
        /// </summary>
        public string PhotoPath
        {
            get { return photoPath; }
            set { photoPath = value; }
        }


        //СТАТИЧЕСКИЕ ПОЛЯ

        /// <summary>
        /// Название банка.
        /// Все объекты Client используют одно и то же значение BankName.
        /// </summary>
        public static string BankName;

        /// <summary>
        /// Константа — комиссия за перевод (в процентах).
        /// </summary>
        public const decimal TransferFeePercent = 1.5m;


        /// Дата регистрации клиента.
        /// У каждого объекта может быть своё значение RegistrationDate.

        public readonly DateTime RegistrationDate;


        //СТАТИЧЕСКИЙ КОНСТРУКТОР

        /// <summary>
        /// Установил название банка.
        /// </summary>
        static Client()
        {
            BankName = "ООО Тмыв Денег";
        }


        // КОНСТРУКТОРЫ ЭКЗЕМПЛЯРА


        public Client()
        {
            Id = 0;
            lastName = "";
            FirstName = "";
            MiddleName = "";
            PassportNumber = "";
            PhoneNumber = "";
            Balance = 0m;
            IsVip = false;
            birthDate = new DateTime(2000, 1, 1);
            photoPath = "";
            RegistrationDate = DateTime.Now;
        }

        /// <summary>
        /// Конструктор с двумя параметрами: фамилия и имя.
        /// </summary>
        /// <param name="lastName">Фамилия клиента</param>
        /// <param name="firstName">Имя клиента</param>
        public Client(string lastName, string firstName)
        {
            this.Id = 0;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.MiddleName = "";
            this.PassportNumber = "";
            this.PhoneNumber = "";
            this.Balance = 0m;
            this.IsVip = false;
            this.birthDate = new DateTime(2000, 1, 1);
            this.photoPath = "";
            this.RegistrationDate = DateTime.Now;
        }

        /// <summary>
        /// Конструктор с тремя параметрами: фамилия, имя, начальный баланс.
        /// Ещё один вариант перегруженного конструктора.
        /// </summary>
        public Client(string lastName, string firstName, decimal initialBalance)
        {
            this.Id = 0;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.MiddleName = "";
            this.PassportNumber = "";
            this.PhoneNumber = "";
            this.Balance = initialBalance;
            this.IsVip = false;
            this.birthDate = new DateTime(2000, 1, 1);
            this.photoPath = "";
            this.RegistrationDate = DateTime.Now;
        }

        /// <summary>
        /// Полный конструктор со всеми параметрами.
        /// Используется когда нужно создать клиента с полной информацией сразу.
        /// </summary>
        public Client(int id, string lastName, string firstName, string middleName,
                      string passportNumber, string phoneNumber, decimal balance, bool isVip)
        {
            this.Id = id;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.PassportNumber = passportNumber;
            this.PhoneNumber = phoneNumber;
            this.Balance = balance;
            this.IsVip = isVip;
            this.birthDate = new DateTime(2000, 1, 1);
            this.photoPath = "";
            this.RegistrationDate = DateTime.Now;
        }


        // МЕТОДЫ КЛАССА

        /// <summary>
        /// Возвращает полное имя клиента (ФИО).
        /// </summary>
        public string GetFullName()
        {
            return $"{LastName} {FirstName} {MiddleName}".Trim();
        }

        /// <summary>
        /// Пополнение баланса на указанную сумму.
        /// Проверяет, что сумма положительная.
        /// </summary>
        /// <param name="amount">Сумма пополнения</param>
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
            }
        }

        /// <summary>
        /// Снятие денег со счёта.
        /// Проверяет, что сумма положительная и не превышает баланс.
        /// </summary>
        /// <param name="amount">Сумма для снятия</param>
        /// <returns>true, если операция успешна; false, если недостаточно средств</returns>
        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Возвращает полную информацию о клиенте в виде строки.
        /// Использует статическое поле BankName и readonly поле RegistrationDate.
        /// </summary>
        public string GetClientInfo()
        {
            string info = $"Информация о клиенте:\n";
            info += $"Банк: {BankName}\n";
            info += $"ID клиента: {Id}\n";
            info += $"ФИО: {GetFullName()}\n";
            info += $"Паспорт: {PassportNumber}\n";
            info += $"Телефон: {PhoneNumber}\n";
            info += $"Баланс: {Balance:F2} руб.\n";
            info += $"VIP-клиент: {(IsVip ? "Да" : "Нет")}\n";
            info += $"Дата рождения: {BirthDate:dd.MM.yyyy}\n";
            info += $"Дата регистрации: {RegistrationDate:dd.MM.yyyy}\n";
            info += $"Комиссия за перевод: {TransferFeePercent}%";

            return info;
        }

        // ПЕРЕГРУЖЕННЫЕ МЕТОДЫ (демонстрация полиморфизма)

        /// <summary>
        /// Перевод средств другому клиенту.
        /// Вариант 1: перевод без комментария.
        /// </summary>
        public bool Transfer(Client recipient, decimal amount)
        {
            if (amount <= 0 || amount > Balance)
            {
                return false;
            }

            Balance -= amount;
            recipient.Balance += amount;
            return true;
        }

        /// <summary>
        /// Перевод средств другому клиенту.
        /// Вариант 2: перевод с комментарием (перегруженный метод).
        /// </summary>
        public bool Transfer(Client recipient, decimal amount, string comment)
        {
            if (amount <= 0 || amount > Balance)
            {
                return false;
            }

            Balance -= amount;
            recipient.Balance += amount;
            // Комментарий можно сохранить в лог или отобразить
            return true;
        }

        /// <summary>
        /// Перевод средств другому клиенту.
        /// Вариант 3: перевод с учетом комиссии (перегруженный метод).
        /// </summary>
        public bool Transfer(Client recipient, decimal amount, bool withFee)
        {
            if (amount <= 0)
            {
                return false;
            }

            decimal totalAmount = amount;
            if (withFee)
            {
                totalAmount = amount + (amount * TransferFeePercent / 100);
            }

            if (totalAmount > Balance)
            {
                return false;
            }

            Balance -= totalAmount;
            recipient.Balance += amount;
            return true;
        }

        // СТАТИЧЕСКИЙ МЕТОД

        /// <summary>
        /// Статический метод для расчета возраста клиента по дате рождения.
        /// Может быть вызван без создания объекта: Client.CalculateAge(birthDate)
        /// </summary>
        public static int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;

            // Если день рождения ещё не наступил в этом году, вычитаем 1
            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        /// <summary>
        /// Статический метод для сравнения балансов двух клиентов.
        /// Возвращает клиента с большим балансом.
        /// </summary>
        public static Client GetRicherClient(Client client1, Client client2)
        {
            if (client1 == null || client2 == null)
            {
                return null;
            }

            return client1.Balance >= client2.Balance ? client1 : client2;
        }

        /// <summary>
        /// Переопределение метода ToString() из базового класса Object.
        /// Этот метод вызывается автоматически при выводе объекта в строку.
        /// </summary>
        public override string ToString()
        {
            return $"{GetFullName()}, Баланс: {Balance:F2} руб.";
        }
    }
}
