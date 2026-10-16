namespace Api.Modules.Shared.Domain;

public abstract class Entity
{
    public Guid Id { get; set;}
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }


    protected Entity()
    {
        Id = Guid.NewGuid();
        IsDeleted = false;
    }


    public void Delete()
        => IsDeleted = true;

}