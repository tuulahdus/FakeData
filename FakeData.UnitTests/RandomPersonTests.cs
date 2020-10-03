using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions;
using FakeData.Extensions;
using FakeData.Model;
using FakeData.Random;
using FakeData.UnitTests.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace FakeData.UnitTests
{
    public class RandomAddressTests {

        [Theory]
        [InlineData("de-DE")]
        [InlineData("de-CH")]
        [InlineData("de-AT")]
        [InlineData("nl-NL")]
        [InlineData("nl-BE")]
        [InlineData("nb-NO")]
        [InlineData("fi-FI")]
        [InlineData("sv-SE")]
        public void GetAddress_Returns_Different_Addresses_When_Called_Twice(string locale)
        {
            var addresses = new RandomAddress(locale);
            Assert.NotEqual(addresses.GetAddress().DumpString(), addresses.GetAddress().DumpString());
        }

        [Theory]
        [InlineData("de-DE")]
        [InlineData("de-CH")]
        [InlineData("de-AT")]
        [InlineData("nl-NL")]
        [InlineData("nl-BE")]
        [InlineData("nb-NO")]
        [InlineData("fi-FI")]
        [InlineData("sv-SE")]
        public void CompleteStreetNumber_Returns_Formatted_Street_Name_And_Street_Number(string locale) {
            var address = new RandomAddress(locale).GetAddress();
            Assert.Equal($"{address.Street},{address.StreetNumber},{address.StreetNumber2}".TrimEnd(','), 
                            address.CompleteStreetNumber());
        }
    }
    public class RandomPersonTests
    {
        private readonly ITestOutputHelper output;

        public RandomPersonTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData("de-DE", "Deutschland")]
        [InlineData("de-CH", "Schweiz")]
        [InlineData("de-AT", "Österreich")]
        [InlineData("nl-NL", "Nederland")]
        [InlineData("nl-BE", "België")]
        [InlineData("nb-NO", "Norge")]
        [InlineData("fi-FI", "Suomi")]
        [InlineData("sv-SE", "Sverige")]
        public void Person_Address_Has_Correct_Country(string locale, string expectedCountry)
        {
            Assert.Equal(expectedCountry ,new RandomPerson(locale).GetPerson.BillingAddress.Country);
        }
        
        [Theory]
        [InlineData("de-DE")]
        [InlineData("de-CH")]
        [InlineData("de-AT")]
        [InlineData("nl-NL")]
        [InlineData("nl-BE")]
        [InlineData("nb-NO")]
        [InlineData("fi-FI")]
        [InlineData("sv-SE")]
        public void GetPerson_Generates_Persons_With_Different_Names(string locale) {
            var randomPerson = new RandomPerson(locale);
            Assert.NotEqual(randomPerson.GetPerson.FullName , randomPerson.GetPerson.FullName);
        }

        [Theory]
        [InlineData("nb-NO")]
        [InlineData("fi-FI")]
        [InlineData("sv-SE")]
        public void GetPerson_Generates_Persons_With_Different_PersonalIds(string locale) {
            var randomPerson = new RandomPerson(locale);
            Assert.NotEqual(randomPerson.GetPerson.PersonalIdNumber , randomPerson.GetPerson.PersonalIdNumber);
        }

        [Fact]
        public void Default_Generation_Rules_Can_Be_Overriden()
        {
            string emailDomain = "afterpaytest.com";
            var fakePerson = new RandomPerson("de-DE").RuleFor(u => u.Email, 
                (f, u) => f.Internet.Email (u.FirstName, u.LastName, emailDomain).ToLower()).GetPerson;
            Assert.EndsWith(emailDomain, fakePerson.Email);
        }
    }
}