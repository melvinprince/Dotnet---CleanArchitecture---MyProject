// File: src/Application/Common/Models/BorrowerDto.cs
namespace MyProject.Application.Common.Models
{
    public record BorrowerDto(
        Guid   Id,
        string FullName,
        string Email,
        string? PhoneNumber,
        DateTime DateRegistered
    );
}
