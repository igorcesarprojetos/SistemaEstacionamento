using SistemaEstacionamento.Main.Models;

namespace SistemaEstacionamento.Main.Utilitarios.Helper.Interfaces
{
    public interface ISessao
    {
        void CriarSessaoUsuario(Usuario usuario);
        void RemoverSessaoUsuario();
        Usuario BuscarSessaoUsuario();
    }
}
