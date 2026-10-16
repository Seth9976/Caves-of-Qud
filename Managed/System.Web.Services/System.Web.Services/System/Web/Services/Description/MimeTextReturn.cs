using System;

namespace System.Web.Services.Description
{
	// Token: 0x020000DB RID: 219
	internal class MimeTextReturn : MimeReturn
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00018D9E File Offset: 0x00016F9E
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x00018DA6 File Offset: 0x00016FA6
		internal MimeTextBinding TextBinding
		{
			get
			{
				return this.textBinding;
			}
			set
			{
				this.textBinding = value;
			}
		}

		// Token: 0x0400039D RID: 925
		private MimeTextBinding textBinding;
	}
}
