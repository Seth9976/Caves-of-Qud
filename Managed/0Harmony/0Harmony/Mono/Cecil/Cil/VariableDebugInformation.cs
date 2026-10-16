using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002DB RID: 731
	public sealed class VariableDebugInformation : DebugInformation
	{
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x0003C97E File Offset: 0x0003AB7E
		public int Index
		{
			get
			{
				return this.index.Index;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x0003C98B File Offset: 0x0003AB8B
		// (set) Token: 0x0600129C RID: 4764 RVA: 0x0003C993 File Offset: 0x0003AB93
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

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x0003C99C File Offset: 0x0003AB9C
		// (set) Token: 0x0600129E RID: 4766 RVA: 0x0003C9A4 File Offset: 0x0003ABA4
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

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x0003C9AD File Offset: 0x0003ABAD
		// (set) Token: 0x060012A0 RID: 4768 RVA: 0x0003C9BB File Offset: 0x0003ABBB
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

		// Token: 0x060012A1 RID: 4769 RVA: 0x0003C9D0 File Offset: 0x0003ABD0
		internal VariableDebugInformation(int index, string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.index = new VariableIndex(index);
			this.name = name;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0003C9FC File Offset: 0x0003ABFC
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

		// Token: 0x0400096B RID: 2411
		private string name;

		// Token: 0x0400096C RID: 2412
		private ushort attributes;

		// Token: 0x0400096D RID: 2413
		internal VariableIndex index;
	}
}
