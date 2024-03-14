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

        

        public record TransferProject
        {
            public string Name { get; set; }
        }

        public record TransferProjectId : TransferProject
        {
            public long Id { get; set; }
        }

        [HttpPost("addproject")]

        public IActionResult AddProject([FromBody] TransferProject project)
        {
            if(project.Name.Length > 3 && project.Name.Length < 32)
            {
                Project check = _RepProjects.Items.Where(x => x.Name == project.Name).FirstOrDefault();

                if (check == null)
                {
                    Project newProject = new Project { Name = project.Name };

                    _RepProjects.Add(newProject);
                }
                else return BadRequest(new { ErrorMessage = "Проект с указанным именем уже существует." });

                // Возвращаем успешный статус
                return Ok();
            }

            return NoContent();
        }

        
            [HttpPost("removeproject")]

        public IActionResult RemoveProject([FromBody] TransferProject project)
        {
            if (project.Name != null)
            {
                Project check = _RepProjects.Items.Where(x => x.Name == project.Name).FirstOrDefault();

                if (check != null)
                {
                    _RepProjects.Remove(check.Id);
                }
                // Возвращаем успешный статус
                return Ok();
            }

            return NoContent();
        }


        [HttpPost("editproject")]
        public IActionResult ChangeProject([FromBody] TransferProjectId project)
        {
            if (project != null && project.Id != 0 && project.Name != null && project.Name.Length >= 3 && project.Name.Length < 32)
            {
                Project check = _RepProjects.Items.Where(x => x.Id == project.Id).FirstOrDefault();

                if (check != null)
                {
                    check.Name = project.Name;
                    _RepProjects.Update(check);
                }
                else return NotFound(new { ErrorMessage = "Проект не получается найти в базе данных" });
                // Возвращаем успешный статус
                return Ok();
            }

            return NoContent();
        }


        public record TransferVersion
        {
            public long ProjectId { get; set; }

            public int? N { get; set; }

            public int? Nn { get; set; }

            public int? Nnn { get; set; }

            public string? Descr { get; set; }

        }


        [HttpPost("addversion")]

        public IActionResult AddVersion([FromBody] TransferVersion version)
        {
            if (version != null && version.ProjectId != 0 && (version.N != null && version.Nn != null && version.Nnn != null))
            {
                Project check = _RepProjects.Items.Where(x => x.Id == version.ProjectId).FirstOrDefault();

                if (check != null)
                {
                    Version checkversion = check.Versions.Where(x => (x.N == version.N) && (x.Nn == version.Nn) && (x.Nnn == version.Nnn)).FirstOrDefault();

                    if (checkversion == null)
                    {
                        Version newversion = new Version
                        {
                            ProjectId = version.ProjectId,
                            N = version.N,
                            Nn = version.Nn,
                            Nnn = version.Nnn,
                            Descr = version.Descr
                        };
                        _RepVersions.Add(newversion);

                    }
                    else return BadRequest(new { ErrorMessage = "Такая версия в данном проекте уже имеется" });

                }
                else return BadRequest(new { ErrorMessage = "Проекта не существует в базе данных"});
                // Возвращаем успешный статус
                return Ok();
            }

            return NoContent();
        }

        [HttpPost("removeversion")]

        public IActionResult RemoveVersion([FromBody] TransferVersion version)
        {
            if (version != null && version.ProjectId != 0)
            {
                Project reqproject = _RepProjects.Items.Where(x => x.Id == version.ProjectId).FirstOrDefault();

                if(reqproject != null)
                {
                    Version reqversion = reqproject.Versions.Where(x => x.N == version.N && x.Nn == version.Nn && x.Nnn == version.Nnn).FirstOrDefault();

                    if(reqversion != null)
                    {
                        _RepVersions.Remove(reqversion.Id);
                    }
                    else return BadRequest(new { ErrorMessage = "Версии в данном проекте не существует" });
                }
                else return BadRequest(new { ErrorMessage = "Проекта не существует в базе данных" });

                // Возвращаем успешный статус
                return Ok();
            }

            return NoContent();
        }


        public record TransferVersionId : TransferVersion
        {
            public long Id { get; set; }

        }

        [HttpPost("changeversion")]

        public IActionResult ChangeVersion([FromBody] TransferVersionId version)
        {
            if (version != null && version.ProjectId != 0 && (version.N != null && version.Nn != null && version.Nnn != null))
            {
                Project check = _RepProjects.Items.Where(x => x.Id == version.ProjectId).FirstOrDefault();

                if (check != null)
                {
                    Version checkversion = check.Versions.Where(x => (x.N == version.N) && (x.Nn == version.Nn) && (x.Nnn == version.Nnn)).FirstOrDefault();

                    if (checkversion == null)
                    {
                        checkversion = check.Versions.Where(x => x.Id == version.Id).FirstOrDefault();
                        
                        if (checkversion != null)
                        {
                            Version UpdateVersion = new Version { Id = checkversion.Id, N = version.N, Nn = version.Nn, Nnn = version.Nnn, Descr = version.Descr, ProjectId = checkversion.ProjectId, Examinations = checkversion.Examinations };
                            _RepVersions.Update(UpdateVersion);
                        }
                        else return NotFound(new { ErrorMessage = "Версия с указанным Id не найдена в проекте" });

                    }
                    else return BadRequest(new { ErrorMessage = "Такая версия в данном проекте уже имеется" });

                }
                else return NotFound(new { ErrorMessage = "Проект с таким Id не найден" });
                // Возвращаем успешный статус
                return Ok();
            }

            return NoContent();
        }


    }
}
