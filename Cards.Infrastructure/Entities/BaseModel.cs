namespace Cards.Infrastructure.Entities;

public class BaseModel
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid CreatedBy{ get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    public DateTime DeletedOn { get; set; }
    public Guid DeletedBy { get; set; }
}