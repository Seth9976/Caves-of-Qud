using System;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000004 RID: 4
	public struct CapturedVariable
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000022B8 File Offset: 0x000004B8
		public CapturedVariable(string name, string captured_name, CapturedVariable.CapturedKind kind)
		{
			this.Name = name;
			this.CapturedName = captured_name;
			this.Kind = kind;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022CF File Offset: 0x000004CF
		internal CapturedVariable(MyBinaryReader reader)
		{
			this.Name = reader.ReadString();
			this.CapturedName = reader.ReadString();
			this.Kind = (CapturedVariable.CapturedKind)reader.ReadByte();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022F5 File Offset: 0x000004F5
		internal void Write(MyBinaryWriter bw)
		{
			bw.Write(this.Name);
			bw.Write(this.CapturedName);
			bw.Write((byte)this.Kind);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000231B File Offset: 0x0000051B
		public override string ToString()
		{
			return string.Format("[CapturedVariable {0}:{1}:{2}]", this.Name, this.CapturedName, this.Kind);
		}

		// Token: 0x04000006 RID: 6
		public readonly string Name;

		// Token: 0x04000007 RID: 7
		public readonly string CapturedName;

		// Token: 0x04000008 RID: 8
		public readonly CapturedVariable.CapturedKind Kind;

		// Token: 0x02000021 RID: 33
		public enum CapturedKind : byte
		{
			// Token: 0x040000A5 RID: 165
			Local,
			// Token: 0x040000A6 RID: 166
			Parameter,
			// Token: 0x040000A7 RID: 167
			This
		}
	}
}
