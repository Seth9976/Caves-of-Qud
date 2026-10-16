using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000132 RID: 306
	public sealed class VariableDebugInformation : DebugInformation
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x00025B76 File Offset: 0x00023D76
		public int Index
		{
			get
			{
				return this.index.Index;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00025B83 File Offset: 0x00023D83
		// (set) Token: 0x06000B62 RID: 2914 RVA: 0x00025B8B File Offset: 0x00023D8B
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x00025B94 File Offset: 0x00023D94
		// (set) Token: 0x06000B64 RID: 2916 RVA: 0x00025B9C File Offset: 0x00023D9C
		public VariableAttributes Attributes
		{
			get
			{
				return (VariableAttributes)this.attributes;
			}
			set
			{
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00025BA5 File Offset: 0x00023DA5
		// (set) Token: 0x06000B66 RID: 2918 RVA: 0x00025BB3 File Offset: 0x00023DB3
		public bool IsDebuggerHidden
		{
			get
			{
				return this.attributes.GetAttributes(1);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1, value);
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00025BC8 File Offset: 0x00023DC8
		internal VariableDebugInformation(int index, string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.index = new VariableIndex(index);
			this.name = name;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00025BF4 File Offset: 0x00023DF4
		public VariableDebugInformation(VariableDefinition variable, string name)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.index = new VariableIndex(variable);
			this.name = name;
			this.token = new MetadataToken(TokenType.LocalVariable);
		}

		// Token: 0x040006DB RID: 1755
		private string name;

		// Token: 0x040006DC RID: 1756
		private ushort attributes;

		// Token: 0x040006DD RID: 1757
		internal VariableIndex index;
	}
}
