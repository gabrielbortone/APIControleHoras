using ApiTeste.Models;
using ApiTeste.Models.Data;
using ApiTeste.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTeste.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context { get; set; }
        private DesenvolvedorRepository _desenvolvedorRepo;
        private EnderecoRepository _enderecoRepo;
        private TelefoneRepository _telefoneRepo;
        private ProjetoRepository _projetoRepository;
        private PontoRepository _pontoRepository;

        public IDesenvolvedorRepository DesenvolvedorRepository 
        {
            get
            {
                return _desenvolvedorRepo = _desenvolvedorRepo ?? new DesenvolvedorRepository(_context);
            }
        }
        public IEnderecoRepository EnderecoRepository
        {
            get
            {
                return _enderecoRepo = _enderecoRepo ?? new EnderecoRepository(_context);
            }
        }

        public ITelefoneRepository TelefoneRepository
        {
            get
            {
                return _telefoneRepo = _telefoneRepo ?? new TelefoneRepository(_context);
            }
        }

        public IProjetoRepository ProjetoRepository
        {
            get
            {
                return _projetoRepository = _projetoRepository ?? new ProjetoRepository(_context);
            }
        }

        public IPontoRepository PontoRepository
        {
            get
            {
                return _pontoRepository = _pontoRepository ?? new PontoRepository(_context);
            }
        }

        public UnitOfWork(AppDbContext context)
        {
            _context = context; 
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
