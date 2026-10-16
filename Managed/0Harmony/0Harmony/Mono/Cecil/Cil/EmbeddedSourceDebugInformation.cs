using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002E7 RID: 743
	internal sealed class EmbeddedSourceDebugInformation : CustomDebugInformation
	{
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0003CD5F File Offset: 0x0003AF5F
		// (set) Token: 0x060012D8 RID: 4824 RVA: 0x0003CD75 File Offset: 0x0003AF75
		public byte[] Content
		{
			get
			{
				if (!this.resolved)
				{
					this.Resolve();
				}
				return this.content;
			}
			set
			{
				this.content = value;
				this.resolved = true;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0003CD85 File Offset: 0x0003AF85
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x0003CD9B File Offset: 0x0003AF9B
		public bool Compress
		{
			get
			{
				if (!this.resolved)
				{
					this.Resolve();
				}
				return this.compress;
			}
			set
			{
				this.compress = value;
				this.resolved = true;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x0003CDAB File Offset: 0x0003AFAB
		public override CustomDebugInformationKind Kind
		{
			get
			{
				return CustomDebugInformationKind.EmbeddedSource;
			}
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0003CDAE File Offset: 0x0003AFAE
		internal EmbeddedSourceDebugInformation(uint index, MetadataReader debug_reader)
			: base(EmbeddedSourceDebugInformation.KindIdentifier)
		{
			this.index = index;
			this.debug_reader = debug_reader;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0003CDC9 File Offset: 0x0003AFC9
		public EmbeddedSourceDebugInformation(byte[] content, bool compress)
			: base(EmbeddedSourceDebugInformation.KindIdentifier)
		{
			this.resolved = true;
			this.content = content;
			this.compress = compress;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0003CDEB File Offset: 0x0003AFEB
		internal byte[] ReadRawEmbeddedSourceDebugInformation()
		{
			if (this.debug_reader == null)
			{
				throw new InvalidOperationException();
			}
			return this.debug_reader.ReadRawEmbeddedSourceDebugInformation(this.index);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0003CE0C File Offset: 0x0003B00C
		private void Resolve()
		{
			if (this.resolved)
			{
				return;
			}
			if (this.debug_reader == null)
			{
				throw new InvalidOperationException();
			}
			Row<byte[], bool> row = this.debug_reader.ReadEmbeddedSourceDebugInformation(this.index);
			this.content = row.Col1;
			this.compress = row.Col2;
			this.resolved = true;
		}

		// Token: 0x04000995 RID: 2453
		internal uint index;

		// Token: 0x04000996 RID: 2454
		internal MetadataReader debug_reader;

		// Token: 0x04000997 RID: 2455
		internal bool resolved;

		// Token: 0x04000998 RID: 2456
		internal byte[] content;

		// Token: 0x04000999 RID: 2457
		internal bool compress;

		// Token: 0x0400099A RID: 2458
		public static Guid KindIdentifier = new Guid("{0E8A571B-6926-466E-B4AD-8AB04611F5FE}");
	}
}
