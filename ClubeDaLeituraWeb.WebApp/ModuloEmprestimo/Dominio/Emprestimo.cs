using ClubeDaLeituraWeb.WebApp.Compartilhado.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;

public sealed class Emprestimo : EntidadeBase<Emprestimo>
{
    public Amigo? Amigo { get; set; }
    public Revista? Revista { get; set; }

    public DateTime DataEmprestimo { get; set; }
    public DateTime DataDevolucaoPrevista { get; set; }
    public DateTime? DataDevolucaoReal { get; set; }

    public StatusEmprestimo Status { get; set; }

    public Emprestimo() { }

    public Emprestimo(Amigo amigo, Revista revista)
    {
        Amigo = amigo;
        Revista = revista;

        DataEmprestimo = DateTime.Now;
        DataDevolucaoPrevista = DataEmprestimo.AddDays(revista.Caixa!.DiasDeEmprestimo);
        Status = StatusEmprestimo.Aberto;
    }

    public void RegistrarDevolucao()
    {
        DataDevolucaoReal = DateTime.Now;
        Status = StatusEmprestimo.Concluido;

        if (Revista != null)
            Revista.Status = StatusRevista.Disponivel;
    }

    public void AtualizarStatus()
    {
        if (Status == StatusEmprestimo.Concluido)
            return;

        Status = DateTime.Now.Date > DataDevolucaoPrevista.Date
            ? StatusEmprestimo.Atrasado
            : StatusEmprestimo.Aberto;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Amigo == null)
            erros.Add("O campo \"Amigo\" é obrigatório.");

        if (Revista == null)
            erros.Add("O campo \"Revista\" é obrigatório.");

        if (DataEmprestimo == DateTime.MinValue)
            erros.Add("O campo \"Data de Empréstimo\" é obrigatório.");

        if (DataDevolucaoPrevista == DateTime.MinValue)
            erros.Add("O campo \"Data de Devolução\" é obrigatório.");

        return erros;
    }

    public override void AtualizarDados(Emprestimo entidadeAtualizada)
    {
        Amigo = entidadeAtualizada.Amigo;
        Revista = entidadeAtualizada.Revista;
        DataEmprestimo = entidadeAtualizada.DataEmprestimo;
        DataDevolucaoPrevista = entidadeAtualizada.DataDevolucaoPrevista;
        DataDevolucaoReal = entidadeAtualizada.DataDevolucaoReal;
        Status = entidadeAtualizada.Status;
    }
}