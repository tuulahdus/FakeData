using System;
using Bogus;

namespace FakeData.Model {
    public interface IAddress {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string StreetNumber2 { get; set; }
        public string ZipCode { get; set; }

        public string CompleteStreetNumber(string format = "{0},{1},{2}");

    }
}