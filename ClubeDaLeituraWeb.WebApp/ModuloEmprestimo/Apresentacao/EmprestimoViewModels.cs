using System;
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