using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/api/fileupload")]
    public class FileUploadController : ControllerBase
    {
        private readonly ILogger<FileUploadController> _logger;
        public FileUploadController(ILogger<FileUploadController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromForm] ProjectXmlFile file)
        {
            if (file == null)
            {
                return BadRequest("file shall be provided");
            }
            _logger.LogInformation("file uploaded: " + file.FileName);
            var path = Path.Combine("../toProcess", file.FileName);
            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "failed to overwrite existing file" });
                throw;
            }

            try
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.FormFile.CopyTo(stream);
                }
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "failed to create new file" });
                throw;
            }

            return Ok(file.FileName);
        }
    }
}