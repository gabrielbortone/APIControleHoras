using ApiTeste.DTOs;
using ApiTeste.Models;
using ApiTeste.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ApiTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PontoController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public PontoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtem todas os pontos de horarios que os desenvolvedores trabalharam
        /// </summary>
        /// <returns>Retorna todos os controles de horários feito pelos programadores</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var controleHoras = _unitOfWork.PontoRepository.Get();
            if (controleHoras == null)
            {
                return NoContent();
            }
            var pontosDTO = _mapper.Map<List<PontoDTO>>(controleHoras);
            return Ok(pontosDTO);
        }

        /// <summary>
        /// Obtem um ponto pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna o ponto pelo seu identificador</returns>
        [HttpGet("{id}")]
        public IActionResult GetPonto(int id)
        {
            var controleHoras = _unitOfWork.PontoRepository.GetById(id);

            if (controleHoras == null)
            {
                return NotFound();
            }
            var pontoDTO = _mapper.Map<PontoDTO>(controleHoras);

            return Ok(pontoDTO);
        }

        /// <summary>
        /// Obtem um ponto pelo id do desenvolvedor
        /// </summary>
        /// <param name="id_Desenvolvedor"></param>
        /// <returns>Retorna o ponto pelo identificador do desenvolvedor</returns>
        [HttpGet("desenvolvedor/{id_Desenvolvedor}")]
        public IActionResult GetPontosByDesenvolvedor(string id_Desenvolvedor)
        {
            var controleHoras = _unitOfWork.PontoRepository.GetPontosByDesenvolvedor(id_Desenvolvedor);

            if (controleHoras == null)
            {
                return NotFound();
            }

            var pontosDTO = _mapper.Map<List<PontoDTO>>(controleHoras);

            return Ok(pontosDTO);
        }

        /// <summary>
        /// Obtem um ponto pelo id do projeto
        /// </summary>
        /// <param name="id_Projeto"></param>
        /// <returns>Retorna o ponto pelo identificador do projeto</returns>
        [HttpGet("projeto/{id_Projeto}")]
        public IActionResult GetPontosByProjeto(int id_Projeto)
        {
            var controleHoras = _unitOfWork.PontoRepository.GetPontosByProjeto(id_Projeto);

            if (controleHoras == null)
            {
                return NotFound();
            }

            var pontosDTO = _mapper.Map<List<PontoDTO>>(controleHoras);

            return Ok(pontosDTO);
        }

        // api/ponto
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST api/ponto
        ///     {
        ///     "InicioJornada":{
	    ///         "Ano": 2020,
	    ///         "Mes": 8,
	    ///         "Dia" 15,
        ///         "Hora" : 12,
        ///         "Minuto" :30
        ///     },
        ///     "FimJornada":{
        ///	        "Ano": 2020,
        ///	        "Mes": 8,
        ///	        "Dia" 15,
        ///         "Hora" : 18,
        ///         "Minuto" :30
        ///     },
        ///     "Id_Desenvolvedor": 1,
        ///     "Id_Projeto": 1
        ///     }
        /// </remarks>
        [HttpPost]
        public IActionResult Post(PontoDTO pontoDTO)
        {
            Projeto projeto = _unitOfWork.ProjetoRepository.GetById(pontoDTO.Id_Projeto);
            Desenvolvedor desenvolvedor = _unitOfWork.DesenvolvedorRepository.GetDesenvolvedorById(pontoDTO.Id_Desenvolvedor);

            if (projeto == null || desenvolvedor == null)
            {
                return BadRequest("Projeto ou desenvolvedor inexistentes");
            }

            var ponto = _mapper.Map<Ponto>(pontoDTO); ;

            try
            {
                _unitOfWork.PontoRepository.Add(ponto);
                _unitOfWork.Commit();
                return CreatedAtAction("GetPonto", new { id = projeto.ProjetoId }, projeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Atualiza um ponto pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna o ponto alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, Ponto ponto)
        {
            if (id != ponto.PontoId)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.PontoRepository.Update(ponto);
                _unitOfWork.Commit();
                var pontoDTO = _mapper.Map<PontoDTO>(ponto);
                return Ok(pontoDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ponto != null)
                {
                    return NoContent();
                }
                else
                {
                    throw;
                }
            }
        }

        // api/ponto/delete/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ponto = _unitOfWork.PontoRepository.GetById(id);
            if (ponto == null)
            {
                return NotFound();
            }

            _unitOfWork.PontoRepository.Delete(ponto);
            _unitOfWork.Commit();
            var pontoDTO = _mapper.Map<PontoDTO>(ponto);

            return Ok(pontoDTO);
        }
    }
}
