using System;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000013 RID: 19
	public struct NamespaceEntry
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00004BCC File Offset: 0x00002DCC
		public NamespaceEntry(string name, int index, string[] using_clauses, int parent)
		{
			this.Name = name;
			this.Index = index;
			this.Parent = parent;
			this.UsingClauses = ((using_clauses != null) ? using_clauses : new string[0]);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004BF8 File Offset: 0x00002DF8
		internal NamespaceEntry(MonoSymbolFile file, MyBinaryReader reader)
		{
			this.Name = reader.ReadString();
			this.Index = reader.ReadLeb128();
			this.Parent = reader.ReadLeb128();
			int num = reader.ReadLeb128();
			this.UsingClauses = new string[num];
			for (int i = 0; i < num; i++)
			{
				this.UsingClauses[i] = reader.ReadString();
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004C58 File Offset: 0x00002E58
		internal void Write(MonoSymbolFile file, MyBinaryWriter bw)
		{
			bw.Write(this.Name);
			bw.WriteLeb128(this.Index);
			bw.WriteLeb128(this.Parent);
			bw.WriteLeb128(this.UsingClauses.Length);
			string[] usingClauses = this.UsingClauses;
			foreach (string text in usingClauses)
			{
				bw.Write(text);
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004CB9 File Offset: 0x00002EB9
		public override string ToString()
		{
			return string.Format("[Namespace {0}:{1}:{2}]", this.Name, this.Index, this.Parent);
		}

		// Token: 0x04000062 RID: 98
		public readonly string Name;

		// Token: 0x04000063 RID: 99
		public readonly int Index;

		// Token: 0x04000064 RID: 100
		public readonly int Parent;

		// Token: 0x04000065 RID: 101
		public readonly string[] UsingClauses;
	}
}
