using System.Xml.Linq;

namespace CostXMLParser
{
    /// <summary>
    /// This class contains the deserialization stage
    /// will covert the raw dom to a data object that is easier to deal with.
    /// </summary>
    public class Deserializer
    {
        private XDocument _xml;

        // constructor, read from file
        public Deserializer(string path)
        {
            ReadFromFile(path);

            if (checkCompatibility())
            {
                Console.WriteLine("Start Parsing...");
            }
        }

        void ReadFromFile(string path)
        {
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(path);

                _xml = XDocument.Load(sr);

                //close the file
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception When Parsing: " + e.Message);
            }
        }

        bool checkCompatibility()
        {
            if (_xml.Element("ConstructProject") == null)
            {
                Console.WriteLine("Error: This is not a Construct Project File");
                return false;
            }
            else
            {
                var standard = _xml.Element("ConstructProject").Attribute("Standard").Value;
                var standardVer = _xml.Element("ConstructProject").Attribute("StandardVer").Value;
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
    }
}