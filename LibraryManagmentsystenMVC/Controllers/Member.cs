using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LibraryManagmentsystenMVC.Controllers
{
    public class Member : Controller
    {
        private const string API_BASE_URL = "https://localhost:7041/";
        // GET: BorrowBookController
        public async Task<ActionResult> IndexAsync()
        {
            List<Member> members = new List<Member>();

            try
            {
                // Create a new instance of HttpClient
                using (HttpClient httpClient = new HttpClient())
                {
                    // Set the base address of the HttpClient to your Web API URL
                    httpClient.BaseAddress = new Uri("https://localhost:7041/");

                    // Send a GET request to the "GetMember" endpoint
                    HttpResponseMessage response = await httpClient.GetAsync("GetMember");

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string data = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON data into a list of members
                        members = JsonConvert.DeserializeObject<List<Member>>(data);
                    }
                    else
                    {
                        // Handle unsuccessful response (optional)
                        // For example, log the error or display a message to the user
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the request (optional)
                // For example, log the exception or display an error message to the user
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return View(members);

        
          
        }
        //DeleteBorrowedBook
        [HttpPost]
        public async Task<IActionResult> DeleteBorrowedBook(int borrowID)
        {
            try
            {
                // Create an HttpClient instance
                using (HttpClient client = new HttpClient())
                {
                    // Set the base address of your API
                    client.BaseAddress = new Uri(API_BASE_URL);

                    // Set the Accept header for JSON responses
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Send a DELETE request to the API endpoint to delete the borrowed book
                    HttpResponseMessage response = await client.DeleteAsync($"DeleteBorrowedBook/{borrowID}");

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Redirect to the Index action to refresh the list of borrowed books
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // If not successful, display an error message
                        TempData["ErrorMessage"] = "Failed to delete borrowed book.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            }

            // Redirect back to the Index action
            return RedirectToAction("Index");
        }

       
    }
}
