using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BankLibrary;

namespace BankTestApp
{

    public partial class Form1 : Form
    {
        // КОЛЛЕКЦИЯ для хранения клиентов
        private List<Client> clients = new List<Client>();

        // Текущий выбранный клиент
        private Client currentClient;

        public Form1()
        {
            InitializeComponent();
            InitializeBindings();
        }

        /// <summary>
        /// Настройка привязки данных к элементам формы
        /// </summary>
        private void InitializeBindings()
        {
            // Создаем первого клиента по умолчанию
            currentClient = new Client("Иванов", "Иван", 10000m);
            currentClient.BirthDate = new DateTime(1990, 5, 15);
            currentClient.IsVip = false;

            // Привязка свойств объекта к элементам управления
            txtLastName.DataBindings.Add("Text", currentClient, "LastName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtFirstName.DataBindings.Add("Text", currentClient, "FirstName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtBalance.DataBindings.Add("Text", currentClient, "Balance", true, DataSourceUpdateMode.OnPropertyChanged, "0", "F2");
            dtpBirthDate.DataBindings.Add("Value", currentClient, "BirthDate", false, DataSourceUpdateMode.OnPropertyChanged);
            chkIsVip.DataBindings.Add("Checked", currentClient, "IsVip", false, DataSourceUpdateMode.OnPropertyChanged);
        }


        /// <summary>
        /// Добавление нового клиента в коллекцию
        /// </summary>
        private void btnAddClient_Click(object sender, EventArgs e)
        {
            try
            {
                // Создаем нового клиента с данными из формы
                Client newClient = new Client
                {
                    Id = clients.Count + 1,
                    LastName = txtLastName.Text,
                    FirstName = txtFirstName.Text,
                    Balance = decimal.Parse(txtBalance.Text),
                    BirthDate = dtpBirthDate.Value,
                    IsVip = chkIsVip.Checked
                };

                // Добавляем в коллекцию
                clients.Add(newClient);

                // Обновляем ListBox
                UpdateClientList();

                Output($"✅ Клиент добавлен в коллекцию!\n\n" +
                       $"Всего клиентов: {clients.Count}\n" +
                       $"Добавлен: {newClient.ToString()}");
            }
            catch (ArgumentException ex)
            {
                Output($"❌ ОШИБКА: {ex.Message}");
            }
            catch (FormatException)
            {
                Output("❌ ОШИБКА: Проверьте правильность ввода данных!");
            }
        }

        /// <summary>
        /// Обновление списка клиентов в ListBox
        /// </summary>
        private void UpdateClientList()
        {
            listBoxClients.Items.Clear();
            foreach (Client c in clients)
            {
                listBoxClients.Items.Add(c.ToString());
            }
        }

        /// <summary>
        /// Вызов статического метода класса Client
        /// </summary>
        private void btnStaticMethod_Click(object sender, EventArgs e)
        {
            if (currentClient == null)
            {
                Output("❌ ОШИБКА: Клиент не создан!");
                return;
            }

            // Демонстрация статического метода
            int age = Client.CalculateAge(currentClient.BirthDate);

            string info = "🔍 ДЕМОНСТРАЦИЯ СТАТИЧЕСКОГО МЕТОДА\n\n";
            info += $"Вызван метод: Client.CalculateAge(birthDate)\n\n";
            info += $"Клиент: {currentClient.GetFullName()}\n";
            info += $"Дата рождения: {currentClient.BirthDate:dd.MM.yyyy}\n";
            info += $"Возраст: {age} лет\n\n";
            info += "Статический метод вызывается через имя класса,\n";
            info += "без создания объекта!";

            // Если есть несколько клиентов, сравним их балансы
            if (clients.Count >= 2)
            {
                Client richer = Client.GetRicherClient(clients[0], clients[1]);
                info += $"\n\n─────────────────────────\n";
                info += $"Сравнение балансов:\n";
                info += $"Клиент 1: {clients[0].GetFullName()} - {clients[0].Balance:F2} руб.\n";
                info += $"Клиент 2: {clients[1].GetFullName()} - {clients[1].Balance:F2} руб.\n";
                info += $"Богаче: {richer?.GetFullName()}";
            }

            Output(info);
        }

        /// <summary>
        /// Загрузка фотографии через OpenFileDialog
        /// </summary>
        private void btnLoadFoto_Click(object sender, EventArgs e)
        {
            if (openFileDialogFoto.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Загружаем изображение
                    Image img = Image.FromFile(openFileDialogFoto.FileName);
                    pictureBoxFoto.Image = img;
                    pictureBoxFoto.SizeMode = PictureBoxSizeMode.Zoom;

                    // Сохраняем путь к фото в текущем клиенте
                    if (currentClient != null)
                    {
                        currentClient.PhotoPath = openFileDialogFoto.FileName;
                    }

                    Output($"✅ Фото загружено успешно!\n\n" +
                           $"Путь: {openFileDialogFoto.FileName}\n\n" +
                           "Демонстрация работы с графикой:\n" +
                           "- OpenFileDialog для выбора файла\n" +
                           "- PictureBox для отображения изображения\n" +
                           "- Image.FromFile() для загрузки");
                }
                catch (Exception ex)
                {
                    Output($"❌ ОШИБКА при загрузке изображения:\n{ex.Message}");
                }
            }
        }

        /// <summary>
        /// Обработчик выбора клиента из списка
        /// </summary>
        private void listBoxClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxClients.SelectedIndex >= 0)
            {
                Client selectedClient = clients[listBoxClients.SelectedIndex];

                // Отображаем информацию о выбранном клиенте
                Output($"📋 ВЫБРАН КЛИЕНТ:\n\n{selectedClient.GetClientInfo()}");

                // Если есть фото, загружаем его
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

        private void Output(string text)
        {
            txtOutput.Text = text;
        }
    }
}
