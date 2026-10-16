using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000E1 RID: 225
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class PathReferenceAttribute : Attribute
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x00002082 File Offset: 0x00000282
		public PathReferenceAttribute()
		{
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000086E2 File Offset: 0x000068E2
		public PathReferenceAttribute([PathReference] [NotNull] string basePath)
		{
			this.BasePath = basePath;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x000086F3 File Offset: 0x000068F3
		[CanBeNull]
		public string BasePath { get; }
	}
}
