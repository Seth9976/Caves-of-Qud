using System;
using System.Collections;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000319 RID: 793
	internal class NamespaceInfo
	{
		// Token: 0x04000A64 RID: 2660
		public string Name;

		// Token: 0x04000A65 RID: 2661
		public int NamespaceID;

		// Token: 0x04000A66 RID: 2662
		public ArrayList UsingClauses = new ArrayList();
	}
}
