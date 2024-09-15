namespace BankApplicationAPI.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public string EmployeeId { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
