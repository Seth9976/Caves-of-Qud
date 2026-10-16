using System;

namespace UnityEngine.Android
{
	// Token: 0x0200001D RID: 29
	public class AndroidAssetPackUseMobileDataRequestResult
	{
		// Token: 0x06000270 RID: 624 RVA: 0x0000A374 File Offset: 0x00008574
		internal AndroidAssetPackUseMobileDataRequestResult(bool allowed)
		{
			this.allowed = allowed;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000A385 File Offset: 0x00008585
		public bool allowed { get; }
	}
}
