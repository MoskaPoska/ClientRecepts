using Klopotenko;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ClientRecepts
{
    public partial class Form1 : Form
    {
        IPAddress address = Dns.GetHostAddresses(Dns.GetHostName())[2];
        int port = 11000;
        IPEndPoint endPoint;
        UdpClient udpClient;
        public Form1()
        {
            InitializeComponent();
            endPoint = new IPEndPoint(address, port);
        }
        private void SendM()
        {
            udpClient = new UdpClient();
            List<Food> foods = new List<Food>();
            foreach (var item in groupBox1.Controls)
            {
                if (item is CheckBox)
                {
                    if ((item as CheckBox).Checked)
                    {
                        foods.Add(new Food((item as CheckBox).Text));
                    }
                }
                string foodString = JsonSerializer.Serialize<List<Food>>(foods);
                byte[] foodByts = Encoding.Default.GetBytes(foodString);
                udpClient.Send(foodByts, foodByts.Length, endPoint);
            }
        }
        private void GetM()
        {
            try
            {
                IPEndPoint nullPoint = null;
                {
                    byte[] recipesByte = udpClient.Receive(ref nullPoint);
                    string recipesString = Encoding.UTF8.GetString(recipesByte);
                    if (recipesString != null)
                    {
                        textBox1.BeginInvoke(new Action<string>(AddText), $"{recipesString}");
                    }
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddText(string str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(str);
            textBox1.Text = sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendM();
            Thread thread = new Thread(new ThreadStart(GetM));
            thread.IsBackground = true;
            thread.Start();

        }
    }
}