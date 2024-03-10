using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using try2.DAL.Interfaces;
using try2.DAL.Models;
using try2.Domain.Entities;
using Version = try2.DAL.Models.Version;

namespace try2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AccountsController : ControllerBase
    {
        private readonly IRepository<Version> _RepVersions;

        private readonly IRepository<Project> _RepProjects;

        public AccountsController(IRepository<Project> Projects, IRepository<Version> Versions)
        {
            _RepProjects = Projects;
            _RepVersions = Versions;
        }

        [HttpGet("projects")]
        public ICollection<Project> Get()
        {
            return _RepProjects.Items.Include(p => p.Versions).ToList();
        }

        

        public record NewProject
        {
            public string Name { get; set; }
        }

        [HttpPost]
        [Route("/addproject")]

        public IActionResult Add([FromBody] NewProject project)
        {
            if(project.Name != null)
            {
                Project check = _RepProjects.Items.Where(x => x.Name == project.Name).FirstOrDefault();

                if(check == null)
                {
                    Project newProject = new Project { Name = project.Name };

                    _RepProjects.Add(newProject);
                }

                // Возвращаем успешный статус
                return Ok();
            }

            return NoContent();
        }

    }
}
