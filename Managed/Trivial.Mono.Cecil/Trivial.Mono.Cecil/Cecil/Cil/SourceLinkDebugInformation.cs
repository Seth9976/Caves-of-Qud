using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x0200012B RID: 299
	public sealed class SourceLinkDebugInformation : CustomDebugInformation
	{
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00025907 File Offset: 0x00023B07
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x0002590F File Offset: 0x00023B0F
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

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00025918 File Offset: 0x00023B18
		public override CustomDebugInformationKind Kind
		{
			get
			{
				return CustomDebugInformationKind.SourceLink;
			}
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002591B File Offset: 0x00023B1B
		public SourceLinkDebugInformation(string content)
			: base(SourceLinkDebugInformation.KindIdentifier)
		{
			this.content = content;
		}

		// Token: 0x040006AB RID: 1707
		internal string content;

		// Token: 0x040006AC RID: 1708
		public static Guid KindIdentifier = new Guid("{CC110556-A091-4D38-9FEC-25AB9A351A6A}");
	}
}
