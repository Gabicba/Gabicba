
using ApiTpEncode.Models.DTOs;

namespace ApiTpEncode.Servicios
{
    public interface IUsuarioServicio
    {
        Task<IEnumerable<UsuarioGetDTO>> GetAllUsuariosAsync(); // Cambiar a UsuarioGetDTO
        Task<UsuarioGetDTO> GetUsuarioByIdAsync(int id); // Cambiar a UsuarioGetDTO
        Task<UsuarioGetDTO> AddUsuarioAsync(UsuarioPostDTO usuarioDTO); // Cambiar a UsuarioPostDTO
        Task UpdateUsuarioAsync(int id, UsuarioPutDTO usuarioDTO); // Cambiar a UsuarioPutDTO
        Task DeleteUsuarioAsync(int id);
        Task<bool> UsuarioExistsAsync(int id);

    }
}
