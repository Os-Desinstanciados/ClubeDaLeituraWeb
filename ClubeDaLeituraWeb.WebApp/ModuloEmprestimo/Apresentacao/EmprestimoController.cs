using Microsoft.AspNetCore.Mvc;

namespace ClubeDaLeituraWeb.WebApp.ModuloEmprestimo.Apresentacao;

public class EmprestimoController : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarEmprestimosViewModel> emprestimos = new();

        return View(emprestimos);
    }
}