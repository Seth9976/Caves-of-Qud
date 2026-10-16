using System;
using System.Configuration;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x0200059E RID: 1438
	internal sealed class HandlersUtil
	{
		// Token: 0x06003D15 RID: 15637 RVA: 0x00002050 File Offset: 0x00000250
		private HandlersUtil()
		{
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x000A01DC File Offset: 0x0009E3DC
		public static string ExtractAttributeValue(string attKey, XmlNode node)
		{
			return HandlersUtil.ExtractAttributeValue(attKey, node, false);
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x000A01E6 File Offset: 0x0009E3E6
		public static string ExtractAttributeValue(string attKey, XmlNode node, bool optional)
		{
			return HandlersUtil.ExtractAttributeValue(attKey, node, optional, false);
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x000A01F4 File Offset: 0x0009E3F4
		public static string ExtractAttributeValue(string attKey, XmlNode node, bool optional, bool allowEmpty)
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
			if (!allowEmpty && value == string.Empty)
			{
				HandlersUtil.ThrowException((optional ? "Optional" : "Required") + " attribute is empty: " + attKey, node);
			}
			return value;
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x000A027D File Offset: 0x0009E47D
		public static void ThrowException(string msg, XmlNode node)
		{
			if (node != null && node.Name != string.Empty)
			{
				msg = msg + " (node name: " + node.Name + ") ";
			}
			throw new ConfigurationException(msg, node);
		}
	}
}
