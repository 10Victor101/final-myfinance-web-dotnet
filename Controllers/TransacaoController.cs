using final_my_finance.Data;
using final_my_finance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace final_my_finance.Controllers
{
    public class TransacaoController : Controller
    {
        private readonly ILogger<TransacaoController> _logger;
        private readonly ApplicationDbContext _context;

        public TransacaoController(ILogger<TransacaoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult TransacaoList()
        {
            var transacoes = _context.Transacao.Include(t => t.PlanoConta).ToList();
            var planosContas = _context.PlanoConta.ToList();

            ViewBag.Transacoes = transacoes;
            ViewBag.PlanosContas = planosContas;

            return View();
        }

        [HttpPost]
        public ActionResult SalvarTransacao([FromBody] Transacao transacao)
        {
            try
            {
                var transacaoExistente = _context.Transacao.Find(transacao.Id);
                if (transacaoExistente == null)
                {
                    _context.Transacao.Add(transacao);
                }
                else
                {
                    transacaoExistente.Data = transacao.Data;
                    transacaoExistente.Valor = transacao.Valor;
                    transacaoExistente.Historico = transacao.Historico;
                    transacaoExistente.PlanoContaId = transacao.PlanoContaId;
                }

                _context.SaveChanges();
                return Json(new { success = true, message = "Transação salva com sucesso!" });
            }
            catch
            {
                return Json(new { success = false, message = "Ocorreu um erro ao salvar a transação." });
            }
        }

        [HttpDelete]
        public ActionResult ExcluirTransacao(int transacaoId)
        {
            try
            {
                var transacao = _context.Transacao.Find(transacaoId);
                _context.Transacao.Remove(transacao);

                _context.SaveChanges();

                return Json(new { success = true, message = "Transação excluido com sucesso!" });
            }
            catch
            {
                return Json(new { success = false, message = "Ocorreu um erro ao excluir a transação." });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
