using CostXMLParser;
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
            list.Add(new ProcessingMethod() { ProcessName = "生成打包后的xml", ProcessDescription = "单个单位工程的xml" });
            list.Add(new ProcessingMethod() { ProcessName = "生成单位工程费用汇总表", ProcessDescription = "生成单位工程费用汇总表" });
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
            // sort list by date
            list.Sort((x, y) => DateTime.Compare(DateTime.Parse(y.DateUploaded), DateTime.Parse(x.DateUploaded)));
            return Ok(list);
        }

        [HttpPost]
        [Route("process-project")]
        public IActionResult ProcessProject([FromBody] ProcessProjectRequest request)
        {
            var path = Path.Combine("../toProcess", request.FileName);
            if (!System.IO.File.Exists(path))
            {
                return NotFound("target file not found at " + path);
            }

            var outputFolder = Path.Combine("../output/" + request.FileName[1..8] + " ");
            Console.WriteLine("outputFolder: " + outputFolder);

            var deserializer = new Deserializer(path);

            var exporter = new Exporter(deserializer.Project);
            if (request.IsExportFolderedRaw)
            {
                exporter.ExportFoldered(outputFolder);
            }

            if (request.IsExportSummary)
            {
                exporter.ExportSummaryCSV(outputFolder);
            }

            return Ok("ok");
        }
    }

    public class ProcessProjectRequest
    {
        public string FileName { get; set; }
        public bool IsExportFolderedRaw { get; set; }
        public bool IsExportSummary { get; set; }
    }
}