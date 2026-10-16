using System;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000306 RID: 774
	public class CodeBlockEntry
	{
		// Token: 0x06001360 RID: 4960 RVA: 0x0003EAC9 File Offset: 0x0003CCC9
		public CodeBlockEntry(int index, int parent, CodeBlockEntry.Type type, int start_offset)
		{
			this.Index = index;
			this.Parent = parent;
			this.BlockType = type;
			this.StartOffset = start_offset;
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0003EAF0 File Offset: 0x0003CCF0
		internal CodeBlockEntry(int index, MyBinaryReader reader)
		{
			this.Index = index;
			int num = reader.ReadLeb128();
			this.BlockType = (CodeBlockEntry.Type)(num & 63);
			this.Parent = reader.ReadLeb128();
			this.StartOffset = reader.ReadLeb128();
			this.EndOffset = reader.ReadLeb128();
			if ((num & 64) != 0)
			{
				int num2 = (int)reader.ReadInt16();
				reader.BaseStream.Position += (long)num2;
			}
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0003EB60 File Offset: 0x0003CD60
		public void Close(int end_offset)
		{
			this.EndOffset = end_offset;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0003EB69 File Offset: 0x0003CD69
		internal void Write(MyBinaryWriter bw)
		{
			bw.WriteLeb128((int)this.BlockType);
			bw.WriteLeb128(this.Parent);
			bw.WriteLeb128(this.StartOffset);
			bw.WriteLeb128(this.EndOffset);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0003EB9C File Offset: 0x0003CD9C
		public override string ToString()
		{
			return string.Format("[CodeBlock {0}:{1}:{2}:{3}:{4}]", new object[] { this.Index, this.Parent, this.BlockType, this.StartOffset, this.EndOffset });
		}

		// Token: 0x040009F2 RID: 2546
		public int Index;

		// Token: 0x040009F3 RID: 2547
		public int Parent;

		// Token: 0x040009F4 RID: 2548
		public CodeBlockEntry.Type BlockType;

		// Token: 0x040009F5 RID: 2549
		public int StartOffset;

		// Token: 0x040009F6 RID: 2550
		public int EndOffset;

		// Token: 0x02000307 RID: 775
		public enum Type
		{
			// Token: 0x040009F8 RID: 2552
			Lexical = 1,
			// Token: 0x040009F9 RID: 2553
			CompilerGenerated,
			// Token: 0x040009FA RID: 2554
			IteratorBody,
			// Token: 0x040009FB RID: 2555
			IteratorDispatcher
		}
	}
}
