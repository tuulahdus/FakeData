using System;
using Bogus;

namespace FakeData.Model {
    public interface IPerson : ILocaleAware {
        public string Initials { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => FirstName + " " + LastName; }
        public IAddress BillingAddress { get; set; }
        public IAddress DeliveryAddress { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public DateTime? BirthDate { get; set; }
        public String PersonalIdNumber { get; set; }
    }
}