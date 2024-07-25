using Newtonsoft.Json.Linq;
using RestSharp;
using System;
namespace Lesson30
{
    public class Tests
    {
        private const string BaseUrl = "https://petstore.swagger.io/v2/";
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreatePetTest()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("pet", Method.Post);
            var userRequest = new 
            { id = 0, 
                category = new { id = 0, name = "string" }, 
                name = "doggie", 
                photoUrls = new string[] { "string" }, 
                tags = new[] { new { id = 0, name = "string" } }, status = "available" };
            request.AddJsonBody(userRequest);

            var response = client.Execute(request);

            var jsonResponse = JObject.Parse(response.Content);
            Assert.That((int)response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void UpdatePetTest()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("pet", Method.Post);
            var petname = RandomString(3);


            var userRequest = new
            {
                id = 12,
                category = new 
                { id = 12, name = petname }                
            };
            request.AddJsonBody(userRequest);

            var response = client.Execute(request);

            var jsonResponse = JObject.Parse(response.Content);
            var category = jsonResponse["category"];

            Assert.That((int)response.StatusCode, Is.EqualTo(200));
            Assert.That((string)category["name"], Is.EqualTo(petname));
        }

        [Test]
        public void CreateAndDeletePetTest()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("pet", Method.Post);
            var petname = RandomString(3);
            var userRequest = new
            {
                id = 0,
                category = new { id = 100, name = petname },
                name = "doggie",
                photoUrls = new string[] { "string" },
                tags = new[] { new { id = 100, name = "string" } },
                status = "available"
            };
            request.AddJsonBody(userRequest);

            var response = client.Execute(request);

            var jsonResponse = JObject.Parse(response.Content);
            Assert.That((int)response.StatusCode, Is.EqualTo(200));

        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }      

    }
}