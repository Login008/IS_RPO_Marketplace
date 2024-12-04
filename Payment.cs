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
    public partial class Payment : Form
    {
        int deliveryType;
        int paymentType;
        List<Product> products = new List<Product>();
        public Payment(int deliveryType, int paymentType, List<Product> products)
        {
            InitializeComponent();
            this.deliveryType = deliveryType;
            this.paymentType = paymentType;
            this.products = products;
        }
    }
}
