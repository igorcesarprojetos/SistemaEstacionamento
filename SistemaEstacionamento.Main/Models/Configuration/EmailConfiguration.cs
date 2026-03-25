namespace SistemaEstacionamento.Main.Models.Configuration
{
    public class EmailConfiguration
    {
        public string NomeRemetente { get; set; }
        public string EmailRemetente { get; set; }
        public string Senha { get; set; }
        public string EnderecoServidorEmail { get; set; }
        public int PortaServidorEmail { get; set; }
        public bool UsarSsl { get; set; }
    }
}
