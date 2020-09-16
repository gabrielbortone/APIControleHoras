using ApiTeste.Models.ObjectTypes;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTeste.Models
{
    public class Desenvolvedor : IdentityUser
    {
        [Required]
        [Display(Name = "Informe o seu nome")]
        [StringLength(30, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Informe o seu sobrenome")]
        [StringLength(30, MinimumLength = 3)]
        public string Sobrenome { get; set; }

        [Required]
        [Display(Name = "Informe o seu número de CPF")]
        [StringLength(11, MinimumLength = 11)]
        public string CPF { get; set; }

        [Required]
        [Display(Name = "Informe a url de seu linkedin")]
        [StringLength(60, MinimumLength = 11)]
        public string LinkedinUrl { get; set; }

        [Required]
        [Display(Name ="Informe a url de seu github")]
        [StringLength(60, MinimumLength = 11)]
        public string GithubUrl { get; set; }

        [Required]
        [Display(Name = "Informe a data de contratação")]
        [DataType(DataType.Date)]
        public DateTime DataContratacao { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataDemissao { get; set; }

        [Required]
        public Endereco Endereco { get; set; }

        [Required]
        public Telefone Telefone { get; set; }
        public virtual IEnumerable<Ponto> ControleHoras { get; set; }

        [NotMapped]
        public int TotalSemanal { get; set; }

        public Desenvolvedor()
        {
            TotalSemanal = 0;
        }

        public Desenvolvedor(string nome,string sobrenome, string cpf, 
            string linkedinUrl, string githubUrl, DateTime dataContratacao,
            Endereco endereco, Telefone telefone)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cpf;
            LinkedinUrl = linkedinUrl;
            GithubUrl = githubUrl;
            DataContratacao = dataContratacao;
            Endereco = endereco;
            Telefone = telefone;
            TotalSemanal = 0;
        }

        public Desenvolvedor(string nome, string sobrenome, string cPF, string linkedinUrl, string githubUrl, DateTime dataContratacao)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            LinkedinUrl = linkedinUrl;
            GithubUrl = githubUrl;
            DataContratacao = dataContratacao;
            TotalSemanal = 0;
        }

        
    }
}
