using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marketplace
{
    public partial class lk_Buyer : Form
    {
        public lk_Buyer()
        {
            InitializeComponent();
            LoadData();
            LoadBusket();
        }
        public void LoadBusket()
        {
            flowLayoutPanel2.Controls.Clear();
            Product.ReadingBasket();
            Point currPos = new Point(12, 12);
            double finalPrice = 0;
            int count = 0;

            foreach (Product product in Product.basketList)
            {
                if (product.basketOwner == Account.online.login)
                {
                    Panel panel = new Panel();
                    panel.Size = new Size(200, 270);
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
                    label.Size = new Size(200, 70);
                    label.Location = new Point(2, 120);
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.Text = $"Название: {product.name}\nЦена: {product.price}\nПродавец: {product.owner}";
                    panel.Controls.Add(label);

                    Panel panelQuant = new Panel();
                    panelQuant.Size = new Size(125, 30);
                    panelQuant.BorderStyle = BorderStyle.FixedSingle;
                    panelQuant.Location = new Point(36, 190);

                    Button buttonMin = new Button();
                    buttonMin.Font = new Font(label.Font.FontFamily, 7, label.Font.Style);
                    buttonMin.Size = new Size(40, 30);
                    buttonMin.Location = new Point(0, 0);
                    buttonMin.Text = "-";
                    buttonMin.Tag = product;
                    buttonMin.Click += ButDec;
                    panelQuant.Controls.Add(buttonMin);

                    Label labelQuant = new Label();
                    labelQuant.Font = new Font(labelQuant.Font.FontFamily, 7, labelQuant.Font.Style);
                    labelQuant.Size = new Size(45, 30);
                    labelQuant.Location = new Point(40, 0);
                    labelQuant.TextAlign = ContentAlignment.MiddleCenter;
                    labelQuant.Text = $"{product.basketCount}";
                    panelQuant.Controls.Add(labelQuant);

                    Button buttonPlus = new Button();
                    buttonPlus.Font = new Font(label.Font.FontFamily, 7, label.Font.Style);
                    buttonPlus.Size = new Size(40, 30);
                    buttonPlus.Location = new Point(85, 0);
                    buttonPlus.Text = "+";
                    buttonPlus.Tag = product;
                    buttonPlus.Click += ButAdd;
                    panelQuant.Controls.Add(buttonPlus);

                    panel.Controls.Add(panelQuant);

                    Button buttonDel = new Button();
                    buttonDel.Font = new Font(label.Font.FontFamily, 7, label.Font.Style);
                    buttonDel.Size = new Size(120, 40);
                    buttonDel.Location = new Point(36, 230);
                    buttonDel.Text = "Удалить из корзины";
                    buttonDel.Tag = product;
                    buttonDel.Click += ButDel;
                    panel.Controls.Add(buttonDel);

                    flowLayoutPanel2.Controls.Add(panel);
                    finalPrice += product.price * product.basketCount;
                    count += product.basketCount;
                }
            }
            label1.Text = "Итог: " + finalPrice.ToString() + " Р";
            label2.Text = "Товары: " + count.ToString() + " шт.";
        }
        private void ButAdd(object sender, EventArgs e)
        {
            var ret = ((Control)sender).Tag as Product;
            if (ret.basketCount >= ret.count)
            {
                MessageBox.Show("У продавца нет в наличии больше единиц товара");
                return;
            }
            for (int i = 0; i < Product.basketList.Count; i++)
            {
                if (Product.basketList[i] == ret)
                    Product.basketList[i].basketCount += 1;
            }
            Product.WritingBasket();
            LoadBusket();
        }
        private void ButDec(object sender, EventArgs e)
        {
            var ret = ((Control)sender).Tag as Product;
            if (ret.basketCount <= 1)
            {
                for (int i = 0; i < Product.basketList.Count; i++)
                {
                    if (Product.basketList[i] == ret)
                    {
                        Product.basketList.Remove(Product.basketList[i]);
                        Product.WritingBasket();
                        LoadBusket();
                    }
                }
            }
            else
            {
                for (int i = 0; i < Product.basketList.Count; i++)
                {
                    if (Product.basketList[i] == ret)
                        Product.basketList[i].basketCount -= 1;
                }
                Product.WritingBasket();
                LoadBusket();
            }
        }
        private void ButDel(object sender, EventArgs e)
        {
            var ret = ((Control)sender).Tag as Product;
            for (int i = 0; i < Product.basketList.Count; i++)
            {
                if (Product.basketList[i] == ret)
                {
                    Product.basketList.Remove(Product.basketList[i]);
                    Product.WritingBasket();
                    LoadBusket();
                }
            }
        }
        public void LoadData()
        {
            flowLayoutPanel1.Controls.Clear();
            Product.Reading();
            Point currPos = new Point(12, 12);

            foreach (Product product in Product.list)
            {
                Panel panel = new Panel();
                panel.Size = new Size(200, 220);
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
                label.Size = new Size(200, 70);
                label.Location = new Point(2, 120);
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Text = $"Название: {product.name}\nЦена: {product.price}\nКоличество: {product.count}\nПродавец: {product.owner}";
                panel.Controls.Add(label);
                Button button = new Button();
                button.Font = new Font(label.Font.FontFamily, 7, label.Font.Style);
                button.Size = new Size(125, 30);
                button.Location = new Point(36, 190);
                button.Text = "Добавить";
                button.Tag = product;
                button.Click += Button_Click;
                panel.Controls.Add(button);
                flowLayoutPanel1.Controls.Add(panel);
            }
        }
        public void Button_Click(object sender, EventArgs e)
        {
            var item = ((Control)sender).Tag as Product;
            for (int i = 0; i < Product.basketList.Count; i++)
            {
                if (Product.basketList[i].name == item.name && Product.basketList[i].price == item.price
                    && Product.basketList[i].count == item.count &&
                    Product.basketList[i].placeOfContaining == item.placeOfContaining &&
                    Product.basketList[i].visual == item.visual && Product.basketList[i].owner == item.owner &&
                    Product.basketList[i].basketOwner == Account.online.login)
                {
                    if (item.basketCount >= item.count)
                    {
                        MessageBox.Show($"У продавца нет в наличии больше {item.count} единиц товара");
                        return;
                    }
                    Product.basketList[i].basketCount += 1;
                    Product.basketList[i].basketOwner = Account.online.login;
                    Product.WritingBasket();
                    LoadBusket();
                    return;
                }
            }
            item.basketCount = 1;
            item.basketOwner = Account.online.login;
            Product.basketList.Add(item);
            Product.WritingBasket();
            LoadBusket();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in Product.basketList)
            {
                if (item.basketOwner == Account.online.login)
                {
                    if (comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1)
                    {
                        Payment f1 = new Payment(comboBox1.SelectedIndex, comboBox2.SelectedIndex, Product.basketList);
                        f1.Show();
                    }
                    else
                        MessageBox.Show("Выберите вид доставки и вид оплаты");
                    return;
                }
            }
            MessageBox.Show("Для оформления заказа добавьте товары в корзину");
        }
    }
}
