using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002F6 RID: 758
	public abstract class VariableReference
	{
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x0003D797 File Offset: 0x0003B997
		// (set) Token: 0x0600131A RID: 4890 RVA: 0x0003D79F File Offset: 0x0003B99F
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

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x0003D7A8 File Offset: 0x0003B9A8
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0003D7B0 File Offset: 0x0003B9B0
		internal VariableReference(TypeReference variable_type)
		{
			this.variable_type = variable_type;
		}

		// Token: 0x0600131D RID: 4893
		public abstract VariableDefinition Resolve();

		// Token: 0x0600131E RID: 4894 RVA: 0x0003D7C6 File Offset: 0x0003B9C6
		public override string ToString()
		{
			if (this.index >= 0)
			{
				return "V_" + this.index.ToString();
			}
			return string.Empty;
		}

		// Token: 0x040009B1 RID: 2481
		internal int index = -1;

		// Token: 0x040009B2 RID: 2482
		protected TypeReference variable_type;
	}
}
