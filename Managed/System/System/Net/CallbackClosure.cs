using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000561 RID: 1377
	internal class CallbackClosure
	{
		// Token: 0x06002C9D RID: 11421 RVA: 0x00098092 File Offset: 0x00096292
		internal CallbackClosure(ExecutionContext context, AsyncCallback callback)
		{
			if (callback != null)
			{
				this._savedCallback = callback;
				this._savedContext = context;
			}
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x000980AB File Offset: 0x000962AB
		internal bool IsCompatible(AsyncCallback callback)
		{
			return callback != null && this._savedCallback != null && object.Equals(this._savedCallback, callback);
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002C9F RID: 11423 RVA: 0x000980CB File Offset: 0x000962CB
		internal AsyncCallback AsyncCallback
		{
			get
			{
				return this._savedCallback;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002CA0 RID: 11424 RVA: 0x000980D3 File Offset: 0x000962D3
		internal ExecutionContext Context
		{
			get
			{
				return this._savedContext;
			}
		}

		// Token: 0x04001819 RID: 6169
		private AsyncCallback _savedCallback;

		// Token: 0x0400181A RID: 6170
		private ExecutionContext _savedContext;
	}
}
