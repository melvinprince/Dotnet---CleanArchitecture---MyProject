// File: src/Application/Common/Models/AuthorDto.cs
namespace Application.Common.Models
{
    public record AuthorDto(
        Guid   Id,
        string FirstName,
        string LastName,
        DateTime? DateOfBirth,
        string? Biography
    );
}
