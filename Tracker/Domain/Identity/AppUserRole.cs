namespace Domain.Identity;
public class AppUserRole
{
    /// <summary>
    /// Gets or sets the primary key of the user that is linked to a role.
    /// </summary>
    public virtual Guid UserId { get; set; } = default!;

    public virtual AppUser User { get; set; } = default!;

    /// <summary>
    /// Gets or sets the primary key of the role that is linked to the user.
    /// </summary>
    public virtual Guid RoleId { get; set; } = default!;

    public virtual AppRole Role { get; set; } = default!;
}
