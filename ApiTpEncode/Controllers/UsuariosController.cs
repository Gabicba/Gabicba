using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiTpEncode.Data;
using ApiTpEncode.Models;
using ApiTpEncode.Repositories;
using ApiTpEncode.Servicios;
using ApiTpEncode.Models.DTOs;

namespace ApiTpEncode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        //inyeccion de repository
        private readonly IUsuarioServicio _usuarioServicio;

        public UsuariosController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioGetDTO>>> Getusuarios()
        {
            var usuarios = await _usuarioServicio.GetAllUsuariosAsync(); // Usando el repositorio
            return Ok(usuarios); //return list usuarios
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioGetDTO>> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _usuarioServicio.GetUsuarioByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok (usuario);
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUsuario(int id, UsuarioPutDTO usuarioDTO)
        {

            try
            {
                // Llama al método de actualización en el repositorio
                await _usuarioServicio.UpdateUsuarioAsync(id, usuarioDTO);
            }
            catch (KeyNotFoundException ex)
            {
                // Manejo de excepciones por concurrencia
                return NotFound(ex.Message); // Si la entidad no se encuentra, devuelve NotFound 404
            }
            catch (Exception ex)
            {
                // Manejo de otras excepciones
                // Puedes registrar el error (ex) si lo deseas
                return BadRequest(ex.Message); // Devuelve un error interno 400
            }
            return NoContent();

        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioPostDTO usuarioDTO)
        {
            await _usuarioServicio.AddUsuarioAsync(usuarioDTO);
           
            return StatusCode(201);//retur 201
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            try
            {
                await _usuarioServicio.DeleteUsuarioAsync(id);
            }
            catch (KeyNotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }

    }
}
