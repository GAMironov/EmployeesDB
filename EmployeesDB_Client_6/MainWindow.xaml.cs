using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;

namespace EmployeesDB_Client_6
{
    public partial class MainWindow : Window
    {
       

        public MainWindow()
        { 
            InitializeComponent();

            Hide();

            AuthorizationWindow AuthorizationWindow = new EmployeesDB_Client_6.AuthorizationWindow();

            //AuthorizationWindow.Show();
            

            if (AuthorizationWindow.ShowDialog() == true)
            {
                Show();
            }
            
        }

        //public void Autho()
        //{
        //    AuthorizationWindow AuthorizationWindow = new EmployeesDB_Client_6.AuthorizationWindow();
        //    AuthorizationWindow.Show();
        //}

        //public string Connection(string[] args)
        //{

        //    string appDataPath = @"~/App_Data/";
        //    string fileName = "Server_Connection_path.txt";
        //    string PathToFile = String.Concat(appDataPath, fileName);

        //    FileStream fs1 = new FileStream(PathToFile, FileMode.Open);
        //    StreamReader reader = new StreamReader(fs1);
        //    string connectionString = reader.ReadToEnd();
        //    reader.Close();

        //    return connectionString;
        //}
        public async Task<string> RequestAndResponsToDataBaseCommandToDB(string sqlcommand)
        {
            //string appDataPath = @"~App_Data/";
            //string fileName = "Server_Connection_path.txt";
            //string PathToFile = String.Concat(appDataPath, fileName);

            FileStream fs1 = new FileStream("Server_Connection_path.txt", FileMode.Open);
            StreamReader reader = new StreamReader(fs1);
            string connectionString = reader.ReadToEnd();
            reader.Close();

            string JSONData = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(sqlcommand));

            WebRequest request = WebRequest.Create(connectionString + "/DataBase/CommandToDB");

            request.Method = "POST";

            string query = $"s={JSONData}";

            byte[] byteMsg = Encoding.UTF8.GetBytes(query);

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = byteMsg.Length;

            using (Stream stream = await request.GetRequestStreamAsync())
            {
                await stream.WriteAsync(byteMsg, 0, byteMsg.Length);
            }

            WebResponse response = await request.GetResponseAsync();

            string answer = null;

            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader sR = new StreamReader(s))
                {
                    answer = await sR.ReadToEndAsync();
                }
            }

            response.Close();

            string answerDeseriliaseStingReturn = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<string>(answer));

            return answerDeseriliaseStingReturn;
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            //List<Employee> responseEmployees = new List<Employee>();

            string sqlcommand = "select emp.Id, emp.Family, emp.Name, emp.MiddleName, emp.Birthday, cnts.Phone, cnts.Email " +
                                "from Employees as emp left join Contacts as cnts on cnts.EmployeeID = emp.ID";

            string answerDeseriliaseSting = await RequestAndResponsToDataBaseCommandToDB(sqlcommand);

            int k = 0;

            IEnumerable<char> evens = from c in answerDeseriliaseSting
                                      where c == '\n'
                                      select 'm';

            foreach (char c in evens)
            {
                k++;
            }

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            listBox6.Items.Clear();

            string name = null;
            string family = null;
            string middlename = null;
            string birthday = null;
            string phone = null;
            string email = null;

            for (int i = 1; i <= k; i++)
            {
                char c = '\n';
                int indexOfChar = answerDeseriliaseSting.IndexOf(c);
                string record = answerDeseriliaseSting.Substring(0, indexOfChar);
                answerDeseriliaseSting = answerDeseriliaseSting.Substring(indexOfChar + 1);

                c = ' ';
                indexOfChar = record.IndexOf(c);
                record = record.Substring(indexOfChar + 1);

                indexOfChar = record.IndexOf(c);
                family = record.Substring(0, indexOfChar);
                record = record.Substring(indexOfChar + 1);
                listBox1.Items.Add(family);

                indexOfChar = record.IndexOf(c);
                name = record.Substring(0, indexOfChar);
                record = record.Substring(indexOfChar + 1);
                listBox2.Items.Add(name);

                indexOfChar = record.IndexOf(c);
                middlename = record.Substring(0, indexOfChar);
                record = record.Substring(indexOfChar + 1);
                listBox3.Items.Add(middlename);

                indexOfChar = record.IndexOf(c);
                birthday = record.Substring(0, indexOfChar);
                record = record.Substring(indexOfChar + 9);
                listBox4.Items.Add(birthday);

                indexOfChar = record.IndexOf(c);
                phone = record.Substring(0, indexOfChar);
                record = record.Substring(indexOfChar + 1);
                listBox5.Items.Add(phone);

                indexOfChar = record.IndexOf(c);
                email = record.Substring(0, indexOfChar);
                record = record.Substring(indexOfChar + 1);
                listBox6.Items.Add(email);
            }

        }

        private async void Button3_Click(object sender, EventArgs e)
        {
            SearchWindow searchWindow = new EmployeesDB_Client_6.SearchWindow();
            searchWindow.Show();
        }

        private async void Button4_Click(object sender, EventArgs e)
        {
            AddWindow AddWindow = new EmployeesDB_Client_6.AddWindow();
            AddWindow.Show();

            
        }
    }

    
}
