using System;

namespace System.Web.Handlers
{
	// Token: 0x02000105 RID: 261
	[Serializable]
	internal class TraceNotAvailableException : HttpException
	{
		// Token: 0x06000D8A RID: 3466 RVA: 0x00024C84 File Offset: 0x00022E84
		public TraceNotAvailableException(bool notLocal)
			: base(notLocal ? 403 : 500, "Trace Error")
		{
			this.notLocal = notLocal;
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00024CA7 File Offset: 0x00022EA7
		internal override string Description
		{
			get
			{
				if (this.notLocal)
				{
					return "Trace is not enabled for remote clients.";
				}
				return "Trace.axd is not enabled in the configuration file for this application.";
			}
		}

		// Token: 0x0400116D RID: 4461
		private bool notLocal;
	}
}
