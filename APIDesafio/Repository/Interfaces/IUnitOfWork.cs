namespace ApiTeste.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IDesenvolvedorRepository DesenvolvedorRepository { get; }
        IEnderecoRepository EnderecoRepository { get; }
        ITelefoneRepository TelefoneRepository { get; }
        IProjetoRepository ProjetoRepository { get; }
        IPontoRepository PontoRepository { get; }
        public void Commit();


    }
}
