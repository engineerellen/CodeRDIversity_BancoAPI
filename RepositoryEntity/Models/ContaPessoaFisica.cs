using System;
using System.Collections.Generic;

namespace RepositoryEntity.Models;

public partial class ContaPessoaFisica
{
    public int IdContaPf { get; set; }

    public string Cpf { get; set; } = null!;

    public string NomeCliente { get; set; } = null!;

    public string? Genero { get; set; }

    public string Endereco { get; set; } = null!;

    public string? Profissao { get; set; }

    public decimal RendaFamiliar { get; set; }

    public int IdConta { get; set; }

    public virtual Contum IdContaNavigation { get; set; } = null!;
}
