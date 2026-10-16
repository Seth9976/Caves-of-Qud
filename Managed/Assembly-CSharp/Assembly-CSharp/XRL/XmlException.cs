using System;
using System.Xml;

namespace XRL
{
	// Token: 0x02000529 RID: 1321
	internal class XmlException : Exception
	{
		// Token: 0x060038A5 RID: 14501 RVA: 0x001164D0 File Offset: 0x001146D0
		public XmlException(string Message, XmlTextReader Reader)
			: base(Message + " at line " + Reader.LineNumber.ToString())
		{
		}
	}
}
