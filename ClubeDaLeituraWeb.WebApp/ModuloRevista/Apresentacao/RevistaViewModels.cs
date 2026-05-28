using System.ComponentModel.DataAnnotations;

namespace ClubeDaLeituraWeb.WebApp.ModuloRevista.Apresentacao;

public record ListarRevistasViewModel(
    string Id,
    string Titulo,
    int NumeroEdicao,
    DateTime AnoPublicacao,
    string Caixa
);

public record CadastrarRevistaViewModel(
    [Required(ErrorMessage = "O campo \"Título\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "O campo \"Título\" deve conter entre 2 e 100 caracteres.")]
    string Titulo,

    [Range(1, int.MaxValue,
        ErrorMessage = "O campo \"Número da Edição\" deve ser positivo.")]
    int NumeroEdicao,

    [Required(ErrorMessage = "O campo \"Ano de Publicação\" deve ser preenchido.")]
    DateTime AnoPublicacao,

    [Required(ErrorMessage = "O campo \"Caixa\" deve ser selecionado.")]
    string CaixaId
);

public record EditarRevistaViewModel(
    string Id,

    [Required(ErrorMessage = "O campo \"Título\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "O campo \"Título\" deve conter entre 2 e 100 caracteres.")]
    string Titulo,

    [Range(1, int.MaxValue,
        ErrorMessage = "O campo \"Número da Edição\" deve ser positivo.")]
    int NumeroEdicao,

    [Required(ErrorMessage = "O campo \"Ano de Publicação\" deve ser preenchido.")]
    DateTime AnoPublicacao,

    [Required(ErrorMessage = "O campo \"Caixa\" deve ser selecionado.")]
    string CaixaId
);

public record ExcluirRevistaViewModel(
    string Id,
    string Titulo,
    int NumeroEdicao,
    DateTime AnoPublicacao,
    string Caixa
);