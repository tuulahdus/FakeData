using System.Collections.Generic;
using Bogus;
using Bogus.DataSets;

namespace FakeData.Extensions
{
    public static class PhoneNumbersExtension
    {
        public static string CellPhone(this PhoneNumbers phoneNumbers, string locale = "en") {
            return CellPhone(phoneNumbers.Locale);
        }

        private static Dictionary<string, string> phoneFormats = new Dictionary<string, string> {
            {"nl", "003163#######"},
            {"nl_BE", "0032470######"},
            {"de", "0049151######"},
            {"fi", "0035840#######"},
            {"sv", "004610#######"},
            {"nb_NO", "00474#######"},
            {"dk", "004520######"},
            {"de_AT", "0043676######"},
            {"de_CH", "004178#######"}
        };

        public static string CellPhone(string locale = "en", string format = null) {
            var cell_format = format ?? phoneFormats.GetValueOrDefault(locale);
            var faker = new Faker(locale);
            return faker.Phone.PhoneNumber(cell_format);
        }
    }
}