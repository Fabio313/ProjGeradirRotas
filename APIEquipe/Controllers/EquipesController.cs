using System.Collections.Generic;
using APIEquipe.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace APIEquipe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipesController : ControllerBase
    {
        private readonly EquipeService _equipeService;

        public EquipesController(EquipeService pessoaService)
        {
            _equipeService = pessoaService;
        }

        [HttpGet]
        public ActionResult<List<Equipe>> Get() =>
            _equipeService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCliente")]
        public ActionResult<Equipe> Get(string id)
        {
            var cliente = _equipeService.Get(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpPost]
        public IActionResult CreateAsync(Equipe pessoa)
        {
            _equipeService.Create(pessoa);

            return CreatedAtRoute("GetCliente", new { id = pessoa.Id.ToString() }, pessoa);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Equipe personIn)
        {
            var cliente = _equipeService.Get(id);

            if (cliente == null)
            {
                return NotFound();
            }
            _equipeService.Update(id, personIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var person = _equipeService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            _equipeService.Remove(person.Id);

            return NoContent();
        }
    }
}
