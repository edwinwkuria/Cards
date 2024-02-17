namespace Cards.API.Responses;

public class ErrorResponse
{
    public ErrorResponse(string error)
    {
        Error = error;
    }

    public string Error { get; set; }
}