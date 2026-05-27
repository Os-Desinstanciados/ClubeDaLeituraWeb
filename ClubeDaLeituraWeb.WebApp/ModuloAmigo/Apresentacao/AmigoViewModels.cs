using System.ComponentModel.DataAnnotations;

namespace ClubeDaLeituraWeb.WebApp.ModuloAmigo.Apresentacao;

public record ListarAmigosViewModel(
    string Id,
    string Nome,
    string NomeResponsavel,
    string Telefone
);

public record CadastrarAmigoViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,
    
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome do Responável\" deve conter entre 3 e 100 caracteres.")]
    string NomeResponsavel,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [StringLength(11, MinimumLength = 10, ErrorMessage = "O campo \"Telefone\" deve conter entre 10 e 11 caracteres.")]
    string Telefone
);

public record EditarAmigoViewModel(
    string Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,
    
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome do Responável\" deve conter entre 3 e 100 caracteres.")]
    string NomeResponsavel,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [StringLength(11, MinimumLength = 10, ErrorMessage = "O campo \"Telefone\" deve conter entre 10 e 11 caracteres.")]
    string Telefone
);

public record ExcluirAmigoViewModel(
    string Id,
    string Nome,
    string NomeResponsavel,
    string Telefone
);
