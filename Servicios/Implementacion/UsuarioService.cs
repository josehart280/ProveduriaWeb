using Microsoft.EntityFrameworkCore;
using ProveduriaWeb.Models;
using ProveduriaWeb.Servicios.Contrato;

namespace ProveduriaWeb.Servicios.Implementacion
   
{
    public class UsuarioService : IUsuarioService
    {

        private readonly DbContext _dbContext;
        
        public UsuarioService(DbContext dbContext)
        {

            _dbContext = dbContext;

        }

        public async Task<Usuario> GetUsuarios(string correo, string contrasena)
        {
            Usuario usuario_Encontrado = await _dbContext.Usuario.Where(u=> u.Correo == correo &&  u.Contasena == contrasena).FirstOrDefaultAsync();
        }

        public Task<Usuario> SaveUsuarios(Usuario modelo)
        {
            throw new NotImplementedException();
        }
    }
}
