namespace SLD2MAP
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Xml;

	public class Program
	{
		private const string filename = @"sample\sld.xml";

		private static void Main()
		{
			List<string> lines = new List<string>();
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(filename);

			XmlNodeList nodes = xmlDoc.SelectNodes("//*");

			foreach (XmlNode xmlNode in nodes)
			{
				if (xmlNode.Name == "ColorMapEntry")
				{
					// Remove the # or the hex color e.g. #0080FF
					string color = xmlNode.Attributes["color"].Value.Remove(0, 1);
					string classid = xmlNode.Attributes["quantity"].Value;
					string classname = xmlNode.Attributes["label"].Value;

					int r = Convert.ToInt32(color.Substring(0, 2), 16);
					int g = Convert.ToInt32(color.Substring(2, 2), 16);
					int b = Convert.ToInt32(color.Substring(4, 2), 16);
					lines.Add(
						string.Format(
							"CLASS{0}  EXPRESSION ([pixel]=={1}){0}  NAME \"{2}\"{0}  STYLE{0}    COLOR {3} {4} {5}{0}  END{0}END{0}",
							Environment.NewLine, classid, classname, r, g, b));
				}
			}
			File.WriteAllLines(filename.Replace(".xml", "") + "2map.txt", lines);
			Console.WriteLine("Done. press a key");
			Console.ReadKey();
		}
	}
}