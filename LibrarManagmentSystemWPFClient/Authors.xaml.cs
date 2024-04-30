using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;
using LibraryManagementSystemWPFClient.ViewModels;


namespace LibrarManagmentSystemWPFClient
{
	public partial class Authors : Window
    {
        public Authors()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            string authorName = AuthorName.Text;
            string authorBio= AuthorBio.Text;

            
            if (!string.IsNullOrWhiteSpace(authorName) && !string.IsNullOrWhiteSpace(authorBio))
            {
               
                HttpClient client = new HttpClient();

                
                client.BaseAddress = new Uri("https://localhost:7041/");

              
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var data = new
                    {
                        AuthorName = authorName,
                        AuthorBio = authorBio
                    };
                    
                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
             
                    HttpResponseMessage response = client.PostAsync("InsertAuthor", new StringContent(jsonData, Encoding.UTF8, "application/json")).Result;

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
                finally
                {
         
                    client.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Please enter some text to save.");
            }
        }

        private void ShowList_Click(object sender, RoutedEventArgs e)
        {
            SomeMethod();
        }
        private async Task SomeMethod()
        {



            try
            {
        
                using (HttpClient client = new HttpClient())
                {
                 
                    client.BaseAddress = new Uri("https://localhost:7041/");

                   
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

         
                    HttpResponseMessage response = await client.GetAsync("GetAuthor");

   
                    if (response.IsSuccessStatusCode)
                    {
                  
                        string responseData = await response.Content.ReadAsStringAsync();

                   
                        List<Author> authors = JsonConvert.DeserializeObject<List<Author>>(responseData);
                        GrideAuthor.ItemsSource = authors;
                       
                        
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
        private void GrideAuthor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is DataGridRow)
            {
    
                Author selectedAuthor = GrideAuthor.SelectedItem as Author;

                if (selectedAuthor != null)
                {
              
                    AuthorName.Text = selectedAuthor.AuthorName;
                    AuthorBio.Text = selectedAuthor.AuthorBio;
                }
            }
        }

        private void RefreshDataGrid()
        {

            SomeMethod();
        }

        private void UpdateAuthor_Click(object sender, RoutedEventArgs e)
        {
            UpdateMethod();
        }

        private async Task UpdateMethod()
        {

            Author selectedAuthor = GrideAuthor.SelectedItem as Author;

            if (selectedAuthor != null)
            {
                try
                {
         
                    using (HttpClient client = new HttpClient())
                    {
                        
                        client.BaseAddress = new Uri("https://localhost:7041/");

                      
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    
                        var jsonContent = JsonConvert.SerializeObject(selectedAuthor);
                        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                  
                        HttpResponseMessage response = await client.PutAsync("UpdateAuthor", content);

                     
                        if (response.IsSuccessStatusCode)
                        {
                     
                            MessageBox.Show("Author updated successfully!");

                         
                            RefreshDataGrid();
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

        private void DeleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            DeleteMethod().GetAwaiter();
        }

        private async Task DeleteMethod()
        {
         
            Author selectedAuthor = GrideAuthor.SelectedItem as Author;

            if (selectedAuthor != null)
            {
                try
                {
               
                    using (HttpClient client = new HttpClient())
                    {
                        
                        client.BaseAddress = new Uri("https://localhost:7041/");

                       
                        HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7041/DeleteAuthor/{selectedAuthor.AuthorID}");

                    
                        if (response.IsSuccessStatusCode)
                        {
                        
                            MessageBox.Show("Author deleted successfully!");
                            RefreshDataGrid();
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
                MessageBox.Show("Please select an author to delete.");
            }
        }
    }
        
}
