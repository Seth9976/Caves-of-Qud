using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x0200012A RID: 298
	public sealed class SequencePoint
	{
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00025834 File Offset: 0x00023A34
		public int Offset
		{
			get
			{
				return this.offset.Offset;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00025841 File Offset: 0x00023A41
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x00025849 File Offset: 0x00023A49
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

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00025852 File Offset: 0x00023A52
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x0002585A File Offset: 0x00023A5A
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

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00025863 File Offset: 0x00023A63
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x0002586B File Offset: 0x00023A6B
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

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x00025874 File Offset: 0x00023A74
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x0002587C File Offset: 0x00023A7C
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

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00025885 File Offset: 0x00023A85
		public bool IsHidden
		{
			get
			{
				return this.start_line == 16707566 && this.start_line == this.end_line;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x000258A4 File Offset: 0x00023AA4
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x000258AC File Offset: 0x00023AAC
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

		// Token: 0x06000B4C RID: 2892 RVA: 0x000258B5 File Offset: 0x00023AB5
		internal SequencePoint(int offset, Document document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			this.offset = new InstructionOffset(offset);
			this.document = document;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000258DE File Offset: 0x00023ADE
		public SequencePoint(Instruction instruction, Document document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			this.offset = new InstructionOffset(instruction);
			this.document = document;
		}

		// Token: 0x040006A5 RID: 1701
		internal InstructionOffset offset;

		// Token: 0x040006A6 RID: 1702
		private Document document;

		// Token: 0x040006A7 RID: 1703
		private int start_line;

		// Token: 0x040006A8 RID: 1704
		private int start_column;

		// Token: 0x040006A9 RID: 1705
		private int end_line;

		// Token: 0x040006AA RID: 1706
		private int end_column;
	}
}
