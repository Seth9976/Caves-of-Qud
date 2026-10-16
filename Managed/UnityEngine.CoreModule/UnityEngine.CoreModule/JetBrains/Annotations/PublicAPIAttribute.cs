using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000DB RID: 219
	[AttributeUsage(AttributeTargets.All, Inherited = false)]
	[MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
	public sealed class PublicAPIAttribute : Attribute
	{
		// Token: 0x06000461 RID: 1121 RVA: 0x00002082 File Offset: 0x00000282
		public PublicAPIAttribute()
		{
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00008686 File Offset: 0x00006886
		public PublicAPIAttribute([NotNull] string comment)
		{
			this.Comment = comment;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00008697 File Offset: 0x00006897
		[CanBeNull]
		public string Comment { get; }
	}
}
