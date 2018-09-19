using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:6156");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "test-api-cid", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("test-api");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:6156/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            return StatusCode((int) HttpStatusCode.OK);
        }

        [HttpGet("password")]
        public async Task<IActionResult> Get([FromQuery(Name = "username")] string username, [FromQuery(Name = "password")] string password)
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:6156");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "test-api-cid", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(username, password);

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:6156/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            return StatusCode((int) HttpStatusCode.OK);
        }
    }
}