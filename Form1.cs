using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab8_OOAP_
{
    public partial class Form1 : Form
    {
        // Декларація елементів управління
        private Button btnChargeLaptopNewVagon;
        private Button btnChargeLaptopOldVagon;
        private Label lblInstructions;

        public Form1()
        {
            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
            // Ініціалізація і додавання елементів управління

            // Підпис з інструкцією
            lblInstructions = new Label
            {
                Text = "Виберіть тип вагона для заряджання ноутбука:",
                Location = new Point(10, 10),
                Width = 300
            };
            Controls.Add(lblInstructions);

            // Кнопка для заряджання ноутбука в новому вагоні
            btnChargeLaptopNewVagon = new Button
            {
                Text = "Зарядити в новому вагоні",
                Location = new Point(10, 50),
                Width = 200
            };
            // Обробник події для кнопки нової системи
            btnChargeLaptopNewVagon.Click += new EventHandler(btnChargeLaptopNewVagon_Click);
            Controls.Add(btnChargeLaptopNewVagon);

            // Кнопка для заряджання ноутбука в старому вагоні через адаптер
            btnChargeLaptopOldVagon = new Button
            {
                Text = "Зарядити в старому вагоні",
                Location = new Point(10, 90),
                Width = 200
            };
            // Обробник події для кнопки старої системи
            btnChargeLaptopOldVagon.Click += new EventHandler(btnChargeLaptopOldVagon_Click);
            Controls.Add(btnChargeLaptopOldVagon);

            // Налаштування форми
            this.Text = "Система заряджання у вагоні";
            this.Size = new Size(400, 200);
        }

        // Обробник події для кнопки заряджання в новому вагоні
        private void btnChargeLaptopNewVagon_Click(object sender, EventArgs e)
        {
            // Створення об'єкта ноутбука та нової системи вагону
            Laptop laptop = new Laptop();
            NewVagonSystem newVagon = new NewVagonSystem();

            // Зарядка ноутбука через нову систему
            laptop.Charge(newVagon); // Викликає метод MatchSocket() для нової системи
        }

        // Обробник події для кнопки заряджання в старому вагоні через адаптер
        private void btnChargeLaptopOldVagon_Click(object sender, EventArgs e)
        {
            // Створення об'єкта ноутбука та старої системи вагону
            Laptop laptop = new Laptop();
            OldVagonSystem oldVagon = new OldVagonSystem();

            // Створення адаптера, який дозволяє старій системі працювати з новим інтерфейсом
            OldToNewAdapter adapter = new OldToNewAdapter(oldVagon);

            // Зарядка ноутбука через адаптер (це дозволяє використовувати стару систему як нову)
            laptop.Charge(adapter); // Викликає метод MatchSocket() через адаптер
        }
    }

    // Інтерфейс для нової системи вагону
    public interface INewVagonSystem
    {
        void MatchSocket(); // Метод, який заряджає ноутбук, використовуючи нову систему
    }

    // Клас нової системи вагону, який реалізує INewVagonSystem
    public class NewVagonSystem : INewVagonSystem
    {
        public void MatchSocket()
        {
            // Логіка зарядки для нової системи
            MessageBox.Show("Ноутбук успішно заряджено в новому вагоні!");
        }
    }

    // Клас старої системи вагону
    public class OldVagonSystem
    {
        public void ThinSocket()
        {
            // Логіка зарядки для старої системи
            MessageBox.Show("Ноутбук успішно заряджено в старому вагоні через адаптер!");
        }
    }

    // Адаптер для старої системи вагону, щоб відповідати новому інтерфейсу INewVagonSystem
    public class OldToNewAdapter : INewVagonSystem
    {
        private readonly OldVagonSystem _oldVagonSystem; // Зберігає стару систему вагону

        // Конструктор, який приймає стару систему та адаптує її до нового інтерфейсу
        public OldToNewAdapter(OldVagonSystem oldVagonSystem)
        {
            _oldVagonSystem = oldVagonSystem; // Ініціалізація старої системи
        }

        // Реалізація методу MatchSocket() з нового інтерфейсу
        // Цей метод викликає метод старої системи через адаптер
        public void MatchSocket()
        {
            _oldVagonSystem.ThinSocket(); // Викликається метод старої системи для заряджання
        }
    }

    // Клас ноутбука, який може заряджатися через систему вагону
    public class Laptop
    {
        // Метод для заряджання ноутбука через систему вагону, яка реалізує INewVagonSystem
        public void Charge(INewVagonSystem vagonSystem)
        {
            vagonSystem.MatchSocket(); // Викликається метод MatchSocket() системи
        }
    }
}