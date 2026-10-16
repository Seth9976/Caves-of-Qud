using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000DE RID: 222
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class MustUseReturnValueAttribute : Attribute
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x00002082 File Offset: 0x00000282
		public MustUseReturnValueAttribute()
		{
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000869F File Offset: 0x0000689F
		public MustUseReturnValueAttribute([NotNull] string justification)
		{
			this.Justification = justification;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x000086B0 File Offset: 0x000068B0
		[CanBeNull]
		public string Justification { get; }
	}
}
