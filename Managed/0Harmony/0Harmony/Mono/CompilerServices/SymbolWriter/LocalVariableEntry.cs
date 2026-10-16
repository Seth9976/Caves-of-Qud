using System;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000308 RID: 776
	public struct LocalVariableEntry
	{
		// Token: 0x06001365 RID: 4965 RVA: 0x0003EBFF File Offset: 0x0003CDFF
		public LocalVariableEntry(int index, string name, int block)
		{
			this.Index = index;
			this.Name = name;
			this.BlockIndex = block;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0003EC16 File Offset: 0x0003CE16
		internal LocalVariableEntry(MonoSymbolFile file, MyBinaryReader reader)
		{
			this.Index = reader.ReadLeb128();
			this.Name = reader.ReadString();
			this.BlockIndex = reader.ReadLeb128();
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0003EC3C File Offset: 0x0003CE3C
		internal void Write(MonoSymbolFile file, MyBinaryWriter bw)
		{
			bw.WriteLeb128(this.Index);
			bw.Write(this.Name);
			bw.WriteLeb128(this.BlockIndex);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0003EC62 File Offset: 0x0003CE62
		public override string ToString()
		{
			return string.Format("[LocalVariable {0}:{1}:{2}]", this.Name, this.Index, this.BlockIndex - 1);
		}

		// Token: 0x040009FC RID: 2556
		public readonly int Index;

		// Token: 0x040009FD RID: 2557
		public readonly string Name;

		// Token: 0x040009FE RID: 2558
		public readonly int BlockIndex;
	}
}
