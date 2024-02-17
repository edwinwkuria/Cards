using Cards.Infrastructure.DataTypes;

namespace Cards.Services.DTOModels;

public class SearchDTO
{
        public SearchDTO(string? name, string? colour, CardStatus? status, DateTime? createdDate, int page, int size, int offset, int limit, string sortBy, string sortOrder)
        {
                Name = name;
                Colour = colour;
                Status = status;
                CreatedDate = createdDate;
                Page = page;
                Size = size;
                Offset = offset;
                Limit = limit;
                this.SortBy = sortBy;
                this.SortOrder = sortOrder;
        }
        public string? Name { get; set; }
        public string? Colour { get; set; }
        public CardStatus? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
}