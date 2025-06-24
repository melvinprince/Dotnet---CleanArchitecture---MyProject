using System;

namespace Domain.Entities
{
    public class Borrower
    {
        public Guid Id { get;  set; }
        public required string FullName { get;  set; }
        public required string Email { get;  set; }
        public string? PhoneNumber { get;  set; }
        public DateTime DateRegistered { get;  set; }

        // Default constructor for EF and serialization
        public Borrower()
        {
        }

        // Constructor to set required fields
        public Borrower(Guid id, string fullName, string email, string? phoneNumber = null)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            DateRegistered = DateTime.UtcNow;
        }
    }
}
