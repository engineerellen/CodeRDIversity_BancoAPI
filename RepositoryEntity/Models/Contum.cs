using System;
using System.Collections.Generic;

namespace RepositoryEntity.Models;

public partial class Contum
{
    public int IdConta { get; set; }

    public string Agencia { get; set; } = null!;

    public string NumeroConta { get; set; } = null!;

    public string? Digito { get; set; }

    public string? Pix { get; set; }

    public bool Ativo { get; set; }

    public decimal ValorConta { get; set; }

    public string NomeConta { get; set; } = null!;

    public int IdTipoConta { get; set; }

    public virtual ICollection<ContaPessoaFisica> ContaPessoaFisicas { get; set; } = new List<ContaPessoaFisica>();

    public virtual ICollection<ContaPessoaJuridica> ContaPessoaJuridicas { get; set; } = new List<ContaPessoaJuridica>();

    public virtual TipoContum IdTipoContaNavigation { get; set; } = null!;
}
