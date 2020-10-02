using CsvHelper.Configuration.Attributes;
using FakeData.Model;

namespace FakeData.Model {
    public class Address : IAddress {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        [Index(0)]
        public string City { get; set; }
        [Index(1)]
        public string Street { get; set; }
        [Index(2)]
        public string StreetNumber { get; set; }
        [Index(3)]
        public string StreetNumber2 { get; set; }
        [Index(4)]
        public string ZipCode { get; set; }
    }
}