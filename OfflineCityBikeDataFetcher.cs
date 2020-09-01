
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

public class OfflineCityBikeDataFetcher : ICityBikeDataFetcher
{
    public Task<int> GetBikeCountInStation(string stationName)
    {
        IEnumerable<string> file = File.ReadLines("bikedata.txt");

        foreach(string line in file) {
            if(line.StartsWith(stationName)) {
                String[] splitResult = line.Split(':', 2, StringSplitOptions.None);
                int bikeCount = Convert.ToInt32(splitResult[1].TrimStart());
                return Task.Run( () => { return bikeCount; } );
            }
        }
        return null;
    }
}