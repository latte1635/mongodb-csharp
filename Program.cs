using System;
using System.Threading.Tasks;
using CustomExceptions;

namespace teht1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string stationName = "Munkkiniemen aukio";
            int bikeCount = -1;

            if(args.Length == 0)
                throw new ArgumentException("No argument provided");

            ICityBikeDataFetcher dataFetcher = null;

            if(args[0] == "online")
                dataFetcher = new RealTimeCityBikeDataFetcher();
            else if(args[0] == "offline")
                dataFetcher = new OfflineCityBikeDataFetcher();
            else 
                throw new ArgumentException("Argument must be 'online' or 'offline'");

            try {
                bikeCount = await dataFetcher.GetBikeCountInStation(stationName);
            }
            catch {
                throw new NotFoundException("Not found: " + stationName);
            }
            Console.WriteLine(bikeCount);
        }
    }
}
