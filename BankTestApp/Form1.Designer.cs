using System.Drawing;
using System.Windows.Forms;

namespace BankTestApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        // Элементы управления
        private ListBox listBoxClients;
        private Button btnAddClient;
        private Button btnStaticMethod;

        private TextBox txtLastName;
        private TextBox txtFirstName;
        private TextBox txtBalance;
        private DateTimePicker dtpBirthDate;
        private CheckBox chkIsVip;

        private PictureBox pictureBoxFoto;
        private Button btnLoadFoto;
        private OpenFileDialog openFileDialogFoto;

        private TextBox txtOutput;

        private void InitializeComponent()
        {
            this.listBoxClients = new ListBox();
            this.btnAddClient = new Button();
            this.btnStaticMethod = new Button();
            this.txtLastName = new TextBox();
            this.txtFirstName = new TextBox();
            this.txtBalance = new TextBox();
            this.dtpBirthDate = new DateTimePicker();
            this.chkIsVip = new CheckBox();
            this.pictureBoxFoto = new PictureBox();
            this.btnLoadFoto = new Button();
            this.openFileDialogFoto = new OpenFileDialog();
            this.txtOutput = new TextBox();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFoto)).BeginInit();
            this.SuspendLayout();

            // --- Левая панель: Коллекция ---
            this.listBoxClients.FormattingEnabled = true;
            this.listBoxClients.Location = new Point(20, 50);
            this.listBoxClients.Size = new Size(220, 350);
            this.listBoxClients.SelectedIndexChanged += new System.EventHandler(this.listBoxClients_SelectedIndexChanged);

            this.btnAddClient.Text = "Добавить клиента";
            this.btnAddClient.Location = new Point(20, 410);
            this.btnAddClient.Size = new Size(220, 30);
            this.btnAddClient.Click += new System.EventHandler(this.btnAddClient_Click);

            this.btnStaticMethod.Text = "Вызвать статический метод";
            this.btnStaticMethod.Location = new Point(20, 450);
            this.btnStaticMethod.Size = new Size(220, 30);
            this.btnStaticMethod.Click += new System.EventHandler(this.btnStaticMethod_Click);

            // --- Правая панель: Привязка свойств ---
            Label lbl = new Label() { Text = "Фамилия (с проверкой загл. буквы):", Location = new Point(260, 30), AutoSize = true };
            this.txtLastName.Location = new Point(260, 50); this.txtLastName.Size = new Size(200, 20);

            Label lbl2 = new Label() { Text = "Имя:", Location = new Point(260, 80), AutoSize = true };
            this.txtFirstName.Location = new Point(260, 100); this.txtFirstName.Size = new Size(200, 20);

            Label lbl3 = new Label() { Text = "Баланс:", Location = new Point(260, 130), AutoSize = true };
            this.txtBalance.Location = new Point(260, 150); this.txtBalance.Size = new Size(200, 20);

            Label lbl4 = new Label() { Text = "Дата рождения (DateTimePicker):", Location = new Point(260, 180), AutoSize = true };
            this.dtpBirthDate.Location = new Point(260, 200); this.dtpBirthDate.Size = new Size(200, 20);
            this.dtpBirthDate.Format = DateTimePickerFormat.Short;

            this.chkIsVip.Text = "VIP-клиент (CheckBox)";
            this.chkIsVip.Location = new Point(260, 230); this.chkIsVip.AutoSize = true;

            // --- Нижняя панель: Графика и перегрузка методов ---
            this.pictureBoxFoto.Location = new Point(480, 50);
            this.pictureBoxFoto.Size = new Size(280, 250);
            this.pictureBoxFoto.BorderStyle = BorderStyle.FixedSingle;

            this.btnLoadFoto.Text = "Загрузить фото (OpenFileDialog)";
            this.btnLoadFoto.Location = new Point(480, 310);
            this.btnLoadFoto.Size = new Size(280, 30);
            this.btnLoadFoto.Click += new System.EventHandler(this.btnLoadFoto_Click);

            this.openFileDialogFoto.Filter = "Изображения|*.jpg;*.png;*.bmp";

            // --- Вывод информации ---
            this.txtOutput.Location = new Point(20, 500);
            this.txtOutput.Size = new Size(740, 120);
            this.txtOutput.Multiline = true;
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = ScrollBars.Vertical;

            // --- Настройки формы ---
            this.ClientSize = new Size(780, 640);
            this.Controls.Add(this.listBoxClients);
            this.Controls.Add(this.btnAddClient);
            this.Controls.Add(this.btnStaticMethod);
            this.Controls.Add(lbl); this.Controls.Add(this.txtLastName);
            this.Controls.Add(lbl2); this.Controls.Add(this.txtFirstName);
            this.Controls.Add(lbl3); this.Controls.Add(this.txtBalance);
            this.Controls.Add(lbl4); this.Controls.Add(this.dtpBirthDate);
            this.Controls.Add(this.chkIsVip);
            this.Controls.Add(this.pictureBoxFoto);
            this.Controls.Add(this.btnLoadFoto);
            this.Controls.Add(this.txtOutput);

            this.Font = new Font("Segoe UI", 9F);
            this.Text = "Лаб. работа 2: Свойства, Коллекции, Методы";
            this.StartPosition = FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}