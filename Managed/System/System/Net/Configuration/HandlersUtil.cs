using System;
using System.Configuration;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x02000767 RID: 1895
	internal class HandlersUtil
	{
		// Token: 0x06003BB7 RID: 15287 RVA: 0x0000219B File Offset: 0x0000039B
		private HandlersUtil()
		{
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x000CC950 File Offset: 0x000CAB50
		internal static string ExtractAttributeValue(string attKey, XmlNode node)
		{
			return HandlersUtil.ExtractAttributeValue(attKey, node, false);
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x000CC95C File Offset: 0x000CAB5C
		internal static string ExtractAttributeValue(string attKey, XmlNode node, bool optional)
		{
			if (node.Attributes == null)
			{
				if (optional)
				{
					return null;
				}
				HandlersUtil.ThrowException("Required attribute not found: " + attKey, node);
			}
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(attKey);
			if (xmlNode == null)
			{
				if (optional)
				{
					return null;
				}
				HandlersUtil.ThrowException("Required attribute not found: " + attKey, node);
			}
			string value = xmlNode.Value;
			if (value == string.Empty)
			{
				HandlersUtil.ThrowException((optional ? "Optional" : "Required") + " attribute is empty: " + attKey, node);
			}
			return value;
		}

		// Token: 0x06003BBA RID: 15290 RVA: 0x000CC9E0 File Offset: 0x000CABE0
		internal static void ThrowException(string msg, XmlNode node)
		{
			if (node != null && node.Name != string.Empty)
			{
				msg = msg + " (node name: " + node.Name + ") ";
			}
			throw new ConfigurationException(msg, node);
		}
	}
}
