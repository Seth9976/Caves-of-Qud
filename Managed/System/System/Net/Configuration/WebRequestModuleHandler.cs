using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x02000783 RID: 1923
	internal class WebRequestModuleHandler : IConfigurationSectionHandler
	{
		// Token: 0x06003C90 RID: 15504 RVA: 0x000CE46C File Offset: 0x000CC66C
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			if (section.Attributes != null && section.Attributes.Count != 0)
			{
				HandlersUtil.ThrowException("Unrecognized attribute", section);
			}
			foreach (object obj in section.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType != XmlNodeType.Element)
					{
						HandlersUtil.ThrowException("Only elements allowed", xmlNode);
					}
					string name = xmlNode.Name;
					if (name == "clear")
					{
						if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
						{
							HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
						}
						WebRequest.PrefixList = new ArrayList();
					}
					else
					{
						if (name == "add")
						{
							if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
							{
								HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
							}
							throw new NotImplementedException();
						}
						if (name == "remove")
						{
							if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
							{
								HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
							}
							throw new NotImplementedException();
						}
						HandlersUtil.ThrowException("Unexpected element", xmlNode);
					}
				}
			}
			return null;
		}
	}
}
