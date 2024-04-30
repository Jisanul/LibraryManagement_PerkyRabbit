using LibraryManagementRazorClient.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace LibraryManagementRazorClient.Controllers
{
    public class BorrowBookController : Controller
    {
        private const string API_BASE_URL = "https://localhost:7041/";
        // GET: BorrowBookController
        public ActionResult Index()
        {
            // Create an HttpClient instance
            using (HttpClient client = new HttpClient())
            {
                // Set the base address of your API
                client.BaseAddress = new Uri(API_BASE_URL);

                // Set the Accept header for JSON responses
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Send a GET request to the API endpoint to retrieve the list of authors
                HttpResponseMessage response = client.GetAsync("GetBorrowedBooks").GetAwaiter().GetResult();

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    // Deserialize the JSON string to a list of authors
                    List<BorrowedBook> borrowedBooks = JsonConvert.DeserializeObject<List<BorrowedBook>>(responseData);


                    return View(borrowedBooks);

                }
            }

            return View();
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

        //InsertBorrowedBook
        [HttpPost]
        public async Task<IActionResult> InsertBorrowedBook(BorrowedBook borrowedBook)
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

                    // Serialize the BorrowedBook object to JSON
                    string jsonBody = JsonConvert.SerializeObject(borrowedBook);

                    // Create a StringContent with the JSON data
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send a POST request to the API endpoint to insert the borrowed book
                    HttpResponseMessage response = await client.PostAsync("InsertBorrowedBook", content);

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Redirect to the Index action to refresh the list of borrowed books
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // If not successful, display an error message
                        TempData["ErrorMessage"] = "Failed to insert borrowed book.";
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

        //UpdateBorrowedBook
        [HttpPost]
        public async Task<IActionResult> UpdateBorrowedBook(BorrowedBook borrowedBook)
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

                    // Serialize the BorrowedBook object to JSON
                    string jsonBody = JsonConvert.SerializeObject(borrowedBook);

                    // Create a StringContent with the JSON data
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send a POST request to the API endpoint to update the borrowed book
                    HttpResponseMessage response = await client.PostAsync("UpdateBorrowedBook", content);

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Redirect to the Index action to refresh the list of borrowed books
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // If not successful, display an error message
                        TempData["ErrorMessage"] = "Failed to update borrowed book.";
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
