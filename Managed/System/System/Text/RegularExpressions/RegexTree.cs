using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000211 RID: 529
	internal sealed class RegexTree
	{
		// Token: 0x06000F38 RID: 3896 RVA: 0x00043F26 File Offset: 0x00042126
		internal RegexTree(RegexNode root, Hashtable caps, int[] capNumList, int capTop, Hashtable capNames, string[] capsList, RegexOptions options)
		{
			this.Root = root;
			this.Caps = caps;
			this.CapNumList = capNumList;
			this.CapTop = capTop;
			this.CapNames = capNames;
			this.CapsList = capsList;
			this.Options = options;
		}

		// Token: 0x0400099D RID: 2461
		public readonly RegexNode Root;

		// Token: 0x0400099E RID: 2462
		public readonly Hashtable Caps;

		// Token: 0x0400099F RID: 2463
		public readonly int[] CapNumList;

		// Token: 0x040009A0 RID: 2464
		public readonly int CapTop;

		// Token: 0x040009A1 RID: 2465
		public readonly Hashtable CapNames;

		// Token: 0x040009A2 RID: 2466
		public readonly string[] CapsList;

		// Token: 0x040009A3 RID: 2467
		public readonly RegexOptions Options;
	}
}
