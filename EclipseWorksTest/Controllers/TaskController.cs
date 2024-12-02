using EclipseWorksTest.AppService.AppServices.Interfaces;
using EclipseWorksTest.AppService.DTO.Project;
using EclipseWorksTest.CrossCutting.Validation;
using Microsoft.AspNetCore.Mvc;

namespace EclipseWorksTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IProjectTaskAppService _projectTaskAppService;
        public ProjectTaskController(IProjectTaskAppService projectTaskAppService)
        {
            _projectTaskAppService = projectTaskAppService;
        }

        [HttpGet("{projectId}")]
        [Produces(typeof(IEnumerable<ProjectTaskListDTO>))]
        public IActionResult GetProjectTasksByProjectId(int projectId)
        {
            var result = _projectTaskAppService.GetAllListDTOByProjectId(projectId);
            return Ok(result);
        }

        [HttpPost]
        [Produces(typeof(EclipseValidationResult))]
        public IActionResult CreateProjectTask(ProjectTaskAddDTO projectTask)
        {
            var result = _projectTaskAppService.Add(projectTask);

            return result.IsValid ? Created() : BadRequest(result);
        }

        [HttpPatch]
        public IActionResult PatchProjectTask([FromBody] ProjectTaskPatchDTO projectTask)
        {
            var result = _projectTaskAppService.Update(projectTask);

            return result.IsValid ? Ok() : BadRequest(result);
        }

        [HttpDelete]
        [Produces(typeof(EclipseValidationResult))]
        public IActionResult DeleteProjectTaskById(int projectTaskId)
        {
            var result = _projectTaskAppService.Remove(projectTaskId);

            return result.IsValid ? Ok() : BadRequest(result);
        }

        [HttpPost("/addComment")]
        [Produces(typeof(ProjectTaskCommentListDTO))]
        public IActionResult AddComment([FromBody] ProjectTaskCommentAddDTO comment)
        {
            var result = _projectTaskAppService.AddComment(comment);

            return result.IsValid ? Ok() : BadRequest(result);
        }
    }
}
