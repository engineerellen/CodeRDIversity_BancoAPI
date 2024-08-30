using RepositoryEntity.Models;
using System;
using System.Collections.Generic;

namespace RepositoryEntity;

public partial class Historico
{
    public int IdHistorico { get; set; }

    public DateTime DtTransacao { get; set; }

    public int IdTipoTransacao { get; set; }

    public decimal Valor { get; set; }

    public int IdConta { get; set; }

    public virtual TipoTransacao IdTipoTransacaoNavigation { get; set; } = null!;
}
