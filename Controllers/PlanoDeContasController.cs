using final_my_finance.Data;
using final_my_finance.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace final_my_finance.Controllers
{
    public class PlanoDeContasController : Controller
    {
        private readonly ILogger<PlanoDeContasController> _logger;
        private readonly ApplicationDbContext _context;

        public PlanoDeContasController(ILogger<PlanoDeContasController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult PlanoDeContasList()
        {
            var planoDeContas = _context.PlanoConta.ToList();
            return View(planoDeContas);
        }  
        
        public IActionResult GraficoPlanoDeContas()
        {
            var planoDeContas = _context.PlanoConta.ToList();
            return View(planoDeContas);
        }

        [HttpPost]
        public ActionResult SalvarPlano([FromBody] PlanoConta plano)
        {
            try
            {
                var planoExistente = _context.PlanoConta.Find(plano.Id);
                if (planoExistente == null)
                {
                    _context.PlanoConta.Add(plano);
                }
                else
                {
                    planoExistente.Nome = plano.Nome;
                    planoExistente.Tipo = plano.Tipo;
                }

                _context.SaveChanges();
                return Json(new { success = true, message = "Plano de contas salvo com sucesso!" });
            }
            catch
            {
                return Json(new { success = false, message = "Ocorreu um erro ao salvar o plano de contas." });
            }
        }

        [HttpDelete]
        public ActionResult ExcluirPlano(int planoId)
        {
            try
            {
                var plano = _context.PlanoConta.Find(planoId);
                _context.PlanoConta.Remove(plano);

                _context.SaveChanges();

                return Json(new { success = true, message = "Plano de contas excluido com sucesso!" });
            }
            catch
            {
                return Json(new { success = false, message = "Ocorreu um erro ao excluir o plano de contas." });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
