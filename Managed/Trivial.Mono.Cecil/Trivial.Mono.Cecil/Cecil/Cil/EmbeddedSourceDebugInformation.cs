using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000108 RID: 264
	public sealed class EmbeddedSourceDebugInformation : CustomDebugInformation
	{
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00022964 File Offset: 0x00020B64
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x0002296C File Offset: 0x00020B6C
		public byte[] Content
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

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00022975 File Offset: 0x00020B75
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x0002297D File Offset: 0x00020B7D
		public bool Compress
		{
			get
			{
				return this.compress;
			}
			set
			{
				this.compress = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00022986 File Offset: 0x00020B86
		public override CustomDebugInformationKind Kind
		{
			get
			{
				return CustomDebugInformationKind.EmbeddedSource;
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00022989 File Offset: 0x00020B89
		public EmbeddedSourceDebugInformation(byte[] content, bool compress)
			: base(EmbeddedSourceDebugInformation.KindIdentifier)
		{
			this.content = content;
			this.compress = compress;
		}

		// Token: 0x04000539 RID: 1337
		internal byte[] content;

		// Token: 0x0400053A RID: 1338
		internal bool compress;

		// Token: 0x0400053B RID: 1339
		public static Guid KindIdentifier = new Guid("{0E8A571B-6926-466E-B4AD-8AB04611F5FE}");
	}
}
