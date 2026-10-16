using System;
using System.Runtime.Serialization;

namespace System.Web.Configuration.nBrowser
{
	// Token: 0x020005F5 RID: 1525
	internal class Exception : Exception
	{
		// Token: 0x06004226 RID: 16934 RVA: 0x000AB391 File Offset: 0x000A9591
		public Exception()
		{
		}

		// Token: 0x06004227 RID: 16935 RVA: 0x000AB399 File Offset: 0x000A9599
		public Exception(string errorMessage)
			: base(errorMessage)
		{
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x000AB3A2 File Offset: 0x000A95A2
		public Exception(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x000251B1 File Offset: 0x000233B1
		protected Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
