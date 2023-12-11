using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/api/reactapp")]
    public class ReactAppController : ControllerBase
    {
        private readonly ILogger<ReactAppController> _logger;

        public ReactAppController(ILogger<ReactAppController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("avaliable-processes")]
        public IActionResult Get()
        {
            var list = new List<ProcessingMethod>();
            list.Add(new ProcessingMethod() { ProcessName = "生成单位工程费用汇总表" });
            return Ok(list);
        }

        [HttpGet]
        [Route("uploaded-projects")]
        public IActionResult GetUploadedProjects()
        {
            var list = new List<UploadedProject>();
            var path = Path.Combine("../toProcess");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                DateTime lastModified = System.IO.File.GetLastWriteTime(file);
                list.Add(new UploadedProject() { FileName = Path.GetFileName(file), DateUploaded = lastModified.ToString("yyyy-MM-dd HH:mm:ss") });
            }
            return Ok(list);
        }
    }
}