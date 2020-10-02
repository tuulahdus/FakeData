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
            var person = new Faker(bogusLocale).Person;
            _fakePerson = new Faker<Person> (bogusLocale)
                .CustomInstantiator((f) => { 
                    var person = f.Person;
                    return new Person {
                        Gender = f.PickRandom<Gender>(),
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        Initials = person.Initials(),
                        Phone = person.Phone,
                        Email = person.Email,
                        BirthDate = person.DateOfBirth.TruncateToDayStart(),
                        PersonalIdNumber = person.PersonIdNumber(bogusLocale)
                    };
                })
                .RuleFor (u => u.Mobile, f => f.Phone.CellPhone())
                .RuleFor (u => u.BillingAddress, () => address.GetAddress())
                .RuleFor (u => u.DeliveryAddress, () => address.GetAddress());
        }

        public RandomPerson RuleFor<TProperty>(Expression<Func<Person, TProperty>> property, Func<Faker, Person, TProperty> setter){
            _fakePerson.RuleFor(property, setter);
            return this;
        }
        public IPerson GetPerson => _fakePerson.Generate ();
    }

}