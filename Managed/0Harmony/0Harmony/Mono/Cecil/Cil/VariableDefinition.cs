using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002F5 RID: 757
	public sealed class VariableDefinition : VariableReference
	{
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x0003D781 File Offset: 0x0003B981
		public bool IsPinned
		{
			get
			{
				return this.variable_type.IsPinned;
			}
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0003D78E File Offset: 0x0003B98E
		public VariableDefinition(TypeReference variableType)
			: base(variableType)
		{
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00018380 File Offset: 0x00016580
		public override VariableDefinition Resolve()
		{
			return this;
		}
	}
}
