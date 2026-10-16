using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002D9 RID: 729
	internal struct VariableIndex
	{
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x0003C8BC File Offset: 0x0003AABC
		public int Index
		{
			get
			{
				if (this.variable != null)
				{
					return this.variable.Index;
				}
				if (this.index != null)
				{
					return this.index.Value;
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x0003C8F0 File Offset: 0x0003AAF0
		internal bool IsResolved
		{
			get
			{
				return this.variable != null;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x0003C8FB File Offset: 0x0003AAFB
		internal VariableDefinition ResolvedVariable
		{
			get
			{
				return this.variable;
			}
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0003C903 File Offset: 0x0003AB03
		public VariableIndex(VariableDefinition variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			this.variable = variable;
			this.index = null;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0003C926 File Offset: 0x0003AB26
		public VariableIndex(int index)
		{
			this.variable = null;
			this.index = new int?(index);
		}

		// Token: 0x04000967 RID: 2407
		private readonly VariableDefinition variable;

		// Token: 0x04000968 RID: 2408
		private readonly int? index;
	}
}
