using System;
using System.ComponentModel.DataAnnotations;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Apresentacao;

public record ListarEmprestimosViewModel(
    string Id,
    string Amigo,
    string Revista,
    DateTime DataEmprestimo,
    DateTime DataDevolucaoPrevista,
    DateTime? DataDevolucaoReal,
    string Status,
    bool EstaAtrasado
);

public record CadastrarEmprestimoViewModel(
    [Required(ErrorMessage = "O campo \"Amigo\" é obrigatório.")]
    string AmigoId,

    [Required(ErrorMessage = "O campo \"Revista\" é obrigatório.")]
    string RevistaId
);