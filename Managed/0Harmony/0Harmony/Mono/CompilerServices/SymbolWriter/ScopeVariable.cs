using System;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200030C RID: 780
	public struct ScopeVariable
	{
		// Token: 0x06001371 RID: 4977 RVA: 0x0003ED73 File Offset: 0x0003CF73
		public ScopeVariable(int scope, int index)
		{
			this.Scope = scope;
			this.Index = index;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0003ED83 File Offset: 0x0003CF83
		internal ScopeVariable(MyBinaryReader reader)
		{
			this.Scope = reader.ReadLeb128();
			this.Index = reader.ReadLeb128();
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0003ED9D File Offset: 0x0003CF9D
		internal void Write(MyBinaryWriter bw)
		{
			bw.WriteLeb128(this.Scope);
			bw.WriteLeb128(this.Index);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0003EDB7 File Offset: 0x0003CFB7
		public override string ToString()
		{
			return string.Format("[ScopeVariable {0}:{1}]", this.Scope, this.Index);
		}

		// Token: 0x04000A08 RID: 2568
		public readonly int Scope;

		// Token: 0x04000A09 RID: 2569
		public readonly int Index;
	}
}
