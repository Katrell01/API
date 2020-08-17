using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ApiData
{

    public class TaxJarData
    {
           public string from_country { get; set; }
           public string from_zip { get; set; }
           public string from_state { get; set; }
           public string from_city { get; set; }
           public string from_street { get; set; }
           public string to_country { get; set; }
           public string to_zip { get; set; }
           public string to_state { get; set; }
           public double shipping { get; set; }
           public float amount { get; set; }
    }

    public class ReturnData
    {
         public string HtttpSuccessCode { get; set; }
         public string Phrase { get; set; }
         public Task<string> content { get; set; }
        public string header { get; set; }
    }


   public class Program
    {
        static void Main(string[] args)
        {
            Program model = new Program();
            //Uncomment to see Content returned.


            //var Location = model.GetTaxesByLocation("https://api.taxjar.com/v2/rates/").content;
            //Console.WriteLine(Location.Result);

            //var tax = model.PostTaxesForOrders("https://api.taxjar.com/v2/taxes").Result.content;
            //Console.WriteLine(tax.Result);


            //Console.ReadLine();

        }

      public ReturnData GetTaxesByLocation(string Url)
        {
            string baseUrl = "https://api.taxjar.com/v2/rates/90404";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "5da2f821eee4035db4771edab942a4cc");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUrl))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "5da2f821eee4035db4771edab942a4cc");
                    // List data response.
                    using (HttpResponseMessage response = client.GetAsync(baseUrl).Result)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                                return new ReturnData()
                                {
                                         HtttpSuccessCode = response.StatusCode.ToString(),
                                         Phrase =   response.ReasonPhrase,
                                         content = response.Content.ReadAsStringAsync(),
                                         header = response.Content.Headers.ToString()
                                };
                        }
                        else
                        {
                            return new ReturnData()
                            {
                                HtttpSuccessCode = response.ToString(),
                                Phrase = response.ReasonPhrase
                            };
                        }
                    }
                    
                }
               
            }
           
        }


        public  async  Task<ReturnData>  PostTaxesForOrders(string baseUrl, [Optional] TaxJarData data)
        {
            var content = new TaxJarData();

            if (data != null)
            {
                content = data;
            }
            else
            {
                content = new TaxJarData()
                {
                    from_country = "US",
                    from_zip = "92093",
                    from_state = "CA",
                    from_city = "La Jolla",
                    from_street = "9500 Gilman Drive",
                    to_country = "US",
                    to_zip = "90002",
                    to_state = "CA",
                    amount = 15,
                    shipping = 1.5
                };
            }
            var jsonContent = JsonConvert.SerializeObject(content);
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, baseUrl))
                {
                    request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "5da2f821eee4035db4771edab942a4cc");

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(baseUrl);
                    using (HttpResponseMessage response = await  client.SendAsync(request))
                    {
                        string responseAsString = await response.Content.ReadAsStringAsync();
                        if (responseAsString != null)
                        {
                            return new ReturnData()
                            {
                                HtttpSuccessCode = response.StatusCode.ToString(),
                                Phrase = response.ReasonPhrase,
                                content = response.Content.ReadAsStringAsync(),

                                header = response.Content.Headers.ToString()
                            };
                        }
                        else
                        {
                            return new ReturnData()
                            {
                                HtttpSuccessCode = response.StatusCode.ToString(),
                                Phrase = response.ReasonPhrase,
                                content = response.Content.ReadAsStringAsync(),

                                header = response.Content.Headers.ToString()


                            };
                                       
                        }

                      }

                    }

                }

            }

        }
    }

