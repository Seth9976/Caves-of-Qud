using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000118 RID: 280
	public struct InstructionOffset
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x000235A1 File Offset: 0x000217A1
		public int Offset
		{
			get
			{
				if (this.instruction != null)
				{
					return this.instruction.Offset;
				}
				if (this.offset != null)
				{
					return this.offset.Value;
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x000235D5 File Offset: 0x000217D5
		public bool IsEndOfMethod
		{
			get
			{
				return this.instruction == null && this.offset == null;
			}
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x000235EF File Offset: 0x000217EF
		public InstructionOffset(Instruction instruction)
		{
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			this.instruction = instruction;
			this.offset = default(int?);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00023612 File Offset: 0x00021812
		public InstructionOffset(int offset)
		{
			this.instruction = null;
			this.offset = new int?(offset);
		}

		// Token: 0x0400057B RID: 1403
		private readonly Instruction instruction;

		// Token: 0x0400057C RID: 1404
		private readonly int? offset;
	}
}
