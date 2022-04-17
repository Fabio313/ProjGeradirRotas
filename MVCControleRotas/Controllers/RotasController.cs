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
    public class RotasController : Controller
    {
        private readonly MVCControleRotasContext _context;

        public RotasController(MVCControleRotasContext context)
        {
            _context = context;
        }

        // GET: Rotas
        public async Task<IActionResult> Index()
        {
            return View(LeitorArquivos.ReadExcel().OrderBy(rota=>rota.Cep));
        }

        // GET: Rotas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rotas = await _context.Rotas
                .FirstOrDefaultAsync(m => m.Data == id);
            if (rotas == null)
            {
                return NotFound();
            }

            return View(rotas);
        }

        // GET: Rotas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Data,Stats,Auditado,CopReverteu,Log,Pdf,Foto,Contrato,Wo,Os,Assinante,Tecnicos,Login,Matricula,Cop,UltimoAlterar,Local,PontoCasaApt,Cidade,Base,Horario,Segmento,Servico,TipoServico,TipoOs,GrupoServico,Endereco,Numero,Complemento,Cep,Node,Bairro,Pacote,Cod,Telefone1,Telefone2,Obs,ObsTecnico,Equipamento")] Rotas rotas)
        {
            var teste = Request.Form["equipesRota"].ToList();
            if (ModelState.IsValid)
            {
                List<Equipe> equipes = new();
                foreach(var equipe in teste)
                {
                    equipes.Add(await ConsultaService.GetIdEquipe(equipe));
                }
                List<Rotas> AllRotas = LeitorArquivos.ReadExcel();
                EscritorArquivos.EscreveDocx(equipes,(List<Rotas>)AllRotas.OrderBy(rota => rota.Cep));

                return RedirectToAction(nameof(Index));
            }
            return View(rotas);
        }

        // GET: Rotas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rotas = await _context.Rotas.FindAsync(id);
            if (rotas == null)
            {
                return NotFound();
            }
            return View(rotas);
        }

        // POST: Rotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Data,Stats,Auditado,CopReverteu,Log,Pdf,Foto,Contrato,Wo,Os,Assinante,Tecnicos,Login,Matricula,Cop,UltimoAlterar,Local,PontoCasaApt,Cidade,Base,Horario,Segmento,Servico,TipoServico,TipoOs,GrupoServico,Endereco,Numero,Complemento,Cep,Node,Bairro,Pacote,Cod,Telefone1,Telefone2,Obs,ObsTecnico,Equipamento")] Rotas rotas)
        {
            if (id != rotas.Data)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rotas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RotasExists(rotas.Data))
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
            return View(rotas);
        }

        // GET: Rotas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rotas = await _context.Rotas
                .FirstOrDefaultAsync(m => m.Data == id);
            if (rotas == null)
            {
                return NotFound();
            }

            return View(rotas);
        }

        // POST: Rotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var rotas = await _context.Rotas.FindAsync(id);
            _context.Rotas.Remove(rotas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RotasExists(string id)
        {
            return _context.Rotas.Any(e => e.Data == id);
        }
    }
}
