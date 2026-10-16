using System;
using System.Xml;

namespace XRL
{
	// Token: 0x0200052A RID: 1322
	internal class XmlUnsupportedElementException : XmlException
	{
		// Token: 0x060038A6 RID: 14502 RVA: 0x001164FC File Offset: 0x001146FC
		public XmlUnsupportedElementException(XmlTextReader Reader)
			: base("unsupported element " + Reader.Name, Reader)
		{
		}
	}
}
