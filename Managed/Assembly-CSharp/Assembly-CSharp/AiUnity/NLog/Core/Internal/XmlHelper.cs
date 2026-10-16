using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace AiUnity.NLog.Core.Internal
{
	// Token: 0x02000273 RID: 627
	public static class XmlHelper
	{
		// Token: 0x06001682 RID: 5762 RVA: 0x00078F5A File Offset: 0x0007715A
		private static string RemoveInvalidXmlChars(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				return XmlHelper.InvalidXmlChars.Replace(text, "");
			}
			return "";
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00078F7A File Offset: 0x0007717A
		public static void WriteAttributeSafeString(this XmlWriter writer, string prefix, string localName, string ns, string value)
		{
			writer.WriteAttributeString(XmlHelper.RemoveInvalidXmlChars(prefix), XmlHelper.RemoveInvalidXmlChars(localName), XmlHelper.RemoveInvalidXmlChars(ns), XmlHelper.RemoveInvalidXmlChars(value));
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00078F9B File Offset: 0x0007719B
		public static void WriteAttributeSafeString(this XmlWriter writer, string thread, string localName)
		{
			writer.WriteAttributeString(XmlHelper.RemoveInvalidXmlChars(thread), XmlHelper.RemoveInvalidXmlChars(localName));
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00078FAF File Offset: 0x000771AF
		public static void WriteElementSafeString(this XmlWriter writer, string prefix, string localName, string ns, string value)
		{
			writer.WriteElementString(XmlHelper.RemoveInvalidXmlChars(prefix), XmlHelper.RemoveInvalidXmlChars(localName), XmlHelper.RemoveInvalidXmlChars(ns), XmlHelper.RemoveInvalidXmlChars(value));
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00078FD0 File Offset: 0x000771D0
		public static void WriteSafeCData(this XmlWriter writer, string text)
		{
			writer.WriteCData(XmlHelper.RemoveInvalidXmlChars(text));
		}

		// Token: 0x04000DE2 RID: 3554
		private static readonly Regex InvalidXmlChars = new Regex("(?<![\\uD800-\\uDBFF])[\\uDC00-\\uDFFF]|[\\uD800-\\uDBFF](?![\\uDC00-\\uDFFF])|[\\x00-\\x08\\x0B\\x0C\\x0E-\\x1F\\x7F-\\x9F\\uFEFF\\uFFFE\\uFFFF]");
	}
}
