using System;
using System.Collections.Generic;

namespace RepositoryEntity.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string SenhaHash { get; set; } = null!;

    public string SenhaSalt { get; set; } = null!;
}
