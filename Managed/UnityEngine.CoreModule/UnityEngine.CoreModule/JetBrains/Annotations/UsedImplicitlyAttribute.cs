using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000D7 RID: 215
	[AttributeUsage(AttributeTargets.All, Inherited = false)]
	public sealed class UsedImplicitlyAttribute : Attribute
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x000085EE File Offset: 0x000067EE
		public UsedImplicitlyAttribute()
			: this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000085FA File Offset: 0x000067FA
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags)
			: this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00008606 File Offset: 0x00006806
		public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
			: this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00008612 File Offset: 0x00006812
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000862A File Offset: 0x0000682A
		public ImplicitUseKindFlags UseKindFlags { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00008632 File Offset: 0x00006832
		public ImplicitUseTargetFlags TargetFlags { get; }
	}
}
