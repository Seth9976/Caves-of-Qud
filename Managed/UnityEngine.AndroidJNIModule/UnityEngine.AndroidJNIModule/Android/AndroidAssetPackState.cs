using System;

namespace UnityEngine.Android
{
	// Token: 0x0200001C RID: 28
	public class AndroidAssetPackState
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0000A33D File Offset: 0x0000853D
		internal AndroidAssetPackState(string name, AndroidAssetPackStatus status, AndroidAssetPackError error)
		{
			this.name = name;
			this.status = status;
			this.error = error;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000A35C File Offset: 0x0000855C
		public string name { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000A364 File Offset: 0x00008564
		public AndroidAssetPackStatus status { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000A36C File Offset: 0x0000856C
		public AndroidAssetPackError error { get; }
	}
}
