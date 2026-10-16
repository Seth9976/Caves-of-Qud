using System;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000005 RID: 5
	public class CodeBlockEntry
	{
		// Token: 0x06000011 RID: 17 RVA: 0x0000233E File Offset: 0x0000053E
		public CodeBlockEntry(int index, int parent, CodeBlockEntry.Type type, int start_offset)
		{
			this.Index = index;
			this.Parent = parent;
			this.BlockType = type;
			this.StartOffset = start_offset;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002364 File Offset: 0x00000564
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

		// Token: 0x06000013 RID: 19 RVA: 0x000023D4 File Offset: 0x000005D4
		public void Close(int end_offset)
		{
			this.EndOffset = end_offset;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023DD File Offset: 0x000005DD
		internal void Write(MyBinaryWriter bw)
		{
			bw.WriteLeb128((int)this.BlockType);
			bw.WriteLeb128(this.Parent);
			bw.WriteLeb128(this.StartOffset);
			bw.WriteLeb128(this.EndOffset);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002410 File Offset: 0x00000610
		public override string ToString()
		{
			return string.Format("[CodeBlock {0}:{1}:{2}:{3}:{4}]", new object[] { this.Index, this.Parent, this.BlockType, this.StartOffset, this.EndOffset });
		}

		// Token: 0x04000009 RID: 9
		public int Index;

		// Token: 0x0400000A RID: 10
		public int Parent;

		// Token: 0x0400000B RID: 11
		public CodeBlockEntry.Type BlockType;

		// Token: 0x0400000C RID: 12
		public int StartOffset;

		// Token: 0x0400000D RID: 13
		public int EndOffset;

		// Token: 0x02000022 RID: 34
		public enum Type
		{
			// Token: 0x040000A9 RID: 169
			Lexical = 1,
			// Token: 0x040000AA RID: 170
			CompilerGenerated,
			// Token: 0x040000AB RID: 171
			IteratorBody,
			// Token: 0x040000AC RID: 172
			IteratorDispatcher
		}
	}
}
