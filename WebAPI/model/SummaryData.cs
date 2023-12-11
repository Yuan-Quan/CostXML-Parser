
namespace WebAPI.Model
{
    public class SummaryData
    {
        public SummaryData()
        {
            Items = new List<SummaryDataItem>();
            Version = "0.1";
        }

        public string Version { get; set; }
        public int RecordCount
        {
            get => Items.Count;
        }


        public List<SummaryDataItem> Items { get; set; }
    }

    public class SummaryDataItem
    {
        public string Sequence { get; set; }
        public string Name { get; set; }
        public string CalculationMethod { get; set; }
        public string Total { get; set; }
    }
}