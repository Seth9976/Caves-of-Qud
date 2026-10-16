using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000D8 RID: 216
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter | AttributeTargets.GenericParameter)]
	public sealed class MeansImplicitUseAttribute : Attribute
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x0000863A File Offset: 0x0000683A
		public MeansImplicitUseAttribute()
			: this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00008646 File Offset: 0x00006846
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags)
			: this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00008652 File Offset: 0x00006852
		public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags)
			: this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000865E File Offset: 0x0000685E
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00008676 File Offset: 0x00006876
		[UsedImplicitly]
		public ImplicitUseKindFlags UseKindFlags { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000867E File Offset: 0x0000687E
		[UsedImplicitly]
		public ImplicitUseTargetFlags TargetFlags { get; }
	}
}
