using ApiTeste.DTOs;
using ApiTeste.Models;
using ApiTeste.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ApiTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjetoController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ProjetoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtem todos os projetos
        /// </summary>>
        /// <returns>Retorna todos os projetos</returns>

        [HttpGet]
        public IActionResult Get()
        {
            var projetos = _unitOfWork.ProjetoRepository.Get();
            if (projetos == null)
            {
                return NoContent();
            }
            var projetosDTO = _mapper.Map<List<ProjetoDTO>>(projetos);
            return Ok(projetosDTO);
        }

        /// <summary>
        /// Obtem um projeto pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna um projeto pelo seu identificador</returns>
        [HttpGet("{id}")]
        public IActionResult GetProjeto(int id)
        {
            var projeto = _unitOfWork.ProjetoRepository.GetById(id);

            if (projeto == null)
            {
                return NotFound();
            }

            var projetoDTO = _mapper.Map<PontoDTO>(projeto);

            return Ok(projetoDTO);
        }

        //api/projeto
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST api/projeto
        ///     {
        ///     "DescricaoCurta": "Descricao curta do projeto",
       ///      "DescricaoDetalhada":"Descricao com detalhes sobre o projeto",
       ///      "Objetivo":"Objetivos a serem alcançados pelo projeto",
       ///      "Recursos":"Recursos que serão usados para se realizar o projeto",
       ///      "PublicoAlvo": "A quem se destina o projeto",
       ///      "Setor": "Qual setor é o projeto, tipo industrial, tecnologico, etc",
       ///      "Fase": 0,
       ///      "DataInicio":{
       ///	        "Ano": 2020,
       ///	        "Mes": 8,
       ///	        "Dia": 15
       ///      }
        ///     }
        /// </remarks>
        [HttpPost]
        public IActionResult Post(ProjetoDTO projetoDTO)
        {
            var projeto = _mapper.Map<Projeto>(projetoDTO);

            try
            {
                _unitOfWork.ProjetoRepository.Add(projeto);
                _unitOfWork.Commit();
                return CreatedAtAction("GetProjeto", new { id = projeto.ProjetoId }, projeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Atualiza um projeto pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projetoVM"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProjetoDTO projetoDTO)
        {
            if (id != projetoDTO.ProjetoId)
            {
                return BadRequest();
            }

            try
            {
                var projeto = _mapper.Map<Projeto>(projetoDTO);
                
                _unitOfWork.ProjetoRepository.Update(projeto);
               
                _unitOfWork.Commit();

                return Ok(projetoDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (projetoDTO == null)
                {
                    return NoContent();
                }
                else
                {
                    throw;
                }
            }
        }
        // api/projeto/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var projeto = _unitOfWork.ProjetoRepository.GetById(id);
            if (projeto == null)
            {
                return NotFound();
            }

            _unitOfWork.ProjetoRepository.Delete(projeto);
            _unitOfWork.Commit();

            var projetoDTO = _mapper.Map<ProjetoDTO>(projeto);

            return Ok(projetoDTO);
        }
    }
}
