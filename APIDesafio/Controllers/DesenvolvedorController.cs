using APIDesafio.DTOs;
using ApiTeste.DTOs;
using ApiTeste.Models;
using ApiTeste.Models.ObjectTypes;
using ApiTeste.Repository.Interfaces;
using ApiTeste.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DesenvolvedorController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private UserManager<Desenvolvedor> _userManager;
        private SignInManager<Desenvolvedor> _signInManager;
        private DesenvolvedorService _desenvolvedorService;
        private IMapper _mapper;

        public DesenvolvedorController(IUnitOfWork unitOfWork, UserManager<Desenvolvedor> userManager,
            SignInManager<Desenvolvedor> signInManager, DesenvolvedorService desenvolvedorService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _desenvolvedorService = desenvolvedorService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtem todos os desenvolvedores cadastrados
        /// </summary>
        /// <returns>Retorna todos desenvolvedores</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var desenvolvedores = _unitOfWork.DesenvolvedorRepository.Get();
            
            if(desenvolvedores == null)
            {
                return NoContent();
            }

            var DesenvolvedoresLista = _mapper.Map<List<DesenvolvedorResumoDTO>>(desenvolvedores);

            return Ok(DesenvolvedoresLista);
        }

        /// <summary>
        /// Obtem um os cincos desenvolvedores que mais trabalharam
        /// </summary>
        /// <returns>Retorna o top 5 desenvolvedores que ultrapassaram a media</returns>
        [HttpGet("GetRacking")]
        public IActionResult GetRacking([FromBody]DataCompletaDTO dataInicial)
        {
            try
            {
                DateTime datainicio = _mapper.Map<DateTime>(dataInicial);
                _desenvolvedorService.ObterTotalSemanal(datainicio);
            }
            
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            var desenvolvedores = _desenvolvedorService.GetDesenvolvedoresByRacking();
            
            if (desenvolvedores == null)
            {
                return NoContent();
            }

            var DesenvolvedoresLista = _mapper.Map<List<DesenvolvedorResumoDTO>>(desenvolvedores);

            return Ok(DesenvolvedoresLista);
        }

        /// <summary>
        /// Obtem um desenvolvedor pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna o desenvolvedor pelo seu identificador</returns>
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var desenvolvedor = _unitOfWork.DesenvolvedorRepository.GetDesenvolvedorById(id);

            if (desenvolvedor == null)
            {
                return NotFound();
            }

            DesenvolvedorResumoDTO desenvolvedorResumoVM = new DesenvolvedorResumoDTO(desenvolvedor.Nome, desenvolvedor.Sobrenome,
                desenvolvedor.LinkedinUrl, desenvolvedor.GithubUrl);

            return Ok(desenvolvedorResumoVM);
        }

        // api/desenvolvedor/
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST api/desenvolvedor
        ///     {
        ///     "Nome": "John",
        ///      "Sobrenome":"Doe",
        ///      "CPF":"12345678911",
        ///      "Email":"newgabrielbortone@gmail.com",
        ///      "Password": "Senh@121416",
        ///      "ConfirmPassword": "Senh@121416"
        ///      "LinkedinUrl": "https://www.linkedin.com/in/gabriel-bortone-368564169/",
        ///      "GithubUrl": "https://www.linkedin.com/in/gabriel-bortone-368564169/",
        ///      "DataContratacao":{
        ///	        "Ano": 2020,
        ///	        "Mes": 8,
        ///	        "Dia" 15
        ///      },
        ///      "Logradouro": "Rua dos Bobos",
        ///      "Numero": 0,
        ///      "CEP": "28950000",
        ///      "Bairro": "Centro",
        ///      "Cidade": "Rio de Janeiro",
        ///      "Estado": "Rio de Janeiro",
        ///      "País": "Brasil",
        ///      "Tipo" : "0 - para celular e 1 para fixo",
        ///      "NumeroTelefone": "22999527345"
        ///     }
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DesenvolvedorDTO desenvolvedorDTO)
        {
            var aux = _unitOfWork.DesenvolvedorRepository.GetDesenvolvedorByCPF(desenvolvedorDTO.CPF);
            if (aux != null)
            {
                return Conflict("Já existe um desenvolvedor com esse CPF");
            }
            try
            {
                Endereco endereco = new Endereco(desenvolvedorDTO.Logradouro, desenvolvedorDTO.Numero,
                    desenvolvedorDTO.CEP, desenvolvedorDTO.Bairro, desenvolvedorDTO.Cidade,
                    desenvolvedorDTO.Estado, desenvolvedorDTO.Pais);
                Telefone telefone = new Telefone(desenvolvedorDTO.Tipo, desenvolvedorDTO.NumeroTelefone);

                var user = new Desenvolvedor
                {
                    Nome = desenvolvedorDTO.Nome,
                    Sobrenome = desenvolvedorDTO.Sobrenome,
                    CPF = desenvolvedorDTO.CPF,
                    LinkedinUrl = desenvolvedorDTO.LinkedinUrl,
                    DataContratacao = new DateTime(desenvolvedorDTO.DataContratacao.Ano,
                    desenvolvedorDTO.DataContratacao.Mes, desenvolvedorDTO.DataContratacao.Dia),
                    UserName = desenvolvedorDTO.Email,
                    Email = desenvolvedorDTO.Email,
                    EmailConfirmed = true,
                    Endereco = endereco,
                    Telefone = telefone
                };

                if (endereco == null || telefone == null || user == null)
                {
                    return BadRequest("Telefone ou endereço inválidos!");
                }

                endereco.Desenvolvedor = user;
                endereco.DesenvolvedorId = user.Id;
                telefone.Desenvolvedor = user;
                telefone.DesenvolvedorId = user.Id;

                _unitOfWork.EnderecoRepository.Add(endereco);
                _unitOfWork.TelefoneRepository.Add(telefone);

                var result = await _userManager.CreateAsync(user, desenvolvedorDTO.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                _unitOfWork.Commit();
                return CreatedAtAction("Get", new { id = user.Id }, desenvolvedorDTO);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Verifica as credenciais de um usuário
        /// </summary>
        /// <param name="userInfo">Um objeto do tipo UserInfo</param>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] UserInfoDTO userInfo)
        {
            //verifica as credenciais do usuário e retorna um valor
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email,
                userInfo.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Inválido....");
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Atualiza as informacoes do desenvolvedor pelo id do mesmo
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna um desenvolvedor com as informacoes alteradas</returns>
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(string id, DesenvolvedorDTO desenvolvedorDTO)
        {
            if (id != desenvolvedorDTO.Id)
            {
                return BadRequest();
            }
            var desenvolvedor = _mapper.Map<Desenvolvedor>(desenvolvedorDTO);
            
            Desenvolvedor aux = _unitOfWork.DesenvolvedorRepository.GetDesenvolvedorById(id);
            desenvolvedor.Endereco = aux.Endereco;
            desenvolvedor.Telefone = aux.Telefone;
            Desenvolvedor aux2 = _unitOfWork.DesenvolvedorRepository.GetDesenvolvedorByCPF(desenvolvedor.CPF);

            if(aux.CPF != desenvolvedor.CPF && desenvolvedor.CPF == aux2.CPF)
            {
                return Conflict("O CPF não pode ser igual ao já cadastrado!");
            }

            try
            {
                _unitOfWork.DesenvolvedorRepository.Update(desenvolvedor);
                _unitOfWork.Commit();

                var desenvolvedorResumoDTO = _mapper.Map<DesenvolvedorResumoDTO>(desenvolvedor);

                return Ok(desenvolvedorResumoDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (aux != null)
                {
                    return NoContent();
                }
                else
                {
                    throw;
                }
            }
        }

        // api/desenvolvedor/delete/1
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var desenvolvedor = _unitOfWork.DesenvolvedorRepository.GetDesenvolvedorById(id);
            if (desenvolvedor == null)
            {
                return NotFound();
            }

            _unitOfWork.DesenvolvedorRepository.Delete(desenvolvedor);
            _unitOfWork.Commit();

            var desenvolvedorDTO = _mapper.Map<DesenvolvedorDTO>(desenvolvedor);

            return Ok(desenvolvedorDTO);
        }
    }
}
