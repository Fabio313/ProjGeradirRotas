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
    public class CidadesController : Controller
    {
        private readonly MVCControleRotasContext _context;

        public CidadesController(MVCControleRotasContext context)
        {
            _context = context;
        }

        // GET: Cidades
        public async Task<IActionResult> Index()
        {
            return View(await ConsultaService.GetCidades());
        }

        // GET: Cidades/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await ConsultaService.GetIdCidades(id);
            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        // GET: Cidades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,UF")] Cidade cidade)
        {
            var teste = Request.Form["equipe"].ToList();

            if (ModelState.IsValid)
            {
                ConsultaService.CreateCidade(cidade);
                foreach (var equipe in teste)
                {
                    var equipeobj = await ConsultaService.GetIdEquipe(equipe);
                    if (equipeobj != null)
                        ConsultaService.UpdateEquipes(equipe, new Equipe() { Id = equipeobj.Id,
                                                                             Nome = equipeobj.Nome,
                                                                             Cidade = cidade });
                }

                return RedirectToAction(nameof(Index));
            }
            return View(cidade);
        }

        // GET: Cidades/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await ConsultaService.GetIdCidades(id);
            if (cidade.Nome == null)
            {
                return NotFound();
            }
            return View(cidade);
        }

        // POST: Cidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nome,UF")] Cidade cidade)
        {
            if (id != cidade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ConsultaService.UpdateCidades(id,cidade);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CidadeExists(cidade.Id))
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
            return View(cidade);
        }

        // GET: Cidades/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await ConsultaService.GetIdCidades(id);
            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        // POST: Cidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cidade = await ConsultaService.GetIdCidades(id);
            ConsultaService.DeleteCidades(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CidadeExists(string id)
        {
            return _context.Cidade.Any(e => e.Id == id);
        }
    }
}
