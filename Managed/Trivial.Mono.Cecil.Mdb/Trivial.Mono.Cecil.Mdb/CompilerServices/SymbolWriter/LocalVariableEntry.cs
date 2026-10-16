using System;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200000C RID: 12
	public struct LocalVariableEntry
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000030AC File Offset: 0x000012AC
		public LocalVariableEntry(int index, string name, int block)
		{
			this.Index = index;
			this.Name = name;
			this.BlockIndex = block;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000030C3 File Offset: 0x000012C3
		internal LocalVariableEntry(MonoSymbolFile file, MyBinaryReader reader)
		{
			this.Index = reader.ReadLeb128();
			this.Name = reader.ReadString();
			this.BlockIndex = reader.ReadLeb128();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000030E9 File Offset: 0x000012E9
		internal void Write(MonoSymbolFile file, MyBinaryWriter bw)
		{
			bw.WriteLeb128(this.Index);
			bw.Write(this.Name);
			bw.WriteLeb128(this.BlockIndex);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000310F File Offset: 0x0000130F
		public override string ToString()
		{
			return string.Format("[LocalVariable {0}:{1}:{2}]", this.Name, this.Index, this.BlockIndex - 1);
		}

		// Token: 0x0400002E RID: 46
		public readonly int Index;

		// Token: 0x0400002F RID: 47
		public readonly string Name;

		// Token: 0x04000030 RID: 48
		public readonly int BlockIndex;
	}
}
