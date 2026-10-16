using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002D7 RID: 727
	public struct InstructionOffset
	{
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x0003C814 File Offset: 0x0003AA14
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

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x0003C848 File Offset: 0x0003AA48
		public bool IsEndOfMethod
		{
			get
			{
				return this.instruction == null && this.offset == null;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x0003C862 File Offset: 0x0003AA62
		internal bool IsResolved
		{
			get
			{
				return this.instruction != null || this.offset == null;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x0003C87C File Offset: 0x0003AA7C
		internal Instruction ResolvedInstruction
		{
			get
			{
				return this.instruction;
			}
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0003C884 File Offset: 0x0003AA84
		public InstructionOffset(Instruction instruction)
		{
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			this.instruction = instruction;
			this.offset = null;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0003C8A7 File Offset: 0x0003AAA7
		public InstructionOffset(int offset)
		{
			this.instruction = null;
			this.offset = new int?(offset);
		}

		// Token: 0x04000962 RID: 2402
		private readonly Instruction instruction;

		// Token: 0x04000963 RID: 2403
		private readonly int? offset;
	}
}
