using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002E5 RID: 741
	internal sealed class StateMachineScope
	{
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x0003CC9C File Offset: 0x0003AE9C
		// (set) Token: 0x060012CE RID: 4814 RVA: 0x0003CCA4 File Offset: 0x0003AEA4
		public InstructionOffset Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x0003CCAD File Offset: 0x0003AEAD
		// (set) Token: 0x060012D0 RID: 4816 RVA: 0x0003CCB5 File Offset: 0x0003AEB5
		public InstructionOffset End
		{
			get
			{
				return this.end;
			}
			set
			{
				this.end = value;
			}
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0003CCBE File Offset: 0x0003AEBE
		internal StateMachineScope(int start, int end)
		{
			this.start = new InstructionOffset(start);
			this.end = new InstructionOffset(end);
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0003CCE0 File Offset: 0x0003AEE0
		public StateMachineScope(Instruction start, Instruction end)
		{
			this.start = new InstructionOffset(start);
			this.end = ((end != null) ? new InstructionOffset(end) : default(InstructionOffset));
		}

		// Token: 0x04000991 RID: 2449
		internal InstructionOffset start;

		// Token: 0x04000992 RID: 2450
		internal InstructionOffset end;
	}
}
