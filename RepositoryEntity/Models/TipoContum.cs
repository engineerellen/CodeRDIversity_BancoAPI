using System;
using System.Collections.Generic;

namespace RepositoryEntity.Models;

public partial class TipoContum
{
    public int IdTipoConta { get; set; }

    public string? DescricaoTipo { get; set; }

    public int? Codigo { get; set; }

    public virtual ICollection<Contum> Conta { get; set; } = new List<Contum>();
}
