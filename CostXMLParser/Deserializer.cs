using System.Xml.Linq;

namespace CostXMLParser
{
    public class UnitProject
    {
        public XElement XDoc;
        public SummaryTable SummaryTable;
        public UnitProject(XElement xdoc)
        {
            XDoc = xdoc;

            Console.WriteLine("Step 3: Parsing Summary Table...");

            SummaryTable = new SummaryTable(XDoc.Element("Summary"));
        }
    }

    public class SingleProject
    {
        public XElement XDoc;
        public UnitProject[] UnitProjects;

        public SingleProject(XElement xdoc)
        {
            XDoc = xdoc;

            Console.WriteLine("Step 2: Parsing Unit Projects...");

            UnitProjects = Deserializer.ExtractUnitProjects(XDoc);
        }
    }

    public class ConstructProject
    {
        public XElement XDoc;
        public SingleProject[] SingleProjects;
        public String ProjectName;

        public ConstructProject(XElement xdoc)
        {
            XDoc = xdoc;
            ProjectName = XDoc.Attribute("Name").Value;
            Console.WriteLine("Project Name: " + ProjectName);
            Console.WriteLine("Step 1: Parsing Single Projects...");
            SingleProjects = Deserializer.ExtractSingleProjects(XDoc);
        }
    }

    /// <summary>
    /// This class contains the deserialization stage
    /// will covert the raw dom to a data object that is easier to deal with.
    /// </summary>
    public class Deserializer
    {
        private XDocument _xml_root;

        public ConstructProject Project;
        // constructor, read from file
        public Deserializer(string path)
        {
            ReadFromFile(path);

            if (CheckCompatibility())
            {
                Console.WriteLine("Start Parsing...");
            }
            else
            {
                Console.WriteLine("Parsing Failed");
                throw new System.Exception("Parsing Failed");
            }

            Project = new ConstructProject(_xml_root.Element("ConstructProject"));



            Console.WriteLine("Parsing Finished");

        }

        void ReadFromFile(string path)
        {
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(path);

                _xml_root = XDocument.Load(sr);

                //close the file
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception When Parsing: " + e.Message);
            }
        }

        bool CheckCompatibility()
        {
            if (_xml_root.Element("ConstructProject") == null)
            {
                Console.WriteLine("Error: This is not a Construct Project File");
                return false;
            }
            else
            {
                var standard = _xml_root.Element("ConstructProject").Attribute("Standard").Value;
                var standardVer = _xml_root.Element("ConstructProject").Attribute("StandardVer").Value;
                if (standard.Equals("云南省工程造价数据交换标准"))
                {
                    Console.WriteLine("Standard: " + standard);
                }
                else
                {
                    Console.WriteLine("Unknown Standard: " + standard);
                    Console.WriteLine("Will try to parse anyway");
                }

                if (standardVer.Equals("2.1")) // TODO: replace with a array of supported versions
                {
                    Console.WriteLine("Standard Version: " + standardVer);
                }
                else
                {
                    Console.WriteLine("Unknown Standard Version: " + standardVer);
                    Console.WriteLine("Will try to parse anyway");
                }
                return true;
            }
        }

        // generate a array of SingleProject from the DOM of ConstructProject
        public static SingleProject[] ExtractSingleProjects(XElement xmlConstructProject)
        {
            var SingleProjects = new List<SingleProject>();
            foreach (var item in xmlConstructProject.Elements("SingleProject"))
            {
                SingleProjects.Add(new SingleProject(item));
            }
            return SingleProjects.ToArray();
        }

        // generate a array of UnitProject from the DOM of SingleProject
        public static UnitProject[] ExtractUnitProjects(XElement xSingleProject)
        {
            var unitProjects = new List<UnitProject>();

            foreach (var item in xSingleProject.Elements("UnitProject"))
            {
                unitProjects.Add(new UnitProject(item));
            }

            return unitProjects.ToArray();
        }
    }
}