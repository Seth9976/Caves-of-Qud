using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C6 RID: 710
	public interface IRuntimePanel : IPanel, IDisposable
	{
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001471 RID: 5233
		PanelSettings panelSettings { get; }

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001472 RID: 5234
		// (set) Token: 0x06001473 RID: 5235
		GameObject selectableGameObject { get; set; }
	}
}
