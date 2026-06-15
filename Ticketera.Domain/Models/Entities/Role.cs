using System;
using System.Collections.Generic;
using Ticketera.Domain;

namespace Ticketera.Domain.Models.Entities;

public partial class Role
{
    public Guid RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
