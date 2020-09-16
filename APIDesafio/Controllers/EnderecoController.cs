using ApiTeste.Models.ObjectTypes;
using ApiTeste.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIDesafio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnderecoController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public EnderecoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtem um endereco pelo seu id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna o endereco pelo seu identificador</returns>
        [HttpGet("{id}")]
        public IActionResult GetEndereco(int id)
        {
            var endereco = _unitOfWork.EnderecoRepository.GetById(id);

            if (endereco == null)
            {
                return NotFound();
            }

            return Ok(endereco);
        }

        /// <summary>
        /// Obtem um endereco pelo id do desenvolvedor
        /// </summary>
        /// <param name="id_Desenvolvedor"></param>
        /// <returns>Retorna o endereco pelo identificador do desenvolvedor</returns>
        [HttpGet("desenvolvedor/{id_Desenvolvedor}")]
        public IActionResult GetEnderecoByDesenvolvedor(string id_Desenvolvedor)
        {
            var endereco = _unitOfWork.EnderecoRepository.GetEnderecoByDesenvolvedor(id_Desenvolvedor);

            if (endereco == null)
            {
                return NotFound();
            }

            return Ok(endereco);
        }

        // api/endereco/
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST api/endereco
        ///     {
        ///      "Logradouro": "Rua dos Bobos",
        ///      "Numero": 0,
        ///      "CEP": "28950000",
        ///      "Bairro": "Centro",
        ///      "Cidade": "Rio de Janeiro",
        ///      "Estado": "Rio de Janeiro",
        ///      "País": "Brasil"
        ///     }
        /// </remarks>
        [HttpPost]
        public IActionResult Post(Endereco endereco)
        {
            var dev = _unitOfWork.DesenvolvedorRepository.GetDesenvolvedorById(endereco.DesenvolvedorId);
            
            if (dev == null)
            {
                return NotFound("Desenvolvedor não encontrado!");
            }

            endereco.Desenvolvedor = dev;

            _unitOfWork.EnderecoRepository.Add(endereco);
            _unitOfWork.Commit();

            return CreatedAtAction("GetEndereco", new { id = endereco.EnderecoId }, endereco);
        }

        /// <summary>
        /// Atualiza um endereço pelo id do projeto
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna o endereço modificado</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, Endereco endereco)
        {
            if (id != endereco.EnderecoId)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.EnderecoRepository.Update(endereco);
                _unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (endereco == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // api/endereco/delete/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var endereco = _unitOfWork.EnderecoRepository.GetById(id);
            if (endereco == null)
            {
                return NotFound();
            }

            _unitOfWork.EnderecoRepository.Delete(endereco);
            _unitOfWork.Commit();
            return Ok(endereco);
        }
    }
}
