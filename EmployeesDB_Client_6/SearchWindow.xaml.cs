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
using System.Windows.Shapes;

namespace EmployeesDB_Client_6
{
    /// <summary>
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        public SearchWindow()
        {
            InitializeComponent();
        }



        public async Task<string> RequestAndResponsToDataBaseCommandToDB(string sqlcommand)
        {

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


        private async void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))

            {
                listBox.Items.Clear();
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                listBox5.Items.Clear();
                listBox8.Items.Clear();

                string selectValue = textBox1.Text;

                string sqlcommand = "select emp.Id, emp.Family, emp.Name, emp.MiddleName, emp.Birthday, cnts.Phone, cnts.Email " +
                    "from Employees as emp left join Contacts as cnts on cnts.EmployeeID = emp.ID where emp.Family like" + "'" + selectValue + "'";

                string answerDeseriliaseSting = await RequestAndResponsToDataBaseCommandToDB(sqlcommand);

                try
                {

                    int k = 0;

                    IEnumerable<char> evens = from c in answerDeseriliaseSting
                                              where c == '\n'
                                              select 'm';

                    foreach (char c in evens)
                    {
                        k++;
                    }

                    string id = null;
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
                        id = record.Substring(0, indexOfChar);
                        record = record.Substring(indexOfChar + 1);
                        listBox8.Items.Add(id);
                        textBox8.Text = id;

                        indexOfChar = record.IndexOf(c);
                        family = record.Substring(0, indexOfChar);
                        record = record.Substring(indexOfChar + 1);
                        listBox.Items.Add(family);
                        textBox2.Text = family;

                        indexOfChar = record.IndexOf(c);
                        name = record.Substring(0, indexOfChar);
                        record = record.Substring(indexOfChar + 1);
                        listBox1.Items.Add(name);
                        textBox3.Text = name;

                        indexOfChar = record.IndexOf(c);
                        middlename = record.Substring(0, indexOfChar);
                        record = record.Substring(indexOfChar + 1);
                        listBox2.Items.Add(middlename);
                        textBox4.Text = middlename;

                        indexOfChar = record.IndexOf(c);
                        birthday = record.Substring(0, indexOfChar);
                        record = record.Substring(indexOfChar + 9);
                        listBox3.Items.Add(birthday);
                        textBox5.Text = birthday;

                        indexOfChar = record.IndexOf(c);
                        phone = record.Substring(0, indexOfChar);
                        record = record.Substring(indexOfChar + 1);
                        listBox4.Items.Add(phone);
                        textBox6.Text = phone;

                        indexOfChar = record.IndexOf(c);
                        email = record.Substring(0, indexOfChar);
                        record = record.Substring(indexOfChar + 1);
                        listBox5.Items.Add(email);
                        textBox7.Text = email;

                        button2.Visibility = Visibility.Visible;
                    }
                }
                catch
                {
                    MessageBox.Show("Нет сотрудника с такой фамилией!");
                }
            }

            else MessageBox.Show("Введите фамилию!");
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            textBox2.Visibility = Visibility.Visible;
            textBox3.Visibility = Visibility.Visible;
            textBox4.Visibility = Visibility.Visible;
            textBox5.Visibility = Visibility.Visible;
            textBox6.Visibility = Visibility.Visible;
            textBox7.Visibility = Visibility.Visible;
            textBox8.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Visible;
        }

        private async void Button3_Click(object sender, EventArgs e)
        {
            string sqlcommand = "update Employees SET family =" + "'" + textBox2.Text + "'" + ", name =" + "'" + textBox3.Text + "'" + ", middleName =" + "'" + textBox4.Text + "'" + ", birthday =" + "'" + textBox5.Text + "'" + " where id like" + "'" + textBox8.Text + "'" + " " + "Update Contacts SET phone =" + "'" + textBox6.Text + "'" + ", email =" + "'" + textBox7.Text + "'" + " where EmployeeID = (select  emp.Id from Employees emp where emp.id like" + "'" + textBox8.Text + "'" + ")";

            string answerDeseriliaseSting = await RequestAndResponsToDataBaseCommandToDB(sqlcommand);
        }

        private async void Button4_Click(object sender, EventArgs e)
        {
            ConfirmationWindow ConfirmationWindow = new EmployeesDB_Client_6.ConfirmationWindow();

            if (ConfirmationWindow.ShowDialog() == true)
            {
                string sqlcommand = " delete from Contacts where EmployeeID=" + textBox8.Text + " ; delete from Employees where id =" + textBox8.Text + ";";

                int k = 0;

                try
                {
                    string answerDeseriliaseSting = await RequestAndResponsToDataBaseCommandToDB(sqlcommand);
                }

                catch
                {
                    MessageBox.Show("Ошибка! Попробуйте позже.");
                    k++;
                }

                finally
                {
                    if (k==0)
                    {
                        MessageBox.Show("Успешно!");
                    }
                }
                
            }
             
        }

    }
}
