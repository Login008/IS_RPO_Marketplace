using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace
{
    public class Product
    {
        static public List<Product> basketList = new List<Product>();
        static public List<Product> list = new List<Product>();

        public string name;
        public double price;
        public int count;
        public string placeOfContaining;
        public string visual;
        public string owner;
        public int basketCount = 1;
        public string basketOwner;
        public Product(string name, double price, int count, string placeOfContaining, string visual, string owner)
        {
            this.name = name;
            this.price = price;
            this.count = count;
            this.placeOfContaining = placeOfContaining;
            this.visual = visual;
            this.owner = owner;
        }
        static public void Reading()
        {
            if (File.Exists("products.txt"))
            {
                list.Clear();
                StreamReader sr = File.OpenText("products.txt");
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split('~');
                    Product product = new Product(line[0], Convert.ToDouble(line[1]), Convert.ToInt32(line[2]), line[3], line[4], line[5]);
                    list.Add(product);
                }
                sr.Close();
            }
        }
        static public void Writing()
        {
            StreamWriter sw = File.CreateText("products.txt");

            foreach (Product product in list)
            {
                sw.WriteLine($"{product.name}~{product.price}~{product.count}~{product.placeOfContaining}~{product.visual}~{product.owner}");
            }

            sw.Close();
        }
        static public void WritingBasket()
        {
            StreamWriter sw = File.CreateText("basketProducts.txt");

            foreach (Product product in basketList)
            {
                sw.WriteLine($"{product.name}~{product.price}~{product.count}~{product.placeOfContaining}~{product.visual}~{product.owner}~{product.basketOwner}~{product.basketCount}");
            }

            sw.Close();
        }
        static public void ReadingBasket()
        {
            if (File.Exists("basketProducts.txt"))
            {
                basketList.Clear();
                StreamReader sr = File.OpenText("basketProducts.txt");
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split('~');
                    Product product = new Product(line[0], Convert.ToDouble(line[1]), Convert.ToInt32(line[2]), line[3], line[4], line[5]);
                    product.basketCount = int.Parse(line[7]);
                    product.basketOwner = line[6];
                    basketList.Add(product);
                }
                sr.Close();
            }
        }
    }
}
