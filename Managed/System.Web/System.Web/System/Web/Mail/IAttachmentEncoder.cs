using System;
using System.IO;

namespace System.Web.Mail
{
	// Token: 0x020000EE RID: 238
	internal interface IAttachmentEncoder
	{
		// Token: 0x06000CCF RID: 3279
		void EncodeStream(Stream ins, Stream outs);
	}
}
