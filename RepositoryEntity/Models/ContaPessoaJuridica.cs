using System;
using System.Collections.Generic;

namespace RepositoryEntity.Models;

public partial class ContaPessoaJuridica
{
    public int IdContaPj { get; set; }

    public string Cnpj { get; set; } = null!;

    public string RazaoSocial { get; set; } = null!;

    public string? NomeFantasia { get; set; }

    public decimal ValorInicial { get; set; }

    public decimal FaturamentoMedio { get; set; }

    public int IdConta { get; set; }

    public virtual Contum IdContaNavigation { get; set; } = null!;
}
