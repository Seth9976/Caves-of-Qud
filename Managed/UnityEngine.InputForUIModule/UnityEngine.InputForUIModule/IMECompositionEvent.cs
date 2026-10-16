using System;
using Unity.IntegerTime;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x02000011 RID: 17
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct IMECompositionEvent : IEventProperties
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002734 File Offset: 0x00000934
		// (set) Token: 0x0600003A RID: 58 RVA: 0x0000273C File Offset: 0x0000093C
		public DiscreteTime timestamp { readonly get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002745 File Offset: 0x00000945
		// (set) Token: 0x0600003C RID: 60 RVA: 0x0000274D File Offset: 0x0000094D
		public EventSource eventSource { readonly get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002756 File Offset: 0x00000956
		// (set) Token: 0x0600003E RID: 62 RVA: 0x0000275E File Offset: 0x0000095E
		public uint playerId { readonly get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002767 File Offset: 0x00000967
		// (set) Token: 0x06000040 RID: 64 RVA: 0x0000276F File Offset: 0x0000096F
		public EventModifiers eventModifiers { readonly get; set; }

		// Token: 0x06000041 RID: 65 RVA: 0x00002778 File Offset: 0x00000978
		public override string ToString()
		{
			return "IME '" + this.compositionString + "'";
		}

		// Token: 0x0400004E RID: 78
		public string compositionString;
	}
}
