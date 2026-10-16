using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200024F RID: 591
	[NativeHeader("Runtime/Scripting/ApiRestrictions.h")]
	internal readonly struct DisableApiScope : IDisposable
	{
		// Token: 0x06001F12 RID: 7954 RVA: 0x0003BB47 File Offset: 0x00039D47
		public DisableApiScope(ApiRestrictions.ContextRestrictions api, Object context)
		{
			this.m_ContextApi = api;
			this.m_Context = context;
			ApiRestrictions.PushDisableApi(api, context);
			this.m_GlobalApi = ApiRestrictions.GlobalRestrictions.GLOBALCOUNT;
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x0003BB67 File Offset: 0x00039D67
		public DisableApiScope(ApiRestrictions.GlobalRestrictions api)
		{
			this.m_GlobalApi = api;
			this.m_Context = null;
			ApiRestrictions.PushDisableApi(api);
			this.m_ContextApi = ApiRestrictions.ContextRestrictions.CONTEXTCOUNT;
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x0003BB88 File Offset: 0x00039D88
		public void Dispose()
		{
			bool flag = this.m_Context != null;
			if (flag)
			{
				ApiRestrictions.PopDisableApi(this.m_ContextApi, this.m_Context);
			}
			else
			{
				ApiRestrictions.PopDisableApi(this.m_GlobalApi);
			}
		}

		// Token: 0x0400092A RID: 2346
		private readonly ApiRestrictions.ContextRestrictions m_ContextApi;

		// Token: 0x0400092B RID: 2347
		private readonly ApiRestrictions.GlobalRestrictions m_GlobalApi;

		// Token: 0x0400092C RID: 2348
		private readonly Object m_Context;
	}
}
