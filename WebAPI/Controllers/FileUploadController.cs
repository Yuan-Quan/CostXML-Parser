using System.Security.Cryptography;
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
            // calculate file md5 hash
            var md5Hash = string.Empty;
            using (var md5 = MD5.Create())
            {
                using var stream = file.FormFile.OpenReadStream();
                var hash = md5.ComputeHash(stream);
                md5Hash = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
            if (CheckExistingProjectUpload(md5Hash, out string fileName))
            {
                _logger.LogInformation("user file uploaded: " + file.FileName + " (existing file: " + fileName + ")");
                return Ok("Same file already uploaded: " + fileName);
            }
            var path = Path.Combine("../toProcess", "[" + md5Hash[..7] + "] " + file.FileName); // first 7 chars of md5 hash should be enough to identify a file
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

            _logger.LogInformation("user file uploaded: " + file.FileName);
            return StatusCode(StatusCodes.Status201Created, new { message = "file uploaded" });
        }

        private bool CheckExistingProjectUpload(string md5Hash, out string fileName)
        {
            md5Hash = md5Hash.ToLowerInvariant()[..7];
            string path = @"../toProcess";
            string[] files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                //check if file name contains the md5 hash
                if (file.Contains(md5Hash))
                {
                    fileName = file;
                    return true;
                }
            }
            fileName = "";
            return false;
        }
    }
}