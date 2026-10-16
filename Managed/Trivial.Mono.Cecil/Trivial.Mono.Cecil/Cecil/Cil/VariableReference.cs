using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000136 RID: 310
	public abstract class VariableReference
	{
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x00025D53 File Offset: 0x00023F53
		// (set) Token: 0x06000B76 RID: 2934 RVA: 0x00025D5B File Offset: 0x00023F5B
		public TypeReference VariableType
		{
			get
			{
				return this.variable_type;
			}
			set
			{
				this.variable_type = value;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x00025D64 File Offset: 0x00023F64
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00025D6C File Offset: 0x00023F6C
		internal VariableReference(TypeReference variable_type)
		{
			this.variable_type = variable_type;
		}

		// Token: 0x06000B79 RID: 2937
		public abstract VariableDefinition Resolve();

		// Token: 0x06000B7A RID: 2938 RVA: 0x00025D82 File Offset: 0x00023F82
		public override string ToString()
		{
			if (this.index >= 0)
			{
				return "V_" + this.index;
			}
			return string.Empty;
		}

		// Token: 0x040006E0 RID: 1760
		internal int index = -1;

		// Token: 0x040006E1 RID: 1761
		protected TypeReference variable_type;
	}
}
