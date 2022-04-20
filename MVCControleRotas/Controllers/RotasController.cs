﻿using System;
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

namespace MVCControleRotas.Controllers
{
    
    public class RotasController : Controller
    {
        private static List<List<string>> _rotaarquivo;
        private readonly MVCControleRotasContext _context;

        public RotasController(MVCControleRotasContext context)
        {
            _context = context;
        }

        // GET: Rotas
        public async Task<IActionResult> Index(IFormFile pathFile)
        {
            _rotaarquivo = LeitorArquivos.ReadExcel(pathFile); ;
            return View(_rotaarquivo[0]);
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
        public async Task<IActionResult> Create(IFormFile pathFile, [Bind("Data,Stats,Auditado,CopReverteu,Log,Pdf,Foto,Contrato,Wo,Os,Assinante,Tecnicos,Login,Matricula,Cop,UltimoAlterar,Local,PontoCasaApt,Cidade,Base,Horario,Segmento,Servico,TipoServico,TipoOs,GrupoServico,Endereco,Numero,Complemento,Cep,Node,Bairro,Pacote,Cod,Telefone1,Telefone2,Obs,ObsTecnico,Equipamento")] Rotas rotas)
        {
            var colunas = Request.Form["colRequeridas"].ToList();
            var cidade = Request.Form["cidadeRota"];
            var servico = Request.Form["servicoRota"];
            var equipes = Request.Form["equipesRota"].ToList();

            if (ModelState.IsValid)
            {
                List<Equipe> equipeslist = new();
                foreach(var equipe in equipes)
                {
                    equipeslist.Add(await ConsultaService.GetIdEquipe(equipe));
                }
                var cidadeobj = await ConsultaService.GetIdCidades(cidade);
                EscritorArquivos.EscreveDocx(equipeslist, _rotaarquivo, cidadeobj,servico,colunas);

                return View(Index(pathFile));
            }
            return View();
        }
    }
}
