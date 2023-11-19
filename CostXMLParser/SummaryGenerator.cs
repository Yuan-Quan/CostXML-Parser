using System.Xml.Linq;

namespace CostXMLParser
{

    // represents a single summary item
    // all parsing is done in the constructor
    public class SummaryItem
    {
        public string? Sequence { get; set; }
        public string Name { get; set; }
        public string CalculationMethod { get; set; }
        public double Total { get; set; }
        public List<SummaryItem>? Children { get; set; }
        public XElement XDoc { get; set; }
        public SummaryItem(XElement xSummaryItem)
        {
            XDoc = xSummaryItem;
            Name = XDoc.Attribute("Name").Value;
            CalculationMethod = XDoc.Attribute("Remark").Value;
            Total = double.Parse(XDoc.Attribute("Total").Value);
            Children = new List<SummaryItem>();
            if (XDoc.Elements("SummaryItem").Count() != 0)
            {
                foreach (var child in XDoc.Elements("SummaryItem"))
                {
                    _ = Children.Append(new SummaryItem(child));
                }
            }
        }
    }

    // represents a summary table
    // all parsing is done in the constructor
    public class SummaryTable
    {
        public XElement XDoc { get; set; }
        // preserve the original xml tree structure
        public List<SummaryItem> Items { get; set; }
        // only have one level of items for display
        public List<SummaryItem> DisplayItems { get; set; }

        public SummaryTable(XElement xdoc)
        {
            XDoc = xdoc;
            Items = new List<SummaryItem>();
            if (xdoc.Elements("SummaryItem").Count() != 0)
            {
                foreach (var item in xdoc.Elements("SummaryItem"))
                {
                    Items.Append(new SummaryItem(item));
                }
            }
        }

        void GenerateDisplayItems()
        {
        }
    }
}
