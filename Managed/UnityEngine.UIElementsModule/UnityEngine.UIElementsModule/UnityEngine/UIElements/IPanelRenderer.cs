using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200032F RID: 815
	internal interface IPanelRenderer
	{
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001793 RID: 6035
		// (set) Token: 0x06001794 RID: 6036
		bool forceGammaRendering { get; set; }

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001795 RID: 6037
		// (set) Token: 0x06001796 RID: 6038
		uint vertexBudget { get; set; }

		// Token: 0x06001797 RID: 6039
		void Reset();

		// Token: 0x06001798 RID: 6040
		void Render();
	}
}
