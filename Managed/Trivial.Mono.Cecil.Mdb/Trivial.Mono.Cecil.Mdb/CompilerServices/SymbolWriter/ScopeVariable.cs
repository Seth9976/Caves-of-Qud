using System;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000016 RID: 22
	public struct ScopeVariable
	{
		// Token: 0x0600009A RID: 154 RVA: 0x0000500B File Offset: 0x0000320B
		public ScopeVariable(int scope, int index)
		{
			this.Scope = scope;
			this.Index = index;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000501B File Offset: 0x0000321B
		internal ScopeVariable(MyBinaryReader reader)
		{
			this.Scope = reader.ReadLeb128();
			this.Index = reader.ReadLeb128();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005035 File Offset: 0x00003235
		internal void Write(MyBinaryWriter bw)
		{
			bw.WriteLeb128(this.Scope);
			bw.WriteLeb128(this.Index);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000504F File Offset: 0x0000324F
		public override string ToString()
		{
			return string.Format("[ScopeVariable {0}:{1}]", this.Scope, this.Index);
		}

		// Token: 0x04000080 RID: 128
		public readonly int Scope;

		// Token: 0x04000081 RID: 129
		public readonly int Index;
	}
}
