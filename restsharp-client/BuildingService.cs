using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSchool.Client
{
    public class BuildingService
    {
        private RestClient _client;

        public BuildingService(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public void GetBuildings()
        {
            var request = new RestRequest("api/buildings", Method.GET);
            var response = _client.Get<List<Building>>(request);

            if(response.IsSuccessful)
            {
                foreach(var b in response.Data)
                {
                    Console.WriteLine($"{b.buildingID} : {b.buildingName}");
                }
            }
            else
            {
                Console.WriteLine(response.Content);
            }
        }

        public void GetBuilding()
        {
            Console.Write("Enter building id: ");
            int id = int.Parse(Console.ReadLine());

            var request = new RestRequest($"api/buildings/{id}", Method.GET);
            var response = _client.Get<Building>(request);

            if (response.IsSuccessful)
            {
                Building b = response.Data;
                Console.WriteLine($"{b.buildingID} : {b.buildingName}");
            }
            else
            {
                Console.WriteLine(response.Content);
            }
        }

        public void AddBuilding()
        {
            Console.Write("Add building name: ");
            string name = Console.ReadLine();

            var request = new RestRequest($"api/buildings?buildingName={name}", Method.POST);
            var response = _client.Post(request);

            if (response.IsSuccessful)
            {
                var location = response.Headers.First(h => h.Name == "Location").Value;
                Console.WriteLine($"Find created building at: {location}");
            }
            else
            {
                Console.WriteLine(response.Content);
            }
        }

        public void EditBuilding()
        {
            Building b = new Building();

            Console.Write("Enter building id to edit: ");
            b.buildingID = int.Parse(Console.ReadLine());

            Console.Write("Enter new building name: ");
            b.buildingName = Console.ReadLine();

            var request = new RestRequest($"api/buildings", Method.PUT);
            request.AddJsonBody(b);
            var response = _client.Put(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Building updated successfully!");
            }
            else
            {
                Console.WriteLine(response.Content);
            }
        }

        public void DeleteBuilding()
        {
            Console.Write("Enter building id: ");
            int id = int.Parse(Console.ReadLine());

            var request = new RestRequest($"api/buildings/{id}", Method.DELETE);
            var response = _client.Delete(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Building deleted!");
            }
            else
            {
                Console.WriteLine(response.Content);
            }
        }
    }
}
