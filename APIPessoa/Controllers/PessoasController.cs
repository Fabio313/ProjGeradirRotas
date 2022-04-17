using System.Collections.Generic;
using System.Threading.Tasks;
using APIPessoa.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace APIPessoa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly PessoaService _pessoaService;

        public PessoasController(PessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet]
        public ActionResult<List<Pessoa>> Get() =>
            _pessoaService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCliente")]
        public ActionResult<Pessoa> Get(string id)
        {
            var cliente = _pessoaService.Get(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpGet("Disponiveis")]
        public ActionResult<List<Pessoa>> GetDisponiveis()
        {
            var cliente = _pessoaService.GetDisponiveis();

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpGet("PessoasTime")]
        public ActionResult<List<Pessoa>> GetPessoasTime(string idtime)
        {
            var cliente = _pessoaService.GetPessoasTime(idtime);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpPost]
        public IActionResult CreateAsync(Pessoa pessoa)
        {
            _pessoaService.Create(pessoa);

            return CreatedAtRoute("GetCliente", new { id = pessoa.Id.ToString() }, pessoa);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Pessoa personIn)
        {
            var cliente = _pessoaService.Get(id);

            if (cliente == null)
            {
                return NotFound();
            }
            _pessoaService.Update(id, personIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var person = _pessoaService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            _pessoaService.Remove(person.Id);

            return NoContent();
        }
    }
}
