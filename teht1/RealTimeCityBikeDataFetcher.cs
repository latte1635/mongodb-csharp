using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher {

    public Task<int> GetBikeCountInStation(string stationName) {

        if(stationName.Any(char.IsDigit))
            throw new ArgumentException("Invalid argument: " + stationName);

        HttpClient client = new HttpClient();
        Task<string> data = client.GetStringAsync("http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental");
        string jsonString = data.Result;

        dynamic bikeRentalStationList = JObject.Parse(jsonString);

        foreach (var station in bikeRentalStationList.stations) {
            if(station.name == stationName) {
                int bikeCount = station.bikesAvailable;
                return Task.Run( () => { return bikeCount; } );
            }
        }
        return null;
    }
}