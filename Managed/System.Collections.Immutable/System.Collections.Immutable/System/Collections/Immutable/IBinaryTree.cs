using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200001A RID: 26
	[NullableContext(2)]
	internal interface IBinaryTree
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000068 RID: 104
		int Height { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000069 RID: 105
		bool IsEmpty { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006A RID: 106
		int Count { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006B RID: 107
		IBinaryTree Left { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006C RID: 108
		IBinaryTree Right { get; }
	}
}
