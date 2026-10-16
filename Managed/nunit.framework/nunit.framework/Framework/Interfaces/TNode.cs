using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000E2 RID: 226
	public class TNode
	{
		// Token: 0x0600071E RID: 1822 RVA: 0x000113E7 File Offset: 0x0000F5E7
		public TNode(string name)
		{
			this.Name = name;
			this.Attributes = new AttributeDictionary();
			this.ChildNodes = new NodeList();
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001140C File Offset: 0x0000F60C
		public TNode(string name, string value)
			: this(name, value, false)
		{
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00011417 File Offset: 0x0000F617
		public TNode(string name, string value, bool valueIsCDATA)
			: this(name)
		{
			this.Value = value;
			this.ValueIsCDATA = valueIsCDATA;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001142E File Offset: 0x0000F62E
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x00011436 File Offset: 0x0000F636
		public string Name { get; private set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001143F File Offset: 0x0000F63F
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x00011447 File Offset: 0x0000F647
		public string Value { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00011450 File Offset: 0x0000F650
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x00011458 File Offset: 0x0000F658
		public bool ValueIsCDATA { get; private set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x00011461 File Offset: 0x0000F661
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x00011469 File Offset: 0x0000F669
		public AttributeDictionary Attributes { get; private set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00011472 File Offset: 0x0000F672
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0001147A File Offset: 0x0000F67A
		public NodeList ChildNodes { get; private set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x00011483 File Offset: 0x0000F683
		public TNode FirstChild
		{
			get
			{
				if (this.ChildNodes.Count != 0)
				{
					return this.ChildNodes[0];
				}
				return null;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x000114A0 File Offset: 0x0000F6A0
		public string OuterXml
		{
			get
			{
				StringWriter stringWriter = new StringWriter();
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
				{
					ConformanceLevel = ConformanceLevel.Fragment
				}))
				{
					this.WriteTo(xmlWriter);
				}
				return stringWriter.ToString();
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x000114F4 File Offset: 0x0000F6F4
		public static TNode FromXml(string xmlText)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xmlText);
			return TNode.FromXml(xmlDocument.FirstChild);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001150C File Offset: 0x0000F70C
		public TNode AddElement(string name)
		{
			TNode tnode = new TNode(name);
			this.ChildNodes.Add(tnode);
			return tnode;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00011530 File Offset: 0x0000F730
		public TNode AddElement(string name, string value)
		{
			TNode tnode = new TNode(name, TNode.EscapeInvalidXmlCharacters(value));
			this.ChildNodes.Add(tnode);
			return tnode;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00011558 File Offset: 0x0000F758
		public TNode AddElementWithCDATA(string name, string value)
		{
			TNode tnode = new TNode(name, TNode.EscapeInvalidXmlCharacters(value), true);
			this.ChildNodes.Add(tnode);
			return tnode;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00011580 File Offset: 0x0000F780
		public void AddAttribute(string name, string value)
		{
			this.Attributes.Add(name, TNode.EscapeInvalidXmlCharacters(value));
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00011594 File Offset: 0x0000F794
		public TNode SelectSingleNode(string xpath)
		{
			NodeList nodeList = this.SelectNodes(xpath);
			if (nodeList.Count <= 0)
			{
				return null;
			}
			return nodeList[0];
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x000115BB File Offset: 0x0000F7BB
		public NodeList SelectNodes(string xpath)
		{
			return TNode.ApplySelection(new NodeList { this }, xpath);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x000115D0 File Offset: 0x0000F7D0
		public void WriteTo(XmlWriter writer)
		{
			writer.WriteStartElement(this.Name);
			foreach (string text in this.Attributes.Keys)
			{
				writer.WriteAttributeString(text, this.Attributes[text]);
			}
			if (this.Value != null)
			{
				if (this.ValueIsCDATA)
				{
					this.WriteCDataTo(writer);
				}
				else
				{
					writer.WriteString(this.Value);
				}
			}
			foreach (TNode tnode in this.ChildNodes)
			{
				tnode.WriteTo(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000116AC File Offset: 0x0000F8AC
		private static TNode FromXml(XmlNode xmlNode)
		{
			TNode tnode = new TNode(xmlNode.Name, xmlNode.InnerText);
			foreach (object obj in xmlNode.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				tnode.AddAttribute(xmlAttribute.Name, xmlAttribute.Value);
			}
			foreach (object obj2 in xmlNode.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj2;
				if (xmlNode2.NodeType == XmlNodeType.Element)
				{
					tnode.ChildNodes.Add(TNode.FromXml(xmlNode2));
				}
			}
			return tnode;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00011784 File Offset: 0x0000F984
		private static NodeList ApplySelection(NodeList nodeList, string xpath)
		{
			Guard.ArgumentNotNullOrEmpty(xpath, "xpath");
			if (xpath[0] == '/')
			{
				throw new ArgumentException("XPath expressions starting with '/' are not supported", "xpath");
			}
			if (xpath.IndexOf("//") >= 0)
			{
				throw new ArgumentException("XPath expressions with '//' are not supported", "xpath");
			}
			string text = xpath;
			string text2 = null;
			int num = xpath.IndexOf('/');
			if (num >= 0)
			{
				text = xpath.Substring(0, num);
				text2 = xpath.Substring(num + 1);
			}
			NodeList nodeList2 = new NodeList();
			TNode.NodeFilter nodeFilter = new TNode.NodeFilter(text);
			foreach (TNode tnode in nodeList)
			{
				foreach (TNode tnode2 in tnode.ChildNodes)
				{
					if (nodeFilter.Pass(tnode2))
					{
						nodeList2.Add(tnode2);
					}
				}
			}
			if (text2 == null)
			{
				return nodeList2;
			}
			return TNode.ApplySelection(nodeList2, text2);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001189C File Offset: 0x0000FA9C
		private static string EscapeInvalidXmlCharacters(string str)
		{
			if (str == null)
			{
				return null;
			}
			return TNode.InvalidXmlCharactersRegex.Replace(str, (Match match) => TNode.CharToUnicodeSequence(match.Value[0]));
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000118D0 File Offset: 0x0000FAD0
		private static string CharToUnicodeSequence(char symbol)
		{
			string text = "\\u{0}";
			int num = (int)symbol;
			return string.Format(text, num.ToString("x4"));
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x000118F8 File Offset: 0x0000FAF8
		private void WriteCDataTo(XmlWriter writer)
		{
			int num = 0;
			string value = this.Value;
			do
			{
				int num2 = value.IndexOf("]]>", num);
				if (num2 < 0)
				{
					goto IL_003A;
				}
				writer.WriteCData(value.Substring(num, num2 - num + 2));
				num = num2 + 2;
			}
			while (num < value.Length);
			return;
			IL_003A:
			if (num > 0)
			{
				writer.WriteCData(value.Substring(num));
				return;
			}
			writer.WriteCData(value);
		}

		// Token: 0x04000194 RID: 404
		private static readonly Regex InvalidXmlCharactersRegex = new Regex("[^\t\n\r -\ufffd]|([\ud800-\udbff](?![\udc00-\udfff]))|((?<![\ud800-\udbff])[\udc00-\udfff])");

		// Token: 0x02000193 RID: 403
		private class NodeFilter
		{
			// Token: 0x06000AEE RID: 2798 RVA: 0x0001AC14 File Offset: 0x00018E14
			public NodeFilter(string xpath)
			{
				this._nodeName = xpath;
				int num = xpath.IndexOf('[');
				if (num >= 0)
				{
					if (!xpath.EndsWith("]"))
					{
						throw new ArgumentException("Invalid property expression", "xpath");
					}
					this._nodeName = xpath.Substring(0, num);
					string text = xpath.Substring(num + 1, xpath.Length - num - 2);
					int num2 = text.IndexOf('=');
					if (num2 < 0 || text[0] != '@')
					{
						throw new ArgumentException("Invalid property expression", "xpath");
					}
					this._propName = text.Substring(1, num2 - 1).Trim();
					this._propValue = text.Substring(num2 + 1).Trim(new char[] { ' ', '"', '\'' });
				}
			}

			// Token: 0x06000AEF RID: 2799 RVA: 0x0001ACDF File Offset: 0x00018EDF
			public bool Pass(TNode node)
			{
				return !(node.Name != this._nodeName) && (this._propName == null || node.Attributes[this._propName] == this._propValue);
			}

			// Token: 0x04000299 RID: 665
			private string _nodeName;

			// Token: 0x0400029A RID: 666
			private string _propName;

			// Token: 0x0400029B RID: 667
			private string _propValue;
		}
	}
}
