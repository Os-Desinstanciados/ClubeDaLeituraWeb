using System.ComponentModel.DataAnnotations;
using ClubeDaLeituraWeb.WebApp.ModuloCaixa.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloCaixa.Apresentacao;

public record ListarCaixasViewModel(
    string Id,
    string Etiqueta,
    CorCaixa Cor,
    int DiasDeEmprestimo
)
{    
    public string ClasseCorBootstrap => Cor switch
    {
        CorCaixa.Azul => "text-primary",
        CorCaixa.Verde => "text-success",
        CorCaixa.Vermelho => "text-danger",
        CorCaixa.Amarelo => "text-warning",
        CorCaixa.Roxo => "text-info",
        _ => "text-dark"
    };
}


public record CadastrarCaixaViewModel(
    [Required(ErrorMessage = "O campo \"Etiqueta\" deve ser preenchido.")]
    [StringLength(50, ErrorMessage = "O campo \"Etiqueta\" deve conter no máximo 50 caracteres.")]
    string Etiqueta,

    [Required(ErrorMessage = "O campo \"Cor\" deve ser preenchido.")]
    CorCaixa Cor,

    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Dias de Empréstimo\" deve conter um valor maior que 0.")]
    int DiasDeEmprestimo
);

public record EditarCaixaViewModel(
    string Id,

    [Required(ErrorMessage = "O campo \"Etiqueta\" deve ser preenchido.")]
    [StringLength(50, ErrorMessage = "O campo \"Etiqueta\" deve conter no máximo 50 caracteres.")]
    string Etiqueta,

    [Required(ErrorMessage = "O campo \"Cor\" deve ser preenchido.")]
    CorCaixa Cor,

    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Dias de Empréstimo\" deve conter um valor maior que 0.")]
    int DiasDeEmprestimo
);

public record ExcluirCaixaViewModel(
    string Id,
    string Etiqueta,
    CorCaixa Cor,
    int DiasDeEmprestimo
);
