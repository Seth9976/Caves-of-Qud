using System;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000313 RID: 787
	public struct NamespaceEntry
	{
		// Token: 0x060013AF RID: 5039 RVA: 0x00040574 File Offset: 0x0003E774
		public NamespaceEntry(string name, int index, string[] using_clauses, int parent)
		{
			this.Name = name;
			this.Index = index;
			this.Parent = parent;
			this.UsingClauses = ((using_clauses != null) ? using_clauses : new string[0]);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x000405A0 File Offset: 0x0003E7A0
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

		// Token: 0x060013B1 RID: 5041 RVA: 0x00040600 File Offset: 0x0003E800
		internal void Write(MonoSymbolFile file, MyBinaryWriter bw)
		{
			bw.Write(this.Name);
			bw.WriteLeb128(this.Index);
			bw.WriteLeb128(this.Parent);
			bw.WriteLeb128(this.UsingClauses.Length);
			foreach (string text in this.UsingClauses)
			{
				bw.Write(text);
			}
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0004065F File Offset: 0x0003E85F
		public override string ToString()
		{
			return string.Format("[Namespace {0}:{1}:{2}]", this.Name, this.Index, this.Parent);
		}

		// Token: 0x04000A45 RID: 2629
		public readonly string Name;

		// Token: 0x04000A46 RID: 2630
		public readonly int Index;

		// Token: 0x04000A47 RID: 2631
		public readonly int Parent;

		// Token: 0x04000A48 RID: 2632
		public readonly string[] UsingClauses;
	}
}
