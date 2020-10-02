using System;
using System.Globalization;
using System.Linq.Expressions;
using Bogus;
using Bogus.Extensions;
using Bogus.Extensions.Finland;
using FakeData.Extensions;
using FakeData.Model;
using Newtonsoft.Json;

namespace FakeData.Random {
    public class Person : IPerson {
        public string Initials { get; set; }

        [JsonIgnore]
        public Gender Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IAddress BillingAddress { get; set; }
        public IAddress DeliveryAddress { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PersonalIdNumber { get; set; }
        [JsonIgnore]
        public string Locale { get ; set ; }
    }
    public enum Gender {
        Male,
        Female
    }
    public class RandomPerson {
        private readonly Faker<Person> _fakePerson;
        public RandomPerson (string locale) {
            var bogusLocale = new CultureInfo(locale).ToBogusLocale();
            var address = new RandomAddress(locale);
            _fakePerson = new Faker<Person> (bogusLocale)
                .RuleFor (u => u.Gender, f => f.PickRandom<Gender> ())
                .RuleFor (u => u.FirstName, (f, u) => f.Name.FirstName ())
                .RuleFor (u => u.Initials, (f, u) => u.FirstName.Substring (0, 1))
                .RuleFor (u => u.LastName, (f, u) => f.Name.LastName ())
                .RuleFor (u => u.BirthDate, (f) => f.Date.Between (DateTime.Now.AddYears (-60), DateTime.Now.AddYears (-18))
                    .TruncateToDayStart ())
                .RuleFor (u => u.Phone, f => f.Phone.PhoneNumber ())
                .RuleFor (u => u.Mobile, f => f.Phone.CellPhone())
                .RuleFor (u => u.Email, (f, u) => f.Internet.Email (u.FirstName, u.LastName))
                .RuleFor (u => u.BillingAddress, (f) => address.GetAddress())
                .RuleFor (u => u.DeliveryAddress, (f, u) => address.GetAddress())
                .RuleFor(u => u.PersonalIdNumber, (f) => f.Person.PersonIdNumber(bogusLocale));
        }

        public RandomPerson RuleFor<TProperty>(Expression<Func<Person, TProperty>> property, Func<Faker, Person, TProperty> setter){
            _fakePerson.RuleFor(property, setter);
            return this;
        }
        public IPerson GetPerson => _fakePerson.Generate ();
    }

}