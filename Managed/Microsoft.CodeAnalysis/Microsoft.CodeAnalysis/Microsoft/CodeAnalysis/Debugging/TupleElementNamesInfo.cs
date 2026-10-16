using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.Debugging
{
	// Token: 0x02000363 RID: 867
	internal struct TupleElementNamesInfo
	{
		// Token: 0x060024F8 RID: 9464 RVA: 0x000732A2 File Offset: 0x000714A2
		internal TupleElementNamesInfo(ImmutableArray<string> elementNames, int slotIndex, string localName, int scopeStart, int scopeEnd)
		{
			this.ElementNames = elementNames;
			this.SlotIndex = slotIndex;
			this.LocalName = localName;
			this.ScopeStart = scopeStart;
			this.ScopeEnd = scopeEnd;
		}

		// Token: 0x04000FF2 RID: 4082
		internal readonly ImmutableArray<string> ElementNames;

		// Token: 0x04000FF3 RID: 4083
		internal readonly int SlotIndex;

		// Token: 0x04000FF4 RID: 4084
		internal readonly string LocalName;

		// Token: 0x04000FF5 RID: 4085
		internal readonly int ScopeStart;

		// Token: 0x04000FF6 RID: 4086
		internal readonly int ScopeEnd;
	}
}
