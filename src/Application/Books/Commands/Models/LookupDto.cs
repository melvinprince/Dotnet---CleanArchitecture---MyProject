using Domain.Entities;

namespace Application.Common.Models;

public class LookupDto
{
    public int Id { get; init; }

    public string? Title { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Book, LookupDto>();
            CreateMap<Author, LookupDto>();
            CreateMap<Borrower, LookupDto>();
        }
    }
}
