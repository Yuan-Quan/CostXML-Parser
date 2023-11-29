using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MagicfluAPITestController : ControllerBase
    {
        private readonly ILogger<MagicfluAPITestController> _logger;

        public MagicfluAPITestController(ILogger<MagicfluAPITestController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetMagicfluAPITestData")]
        public IEnumerable<MagicfluAPITestData> Get()
        {
            var list = new List<MagicfluAPITestData>();
            using (TextFieldParser parser = new TextFieldParser(@"../output/利建大厦Summary/地下工程/地下室土建(单位工程).CSV"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    list.Add(new MagicfluAPITestData()
                    {
                        Sequence = fields[0],
                        Name = fields[1],
                        CalculationMethod = fields[2],
                        Total = fields[3]
                    });
                }
            }
            return list;
        }
    }
}