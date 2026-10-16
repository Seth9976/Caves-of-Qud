using System;
using System.Collections.Generic;
using System.Xml;

namespace AiUnity.NLog.Core.Config
{
	// Token: 0x02000297 RID: 663
	internal class NLogXmlElement
	{
		// Token: 0x06001780 RID: 6016 RVA: 0x0007B1F0 File Offset: 0x000793F0
		public NLogXmlElement(string inputUri)
			: this()
		{
			using (XmlReader xmlReader = XmlReader.Create(inputUri))
			{
				xmlReader.MoveToContent();
				this.Parse(xmlReader);
			}
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0007B234 File Offset: 0x00079434
		public NLogXmlElement(XmlReader reader)
			: this()
		{
			this.Parse(reader);
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0007B243 File Offset: 0x00079443
		private NLogXmlElement()
		{
			this.AttributeValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.Children = new List<NLogXmlElement>();
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x0007B266 File Offset: 0x00079466
		// (set) Token: 0x06001784 RID: 6020 RVA: 0x0007B26E File Offset: 0x0007946E
		public string LocalName { get; private set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x0007B277 File Offset: 0x00079477
		// (set) Token: 0x06001786 RID: 6022 RVA: 0x0007B27F File Offset: 0x0007947F
		public Dictionary<string, string> AttributeValues { get; private set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x0007B288 File Offset: 0x00079488
		// (set) Token: 0x06001788 RID: 6024 RVA: 0x0007B290 File Offset: 0x00079490
		public IList<NLogXmlElement> Children { get; private set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x0007B299 File Offset: 0x00079499
		// (set) Token: 0x0600178A RID: 6026 RVA: 0x0007B2A1 File Offset: 0x000794A1
		public string Value { get; private set; }

		// Token: 0x0600178B RID: 6027 RVA: 0x0007B2AC File Offset: 0x000794AC
		public IEnumerable<NLogXmlElement> Elements(string elementName)
		{
			List<NLogXmlElement> list = new List<NLogXmlElement>();
			foreach (NLogXmlElement nlogXmlElement in this.Children)
			{
				if (nlogXmlElement.LocalName.Equals(elementName, StringComparison.OrdinalIgnoreCase))
				{
					list.Add(nlogXmlElement);
				}
			}
			return list;
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0007B310 File Offset: 0x00079510
		public string GetRequiredAttribute(string attributeName)
		{
			string optionalAttribute = this.GetOptionalAttribute(attributeName, null);
			if (optionalAttribute == null)
			{
				throw new NLogConfigurationException(string.Concat(new string[] { "Expected ", attributeName, " on <", this.LocalName, " />" }));
			}
			return optionalAttribute;
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0007B360 File Offset: 0x00079560
		public bool GetOptionalBooleanAttribute(string attributeName, bool defaultValue)
		{
			string text;
			if (!this.AttributeValues.TryGetValue(attributeName, out text))
			{
				return defaultValue;
			}
			return Convert.ToBoolean(text);
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0007B388 File Offset: 0x00079588
		public string GetOptionalAttribute(string attributeName, string defaultValue)
		{
			string text;
			if (!this.AttributeValues.TryGetValue(attributeName, out text))
			{
				text = defaultValue;
			}
			return text;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x0007B3A8 File Offset: 0x000795A8
		public void AssertName(params string[] allowedNames)
		{
			foreach (string text in allowedNames)
			{
				if (this.LocalName.Equals(text, StringComparison.OrdinalIgnoreCase))
				{
					return;
				}
			}
			throw new InvalidOperationException(string.Concat(new string[]
			{
				"Assertion failed. Expected element name '",
				string.Join("|", allowedNames),
				"', actual: '",
				this.LocalName,
				"'."
			}));
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x0007B418 File Offset: 0x00079618
		private void Parse(XmlReader reader)
		{
			if (reader.MoveToFirstAttribute())
			{
				do
				{
					this.AttributeValues.Add(reader.LocalName, reader.Value);
				}
				while (reader.MoveToNextAttribute());
				reader.MoveToElement();
			}
			this.LocalName = reader.LocalName;
			if (!reader.IsEmptyElement)
			{
				while (reader.Read() && reader.NodeType != XmlNodeType.EndElement)
				{
					if (reader.NodeType == XmlNodeType.CDATA || reader.NodeType == XmlNodeType.Text)
					{
						this.Value += reader.Value;
					}
					else if (reader.NodeType == XmlNodeType.Element)
					{
						this.Children.Add(new NLogXmlElement(reader));
					}
				}
			}
		}
	}
}
