using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002D1 RID: 721
	public sealed class SequencePoint
	{
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0003C53C File Offset: 0x0003A73C
		public int Offset
		{
			get
			{
				return this.offset.Offset;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x0003C549 File Offset: 0x0003A749
		// (set) Token: 0x06001266 RID: 4710 RVA: 0x0003C551 File Offset: 0x0003A751
		public int StartLine
		{
			get
			{
				return this.start_line;
			}
			set
			{
				this.start_line = value;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x0003C55A File Offset: 0x0003A75A
		// (set) Token: 0x06001268 RID: 4712 RVA: 0x0003C562 File Offset: 0x0003A762
		public int StartColumn
		{
			get
			{
				return this.start_column;
			}
			set
			{
				this.start_column = value;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x0003C56B File Offset: 0x0003A76B
		// (set) Token: 0x0600126A RID: 4714 RVA: 0x0003C573 File Offset: 0x0003A773
		public int EndLine
		{
			get
			{
				return this.end_line;
			}
			set
			{
				this.end_line = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x0003C57C File Offset: 0x0003A77C
		// (set) Token: 0x0600126C RID: 4716 RVA: 0x0003C584 File Offset: 0x0003A784
		public int EndColumn
		{
			get
			{
				return this.end_column;
			}
			set
			{
				this.end_column = value;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x0003C58D File Offset: 0x0003A78D
		public bool IsHidden
		{
			get
			{
				return this.start_line == 16707566 && this.start_line == this.end_line;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x0003C5AC File Offset: 0x0003A7AC
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x0003C5B4 File Offset: 0x0003A7B4
		public Document Document
		{
			get
			{
				return this.document;
			}
			set
			{
				this.document = value;
			}
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0003C5BD File Offset: 0x0003A7BD
		internal SequencePoint(int offset, Document document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			this.offset = new InstructionOffset(offset);
			this.document = document;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0003C5E6 File Offset: 0x0003A7E6
		public SequencePoint(Instruction instruction, Document document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			this.offset = new InstructionOffset(instruction);
			this.document = document;
		}

		// Token: 0x04000946 RID: 2374
		internal InstructionOffset offset;

		// Token: 0x04000947 RID: 2375
		private Document document;

		// Token: 0x04000948 RID: 2376
		private int start_line;

		// Token: 0x04000949 RID: 2377
		private int start_column;

		// Token: 0x0400094A RID: 2378
		private int end_line;

		// Token: 0x0400094B RID: 2379
		private int end_column;
	}
}
