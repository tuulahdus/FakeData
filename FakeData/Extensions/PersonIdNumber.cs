using System;
using Bogus;
using Bogus.Extensions.Finland;
using Bogus.Extensions.Denmark;
using Bogus.Extensions.Norway;

namespace FakeData.Random
{
    public static class PersonExtensions
    {
        private static string Personnummer(this Bogus.Person p) 
        {
            var r = p.Random;
            string formattedDateOfBirth = $"{p.DateOfBirth:yyMMdd}";
            int rollingId = r.Int(100, 999);
            String[] digits = (formattedDateOfBirth + rollingId).Split("");
            var checksum = 0;
            for (int i=0; i< digits.Length; i++) {
                var n = int.Parse(digits[i]) * (2 - i % 2);
                checksum += n /10 + n % 10;
            }
            checksum = checksum % 10 == 10 ? 0 : checksum % 10;
            return $"{p.DateOfBirth:ddMMyy}{rollingId}{checksum}";
        }
        public static string PersonIdNumber(this Bogus.Person p, string countryCode) {
            switch (countryCode)
            {  
                case "fi": 
                    return p.Henkilötunnus();
                case "nb_NO": 
                    return p.Fødselsnummer();
                case "sv": 
                    return p.Personnummer();
                case "dk":
                    return p.Cpr();
                default:
                    return null;
            }
        }
        public static string Initials(this Bogus.Person p) {
            return p.FirstName.Substring(0,1);
        }
    }
}