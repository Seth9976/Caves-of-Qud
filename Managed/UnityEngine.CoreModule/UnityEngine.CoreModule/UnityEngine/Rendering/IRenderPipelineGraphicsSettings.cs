using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004CF RID: 1231
	public interface IRenderPipelineGraphicsSettings
	{
		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06002E43 RID: 11843
		int version { get; }

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06002E44 RID: 11844 RVA: 0x00057B3C File Offset: 0x00055D3C
		bool isAvailableInPlayerBuild
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x00002925 File Offset: 0x00000B25
		void Reset()
		{
		}
	}
}
