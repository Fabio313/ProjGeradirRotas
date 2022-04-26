using System.Collections.Generic;
using APIUsuario.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace APIUsuario.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsuariosController : ControllerBase
	{
        private readonly UsuarioService _pessoaService;

        public UsuariosController(UsuarioService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet]
        public ActionResult<List<Usuario>> Get() =>
            _pessoaService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCliente")]
        public ActionResult<Usuario> Get(string id)
        {
            var cliente = _pessoaService.Get(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpGet("Busca")]
        public ActionResult<Usuario> GetUsuario(string login, string senha)
        {
            var cliente = _pessoaService.GetLogin(login,senha);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpGet("EsqueceuSenha")]
        public ActionResult<Usuario> EsqueceuSenha(string login)
        {
            var cliente = _pessoaService.ForgetPassword(login);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpPost]
        public IActionResult CreateAsync(Usuario pessoa)
        {
            _pessoaService.Create(pessoa);

            return CreatedAtRoute("GetCliente", new { id = pessoa.Id.ToString() }, pessoa);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Usuario personIn)
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
