using APIDesafio.DTOs;
using ApiTeste.DTOs;
using ApiTeste.Models;
using AutoMapper;
using System;

namespace APIControleHoras.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Desenvolvedor, DesenvolvedorResumoDTO>().ReverseMap();
            CreateMap<Desenvolvedor, DesenvolvedorDTO>()
                .ForMember(devDTO => devDTO.Id, opt => opt.MapFrom(dev => dev.Id))
                .ForMember(devDTO => devDTO.Email, opt => opt.MapFrom(dev => dev.Email))
                .ForMember(devDTO => devDTO.Logradouro, opt => opt.MapFrom(dev => dev.Endereco.Logradouro))
                .ForMember(devDTO => devDTO.Numero, opt => opt.MapFrom(dev => dev.Endereco.Numero))
                .ForMember(devDTO => devDTO.CEP, opt => opt.MapFrom(dev => dev.Endereco.CEP))
                .ForMember(devDTO => devDTO.Bairro, opt => opt.MapFrom(dev => dev.Endereco.Bairro))
                .ForMember(devDTO => devDTO.Cidade, opt => opt.MapFrom(dev => dev.Endereco.Cidade))
                .ForMember(devDTO => devDTO.Estado, opt => opt.MapFrom(dev => dev.Endereco.Estado))
                .ForMember(devDTO => devDTO.Tipo, opt => opt.MapFrom(dev => dev.Telefone.Tipo))
                .ForMember(devDTO => devDTO.NumeroTelefone, opt => opt.MapFrom(dev => dev.Telefone.Numero))
                .ReverseMap();
            CreateMap<DateTime, DataCompletaDTO>()
                .ForMember(dataDTO => dataDTO.Ano, opt => opt.MapFrom(dateTime => dateTime.Year))
                .ForMember(dataDTO => dataDTO.Mes, opt => opt.MapFrom(dateTime => dateTime.Month))
                .ForMember(dataDTO => dataDTO.Dia, opt => opt.MapFrom(dateTime => dateTime.Day))
                .ForMember(dataDTO => dataDTO.Hora, opt => opt.MapFrom(dateTime => dateTime.Hour))
                .ForMember(dataDTO => dataDTO.Minuto, opt => opt.MapFrom(dateTime => dateTime.Minute))
                .ReverseMap();
            CreateMap<Ponto, PontoDTO>().ReverseMap();
            CreateMap<Projeto, ProjetoDTO>().ReverseMap();
        }
    }
}
