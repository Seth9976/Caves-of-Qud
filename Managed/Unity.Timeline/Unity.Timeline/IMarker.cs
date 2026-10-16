using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000026 RID: 38
	public interface IMarker
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000228 RID: 552
		// (set) Token: 0x06000229 RID: 553
		double time { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600022A RID: 554
		TrackAsset parent { get; }

		// Token: 0x0600022B RID: 555
		void Initialize(TrackAsset parent);
	}
}
