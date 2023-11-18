using System;
using System.Xml.Linq;
using CommandLine;

namespace CostXMLParser.CLI
{

    class Program
    {
        public class Options
        {
            [Option('i', "input", Required = true, HelpText = "Input file to read.")]
            public string? InputFile { get; set; }

            [Option('o', "output", Required = false, HelpText = "Output folder to write.")]
            public string? OutputFolder { get; set; }

            // export option: raw xml
            [Option('F', "foldered", Required = false, HelpText = "Export foldered raw xml.")]
            public bool ExportFolderedRaw { get; set; }
        }

        static void Main(string[] args)
        {
            var parsedArgs = Parser.Default.ParseArguments<Options>(args);

            var deserializer = new Deserializer(parsedArgs.Value.InputFile);

            if (parsedArgs.Value.ExportFolderedRaw)
            {
                if (parsedArgs.Value.OutputFolder == null)
                {
                    Console.WriteLine("ERR: Output folder is required for exporting foldered raw xml.");
                }
                else
                {
                    var exporter = new Exporter(deserializer.Project);
                    exporter.ExportFoldered(parsedArgs.Value.OutputFolder);
                }
            }

        }

    }
}