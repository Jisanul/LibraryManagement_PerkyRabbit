using LibraryManagementSystemWPFClient.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace LibrarManagmentSystemWPFClient
{
	
	public partial class BorrowedBooks : Window
    {
        private List<string> memberNames;
        private List<string> memberName;

        private const string API_BASE_URL = "https://localhost:7041/";
        public BorrowedBooks()
        {
            InitializeComponent();
            memberNames = new List<string>();
            memberName = new List<string>();
            Status.Items.Add("Borrowed");
            Status.Items.Add("Returned");
            Status.Items.Add("Overdue");

            
            MemberBox.ItemsSource = memberNames;
            BookBox.ItemsSource = memberName;

			
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

					
					HttpResponseMessage response = await client.GetAsync("GetMembers");

					
					if (response.IsSuccessStatusCode)
					{
						
						string responseData = await response.Content.ReadAsStringAsync();

					
						List<Member> members = JsonConvert.DeserializeObject<List<Member>>(responseData);
						MemberBox.ItemsSource = members;


					}
					else
					{
						
						MessageBox.Show("Error Code: " + response.StatusCode + "\nMessage: " + response.ReasonPhrase);
					}



				
					var bookResponse = await client.GetAsync("GetBooks");

				
					if (bookResponse.IsSuccessStatusCode)
					{
						
						string responseData = await bookResponse.Content.ReadAsStringAsync();

				
						List<Book> books = JsonConvert.DeserializeObject<List<Book>>(responseData);
						BookBox.ItemsSource = books;


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
        private void ShowList_Click(object sender, RoutedEventArgs e)
        {
            GetListData().GetAwaiter();
        }
        private async Task GetListData()
        {
            try
            {
         
                using (HttpClient client = new HttpClient())
                {
                    
                    client.BaseAddress = new Uri(API_BASE_URL);

                
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            
                    HttpResponseMessage response = await client.GetAsync("GetBorrowedBooks");

          
                    if (response.IsSuccessStatusCode)
                    {
                  
                        string responseData = await response.Content.ReadAsStringAsync();

                     
                        List<BorrowedBook> borrowedBooks = JsonConvert.DeserializeObject<List<BorrowedBook>>(responseData);
                        GridBorrowedBook.ItemsSource = borrowedBooks;


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

        private void SaveBorrowedBook_Click(object sender, RoutedEventArgs e)
		{
	
			int memberId = 0;
			if (MemberBox.SelectedItem is Member selectedMember)
			{
				memberId = selectedMember.MemberID;
			}

			int bookId = 0;
			if (BookBox.SelectedItem is Book selectedBook)
			{
				bookId = selectedBook.BookID;
			}
			DateTime borrowDate = BorrowDate.SelectedDate ?? DateTime.MinValue; 
            DateTime returnDate = ReturnDate.SelectedDate ?? DateTime.MinValue; 
            string status = Status.Text;

          
            if (memberId > 0 && bookId > 0 && borrowDate != DateTime.MinValue && returnDate != DateTime.MinValue)
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
                            ReturnDate = returnDate,
                            Status = status
                        };

                     
                        string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                      
                        HttpResponseMessage response = client.PostAsync("InsertBorrowedBook", new StringContent(jsonData, Encoding.UTF8, "application/json")).Result;

                      
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

		private void DeleteBorrowedBook_Click(object sender, RoutedEventArgs e)
		{
			DeleteBorrowedBookAsync().GetAwaiter();
		}
        private async Task DeleteBorrowedBookAsync()
		{

			BorrowedBook selectedBorrowedBook = GridBorrowedBook.SelectedItem as BorrowedBook;

			if (selectedBorrowedBook != null)
			{
				try
				{
					
					using (HttpClient client = new HttpClient())
					{
					
						client.BaseAddress = new Uri(API_BASE_URL);

						
						HttpResponseMessage response = await client.DeleteAsync($"DeleteBorrowedBook/{selectedBorrowedBook.BorrowedBookID}");

						
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

        private void GrideBorrowedBook_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid grid)
            {
             
                if (grid.SelectedItem != null)
                {
                   
                    var selectedBorrowedBook = (BorrowedBook)grid.SelectedItem;

                    MessageBox.Show($"Selected Borrowed Book:\nID: {selectedBorrowedBook.BorrowedBookID}\nBorrow Date: {selectedBorrowedBook.BorrowDate}");
                }
            }
        }
        private void UpdateBorrowedBook_Click(object sender, RoutedEventArgs e)
        {
            UpdateMethod();
        }
        private async Task UpdateMethod()
        {
            
            BorrowedBook selectedBorrowedBook = GridBorrowedBook.SelectedItem as BorrowedBook;

            if (selectedBorrowedBook != null)
            {
                try
                {
                   
                    using (HttpClient client = new HttpClient())
                    {
                        
                        client.BaseAddress = new Uri("https://localhost:7041/");

                      
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        
                        var jsonContent = JsonConvert.SerializeObject(selectedBorrowedBook);
                        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

               
                        HttpResponseMessage response = await client.PutAsync("UpdateBorrowedBook", content);

                    
                        if (response.IsSuccessStatusCode)
                        {
                        
                            MessageBox.Show("BorrowedBook updated successfully!");

                           
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
            else
            {
                MessageBox.Show("Please select an author to update.");
            }
        }
    }
}
