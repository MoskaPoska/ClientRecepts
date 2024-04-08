using Klopotenko;
using MiNET.Crafting;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ServerRecepts
{
    public partial class Form1 : Form
    {
        IPEndPoint endPoint;
        int port = 11000;
        Thread thread;
        IPAddress address = Dns.GetHostAddresses(Dns.GetHostName())[2];
        List<Recipee> recipesList;
        public Form1()
        {
            InitializeComponent();
            //Process.Start("ClientRecepts.exe");
            endPoint = new IPEndPoint(address, port);
            recipesList = new List<Recipee>()
            {
                new Recipee("Бутерброд", new List<Food>()
                {
                    new Food("Колбаса"),
                    new Food("Сыр"),
                    new Food("Масло")
                }),
                new Recipee("Салат", new List<Food>()
                {
                    new Food("Помидор"),
                    new Food("Масло"),
                    new Food("Огурец"),
                    new Food("Яйцо")
                }),
                new Recipee("Борщ", new List<Food>()
                {
                    new Food("Картошка"),
                    new Food("Капуста"),
                    new Food("Буряк"),
                    new Food("Мясо"),
                    new Food("Морковь")
                })
            };
            if (thread != null)
                return;
            thread = new Thread(ServerStart);
            thread.IsBackground = true;
            thread.Start();        
        }
        public void ServerStart()
        {
            textBox1.BeginInvoke(new Action<string>((str) => { textBox1.Text += str; }));
            IPEndPoint remoteIp = null;
            UdpClient listener = new UdpClient(endPoint);
            try
            {
                byte[] buff = listener.Receive(ref remoteIp);
                List<Food> foods = JsonSerializer.Deserialize<List<Food>>(Encoding.UTF8.GetString(buff));
                if( foods != null ) 
                {
                    string respons = GetRecept(foods);
                    string message = $"{buff.Length} {remoteIp}{DateTime.Now}\n";
                    textBox1.BeginInvoke(new Action<string>((str) => { textBox1.Text += str; }), message);
                    UdpClient client = new UdpClient(11000);
                    try
                    {
                        buff = Encoding.UTF8.GetBytes(respons);
                        client.Send(buff, buff.Length, remoteIp);
                    }
                    catch (SystemException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        client.Close();
                    }                   
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listener.Close();
            }
        }

        private string GetRecept(List<Food> ingredients)
        {
            string[] resepts = new string[recipesList.Count];
            int counter = 0;
            foreach(Recipee item in recipesList) 
            {
                item.Foods.OrderBy(t => t.NameFood);
                foreach(Food i in item.Foods)
                {
                    resepts[counter] += i.NameFood;
                }
                counter++;
            }
            string ingredients_f = "";
            ingredients.OrderBy(t => t.NameFood);
            foreach (Food i in ingredients)
                ingredients_f += i.NameFood;
            List<Recipee> recipes = new List<Recipee>();
            for (int i = 0; i < resepts.Length; i++)             
                if (resepts[i] == ingredients_f)                
                    recipes.Add(recipesList[i]);
            string result = "";
            foreach(var item in  recipes)
            {
                result += item.NameReciptes + " ";
            }
            return result;
        }
    }
}