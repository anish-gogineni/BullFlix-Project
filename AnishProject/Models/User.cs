using System;
using System.Collections.Generic;

namespace AnishProject.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Role { get; set; }
}
