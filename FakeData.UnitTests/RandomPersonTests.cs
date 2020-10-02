using System;
using System.Globalization;
using Bogus;
using Bogus.Extensions;
using FakeData.Extensions;
using FakeData.Random;
using FakeData.UnitTests.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace FakeData.UnitTests
{

    public class RandomPersonTests
    {
        private readonly ITestOutputHelper output;

        public RandomPersonTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void GetAddress_Returns_AddressObject()
        {
            var o = new RandomAddress("de-DE").GetAddress();
            Assert.NotNull(o);
            output.WriteLine(string.Format("Street: {0}", o.Street));
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
        public void Person_Address_Has_Correct_Country(string locale)
        {
            output.Dump("Locale:" + locale);
            var randomPerson = new RandomPerson(locale);
            var fakePerson = randomPerson.GetPerson;
            var expectedCountry = new RegionInfo(new CultureInfo(locale).LCID).DisplayName;
            Assert.Equal(expectedCountry ,fakePerson.BillingAddress.Country);
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
            //Assert.NotEqual(randomPerson.GetPerson.PersonalIdNumber , randomPerson.GetPerson.PersonalIdNumber);
            Assert.NotEqual(randomPerson.GetPerson.FullName , randomPerson.GetPerson.FullName);
            Assert.NotEqual(randomPerson.GetPerson.BillingAddress.Street , randomPerson.GetPerson.BillingAddress.Street );
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
        public void CompleteStreetNumber_returns_formatted_street_and_street_numbers(string locale) {
            var randomPerson = new RandomAddress(locale);
            var address = randomPerson.GetAddress();
            Assert.Equal($"{address.Street},{address.StreetNumber},{address.StreetNumber2}".TrimEnd(','), 
                            address.CompleteStreetNumber());
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

        [Theory]
        [InlineData("de")]
        [InlineData("de_CH")]
        [InlineData("de_AT")]
        [InlineData("nl")]
        [InlineData("nl_BE")]
        [InlineData("nb_NO")]
        [InlineData("fi")]
        [InlineData("sv")]
        public void cellPhone(string locale)
        { 
            var bogusLocale = new CultureInfo(locale).ToBogusLocale();
            var faker = new Faker(bogusLocale);
            output.WriteLine(faker.Phone.CellPhone());
        }
    }
}