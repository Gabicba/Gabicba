using ApiTpEncode.Models;
using ApiTpEncode.Models.DTOs;
using ApiTpEncode.Repositories;
using ApiTpEncode.Servicios;
using AutoMapper;

namespace ApiTpEncode.Servicios
{
    
    public class UsuarioService : IUsuarioServicio
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;//agrega la inyeccion de Imapper
        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioGetDTO>> GetAllUsuariosAsync()
        {
            //obtiene todos los usuarios desde el repositorio
            var usuarios = await _usuarioRepository.GetAllAsync();
            //si no hay usuarios retorna una lista vacia
            if (usuarios == null || ! usuarios.Any()) 
            {
                return Enumerable.Empty<UsuarioGetDTO>();

            }


            //mapea la lista de entidades a una lista dtos
            return _mapper.Map<IEnumerable<UsuarioGetDTO>>(usuarios);
        }

        public async Task<UsuarioGetDTO> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null) 
            {
                throw new KeyNotFoundException("usuario no encontrado");
            }
            //mapea la entidad DTO
            return _mapper.Map<UsuarioGetDTO>(usuario);
        }

        public async Task<UsuarioGetDTO> AddUsuarioAsync(UsuarioPostDTO usuarioDTO)
        {
            //mapea dto entidad
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            //agrega el usuario a la bbdd
            await _usuarioRepository.AddAsync(usuario);
           
            
            //retorna el dto mapeado desde la entidad "revisar este returnnn!!!"
            return _mapper.Map<UsuarioGetDTO>(usuario);
        }

        public async Task UpdateUsuarioAsync(int id,UsuarioPutDTO usuarioDTO)
        {
            var existingUser = await _usuarioRepository.GetByIdAsync(id);
           // var usuario = _mapper.Map<Usuario>(usuarioDTO);

            if ( existingUser == null)
            {
                throw new ArgumentException("usuario no encontrado.");
            }
            //actualiza propiedades directamente
            existingUser.Name = usuarioDTO.Name;
            existingUser.Email = usuarioDTO.Email;
            existingUser.Password = usuarioDTO.Password;

            await _usuarioRepository.UpdateAsync(existingUser);
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            // Buscamos el usuario por su id
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            // Si el usuario no se encuentra, lanzamos una excepción
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }

            // Eliminamos el usuario
            await _usuarioRepository.DeleteAsync(id);
        }

    }
}
