using EclipseWorksTest.AppService.AppServices.Interfaces;
using EclipseWorksTest.AppService.DTO.Project;
using EclipseWorksTest.CrossCutting.Validation;
using EclipseWorksTest.Otel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EclipseWorksTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectAppService _projectAppService;
        private readonly Instrumentation _otel;
        public ProjectController(IProjectAppService projectAppService, Instrumentation otel)
        {
            _projectAppService = projectAppService;
            _otel = otel;
        }

        [HttpGet("{userId}")]
        [Produces(typeof(IEnumerable<ProjectListDTO>))]
        public IActionResult GetProjects(int userId)
        {
            var projects = _projectAppService.GetProjectListDTOyUserId(userId);
            return projects.Any() ? Ok(projects) : NoContent();
        }

        [HttpPost]
        [Produces(typeof(EclipseValidationResult))]
        public IActionResult CreateProject(ProjectManageDTO project)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = _projectAppService.Add(project);
            if (result.IsValid)
            {
                stopwatch.Stop();
                _otel.ProjectCreationProcessTime.Record(stopwatch.Elapsed.TotalMilliseconds);
                _otel.CreatedProjectsCounter.Add(1);

                return Created();
            }
            return BadRequest(result);
        }

        [HttpDelete]
        [Produces(typeof(EclipseValidationResult))]
        public IActionResult DeleteProject(int id)
        {
            var result = _projectAppService.Remove(id);

            return result.IsValid ? Ok() : BadRequest(result);
        }
    }

}
