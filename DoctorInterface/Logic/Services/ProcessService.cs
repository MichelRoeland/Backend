using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class ProcessService
    {
        private static string json;

        private static async Task<string> ProcessRepositoriesAsync()
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = await client.GetStringAsync("http://10.0.170.151/BlockChainApi.svc/AllPatients");

            //http://10.0.170.151/blockchainapi.svc/help
            //var msg = await stringTask;
            //Console.Write(msg);
            return stringTask;
        }

        private static async Task RunAsync()
        {
            json = await ProcessRepositoriesAsync();
        }

        public static List<BlockChainPatient> GetPatientsDeserializeAsync()
        {
            RunAsync().GetAwaiter().GetResult();

            var patients = JsonConvert.DeserializeObject<List<BlockChainPatient>>(json);

            return patients;
        }
    }
}
