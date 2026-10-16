using System;
using System.Collections;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000014 RID: 20
	internal class NamespaceInfo
	{
		// Token: 0x04000066 RID: 102
		public string Name;

		// Token: 0x04000067 RID: 103
		public int NamespaceID;

		// Token: 0x04000068 RID: 104
		public ArrayList UsingClauses = new ArrayList();
	}
}
