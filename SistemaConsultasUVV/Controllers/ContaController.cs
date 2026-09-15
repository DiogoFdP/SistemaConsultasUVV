using GestaoConsultasUVV.Data;
using GestaoConsultasUVV.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestaoConsultasUVV.Controllers
{
    public class ContaController : Controller
    {
        private readonly AppDbContext _context;

        // Injeção de dependência do banco de dados
        public ContaController(AppDbContext context)
        {
            _context = context;
        }

        // --- TELA DE CADASTRO ---
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Verifica se já existe alguém com esse e-mail
                if (_context.Usuarios.Any(u => u.Email == usuario.Email))
                {
                    ModelState.AddModelError("Email", "Este e-mail já está em uso.");
                    return View(usuario);
                }

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(usuario);
        }

        // --- TELA DE LOGIN ---
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);

            if (usuario == null)
            {
                ViewBag.Erro = "E-mail ou senha inválidos!";
                return View();
            }

            // Cria o "Crachá" do usuário logado (Cookie)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var identidade = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identidade);

            await HttpContext.SignInAsync("Cookies", principal);

            // Redireciona para as consultas após logar
            return RedirectToAction("Index", "Consulta");
        }

        // --- SAIR (LOGOUT) ---
        public async Task<IActionResult> Sair()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Login");
        }
    }
}