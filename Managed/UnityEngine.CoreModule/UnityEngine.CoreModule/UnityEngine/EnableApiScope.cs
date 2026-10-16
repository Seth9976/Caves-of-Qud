using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000250 RID: 592
	[NativeHeader("Runtime/Scripting/ApiRestrictions.h")]
	internal readonly struct EnableApiScope : IDisposable
	{
		// Token: 0x06001F15 RID: 7957 RVA: 0x0003BBCA File Offset: 0x00039DCA
		public EnableApiScope(ApiRestrictions.ContextRestrictions api, Object context)
		{
			this.m_ContextApi = api;
			this.m_Context = context;
			ApiRestrictions.PopDisableApi(api, context);
			this.m_GlobalApi = ApiRestrictions.GlobalRestrictions.GLOBALCOUNT;
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x0003BBEA File Offset: 0x00039DEA
		public EnableApiScope(ApiRestrictions.GlobalRestrictions api)
		{
			this.m_GlobalApi = api;
			this.m_Context = null;
			ApiRestrictions.PopDisableApi(api);
			this.m_ContextApi = ApiRestrictions.ContextRestrictions.CONTEXTCOUNT;
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0003BC0C File Offset: 0x00039E0C
		public void Dispose()
		{
			bool flag = this.m_Context != null;
			if (flag)
			{
				ApiRestrictions.PushDisableApi(this.m_ContextApi, this.m_Context);
			}
			else
			{
				ApiRestrictions.PushDisableApi(this.m_GlobalApi);
			}
		}

		// Token: 0x0400092D RID: 2349
		private readonly ApiRestrictions.ContextRestrictions m_ContextApi;

		// Token: 0x0400092E RID: 2350
		private readonly ApiRestrictions.GlobalRestrictions m_GlobalApi;

		// Token: 0x0400092F RID: 2351
		private readonly Object m_Context;
	}
}
