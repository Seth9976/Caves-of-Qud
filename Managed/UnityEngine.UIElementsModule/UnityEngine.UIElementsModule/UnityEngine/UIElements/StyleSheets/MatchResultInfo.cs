using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006DD RID: 1757
	internal struct MatchResultInfo
	{
		// Token: 0x060032EF RID: 13039 RVA: 0x000C559E File Offset: 0x000C379E
		public MatchResultInfo(bool success, PseudoStates triggerPseudoMask, PseudoStates dependencyPseudoMask)
		{
			this.success = success;
			this.triggerPseudoMask = triggerPseudoMask;
			this.dependencyPseudoMask = dependencyPseudoMask;
		}

		// Token: 0x040019E9 RID: 6633
		public readonly bool success;

		// Token: 0x040019EA RID: 6634
		public readonly PseudoStates triggerPseudoMask;

		// Token: 0x040019EB RID: 6635
		public readonly PseudoStates dependencyPseudoMask;
	}
}
