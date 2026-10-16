using System;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	// Token: 0x020000DD RID: 221
	internal class MimeXmlReturn : MimeReturn
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00019206 File Offset: 0x00017406
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x0001920E File Offset: 0x0001740E
		internal XmlTypeMapping TypeMapping
		{
			get
			{
				return this.mapping;
			}
			set
			{
				this.mapping = value;
			}
		}

		// Token: 0x0400039F RID: 927
		private XmlTypeMapping mapping;
	}
}
