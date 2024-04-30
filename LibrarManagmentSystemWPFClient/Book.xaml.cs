using LibraryManagementSystemWPFClient.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

namespace LibraryManagementSystemWPFClient
{
    
    public partial class Book : Window
    {
        private List<string> memberNames;
        private const string API_BASE_URL = "https://localhost:7041/";
        public Book()
        {
            InitializeComponent();
            memberNames = new List<string>();
            PopulateMemberNamesAndBooks().GetAwaiter();
        }
        private async Task PopulateMemberNamesAndBooks()
        {
            try
            {

             
                using (HttpClient client = new HttpClient())
                {
                  
                    client.BaseAddress = new Uri(API_BASE_URL);

                
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("GetAuthor");

                 
                    if (response.IsSuccessStatusCode)
                    {
                   
                        string responseData = await response.Content.ReadAsStringAsync();

                        
                        List<Author> members = JsonConvert.DeserializeObject<List<Author>>(responseData);
                        AuthorIDBox.ItemsSource = members;


                    }
                    else
                    {
                  
                        MessageBox.Show("Error Code: " + response.StatusCode + "\nMessage: " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving member names: {ex.Message}");
            }
        }

        private void ShowListBook_Click(object sender, RoutedEventArgs e)
        {
            GetListData();
        }
        private async Task GetListData()
        {
            try
            {
          
                using (HttpClient client = new HttpClient())
                {
             
                    client.BaseAddress = new Uri(API_BASE_URL);

            
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

       
                    HttpResponseMessage response = await client.GetAsync("GetBooks");

                    
                    if (response.IsSuccessStatusCode)
                    {
                        
                        string responseData = await response.Content.ReadAsStringAsync();

                        
                        List<Book> borrowedBooks = JsonConvert.DeserializeObject<List<Book>>(responseData);
                        GridBook.ItemsSource = borrowedBooks;


                    }
                    else
                    {
                        
                        MessageBox.Show("Error Code: " + response.StatusCode + "\nMessage: " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void SaveBook_Click(object sender, RoutedEventArgs e)
        {
            int memberId = 0;
            if (AuthorIDBox.SelectedItem is Author selectedMember)
            {
                memberId = selectedMember.AuthorID; 
            }

            int bookId = 0;
            
            if (!int.TryParse(AuthorIDBox.Text, out bookId))
            {
                MessageBox.Show("Invalid Book ID.");
                return;
            }

            DateTime borrowDate = PublishedDate.SelectedDate ?? DateTime.MinValue;

            string title = TitleBox.Text;
            string isbn = ISBNBox.Text;  
            string totalCopiesBox = TotalCopiesBox.Text; 
            string availableCopiesBox = AvailableCopiesBox.Text; 

            
            if (memberId > 0 && bookId > 0 && borrowDate != DateTime.MinValue && !string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(isbn))
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(API_BASE_URL);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    try
                    {
                        var data = new
                        {
                            MemberID = memberId,
                            BookID = bookId,
                            BorrowDate = borrowDate,
                            Title = title,
                            ISBN = isbn,
                            TotalCopies = totalCopiesBox,
                            AvailableCopies = availableCopiesBox
                        };

                        string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                        HttpResponseMessage response = client.PostAsync("InsertBook", new StringContent(jsonData, Encoding.UTF8, "application/json")).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Data saved successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Error Code: " + response.StatusCode + "\nMessage: " + response.ReasonPhrase);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill in all the required fields.");
            }

        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            DeleteBorrowedBookAsync().GetAwaiter();
        }
        private async Task DeleteBorrowedBookAsync()
        {
       
            BorrowedBook selectedBorrowedBook = GridBook.SelectedItem as BorrowedBook;

            if (selectedBorrowedBook != null)
            {
                try
                {
               
                    using (HttpClient client = new HttpClient())
                    {
                       
                        client.BaseAddress = new Uri(API_BASE_URL);

                    
                        HttpResponseMessage response = await client.DeleteAsync($"DeleteBook/{selectedBorrowedBook.BookID}");

                    
                        if (response.IsSuccessStatusCode)
                        {
                       
                            MessageBox.Show("BorrowedBook deleted successfully!");
                            await GetListData();
                        }
                        else
                        {
                          
                            MessageBox.Show($"Error Code: {response.StatusCode}\nMessage: {response.ReasonPhrase}");
                        }
                    }
                }
                catch (Exception ex)
                {
              
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a BorrowedBook item to delete.");
            }
        }

        private void GrideBook_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid grid)
            {
         
                if (grid.SelectedItem != null)
                {
                
                    var selectedBorrowedBook = (Book)grid.SelectedItem;

                
                  //  MessageBox.Show($"Selected Borrowed Book:\nID: {selectedBorrowedBook.BookID}\nBook Date: {selectedBorrowedBook.PublishedDate}");
                }
            }
        }

    }
}
