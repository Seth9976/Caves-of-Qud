using System;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000318 RID: 792
	internal class SourceMethodImpl : IMethodDef
	{
		// Token: 0x060013F8 RID: 5112 RVA: 0x000410CA File Offset: 0x0003F2CA
		public SourceMethodImpl(string name, int token, int namespaceID)
		{
			this.name = name;
			this.token = token;
			this.namespaceID = namespaceID;
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x000410E7 File Offset: 0x0003F2E7
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x000410EF File Offset: 0x0003F2EF
		public int NamespaceID
		{
			get
			{
				return this.namespaceID;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x000410F7 File Offset: 0x0003F2F7
		public int Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x04000A61 RID: 2657
		private string name;

		// Token: 0x04000A62 RID: 2658
		private int token;

		// Token: 0x04000A63 RID: 2659
		private int namespaceID;
	}
}
