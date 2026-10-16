using System;
using System.Runtime.ExceptionServices;

namespace Mono.Net.Security
{
	// Token: 0x0200008D RID: 141
	internal class AsyncProtocolResult
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000227 RID: 551 RVA: 0x000063C0 File Offset: 0x000045C0
		public int UserResult { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000228 RID: 552 RVA: 0x000063C8 File Offset: 0x000045C8
		public ExceptionDispatchInfo Error { get; }

		// Token: 0x06000229 RID: 553 RVA: 0x000063D0 File Offset: 0x000045D0
		public AsyncProtocolResult(int result)
		{
			this.UserResult = result;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000063DF File Offset: 0x000045DF
		public AsyncProtocolResult(ExceptionDispatchInfo error)
		{
			this.Error = error;
		}
	}
}
