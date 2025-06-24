// File: src/Application/Common/Models/BookDto.cs
using System;
using Domain.Enums;

namespace Application.Common.Models
{
    public record BookDto(
        Guid       Id,
        string     Title,
        string     ISBN,
        DateTime   PublishedDate,
        Guid       AuthorId,
        BookStatus Status,
        Guid?      BorrowerId    // ← new optional FK
    );
}
