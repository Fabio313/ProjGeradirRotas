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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace MVCControleRotas.Controllers
{

    public class RotasController : Controller
    {
        private static List<List<string>> _rotaarquivo;
        private static string _filePath;
        public static List<string> _nomesColEnd = new List<string> {
"NUMERO",
"BAIRRO",
"COMPLEMENTO",
"CEP",
"CIDADE",
"SERVIÇO"};

        IWebHostEnvironment _appEnvironment;
        public RotasController(IWebHostEnvironment env)
        {
            _appEnvironment = env;
        }

        // GET: Rotas
        public async Task<IActionResult> Index(IFormFile pathFile)
        {
            if (pathFile != null)
            {
                if (pathFile.FileName.Contains(".xlsx"))
                {
                    _rotaarquivo = LeitorArquivos.ReadExcel(pathFile);
                }
                else
                {
                    TempData["error"] = "Tipo de arquivo não compativel";
                    return RedirectToRoute(new { controller = "Home", Action = "Index" });
                }
            }
            return View(_rotaarquivo[0]);
        }

        // GET: Rotas/Create
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

        // POST: Rotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rotas rotas)
        {
            var colunas = Request.Form["colRequeridas"].ToList();
            var cidade = Request.Form["cidadeRota"];
            var servico = Request.Form["servicoRota"];
            var equipes = Request.Form["equipesRota"].ToList();
            if (colunas.Count == 0)
            {
                TempData["error"] = "Nenhuma coluna foi selecionada";
                return RedirectToRoute(new { controller = "Rotas", Action = "Index" });
            }
            if (equipes.Count == 0)
            {
                TempData["error"] = "Nenhuma equipe foi selecionada";
                return RedirectToRoute(new { controller = "Rotas", Action = "Index" });
            }

            if (servico == "none")
            {
                TempData["error"] = "Nenhum serviço foi selecionado";
                return RedirectToRoute(new { controller = "Rotas", Action = "Index" });
            }

            if (ModelState.IsValid)
            {
                List<Equipe> equipeslist = new();
                foreach (var equipe in equipes)
                {
                    equipeslist.Add(await ConsultaService.GetIdEquipe(equipe));
                }
                var cidadeobj = await ConsultaService.GetIdCidades(cidade);
                try
                {
                    _filePath = EscritorArquivos.EscreveDocx(equipeslist, _rotaarquivo, cidadeobj, servico, colunas, _appEnvironment.WebRootPath);
                }
                catch
                {
                    TempData["error"] = "Algo ocorreu na criação do arquivo, verifique se esta faltando alguma coluna de importancia:" +
                                        "SERVIÇO,CIDADE ou alguma coluna relacionada ao endereço: BAIRRO,NUMERO,COMPLEMENTO ou CEP";
                    return RedirectToRoute(new { controller = "Home", Action = "Index" });
                }
                return RedirectToRoute(new { controller = "Rotas", Action = "Download" });
            }
            return View();
        }
        public IActionResult Download()
        {
            return View();
        }
        public IActionResult DownloadFile()
        {
            var fileName = _filePath.Split("//").ToList();
            var file = System.IO.File.ReadAllBytes(_filePath);
            return File(file, "application/octet-stream", fileName.Last().ToString());
        }
    }
}
