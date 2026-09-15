using GestaoConsultasUVV.Data;
using GestaoConsultasUVV.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestaoConsultasUVV.Controllers
{
    [Authorize] 
    public class ConsultaController : Controller
    {
        private readonly AppDbContext _context;

        public ConsultaController(AppDbContext context)
        {
            _context = context;
        }

        // Método auxiliar para pegar o ID do usuário logado
        private int ObterUsuarioLogadoId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        // 1. TELA PRINCIPAL (LISTAR MINHAS CONSULTAS)
        public IActionResult Index()
        {
            var usuarioId = ObterUsuarioLogadoId();
            var consultas = _context.Consultas
                .Where(c => c.UsuarioId == usuarioId)
                .OrderBy(c => c.DataHora)
                .ToList();

            return View(consultas);
        }

        // 2. TELA DE CADASTRAR CONSULTA
        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Consulta consulta)
        {
            consulta.UsuarioId = ObterUsuarioLogadoId();

            
            ModelState.Remove("Usuario");

            if (ModelState.IsValid)
            {
                _context.Consultas.Add(consulta);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(consulta);
        }

        // 3. TELA DE EDITAR
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var consulta = _context.Consultas.FirstOrDefault(c => c.Id == id && c.UsuarioId == ObterUsuarioLogadoId());
            if (consulta == null) return NotFound();

            return View(consulta);
        }

        [HttpPost]
        public IActionResult Editar(Consulta consulta)
        {
            consulta.UsuarioId = ObterUsuarioLogadoId();
            ModelState.Remove("Usuario");

            if (ModelState.IsValid)
            {
                _context.Consultas.Update(consulta);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(consulta);
        }

        // 4. TELA DE EXCLUIR
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var consulta = _context.Consultas.FirstOrDefault(c => c.Id == id && c.UsuarioId == ObterUsuarioLogadoId());
            if (consulta == null) return NotFound();

            return View(consulta);
        }

        [HttpPost, ActionName("Excluir")]
        public IActionResult ConfirmarExclusao(int id)
        {
            var consulta = _context.Consultas.FirstOrDefault(c => c.Id == id && c.UsuarioId == ObterUsuarioLogadoId());
            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}