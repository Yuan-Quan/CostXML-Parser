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
        public string FileName { get; set; }
        public string ProjectName { get; set; }
        public ProcessResultItem ResultRoot { get; set; }

    }

    public class ProcessResultItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public ProcessResultItem[] Children { get; set; }
    }
}