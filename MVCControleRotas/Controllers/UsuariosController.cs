using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCControleRotas.Data;
using Model;
using Model.Services;

namespace MVCControleRotas.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly MVCControleRotasContext _context;
        public static bool logado = false;
        private static bool _logTemporario = false;
        public static string userName;

        public UsuariosController(MVCControleRotasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (UsuariosController.logado == true)
                return View(await ConsultaService.GetUsuarios());
            else
            {
                TempData["error"] = "Faça login para utilizar do sistema";
                return RedirectToRoute(new { controller = "Usuarios", Action = "TelaLogin" });
            }
            
        }

        public async Task<IActionResult> TelaLogin()
        {
            if((await ConsultaService.GetUsuarios()).Count==0)
            {
                _logTemporario = true;
                TempData["error"] = "Nenhum usuário ainda cadastrado por favor cadastre um";
                return RedirectToRoute(new { controller = "Usuarios", Action = "Create" });
            }
            else
            {
                return View();
            }
        }
        
        public async Task<IActionResult> Login()
        {
            var login = Request.Form["userLogin"];
            var senha = Request.Form["userSenha"];
            var usuario = await ConsultaService.GetUsuario(login,senha);
            if (usuario == null)
            {

                TempData["error"] = "Usuario ou senha incorreto";
                return RedirectToRoute(new { controller = "Usuarios", Action = "TelaLogin" });
            }
            logado = true;
            userName = login;
            return RedirectToRoute(new { controller = "Home", Action = "Index" });
        }

        public IActionResult Logout()
        {
            logado = false;
            userName = "";
            return RedirectToRoute(new { controller = "Usuarios", Action = "TelaLogin" });
        }

        public IActionResult EsqueceuSenha()
        {
            return View();
        }

        public async Task<IActionResult> EsqueceuSenhaReturn()
        {
            var login = Request.Form["LoginRequest"];
            var user = await ConsultaService.EsqueceuSenha(login);
            TempData["error"] = "Usuario: "+user.Login+"  \nSenha: "+user.Senha;
            return RedirectToRoute(new { controller = "Usuarios", Action = "TelaLogin" }); ;
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            if (UsuariosController.logado == true || _logTemporario == true)
                return View();
            else
            {
                TempData["error"] = "Faça login para utilizar do sistema";
                return RedirectToRoute(new { controller = "Usuarios", Action = "TelaLogin" });
            }
            
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _logTemporario = false;
                ConsultaService.CreateUsuario(usuario);
                if (logado == false)
                {
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                    return RedirectToRoute(new { controller = "Usuarios", Action = "TelaLogin" });
                }
                else
                    return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await ConsultaService.GetIdUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Login,Senha")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ConsultaService.UpdateUsuario(id,usuario);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await ConsultaService.GetIdUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            ConsultaService.DeleteUsuario(id);

            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(string id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
