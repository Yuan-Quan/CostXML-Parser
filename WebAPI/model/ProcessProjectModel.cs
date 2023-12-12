namespace WebAPI.Model
{
    public class ProcessProjectRequest
    {
        public string FileName { get; set; }
        public bool IsExportFolderedRaw { get; set; }
        public bool IsExportSummary { get; set; }
    }

    public class ProcessProjectResponse
    {
        public string ProjectName { get; set; }
        public List<ProcessResultItem> Results { get; set; }

    }

    public class ProcessResultItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}