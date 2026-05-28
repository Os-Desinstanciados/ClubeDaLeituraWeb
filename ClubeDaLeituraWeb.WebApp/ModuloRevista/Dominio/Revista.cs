using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloCaixa.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;

public sealed class Revista : EntidadeBase<Revista>
{
    public string Titulo { get; set; } = string.Empty;
    public int NumeroEdicao { get; set; }
    public DateTime AnoPublicacao { get; set; }
    public Caixa? Caixa { get; set; }

    public Revista() { }

    public Revista(string titulo, int numeroEdicao, DateTime anoPublicacao, Caixa caixa)
    {
        Titulo = titulo;
        NumeroEdicao = numeroEdicao;
        AnoPublicacao = anoPublicacao;
        Caixa = caixa;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Titulo))
            erros.Add("O campo \"Título\" deve ser preenchido.");
        else if (Titulo.Length < 2 || Titulo.Length > 100)
            erros.Add("O campo \"Título\" deve conter entre 2 e 100 caracteres.");

        if (NumeroEdicao <= 0)
            erros.Add("O campo \"Número da Edição\" deve ser positivo.");

        if (AnoPublicacao == DateTime.MinValue)
            erros.Add("O campo \"Ano de Publicação\" deve ser preenchido.");

        if (Caixa == null)
            erros.Add("O campo \"Caixa\" deve ser selecionado.");

        return erros;
    }

    public override void AtualizarDados(Revista entidadeAtualizada)
    {
        Titulo = entidadeAtualizada.Titulo;
        NumeroEdicao = entidadeAtualizada.NumeroEdicao;
        AnoPublicacao = entidadeAtualizada.AnoPublicacao;
        Caixa = entidadeAtualizada.Caixa;
    }
}