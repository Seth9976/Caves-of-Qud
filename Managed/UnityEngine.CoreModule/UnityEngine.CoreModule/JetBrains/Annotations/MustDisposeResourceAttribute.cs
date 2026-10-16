using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000DF RID: 223
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Parameter)]
	public sealed class MustDisposeResourceAttribute : Attribute
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x000086B8 File Offset: 0x000068B8
		public MustDisposeResourceAttribute()
		{
			this.Value = true;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000086C9 File Offset: 0x000068C9
		public MustDisposeResourceAttribute(bool value)
		{
			this.Value = value;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x000086DA File Offset: 0x000068DA
		public bool Value { get; }
	}
}
