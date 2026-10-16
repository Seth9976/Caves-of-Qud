using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000022 RID: 34
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Field)]
	internal class IgnoreAttribute : Attribute, IBindingsAttribute
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000256F File Offset: 0x0000076F
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002577 File Offset: 0x00000777
		public bool DoesNotContributeToSize { get; set; }
	}
}
