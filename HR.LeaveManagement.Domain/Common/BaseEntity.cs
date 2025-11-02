namespace HR.LeaveManagement.Domain.Common;

public abstract class BaseEntity
{
    protected BaseEntity()
    {            
    }

    public int Id { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
}
