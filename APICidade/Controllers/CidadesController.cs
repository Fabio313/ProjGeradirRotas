using System.Collections.Generic;
using APICidade.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace APICidade.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadesController : ControllerBase
    {
        private readonly CidadeService _cidadeService;

        public CidadesController(CidadeService pessoaService)
        {
            _cidadeService = pessoaService;
        }

        [HttpGet]
        public ActionResult<List<Cidade>> Get() =>
            _cidadeService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCliente")]
        public ActionResult<Cidade> Get(string id)
        {
            var cliente = _cidadeService.Get(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpPost]
        public IActionResult CreateAsync(Cidade pessoa)
        {
            _cidadeService.Create(pessoa);

            return CreatedAtRoute("GetCliente", new { id = pessoa.Id.ToString() }, pessoa);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Cidade personIn)
        {
            var cliente = _cidadeService.Get(id);

            if (cliente == null)
            {
                return NotFound();
            }
            _cidadeService.Update(id, personIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var person = _cidadeService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            _cidadeService.Remove(person.Id);

            return NoContent();
        }
    }
}
