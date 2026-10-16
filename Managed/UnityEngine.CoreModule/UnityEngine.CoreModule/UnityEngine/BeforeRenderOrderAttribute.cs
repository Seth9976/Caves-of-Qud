using System;

namespace UnityEngine
{
	// Token: 0x02000159 RID: 345
	[AttributeUsage(AttributeTargets.Method)]
	public class BeforeRenderOrderAttribute : Attribute
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0001446B File Offset: 0x0001266B
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x00014473 File Offset: 0x00012673
		public int order { get; private set; }

		// Token: 0x06000AFF RID: 2815 RVA: 0x0001447C File Offset: 0x0001267C
		public BeforeRenderOrderAttribute(int order)
		{
			this.order = order;
		}
	}
}
