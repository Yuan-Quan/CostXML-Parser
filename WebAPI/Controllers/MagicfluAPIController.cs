using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/api/magicflu")]
    public class MagicfluAPIController : ControllerBase
    {
        private readonly ILogger<MagicfluAPIController> _logger;

        public MagicfluAPIController(ILogger<MagicfluAPIController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{path?}")]
        public IActionResult Get([FromRoute] string? path)
        {
            if (String.IsNullOrEmpty(path))
            {
                return BadRequest("target path shall be provided");
            }
            path = Uri.UnescapeDataString(path);
            path = Path.Combine("../output", path + ".csv");
            var response = new SummaryData();
            if (!System.IO.File.Exists(path))
            {
                return NotFound("target file not found at " + path);
            }
            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    // skip the first row
                    if (fields[0] == "INDEX")
                    {
                        continue;
                    }
                    response.Items.Add(new SummaryDataItem()
                    {
                        Sequence = fields[0],
                        Name = fields[1],
                        CalculationMethod = fields[2],
                        Total = fields[3]
                    });
                }
            }
            return Ok(response);
        }
    }
}