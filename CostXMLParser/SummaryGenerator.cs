using System.Globalization;
using System.Text;
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
        public List<SummaryItem> Children { get; set; }
        public XElement XDoc { get; set; }
        public SummaryItem()
        {
            Children = new List<SummaryItem>();
        }
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
                    Children.Add(new SummaryItem(child));
                }
            }
        }
        public override string ToString()
        {
            return Sequence + " " + Name + " " + CalculationMethod + " " + Total;
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
                    Items.Add(new SummaryItem(item));
                }
            }

            // generate display items
            DisplayItems = new List<SummaryItem>();
            // create a dummy root
            var root = new SummaryItem();
            foreach (var item in Items)
            {
                root.Children.Add(item);
            }
            // recursively generate display items
            GenerateDisplayItems(root, "");
            //Console.WriteLine("Display Table: ");
            foreach (var item in DisplayItems)
            {
                Console.WriteLine(item.ToString());
            }

        }

        // recursive call to flatten the tree structure to a single level
        void GenerateDisplayItems(SummaryItem item, String prefix)
        {
            if (item.Children.Count == 0)
            {
                return;
            }
            for (int i = 0; i < item.Children.Count; i++)
            {
                var child = item.Children[i];
                if (prefix.Equals(""))
                {
                    child.Sequence = (i + 1).ToString(CultureInfo.InvariantCulture); // or every item will have a prefix "."
                }
                else
                {
                    child.Sequence = prefix + "." + (i + 1).ToString(CultureInfo.InvariantCulture);
                }
                DisplayItems.Add(child);
                GenerateDisplayItems(child, child.Sequence);
            }
        }

        public string ToCSV()
        {
            var sb = new StringBuilder();
            sb.AppendLine("INDEX,ITEM_NAME,CALCULATION_METHOD,TOTAL");
            foreach (var item in DisplayItems)
            {
                sb.Append(item.Sequence + ",");

                // if for some reason the name contains a comma, replace it with a chinese comma
                if (item.Name.Contains(','))
                {
                    var sanitized = item.Name.Replace(',', '，');
                    sb.Append(sanitized + ",");
                }
                else
                {
                    sb.Append(item.Name + ",");
                }

                // if for some reason the calculation method contains a comma, replace it with a chinese comma
                if (item.CalculationMethod.Contains(','))
                {
                    var sanitized = item.CalculationMethod.Replace(',', '，');
                    sb.Append(sanitized + ",");
                }
                else
                {
                    sb.Append(item.CalculationMethod + ",");
                }

                sb.Append(item.Total.ToString(CultureInfo.InvariantCulture));
                sb.AppendLine();

            }
            return sb.ToString();
        }
    }
}
