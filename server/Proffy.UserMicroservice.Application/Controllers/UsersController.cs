using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proffy.UserMicroservice.Application.Services;
using Proffy.UserMicroservice.Domain.AggregatesModel.UserAggregate;
using Proffy.UserMicroservice.Infrastructure.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proffy.UserMicroservice.Application.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("login")]
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
        }

        // Returns list of teachers when searched in https:/proffy/study
        [AllowAnonymous]
        [HttpGet("search/{GuidList}")]
        public async Task<ActionResult<IEnumerable<User>>> Get(string guidList)
        {
            // Recebe guids separados por virgula como string, convert string em lista de guid e pesquisa por professores
            var hashedGuids = new HashSet<Guid>(Array.ConvertAll(guidList.Split(','), s => new Guid(s)).ToList());

            var User = _context.Users
                .Where(u => hashedGuids.Contains(u.Id))
                .ToList();

            return User;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("Usuário não encontrado O.o. Por favor, faça login novamente.");
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
            put.Whatsapp = user.Whatsapp;
            put.Facebook = user.Facebook;
            put.Bio = user.Bio;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return NotFound("Não foi possível salvar as informações.");
            }

            return StatusCode(201, new { message = "Informações atualizadas com sucesso!" });
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [AllowAnonymous]
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
            {}

            user.Password = PasswordService.Cryptography(user.Password);

            user.CreatedAt = DateTime.Now;

            user.Actived = false;

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            // Envia email ao usuario informando conta criada.
            MessageService.SendEmail(user.Email);

            return StatusCode(201, new { message = "Conta criada com sucesso! Você pode fazer login ^^" });
        }
    }
}
