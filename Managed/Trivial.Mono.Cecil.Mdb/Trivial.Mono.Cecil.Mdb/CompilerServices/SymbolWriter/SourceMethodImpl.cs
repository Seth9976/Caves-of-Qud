using System;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000019 RID: 25
	internal class SourceMethodImpl : IMethodDef
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00005618 File Offset: 0x00003818
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00005620 File Offset: 0x00003820
		public int NamespaceID
		{
			get
			{
				return this.namespaceID;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00005628 File Offset: 0x00003828
		public int Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005630 File Offset: 0x00003830
		public SourceMethodImpl(string name, int token, int namespaceID)
		{
			this.name = name;
			this.token = token;
			this.namespaceID = namespaceID;
		}

		// Token: 0x04000092 RID: 146
		private string name;

		// Token: 0x04000093 RID: 147
		private int token;

		// Token: 0x04000094 RID: 148
		private int namespaceID;
	}
}
