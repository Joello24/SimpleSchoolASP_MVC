using System;
using RestSharp;

namespace SimpleSchool.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the host url: ");
            string host = Console.ReadLine();

            var service = new BuildingService(host);

            // service.GetBuildings();
            // service.GetBuilding();
            // service.AddBuilding();
            // service.EditBuilding();
            service.DeleteBuilding();
        }
    }
}
