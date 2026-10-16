using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEngine;

namespace Kobold
{
	// Token: 0x02000361 RID: 865
	public class Configuration
	{
		// Token: 0x06002031 RID: 8241 RVA: 0x0009DCB8 File Offset: 0x0009BEB8
		public static bool WildcardCompare(string pattern, string text, bool caseSensitive = false)
		{
			pattern = pattern.Replace(".", "\\.");
			pattern = pattern.Replace("?", ".");
			pattern = pattern.Replace("*", ".*?");
			pattern = pattern.Replace("\\", "\\\\");
			pattern = pattern.Replace("\\", "\\/");
			pattern = pattern.Replace(" ", "\\s");
			return new Regex(pattern, caseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase).IsMatch(text);
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x0009DD44 File Offset: 0x0009BF44
		public ImportDefinition DetermineImport(string FileName)
		{
			ImportDefinition importDefinition = null;
			foreach (ImportDefinition importDefinition2 in this.Imports)
			{
				if (Configuration.WildcardCompare(importDefinition2.Spec, FileName.Replace('\\', '/'), false))
				{
					importDefinition = importDefinition2;
				}
			}
			return importDefinition;
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x0009DDB0 File Offset: 0x0009BFB0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BasePath: " + this.BasePath);
			foreach (ImportDefinition importDefinition in this.Imports)
			{
				stringBuilder.Append("\nImport Definition: " + importDefinition.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x0009DE38 File Offset: 0x0009C038
		public static Configuration LoadXML(string BasePath, TextAsset Asset)
		{
			Configuration configuration = new Configuration();
			configuration.BasePath = BasePath;
			XmlReader xmlReader = XmlReader.Create(new StringReader(Asset.text));
			while (xmlReader.Read())
			{
				if (xmlReader.Name == "import")
				{
					configuration.Imports.Add(Configuration.LoadImport(xmlReader));
				}
			}
			return configuration;
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x0009DE94 File Offset: 0x0009C094
		public static ImportDefinition LoadImport(XmlReader Reader)
		{
			ImportDefinition importDefinition = new ImportDefinition();
			importDefinition.Spec = Reader.GetAttribute("spec");
			while (Reader.Read())
			{
				if (Reader.Name == "atlas" && Reader.NodeType != XmlNodeType.EndElement)
				{
					importDefinition.DiffuseAtlas = new AtlasDefinition();
					importDefinition.DiffuseAtlas.Compression = Reader.GetAttribute("Compression");
					importDefinition.DiffuseAtlas.Prefix = Reader.GetAttribute("Name");
					importDefinition.DiffuseAtlas.XSize = (int)Convert.ToInt16(Reader.GetAttribute("SizeX"));
					importDefinition.DiffuseAtlas.YSize = (int)Convert.ToInt16(Reader.GetAttribute("SizeY"));
					if (string.IsNullOrEmpty(Reader.GetAttribute("Trim")))
					{
						importDefinition.DiffuseAtlas.Trim = false;
					}
					else
					{
						importDefinition.DiffuseAtlas.Trim = Convert.ToBoolean(Reader.GetAttribute("Trim"));
					}
				}
				if (Reader.NodeType == XmlNodeType.EndElement && Reader.Name == "import")
				{
					return importDefinition;
				}
			}
			return importDefinition;
		}

		// Token: 0x0400106D RID: 4205
		public string BasePath;

		// Token: 0x0400106E RID: 4206
		public List<ImportDefinition> Imports = new List<ImportDefinition>();
	}
}
