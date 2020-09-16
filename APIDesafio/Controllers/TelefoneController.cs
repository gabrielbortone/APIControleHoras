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
    public class TelefoneController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public TelefoneController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtém o telefone pelo id do mesmo
        /// </summary>
        /// <param name="id">Identificador do telefone</param>
        /// <returns>Um telefone</returns>

        [HttpGet("{id}")]
        public IActionResult GetTelefone(int id)
        {
            var telefone = _unitOfWork.TelefoneRepository.GetById(id);

            if (telefone == null)
            {
                return NotFound();
            }

            return Ok(telefone);
        }

        /// <summary>
        /// Obtém o telefone pelo id do desenvolvedor
        /// </summary>
        /// <param name="id_Desenvolvedor">Identificador do desenvolvedor</param>
        /// <returns>O endereço de um desenvolvedor</returns>
        // api/telefone/desenvolvedor/1
        [HttpGet("desenvolvedor/{id_Desenvolvedor}")]
        public IActionResult GetEnderecoByDesenvolvedor(string id_Desenvolvedor)
        {
            var telefone = _unitOfWork.TelefoneRepository.GetTelefoneByDesenvolvedor(id_Desenvolvedor);

            if (telefone == null)
            {
                return NotFound();
            }

            return Ok(telefone);
        }


        //  api/telefone
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST api/desenvolvedor
        ///     {
        ///      "Tipo" : "0 - para celular e 1 para fixo",
        ///      "NumeroTelefone": "22999527345"
        ///     }
        /// </remarks>
        [HttpPost]
        public IActionResult Post(Telefone telefone)
        {
            var dev = _unitOfWork.DesenvolvedorRepository.GetDesenvolvedorById(telefone.DesenvolvedorId);
            
            if (dev == null)
            {
                return NotFound("Desenvolvedor não encontrado!");
            }

            telefone.Desenvolvedor = dev;

            _unitOfWork.TelefoneRepository.Add(telefone);
            _unitOfWork.Commit();
            return CreatedAtAction("GetTelefone", new { id = telefone.TelefoneId }, telefone);
        }

        /// <summary>
        /// Atualiza um telefone pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="telefone"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, Telefone telefone)
        {
            if (id != telefone.TelefoneId)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.TelefoneRepository.Update(telefone);
                _unitOfWork.Commit();
                return Ok(telefone);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (telefone == null)
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

        //  api/telefone/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var telefone = _unitOfWork.TelefoneRepository.GetById(id);
            if (telefone == null)
            {
                return NotFound();
            }

            _unitOfWork.TelefoneRepository.Delete(telefone);
            _unitOfWork.Commit();
            return Ok(telefone);
        }
    }
}
