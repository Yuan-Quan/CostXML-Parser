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
            var response = new SummaryData();
            using (TextFieldParser parser = new TextFieldParser(Path.Combine("../output", "test.csv")))
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