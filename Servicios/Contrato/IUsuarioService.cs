using ProveduriaWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ProveduriaWeb.Servicios.Contrato
{
    public interface IUsuarioService
    {

        Task<Usuario> GetUsuarios(string correo, string contrasena);

        Task<Usuario> SaveUsuarios(Usuario modelo);





    }
}
