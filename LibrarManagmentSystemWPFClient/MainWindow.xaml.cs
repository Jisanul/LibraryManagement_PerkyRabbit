using LibraryManagementSystemWPFClient;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibrarManagmentSystemWPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    //var ljksfk = new HttpClientFactory();
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri("http://localhost:12789/");
        //    //client.DefaultRequestHeaders.Add("appkey", "myapp_key");
        //    client.DefaultRequestHeaders.Accept.Add(
        //       new MediaTypeWithQualityHeaderValue("application/json"));

        //    HttpResponseMessage response = client.GetAsync("api/employee").Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        //var employees = response.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
        //        //grdEmployee.ItemsSource = employees;

        //    }
        //    else
        //    {
        //        MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
        //    }
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Authors authors = new Authors();
            authors.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BorrowedBooks authors = new BorrowedBooks();
            authors.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Book authors = new Book();
            authors.Show();
            this.Close();
        }
    }
}