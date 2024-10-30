using ApiTpEncode.Data;
using ApiTpEncode.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


namespace ApiTpEncode.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDBContext _dbContext;
        public UsuarioRepository(AppDBContext dbContext) 
        {
        _dbContext = dbContext;
        }

        //metodo para agregar un nuevo usuario
        public async Task AddAsync(Usuario usuario)
        {
            _dbContext.usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();
        }

        //metodo para elminar por id
        public async Task DeleteAsync(int id)
        {
            var usuario = await _dbContext.usuarios.FindAsync(id);
            if (usuario != null)
            {
                _dbContext.usuarios.Remove(usuario);
                await _dbContext.SaveChangesAsync();
            }
        }
        
        //metodo para optener todos los usuarios
        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _dbContext.usuarios.ToListAsync();
        }


        //metodo para buscar por id
        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _dbContext.usuarios.FindAsync(id);
        }

       
        //metodo actualizar usuario existente
        public async Task UpdateAsync(Usuario usuario)
        {
            _dbContext.Entry(usuario).State=EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            try
            {
                // Intenta guardar los cambios en la base de datos
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Maneja la excepción aquí, quizás consultando la entidad de nuevo
                return ; // O manejarlo según tu lógica
            }
        }
    }
}
