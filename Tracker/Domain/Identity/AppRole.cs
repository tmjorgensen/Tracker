namespace Domain.Identity;
public class AppRole
{
    /// <summary>
    /// Gets or sets the primary key for this role.
    /// </summary>
    public virtual Guid Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the name for this role.
    /// </summary>
    public virtual string? Name { get; set; }

    /// <summary>
    /// Gets or sets the normalized name for this role.
    /// </summary>
    public virtual string? NormalizedName { get; set; }

    /// <summary>
    /// A random value that should change whenever a role is persisted to the store
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; }
    
    public virtual ICollection<AppRoleClaim> Claims { get; set; } = [];
    public virtual ICollection<AppUserRole> Users { get; set; } = [];

    /// <summary>
    /// Returns the name of the role.
    /// </summary>
    /// <returns>The name of the role.</returns>
    public override string ToString()
    {
        return Name ?? string.Empty;
    }
}
