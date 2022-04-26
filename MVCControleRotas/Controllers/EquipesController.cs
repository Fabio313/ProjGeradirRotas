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
    public class EquipesController : Controller
    {
        private readonly MVCControleRotasContext _context;

        public EquipesController(MVCControleRotasContext context)
        {
            _context = context;
        }

        
        public async Task<JsonResult> GetTimesCidade(string id)
        {
            var equipes = await ConsultaService.GetEquipesCidades(id);
            return Json(equipes);
        }

        // GET: Equipes
        public async Task<IActionResult> Index()
        {
            if (UsuariosController.logado == true)
                return View(await ConsultaService.GetEquipes());
            else
            {
                TempData["error"] = "Faça login para utilizar do sistema";
                return RedirectToRoute(new { controller = "Usuarios", Action = "TelaLogin" });
            }
        }

        // GET: Equipes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipe = await ConsultaService.GetIdEquipe(id);
            if (equipe == null)
            {
                return NotFound();
            }

            return View(equipe);
        }

        // GET: Equipes/Create
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

        // POST: Equipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Equipe equipe)
        {
            ViewBag.sucess = false;
            var teste = Request.Form["pessoa"].ToList();
            if (teste.Count == 0)
            {
                TempData["error"] = "A equipe deve ter ao menos 1 integrante";
                return View(equipe);
            }
            var cidade = Request.Form["cidadeEquipecreate"];
            if(cidade == "none")
                equipe.Cidade = null;
            else
                equipe.Cidade = await ConsultaService.GetIdCidades(cidade);
            if (ModelState.IsValid)
            {
                ConsultaService.CreateEquipe(equipe);
                foreach (var pessoa in teste)
                {
                    var pessoaobj = await ConsultaService.GetIdPessoa(pessoa);
                    if (pessoaobj != null)
                        ConsultaService.UpdatePessoas(pessoa, new Pessoa() { Id = pessoaobj.Id,
                                                                             Nome = pessoaobj.Nome, 
                                                                             Equipe = equipe });
                }

                return RedirectToAction(nameof(Index));
            }
            return View(equipe);
        }

        // GET: Equipes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipe = await ConsultaService.GetIdEquipe(id);
            if (equipe == null)
            {
                return NotFound();
            }
            return View(equipe);
        }

        // POST: Equipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nome")] Equipe equipe)
        {
            if (id != equipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var pessoasAdd = Request.Form["pessoaAdd"].ToList();
                    var pessoasDel = Request.Form["pessoaDel"].ToList();
                    if ((pessoasDel.Count-pessoasAdd.Count) == (await ConsultaService.GetPessoasTime(id)).Count)
                    {
                        TempData["error"] = "A equipe deve ter ao menos 1 integrante";
                        return View(equipe);
                    }
                    var cidade = Request.Form["cidadeEquipe"];
                    if (cidade == "none")
                        equipe.Cidade = null;
                    else
                        equipe.Cidade = await ConsultaService.GetIdCidades(cidade);

                    var equipebusca = await ConsultaService.GetIdEquipe(id);
                    ConsultaService.UpdateEquipes(id,equipe);
                    foreach (Pessoa pessoa in await ConsultaService.GetPessoasTime(id))
                    {
                        ConsultaService.UpdatePessoas(pessoa.Id, new Pessoa()
                        {
                            Id = pessoa.Id,
                            Nome = pessoa.Nome,
                            Equipe = equipe
                        });
                    }
                    
                    foreach (var pessoa in pessoasAdd)
                    {
                        var pessoaobj = await ConsultaService.GetIdPessoa(pessoa);
                        if (pessoaobj != null)
                            ConsultaService.UpdatePessoas(pessoa, new Pessoa()
                            {
                                Id = pessoaobj.Id,
                                Nome = pessoaobj.Nome,
                                Equipe = equipe
                            });
                    }

                    foreach (var pessoa in pessoasDel)
                    {
                        var pessoaobj = await ConsultaService.GetIdPessoa(pessoa);
                        if (pessoaobj != null)
                            ConsultaService.UpdatePessoas(pessoa, new Pessoa()
                            {
                                Id = pessoaobj.Id,
                                Nome = pessoaobj.Nome,
                                Equipe = null
                            });
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipeExists(equipe.Id))
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
            return View(equipe);
        }

        // GET: Equipes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipe = await ConsultaService.GetIdEquipe(id);
            if (equipe == null)
            {
                return NotFound();
            }

            return View(equipe);
        }

        // POST: Equipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var equipe = await ConsultaService.GetIdEquipe(id);
            ConsultaService.DeleteEquipes(id);
            foreach(Pessoa pessoa in await ConsultaService.GetPessoasTime(id))
            {
                ConsultaService.UpdatePessoas(pessoa.Id,new Pessoa() { Id=pessoa.Id,
                                                                       Nome=pessoa.Nome,
                                                                       Equipe=null});
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EquipeExists(string id)
        {
            return _context.Equipe.Any(e => e.Id == id);
        }
    }
}
