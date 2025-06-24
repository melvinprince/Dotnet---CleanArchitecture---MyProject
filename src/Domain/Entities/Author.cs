using System;

namespace Domain.Entities
{
    public class Author
    {
        public Guid Id { get;  set; } // Unique ID for the author
        public required string FirstName { get;  set; }
        public required string LastName { get;  set; }
        public DateTime? DateOfBirth { get;  set; }
        public string? Biography { get;  set; }

        // Default constructor for EF Core and serialization
        public Author()
        {
        }

        // Constructor with required fields
        public Author(Guid id, string firstName, string lastName, DateTime? dateOfBirth = null, string? biography = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Biography = biography;
        }

        // Helper method to get full name
        public string FullName => $"{FirstName} {LastName}";
    }
}
