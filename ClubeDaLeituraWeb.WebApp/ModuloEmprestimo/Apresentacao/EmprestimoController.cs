using ClubeDaLeituraWeb.WebApp.ModuloAmigo.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Dominio;
using ClubeDaLeituraWeb.WebApp.ModuloRevista.Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Apresentacao;

public class EmprestimoController : Controller
{
    private readonly IRepositorioEmprestimo repositorioEmprestimo;
    private readonly IRepositorioAmigo repositorioAmigo;
    private readonly IRepositorioRevista repositorioRevista;

    public EmprestimoController(
    IRepositorioEmprestimo repositorioEmprestimo,
    IRepositorioAmigo repositorioAmigo,
    IRepositorioRevista repositorioRevista)
    {
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Emprestimo> emprestimos = repositorioEmprestimo.SelecionarTodos();

        List<ListarEmprestimosViewModel> listarVms = new();

        foreach (Emprestimo e in emprestimos)
        {
            e.AtualizarStatus();

            ListarEmprestimosViewModel vm = new(
                e.Id,
                e.Amigo!.Nome,
                e.Revista!.Titulo,
                e.DataEmprestimo,
                e.DataDevolucaoPrevista,
                e.DataDevolucaoReal,
                e.Status.ToString(),
                e.Status == StatusEmprestimo.Atrasado
            );

            listarVms.Add(vm);
        }

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CarregarAmigos();
        CarregarRevistasDisponiveis();

        CadastrarEmprestimoViewModel vm = new(string.Empty, string.Empty);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarEmprestimoViewModel vm)
    {
        Amigo? amigoSelecionado = repositorioAmigo.SelecionarPorId(vm.AmigoId);
        Revista? revistaSelecionada = repositorioRevista.SelecionarPorId(vm.RevistaId);

        if (amigoSelecionado == null)
            ModelState.AddModelError("AmigoId", "Selecione um amigo válido.");

        if (revistaSelecionada == null)
            ModelState.AddModelError("RevistaId", "Selecione uma revista válida.");

        bool amigoTemEmprestimoAberto = repositorioEmprestimo.SelecionarTodos()
            .Any(e => e.Amigo!.Id == vm.AmigoId && e.Status != StatusEmprestimo.Concluido);

        if (amigoTemEmprestimoAberto)
            ModelState.AddModelError("AmigoId", "Este amigo já possui um empréstimo ativo.");

        if (revistaSelecionada != null && revistaSelecionada.Status != StatusRevista.Disponivel)
            ModelState.AddModelError("RevistaId", "Esta revista não está disponível para empréstimo.");

        if (!ModelState.IsValid)
        {
            CarregarAmigos();
            CarregarRevistasDisponiveis();

            return View(vm);
        }

        Emprestimo novoEmprestimo = new(amigoSelecionado!, revistaSelecionada!);

        revistaSelecionada!.Status = StatusRevista.Emprestada;

        repositorioEmprestimo.Cadastrar(novoEmprestimo);
        repositorioRevista.Editar(revistaSelecionada.Id, revistaSelecionada);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Devolver(string id)
    {
        Emprestimo? emprestimo = repositorioEmprestimo.SelecionarPorId(id);

        if (emprestimo == null)
            return RedirectToAction(nameof(Listar));

        DevolverEmprestimoViewModel vm = new(
            emprestimo.Id,
            emprestimo.Amigo!.Nome,
            emprestimo.Revista!.Titulo,
            emprestimo.DataEmprestimo,
            emprestimo.DataDevolucaoPrevista
        );

        return View(vm);
    }

    [HttpPost]
    public ActionResult Devolver(DevolverEmprestimoViewModel vm)
    {
        Emprestimo? emprestimo = repositorioEmprestimo.SelecionarPorId(vm.Id);

        if (emprestimo == null)
            return RedirectToAction(nameof(Listar));

        emprestimo.RegistrarDevolucao();

        repositorioEmprestimo.Editar(emprestimo.Id, emprestimo);

        if (emprestimo.Revista != null)
            repositorioRevista.Editar(emprestimo.Revista.Id, emprestimo.Revista);

        return RedirectToAction(nameof(Listar));
    }

    private void CarregarAmigos()
    {
        var amigos = repositorioAmigo.SelecionarTodos();

        ViewBag.TotalAmigos = amigos.Count;

        ViewBag.Amigos = amigos
            .Select(a => new SelectListItem(a.Nome, a.Id))
            .ToList();
    }

    private void CarregarRevistasDisponiveis()
    {
        var revistas = repositorioRevista.SelecionarTodos();

        ViewBag.TotalRevistas = revistas.Count;

        ViewBag.Revistas = revistas
            .Select(r => new SelectListItem($"{r.Titulo} - Edição {r.NumeroEdicao} - {r.Status}", r.Id))
            .ToList();
    }
}