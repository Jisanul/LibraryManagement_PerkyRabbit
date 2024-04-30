using LibraryWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;

namespace LibraryWebApi.Controllers
{
    public class Member : Controller
    {
        Uri baseurl = new Uri("https://localhost:7041/APIs");

        private readonly HttpClient _httpClient;

        public Member() 
        {
             _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseurl;  
        
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            List<Models.Member> members = new List<Models.Member>();

            try
            {
              
                using (HttpClient httpClient = new HttpClient())
                {
                   
                    httpClient.BaseAddress = new Uri("https://localhost:7041/");

                  
                    HttpResponseMessage response = await httpClient.GetAsync("GetMembers");

                    
                    if (response.IsSuccessStatusCode)
                    {
                       
                        string data = await response.Content.ReadAsStringAsync();

                        members = JsonConvert.DeserializeObject<List<Models.Member>>(data);
                    }
                    else
                    {
                        // Handle unsuccessful response (optional)
                      
                    }
                }
            }
            catch (Exception ex)
            {
              
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
        public async Task<IActionResult> CreateAsync(Models.Member m)
        {
            try
            {
                string data = JsonConvert.SerializeObject(m);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("InsertMember", content);
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
            try
            {
              
                using (HttpClient httpClient = new HttpClient())
                {
                
                    httpClient.BaseAddress = new Uri("https://localhost:7041/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                   
                    HttpResponseMessage response = await httpClient.GetAsync($"GetMemberByID?id={id}");

                 
                    if (response.IsSuccessStatusCode)
                    {
                  
                        string data = await response.Content.ReadAsStringAsync();

                    
                        member = JsonConvert.DeserializeObject<Models.Member>(data);
                    }
                    else
                    {
                        // Handle unsuccessful response (optional)
                        
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return View(member);  

        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(Models.Member model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("InsertMember", content);
            
            if (response.IsSuccessStatusCode) 
            {

                return RedirectToAction("Index");
            
            }
            
            return View();
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            Models.Member member = new Models.Member();
            try
            {

                using (HttpClient httpClient = new HttpClient())
                {

                    httpClient.BaseAddress = new Uri("https://localhost:7041/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                    HttpResponseMessage response = await httpClient.GetAsync($"DeleteMember?id={id}");


                    if (response.IsSuccessStatusCode)
                    {

                        string data = await response.Content.ReadAsStringAsync();


                        member = JsonConvert.DeserializeObject<Models.Member>(data);
                    }
                    else
                    {
                        // Handle unsuccessful response (optional)

                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return View(member);

        }

    }
}
