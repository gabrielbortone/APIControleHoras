namespace APIDesafio.DTOs
{
    public class DesenvolvedorResumoDTO
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string LinkedinUrl { get; set; }
        public string GithubUrl { get; set; }
        public int TotalHoras { get; set; }

        public DesenvolvedorResumoDTO(string nome, string sobrenome, string linkedinUrl, string githubUrl)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            LinkedinUrl = linkedinUrl;
            GithubUrl = githubUrl;
        }

        public DesenvolvedorResumoDTO()
        {
        }
    }
}
