using System;
using System.Collections.Generic;

namespace RepositoryEntity.Models;

public partial class Historico
{
    public int IdHistorico { get; set; }

    public DateTime DtTransacao { get; set; }

    public int IdTipoTransacao { get; set; }

    public decimal Valor { get; set; }

    public int IdConta { get; set; }

    public virtual Contum IdContaNavigation { get; set; } = null!;

    public virtual TipoTransacao IdTipoTransacaoNavigation { get; set; } = null!;
}
