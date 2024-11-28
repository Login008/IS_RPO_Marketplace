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
                button.Click += Button_Click;
                panel.Controls.Add(button);
                flowLayoutPanel1.Controls.Add(panel);
            }
        }
        public void Button_Click(object sender, EventArgs e)
        {

        }
    }
}
