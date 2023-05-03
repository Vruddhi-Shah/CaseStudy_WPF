using CaseStudy_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;


namespace CaseStudy_WPF.Configuration
{
    public class EmployeeService
    {
        public static APIResponseModel GetAllEmployee(long pageNo)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Common.baseUrl);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Common.token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage responseMessage = client.GetAsync($"users?page=" + pageNo).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var employeeDetails = responseMessage.Content.ReadAsAsync<APIResponseModel>().Result;
                        return employeeDetails; ;
                    }
                    else
                    {
                        MessageBox.Show("Due to some issue not able to fetch empployees data.");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Due to some issue not able to fetch empployees data.");
            }
            return null;
        }

        public static void DeleteEmployee(int employeeId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Common.baseUrl);
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Common.token}");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));  
                    HttpResponseMessage response = client.DeleteAsync($"users/{employeeId}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Employee detail deleted successfully.");
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Due to some error not able to delete employee detail");
            }
        }

        public static HttpResponseMessage AddEmployee(EmployeeDetails empDetail)
        {
            try
            {
                string json = JsonConvert.SerializeObject(empDetail);
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Common.baseUrl);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Common.token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsJsonAsync("users", empDetail).Result;
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateEmployeeAsync(EmployeeDetails empDetail)
        {

            try
            {
                string json = JsonConvert.SerializeObject(empDetail);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Put, $"{Common.baseUrl}/users/{empDetail.Id}");
                request.Headers.Add("Authorization", $"Bearer {Common.token}");
                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();
                Console.WriteLine(response.Content.ReadAsStringAsync());
                MessageBox.Show("Employee updated successfully.");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static EmployeeDetails GetEmployeeDetails(int employeeId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Common.baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Common.token);
                var response = client.GetAsync($"users/{employeeId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<EmployeeById>().Result;
                    return result.Data;
                }
            }
            return null;
        }
    }
}
