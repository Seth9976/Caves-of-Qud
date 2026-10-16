using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000133 RID: 307
	public sealed class VariableDefinition : VariableReference
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x00025C46 File Offset: 0x00023E46
		public bool IsPinned
		{
			get
			{
				return this.variable_type.IsPinned;
			}
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00025C53 File Offset: 0x00023E53
		public VariableDefinition(TypeReference variableType)
			: base(variableType)
		{
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00002654 File Offset: 0x00000854
		public override VariableDefinition Resolve()
		{
			return this;
		}
	}
}
