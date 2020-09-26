using Application.WebApi.Services;
using Domain.Model.AggregatesModel.UserAggregate;
using Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ProffyContext _context;

        public UsersController(ProffyContext context)
        {
            _context = context;
        }

        /*[HttpPost("login")]
        public async Task<ActionResult<dynamic>> Login(User credentials)
        {
            try
            {
                User user = _context.Users.First(user => user.Email.Equals(credentials.Email));

                if (user.Password.Equals(PasswordService.Cryptography(credentials.Password)))
                {
                    var token = TokenService.GenerateToken();

                    return new
                    {
                        id = user.Id,
                        name = user.Name,
                        token = token
                    };
                }
                else
                {
                    return NotFound("Usuário ou senha incorreto.");
                }
            }
            catch
            {
                return NotFound("Usuário ou senha incorreto.");
            }
        }*/

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("Usuário não encontrado O.o. Por favor, tente novamente ou faça uma conta.");
            }

            user.Password = "";

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest("Não foi possível atualizar. Por favor, faça login novamente.");
            }

            var put = _context.Users.Find(user.Id);

            if (user.Password != string.Empty)
            {
                put.Password = PasswordService.Cryptography(user.Password);
            }

            put.Photo = user.Photo;
            // put.Whatsapp = user.Whatsapp;
            // put.Facebook = user.Facebook;
            // put.Bio = user.Bio;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound("Não foi possível salvar as informações.");
            }

            return StatusCode(201, new { message = "Informações atualizadas com sucesso!" });
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            /*
             * TODO
             * Ver se há forma melhor para verificar se conta já existe
             * 
             * Try Catch impede retornar erro do C# quando não encontra objeto, 
             * que interrompe o fluxo desejado
             */
            try
            {
                var userExists = _context.Users.First(u => u.Email.Equals(user.Email));

                if (userExists != null)
                {
                    return BadRequest("E-mail já cadastrado. Esqueceu a senha?");
                }
            }
            catch
            { }

            user.Password = PasswordService.Cryptography(user.Password);

            user.CreatedAt = DateTime.Now;

            user.Actived = false;

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            // Envia email ao usuario informando conta criada.
            // MessageService.SendEmail(user.Email);

            return StatusCode(201, new { message = "Conta criada com sucesso! Você pode fazer login ^^" });
        }

        // DELETE: api/Users/5
        /*[HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }*/
    }
}
