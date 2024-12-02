using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EclipseWorksTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly int managerRoleUserId = 1;
        public PerformanceController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Prometheus");
        }
        /// <summary>
        /// Retorna o tempo médio de criação de um Project em milisegundos
        /// </summary>
        [HttpGet("{userId}/histograms/project/creationProcessTime")]
        [Produces(typeof(string))]
        public async Task<IActionResult> GetProjectsCreationProcessTime(int userId)
        {
            if (userId != managerRoleUserId)
                return Unauthorized("O usuário não tem a função necessária");

            var response = await _httpClient.GetAsync($"query?query=project_creation_process_time_sum%2Fproject_creation_process_time_count");
            var content = await response.Content.ReadAsStringAsync();
            var document = JsonDocument.Parse(content);

            var value = document
                .RootElement
                .GetProperty("data")
                .GetProperty("result")[0]
                .GetProperty("value")[1].GetString();

            return Ok(value);
        }
        /// <summary>
        /// Retorna a contagem de Projects criados
        /// </summary>
        [HttpGet("{userId}/histograms/project/createdCount")]
        [Produces(typeof(string))]

        public async Task<IActionResult> GetCreateProjectsCount(int userId)
        {
            if (userId != managerRoleUserId)
                return Unauthorized("O usuário não tem a função necessária");

            var response = await _httpClient.GetAsync($"query?query=created_projects_total");
            var content = await response.Content.ReadAsStringAsync();
            var document = JsonDocument.Parse(content);

            var value = document
                .RootElement
                .GetProperty("data")
                .GetProperty("result")[0]
                .GetProperty("value")[1].GetString();

            return Ok(value);
        }
    }
}
