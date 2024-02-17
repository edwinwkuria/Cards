namespace Cards.Services.DTOModels;

public class GetCardsDTO
{
    public GetCardsDTO(int offset, int limit, string sortBy, string sortOrder)
    {
        Offset = offset;
        Limit = limit;
        SortBy = sortBy;
        SortOrder = sortOrder;
    }
    
    public int Offset { get; set; }
    public int Limit { get; set; }
    public string SortBy { get; set; }
    public string SortOrder { get; set; }
}