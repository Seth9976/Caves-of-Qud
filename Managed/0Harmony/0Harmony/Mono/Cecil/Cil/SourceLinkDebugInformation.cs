using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002E8 RID: 744
	internal sealed class SourceLinkDebugInformation : CustomDebugInformation
	{
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x0003CE72 File Offset: 0x0003B072
		// (set) Token: 0x060012E2 RID: 4834 RVA: 0x0003CE7A File Offset: 0x0003B07A
		public string Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x0003CE83 File Offset: 0x0003B083
		public override CustomDebugInformationKind Kind
		{
			get
			{
				return CustomDebugInformationKind.SourceLink;
			}
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0003CE86 File Offset: 0x0003B086
		public SourceLinkDebugInformation(string content)
			: base(SourceLinkDebugInformation.KindIdentifier)
		{
			this.content = content;
		}

		// Token: 0x0400099B RID: 2459
		internal string content;

		// Token: 0x0400099C RID: 2460
		public static Guid KindIdentifier = new Guid("{CC110556-A091-4D38-9FEC-25AB9A351A6A}");
	}
}
