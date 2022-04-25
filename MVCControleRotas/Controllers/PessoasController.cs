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
    public class PessoasController : Controller
    {
        private readonly MVCControleRotasContext _context;

        public PessoasController(MVCControleRotasContext context)
        {
            _context = context;
        }

        // GET: Pessoas
        public async Task<IActionResult> Index()
        {
            if (UsuariosController.logado == true)
                return View(await ConsultaService.GetPessoas());
            else
            {
                TempData["error"] = "Faça login para utilizar do sistema";
                return RedirectToRoute(new { controller = "Usuarios", Action = "TelaLogin" });
            }
            
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await ConsultaService.GetIdPessoa(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            if (UsuariosController.logado == true)
                return View();
            else
            {
                TempData["error"] = "Faça login para utilizar do sistema";
                return RedirectToRoute(new { controller = "Usuarios", Action = "TelaLogin" });
            }
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                ConsultaService.CreatePessoa(pessoa);
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await ConsultaService.GetIdPessoa(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nome")] Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var pessoabusca = await ConsultaService.GetIdPessoa(id);
                    pessoa.Equipe = pessoabusca.Equipe;
                    ConsultaService.UpdatePessoas(id,pessoa);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.Id))
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
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await ConsultaService.GetIdPessoa(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var pessoa = await ConsultaService.GetIdPessoa(id);
            ConsultaService.DeletePessoas(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(string id)
        {
            return _context.Pessoa.Any(e => e.Id == id);
        }
    }
}
