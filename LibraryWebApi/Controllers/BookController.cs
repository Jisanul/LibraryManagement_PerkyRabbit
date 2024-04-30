using LibraryManagementSystem.Models;
using LibraryWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace LibraryWebApi.Controllers
{
    public class BookController : Controller
    {
        Uri baseurl = new Uri("https://localhost:7041/APIs");

        private readonly HttpClient _httpClient;

        public BookController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseurl;

        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            List<Book> members = new List<Book>();

            try
            {
                // Create a new instance of HttpClient
                using (HttpClient httpClient = new HttpClient())
                {
                    // Set the base address of the HttpClient to your Web API URL
                    httpClient.BaseAddress = new Uri("https://localhost:7041/");

                    // Send a GET request to the "GetMember" endpoint
                    HttpResponseMessage response = await httpClient.GetAsync("GetBook");

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string data = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON data into a list of members
                        members = JsonConvert.DeserializeObject<List<Book>>(data);
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

        [HttpGet]
        public IActionResult Create()
        {

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Book m)
        {
            try
            {
                string data = JsonConvert.SerializeObject(m);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("InsertBook", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }


            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }

        [HttpGet]   
        public async Task<IActionResult> EditAsync(int id)
        {
            Models.Member member = new Models.Member();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/GetBookByID" + id).Result;
            if (response.IsSuccessStatusCode)
            {

                string data = response.Content.ReadAsStringAsync().Result;

                // Deserialize the JSON data into a list of members
                member = JsonConvert.DeserializeObject<Models.Member>(data);

            }

            return View(member);



            
        }
    }
}
