using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Bogus;
using CsvHelper;
using FakeData.Helpers;
using FakeData.Model;

namespace FakeData.Random
{
    public class RandomAddress {
        private static readonly System.Random random = new System.Random();
        private static readonly ConcurrentDictionary<string, List<IAddress>> _data = new ConcurrentDictionary<string, List<IAddress>>();
        private readonly CultureInfo _locale;
        private string Country {get;}

        public RandomAddress (string culture) 
        {
             _locale = new CultureInfo(culture);
             Country = new RegionInfo(_locale.LCID).TwoLetterISORegionName;
             
             if(!_data.ContainsKey(Country)) {
                var addresses = CSVReader.ReadData(string.Format("Resources.Addresses.{0}.csv", Country), ReadAddresses);
                _data.TryAdd(Country, addresses);
             }
        }
        public IAddress GetAddress () 
        { 
            var l = _data.GetValueOrDefault(Country);
            return l[random.Next(l.Count)];
        }

        private List<IAddress> ReadAddresses(StreamReader s)
        {
            List<IAddress> records = new List<IAddress>();
            var country = new RegionInfo(_locale.LCID).DisplayName;
            using (var csv = new CsvReader(s, CultureInfo.InvariantCulture))
            {
                while (csv.Read())
                {
                    var record = new Address
                    {
                        City = csv.GetField(0),
                        Street = csv.GetField(1),
                        StreetNumber = csv.GetField(2),
                        StreetNumber2 = csv.GetField(3),
                        ZipCode = csv.GetField(4),
                        Country = country,
                        CountryCode = Country
                    };
                    records.Add(record);
                }
            }
            return records;
        }
    }
}