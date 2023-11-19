namespace CostXMLParser
{
    public class Exporter
    {
        private ConstructProject _project;
        public Exporter(ConstructProject project)
        {
            _project = project;
        }

        // various export methods. only contains the logic of file writing, actual formatting is done in XXXGenerator.cs

        public void ExportFoldered(String path)
        {
            Console.WriteLine("Exporting foldered raw xml to " + path);
            var projectRootPath = Path.Combine(path, _project.ProjectName + "FolderedRaw");
            // create folders for each single project
            foreach (var singleProject in _project.SingleProjects)
            {
                var folderPath = Path.Combine(projectRootPath, singleProject.XDoc.Attribute("Name").Value);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                // create file for each unit project
                foreach (var unitProject in singleProject.UnitProjects)
                {
                    try
                    {
                        var filePath = Path.Combine(folderPath, unitProject.XDoc.Attribute("Name").Value + "(单位工程).xml");
                        if (File.Exists(filePath))
                        {
                            Console.WriteLine("File already exists: " + filePath + " will overwrite it.");
                        }
                        File.WriteAllText(filePath, unitProject.XDoc.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERR: Failed to write file: " + e.Message);
                    }
                }
            }
        }

        public void ExportSummaryCSV(String path)
        {
            Console.WriteLine("Exporting foldered summary to " + path);
            var projectRootPath = Path.Combine(path, _project.ProjectName + "Summary");
            // create folders for each single project
            foreach (var singleProject in _project.SingleProjects)
            {
                var folderPath = Path.Combine(projectRootPath, singleProject.XDoc.Attribute("Name").Value);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                // create file for each unit project
                foreach (var unitProject in singleProject.UnitProjects)
                {
                    try
                    {
                        var filePath = Path.Combine(folderPath, unitProject.XDoc.Attribute("Name").Value + "(单位工程).CSV");
                        if (File.Exists(filePath))
                        {
                            Console.WriteLine("File already exists: " + filePath + " will overwrite it.");
                        }
                        File.WriteAllText(filePath, unitProject.SummaryTable.ToCSV());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERR: Failed to write file: " + e.Message);
                    }
                }
            }
        }
    }
}