using System;
using Bogus;
using Bogus.Extensions.Finland;
using Bogus.Extensions.Denmark;
using Bogus.Extensions.Norway;

namespace FakeData.Random
{
    public static class PersonIdNumberExtension
    {
        public static string PersonIdNumber(this Bogus.Person p, string countryCode) {
            switch (countryCode)
            {  
                case "fi": return p.Henkilötunnus();
                case "no": return new Faker().Person.Fødselsnummer();
                case "dk": return new Faker().Person.Cpr();
                default:
                        return null;
            }
        }
    }
}