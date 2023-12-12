using System.Diagnostics;
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
            var hash = request.FileName[1..8];

            var outputFolder = Path.Combine("../output", hash);
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

            return Ok(hash);
        }

        [HttpGet]
        [Route("result/{hash}")]
        public IActionResult GetResult([FromRoute] string hash)
        {
            var outputFolder = Path.Combine("../output", hash);
            if (!Directory.Exists(outputFolder))
            {
                return NotFound("target folder not found at " + outputFolder);
            }
            var response = GenerateResponse(outputFolder);
            return Ok(response);
        }

        private ProcessProjectResponse GenerateResponse(string outputFolder)
        {
            Console.WriteLine("GenerateResponse:" + outputFolder);

            var response = new ProcessProjectResponse();

            response.ProjectName = "test";
            response.Results = new List<ProcessResultItem>();

            var allfiles = new List<string>(Directory.GetFiles(outputFolder, "*.CSV", SearchOption.AllDirectories));
            var stripedFiles = new List<string>();

            foreach (var item in allfiles)
            {
                if (item.StartsWith("../output\\"))
                {
                    stripedFiles.Add(item[10..]);
                }
            }

            foreach (var item in stripedFiles)
            {
                if (item.EndsWith(".CSV"))
                {
                    var name = item[0..^4].Replace("\\", "/");
                    var url = "http://localhost:7094/api/magicflu/" + Uri.EscapeDataString(item[0..^4].Replace("\\", "/"));
                    response.Results.Add(new ProcessResultItem() { Name = name, Url = url });
                }

            }

            return response;
        }

    }


}