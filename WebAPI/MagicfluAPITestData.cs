
namespace WebAPI
{
    public class MagicfluAPITestData
    {
        public MagicfluAPITestData()
        {
            Items = new List<MagicfluAPITestDataItem>();
            Version = "0.1";
        }

        public string Version { get; set; }
        public int RecordCount
        {
            get => Items.Count;
        }


        public List<MagicfluAPITestDataItem> Items { get; set; }
    }

    public class MagicfluAPITestDataItem
    {
        public string Sequence { get; set; }
        public string Name { get; set; }
        public string CalculationMethod { get; set; }
        public string Total { get; set; }
    }
}