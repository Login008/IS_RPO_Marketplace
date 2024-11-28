using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Marketplace
{
    public partial class lk_seller : Form
    {
        public lk_seller()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            flowLayoutPanel1.Controls.Clear();
            Product.Reading();
            Point currPos = new Point(12, 12);

            foreach (Product product in Product.list)
            {
                if (product.owner == Account.online.login)
                {
                    Panel panel = new Panel();
                    panel.Size = new Size(200, 200);
                    panel.BorderStyle = BorderStyle.FixedSingle;
                    PictureBox pic = new PictureBox();
                    pic.Size = new Size(115, 115);
                    pic.Location = new Point(36, 0);
                    pic.BorderStyle = BorderStyle.FixedSingle;
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                    pic.Image = Image.FromFile(product.visual);
                    panel.Controls.Add(pic);
                    Label label = new Label();
                    label.Font = new Font(label.Font.FontFamily, 7, label.Font.Style);
                    label.Size = new Size(200, 80);
                    label.Location = new Point(2, 120);
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.Text = $"Название: {product.name}\nЦена: {product.price}\nКоличество: {product.count}\nСклад: {product.placeOfContaining}";
                    panel.Controls.Add(label);
                    flowLayoutPanel1.Controls.Add(panel);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                if (numericUpDown1.Value != 0 && numericUpDown2.Value != 0)
                {
                    if (pictureBox1.Image != null)
                    {
                        foreach (Product prod in Product.list)
                        {
                            if (prod.owner == Account.online.login)
                            {
                                if (prod.name == textBox1.Text)
                                {
                                    MessageBox.Show("Товар с таким названием вы уже добавляли");
                                    return;
                                }
                            }
                        }
                        Product product = new Product(textBox1.Text, double.Parse(numericUpDown1.Value.ToString()),
                        int.Parse(numericUpDown2.Value.ToString()), textBox2.Text, label1.Text,
                        Account.online.login);
                        Product.list.Add(product);
                        Product.Writing();
                        LoadData();

                        textBox1.Clear();
                        numericUpDown1.Value = 0;
                        numericUpDown2.Value = 0;
                        textBox2.Clear();
                        label1.Text = "";
                        pictureBox1.Image = null;
                    }
                    else
                        MessageBox.Show("Выберите картинку для товара, нажав на панель над кнопкой Добавить");
                }
                else
                    MessageBox.Show("Выставьте цену и количество");
            }
            else
                MessageBox.Show("Заполните пустые поля");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                string filepath = "";

                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Images (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG | All files (*.*)|*.*";
                    ofd.FilterIndex = 1;
                    ofd.RestoreDirectory = true;

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        filepath = ofd.FileName;
                        string file_name = Path.GetFileName(filepath);
                        pictureBox1.Image = Image.FromFile(file_name);
                        label1.Text = file_name;
                        if (!File.Exists(file_name))
                            File.Copy(filepath, file_name, true);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Эта картинка уже используется для другого товара");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                foreach (var item in Product.list)
                {
                    if (Account.online.login == item.owner)
                    {
                        if (item.name == textBox3.Text)
                        {
                            Product.list.Remove(item);
                            Product.Writing();
                            LoadData();
                            MessageBox.Show("Товар удалён");
                            return;
                        }
                    }
                }
                MessageBox.Show("Товар не найден");
            }
            else
                MessageBox.Show("Заполните поле над кнопкой");
        }
    }
}
