using System;
using System.Collections.Generic;

namespace RepositoryEntity.Models;

public partial class TipoTransacao
{
    public int IdTipoTransacao { get; set; }

    public string Descricao { get; set; } = null!;

    public int Codigo { get; set; }

    public virtual ICollection<Historico> Historicos { get; set; } = new List<Historico>();
}
