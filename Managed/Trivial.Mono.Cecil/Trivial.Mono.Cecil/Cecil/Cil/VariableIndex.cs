using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000135 RID: 309
	public struct VariableIndex
	{
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x00025CE7 File Offset: 0x00023EE7
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

		// Token: 0x06000B73 RID: 2931 RVA: 0x00025D1B File Offset: 0x00023F1B
		public VariableIndex(VariableDefinition variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			this.variable = variable;
			this.index = default(int?);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00025D3E File Offset: 0x00023F3E
		public VariableIndex(int index)
		{
			this.variable = null;
			this.index = new int?(index);
		}

		// Token: 0x040006DE RID: 1758
		private readonly VariableDefinition variable;

		// Token: 0x040006DF RID: 1759
		private readonly int? index;
	}
}
