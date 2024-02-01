using Microsoft.AspNetCore.Mvc;
using try2.DAL.Interfaces;
using try2.Domain.Entities;
using try2.Domain.Models.Entities;

namespace try2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AccountsController : ControllerBase
    {
        private readonly IRepository<User> _RepUsers;

        private readonly IRepository<Profile> _RepProfiles;

        public AccountsController(IRepository<User> Users, IRepository<Profile> Profiles)
        {
            _RepUsers = Users;
            _RepProfiles = Profiles;
        }

        [HttpGet]
        public ICollection<User> Get()
        {
            return _RepUsers.Items.ToList();
        }

        [HttpPost]
        public IActionResult Add([FromBody] User user)
        {

            if (user.Login.Trim() != "" && user.Email.Trim() != "" && user.Password.Trim() != "")
            {
                user.UserType = Domain.Models.Enums.TypeUser.CasualUser;
                // Добавление пользователя в репозиторий
                _RepUsers.Add(user);
                // Возвращаем успешный статус
                return Ok();
            }

            return NoContent();
        }

    }
}
